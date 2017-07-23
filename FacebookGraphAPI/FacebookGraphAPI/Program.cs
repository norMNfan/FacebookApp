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
            // Tyler's login credentials
            // vyler.togt@gmail.com
            // Muchroomcowmoon1

            string message = "Hey, I'm sending this programatically, ";

            string TylerToken = "EAAGsVkZAMInsBACm7nZCoCEV0bko7EUbJcBX1KsSC5m8azC9UVEWuYdVR0WtZBqg3H6nUu2Sy9JSECZC0OGv6KNCHTdgDcZAkcbrrOKiJfhVjxkv2hRvZCibn3cKZC5O65QYc0XAFrpYReuoZApPSqqtUcKSE0Be5tdOHohSQBGFjQZDZD";
            var TylerClient = new FacebookClient(TylerToken);
            //string Token = "EAAG41WnBkZAEBAPm33aMwYTZBHCN0dU8mlUNdZAXmJqFkgOde2avg3QtAINASpjdfZCwQw40WIXnubG7ZBSgqs91SIgnleZCPzdZBwkZAeBWnhJZBKEMgDn7Q7Ss4o3cnPmmRnqTcpwehuRm301Pmw6yih3TTPQWtivgItaGbjGkgXAZDZD";            
            //var client = new FacebookClient(Token);

            // GET all accounts
            List<Account> accounts = GraphCalls.AllAccounts(TylerClient);
            // Set
            // 0 => Cartoon And Anime Lovers
            // 1 => DC & Marvel Lovers
            // 2 => Movie Lovers
            // 3 => Disney Movie Lovers
            // 4 => Beanstalk Market
            // 5 => Fans of the Future

            // Set up page access token
            Page page = GraphCalls.getPage(TylerClient, 0);
            var pageClient = new FacebookClient(page.access_token);

            // GET all of the posts on page feed
            List<Post> posts = GraphCalls.AllPosts(page);

            // GET all of the posts comments
            List<Comment>[] comments = GraphCalls.AllComments(page, posts);

            // POST message to all the comments
            // GraphCalls.sendMessages(comments, page, message);

            // POST something to Page wall
            //GraphCalls.PostPage(page, "THIS IS A TEST POST");
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

        public static string TylerGetAccessToken()
        {
            var client = new FacebookClient();
            dynamic result = client.Get("oauth/access_token", new
            {
                client_id = "470961523270267",
                client_secret = "c8ae181332d6e4d996456bd9d2368480",
                grant_type = "fb_exchange_token",
                fb_exchange_token = "EAAGsVkZAMInsBABG2u9lsCoGlKVxHUxk0hcEG76y7n3odCICQIWPxxg9TvxMDoZCTcZBZASucGmn2QPMA5Amgqqh0D2KFoVY0icQsyig0yADQqFOKFwSOsZBjIPpJyhVFztFYVKXfaSBB0nt2YsQXZAZAm7bus4xcnSZCDzlYORnUPUQySn09bR7uo53YS2Uu4wZD"
            });
            return result.access_token;
        }
    }
}
