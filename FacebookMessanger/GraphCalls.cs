using Facebook;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FacebookGraphAPI.Data;
//using WebApiContrib.Formatting.JavaScriptSerializer;

namespace FacebookGraphAPI
{
    class GraphCalls
    {
        const string pageId = "1130035050388269";
        const string pageAuth = "";
        public static Page getPage(FacebookClient client, int index)
        {
            var result = client.Get("me/accounts");
            JObject pageListJson = JObject.Parse(result.ToString());
            var resultData = pageListJson["data"][index];
            Page page = JsonConvert.DeserializeObject<Page>(resultData.ToString());
            return page;
        }
        public static List<Account> AllAccounts(FacebookClient client)
        {
            var GraphCall = "me/accounts";
            var result = client.Get(GraphCall);
            JObject accountListJson = JObject.Parse(result.ToString());
            List<Account> accounts = new List<Account>();
            int index = 0;
            foreach(var account in accountListJson["data"])
            {
                Account x = new Account(account);
                accounts.Add(x);
                Console.WriteLine("Account # {0} : {1}", index, x.name);
                index++;
            }
            return accounts;
        }
        public static List<Post> AllPosts(Page page)
        {
            FacebookClient client = new FacebookClient(page.access_token);

            var GraphCall = string.Format("{0}/feed?limit=100", page.id);
            var result = client.Get(GraphCall);
            JObject postListJson = JObject.Parse(result.ToString());
            // Create list of posts on a page
            List<Post> posts = new List<Post>();
            int index = 0;

            // Assign all the fields of a post
            foreach(var post in postListJson["data"])
            {
                Post x = new Post(post);
                posts.Add(x);

                Console.WriteLine("Post # {0}", index);
                Console.WriteLine("story: {0}", x.story);
                Console.WriteLine("message: {0}", x.message);
                Console.WriteLine("created_time: {0}", x.created_time);
                Console.WriteLine("id: {0}", x.id);
                Console.WriteLine("**********************************");
                index++;
            }
            return posts;
        }
        public static Post getPost(FacebookClient client, Page page, int postIndex)
        {
            var GraphCall = string.Format("{0}/feed?limit=100", page.id);
            var result = client.Get(GraphCall);
            JObject postListJson = JObject.Parse(result.ToString());
            var postJToken = postListJson["data"][postIndex];
            Post post = new Post(postJToken);
            return post;
        }
        public static List<Comment>[] AllComments(Page page, List<Post> posts)
        {
            FacebookClient client = new FacebookClient(page.access_token);
            List<Comment>[] allComments = new List<Comment>[posts.Count];
            int index = 0;
            foreach(var post in posts)
            {
                Console.WriteLine(index);
                string graphCall = string.Format("{0}/comments?limit=1500", post.id);
                var result = client.Get(graphCall);
                JObject commentListJson = JObject.Parse(result.ToString());
                List<Comment> comments = new List<Comment>();

                // Assign all the fields of a comment
                foreach(var comment in commentListJson["data"])
                {
                    Comment x = new Comment(comment);
                    comments.Add(x);

                    // Check if comment has replies
                    try
                    {
                        graphCall = string.Format("{0}/comments", x.id);
                        result = client.Get(graphCall);
                        commentListJson = JObject.Parse(result.ToString());
                        List<Comment> replyComments = new List<Comment>();
                    }
                    catch(Exception)
                    {
                        // comments has no replies
                    }
                }
                allComments[index] = comments;
                index++;
            }
            return allComments;
        }
        public static List<Comment> getComments(FacebookClient client, Post post)
        {
            string GraphCall = string.Format("{0}/comments?limit=1500", post.id);
            var result = client.Get(GraphCall);
            JObject commentListJson = JObject.Parse(result.ToString());
            List<Comment> comments = new List<Comment>();
            foreach(var comment in commentListJson["data"])
            {
                Comment x = new Comment(comment);
                comments.Add(x);
                GraphCall = string.Format("{0}/comments", x.id);
                var replies = client.Get(GraphCall);
                var repliesListJson = JObject.Parse(replies.ToString());
                foreach(var reply in repliesListJson["data"])
                {
                    if (reply == null) break;
                    Comment y = new Comment(reply);
                    comments.Add(y);
                }
            }
            return comments;
        }
        public static void PostPage(Page page, string message)
        {
            FacebookClient client = new FacebookClient(page.access_token);
            dynamic messagePost = new ExpandoObject();
            messagePost.access_token = page.access_token;
            messagePost.caption = "THIS IS A NEW POST";
            messagePost.message = message;

            try { var result = client.Post(page.id + "/feed", messagePost); }
            catch (FacebookOAuthException) { /* handle something */ }
        }
        public static void sendMessages(FacebookClient client, List<Comment> comments, string message, int messageStartIndex, int messageEndIndex)
        {
            string GraphCall;
            string messageCall;
            string[] name;
            dynamic messageObject = new ExpandoObject();
            messageObject.message = string.Empty;
            
            Dictionary<string, bool> idArray = new Dictionary<string, bool>();
            for (int i = 0; i < comments.Count; i++)
            {
                try
                {
                    idArray.Add(comments[i].from_id, false);
                }
                catch (Exception) { /* from_id already added */ }
            }
            int index = 0;
            foreach (var comment in comments)
            {
                if (index < messageStartIndex|| index > messageEndIndex)
                {
                    index++;
                    try
                    {
                        idArray[comment.from_id] = true; // set from_id to true after message is sent
                    }
                    catch (Exception) {  }
                    continue;
                }
                if (!idArray[comment.from_id])
                {
                    name = comment.from_name.Split(' ');
                    messageCall = "Hey " + name[0] + message;
                    messageObject.message = messageCall;
                    GraphCall = string.Format("{0}/private_replies", comment.id);
                    try
                    {
                        client.Post(GraphCall, messageObject);
                        Random rand = new Random();
                        int wait = rand.Next(25, 45) * 1000;
                        System.Threading.Thread.Sleep(wait);
                        idArray[comment.from_id] = true; // set from_id to true after message is sent
                    }
                    catch (Exception) { /* message not sent */ }
                }
                 index++;
            }
        }
    }
}
