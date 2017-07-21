using Facebook;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static FacebookGraphAPI.Data;

namespace FacebookGraphAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            string message = "Hey, I'm sending this programatically, ";

            string Token = "EAAG41WnBkZAEBAPm33aMwYTZBHCN0dU8mlUNdZAXmJqFkgOde2avg3QtAINASpjdfZCwQw40WIXnubG7ZBSgqs91SIgnleZCPzdZBwkZAeBWnhJZBKEMgDn7Q7Ss4o3cnPmmRnqTcpwehuRm301Pmw6yih3TTPQWtivgItaGbjGkgXAZDZD";            
            var client = new FacebookClient(Token);

            // Set up page access token
            Page page = GraphCalls.getPage(client);
            var pageClient = new FacebookClient(page.access_token);

            // GET all of the posts on page feed
            List<Post> posts = GraphCalls.AllPosts(page);

            // GET all of the posts comments
            List<Comment>[] comments = GraphCalls.AllComments(page, posts);

            // POST message to all the comments
            GraphCalls.sendMessages(comments, page, message);

            // POST something to Page wall
            //GraphCalls.postPage(page, "THIS IS A TEST POST");
        }

        public static string GetAccessToken()
        {
            var client = new FacebookClient();
            dynamic result = client.Get("oauth/access_token", new
            {
                client_id = "484701718548881",
                client_secret = "b3ecb63d467656d404b892498bd3c5ed",
                grant_type = "fb_exchange_token",
                fb_exchange_token = "EAAG41WnBkZAEBAJBatZAAORY1k91hwedHwDYPQKJwR15304PVB0tjMWZArEMJHOZBJVVSxorZC0x24i3OcR5kKuqldwUJ8xK5vIG2xG1KI9ZBXpOulXRUSmhCKUfyheXpDVg12xnIAeESc1qWHeLCdDtJZBp6pWyPgRVwLrig18ZCRHMo3xpMMLGSlxwYw2ZBjI8ZD"
            });
            return result.access_token;
        }
    }
}
