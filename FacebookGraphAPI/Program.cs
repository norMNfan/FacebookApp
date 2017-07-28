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
            // Mushroomcowmoon1
            /**************************************************************************
             * STEP 1
             * SET accounIndex
             * 0 => Cartoon And Anime Lovers
             * 1 => DC & Marvel Lovers
             * 2 => Movie Lovers
             * 3 => Disney Movie Lovers
             * 4 => Beanstalk Market
             * 5 => Fans of the Future
             * 
             * STEP 2
             * RUN AllPosts to see which post you want to use
             * SET postIndex
             * 
             * STEP 3
             * SET message to you're custom message.
             * check GraphCalls.sendMessages for how message is created with user name
             * 
             * STEP 4
             * Run program!
             *
             ****************************************************************************/

            string message = "! You won our Pokemon Keychain Giveaway! Thank you for engaging in our community. Our friends over at Movie Treasures are sponsoring this event and want to give you 3 FREE keychains of your choice! \n\n" +
                "How to Claim Your Prize: \n\n" +
                "1. Follow Our Link >\n" +
                "https://movietreasures.org/collections/anime-lovers/products/premium-quality-pokemon-keychains \n\n" +
                "2. Add Any 3 Keychains to Your Cart \n\n" +
                "3. Enter Coupon Code 'WINNER5' at Checkout \n\n" +
                "4. Hurry! We Can Only Secure Your Exclusive Offer For 24 Hours! \n\n" +
                "** Checkout the Movie Treasures FAQs page for answers to common questions \n\n" +
                "https://movietreasures.org/pages/faqs-2 \n\n" +
                "** Free shipping available in select regions only, all rates are discounted, but ultimately determined by location";

            int accountIndex = 2;
            int postIndex = 4;
            int messageIndex = 0;

            string Token = "EAAGsVkZAMInsBACm7nZCoCEV0bko7EUbJcBX1KsSC5m8azC9UVEWuYdVR0WtZBqg3H6nUu2Sy9JSECZC0OGv6KNCHTdgDcZAkcbrrOKiJfhVjxkv2hRvZCibn3cKZC5O65QYc0XAFrpYReuoZApPSqqtUcKSE0Be5tdOHohSQBGFjQZDZD";

            var client = new FacebookClient(Token);
            Page page = GraphCalls.getPage(client, accountIndex);
            GraphCalls.AllAccounts(client);
            List<Post> posts = GraphCalls.AllPosts(page);

            SendMessages(Token, accountIndex, postIndex, message, messageIndex);
        }

        public static void SendMessages(string Token, int accountIndex, int postIndex, string message, int messageIndex)
        {
            var client = new FacebookClient(Token);
            Page page = GraphCalls.getPage(client, accountIndex);
            var pageClient = new FacebookClient(page.access_token);
            Post post = GraphCalls.getPost(pageClient, page, postIndex);
            List<Comment> comments = GraphCalls.getComments(pageClient, post);
            GraphCalls.sendMessages(pageClient, comments, message, messageIndex);
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
