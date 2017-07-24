﻿using Facebook;
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
        const string pageAuth = "EAACEdEose0cBAN6Yh4zAN8skcmREqgNRAnUbt9L7ZByD0408ZCPd2ZAv9dzD73PMvzLN1M9nQ3sZCbfWXNIntuSfZAfKV6Wkzggwly4BdHyqo2SGzLbjuVaxgP6H0dLUKeiYCLt9PpzBCr8pVrjE07muPMXL9or6u0nE535a6lbrtS1zw6td0O4pya7LfXLqrzZB1W8tZB54gZDZD";
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
            foreach(var account in accountListJson["data"])
            {
                Account x = new Account(account);
                accounts.Add(x);
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
        public static void PostPage(Page page, string message)
        {
            FacebookClient client = new FacebookClient(page.access_token);

            // create post objewct
            dynamic messagePost = new ExpandoObject();
            messagePost.access_token = page.access_token;
            //messagePost.picture = "[A_PICTURE]";
            //messagePost.link = "[SOME_LINK]";
            messagePost.name = "reeee";
            messagePost.caption = "THIS IS A NEW POST";
            messagePost.message = message;
            //messagePost.description = "[SOME_DESCRIPTION]";

            try
            {
                var result = client.Post(page.id + "/feed", messagePost);
            }
            catch (FacebookOAuthException ex)
            {
                // handle something
            }
        }
        public static void sendMessages(List<Comment>[] commentArray, Page page, string message)
        {
            FacebookClient client = new FacebookClient(page.access_token);

            // create message object
            dynamic messageObject = new ExpandoObject();
            messageObject.message = string.Empty;
            
            string GraphCall;
            var numberOfCommentThreads = commentArray.Length;
            for(int i = 0; i < numberOfCommentThreads; i++)
            {
                if (commentArray[i].Count < 1) continue;
                foreach(var comment in commentArray[i])
                {
                    messageObject.message = message +  comment.from_name;
                    GraphCall = string.Format("{0}/private_replies", comment.id);
                    client.Post(GraphCall, messageObject);
                }
            }
        }
    }
}