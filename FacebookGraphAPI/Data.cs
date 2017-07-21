using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookGraphAPI
{
    class Data
    {
        public class Friends
        {
            public List<User> data { get; set; }
        }
        public class User
        {
            public string id { get; set; }
            public string name { get; set; }
        }
        public class Place
        {
            public string id { get; set; }
        }
        public class Post
        {
            public Post(JToken jData)
            {
                id = jData["id"].ToString().Replace("\"", "");
                try { name = jData["name"].ToString().Replace("\"", ""); }
                catch(Exception) { /* name doesn't exit */ }
                try { created_time = jData["created_time"].ToString().Replace("\"", ""); }
                catch(Exception) { /* DNE */}
                try {  message = jData["message"].ToString().Replace("\"", ""); }
                catch (Exception) { /* message doesn't exist */ }
                try { link = jData["link"].ToString().Replace("\"", ""); }
                catch (Exception) { /* link doesn't exist */ }
                try { story = jData["story"].ToString().Replace("\"", ""); }
                catch (Exception) { /* story doesn't exist */ }
            }
            public Post() { }
            public string id { get; set; }
            public string caption { get; set; }
            public string created_time { get; set; } // DateTime object
            public string description { get; set; }
            public User from { get; set; }
            public string is_published { get; set; }
            public string link { get; set; }
            public string message { get; set; }
            public string name { get; set; }
            public string parent_id { get; set; }
            public string picture { get; set; }
            public string place { get; set; }
            public string tag { get; set; }
            public string story { get; set; }
            public string updated_time { get; set; } // DateTime object

        }
        public class Participants
        {
            public string id { get; set; }
            public string name { get; set; }
            public string email { get; set; }

        }
        public class Conversation
        {
            public string id { get; set; }
            public string snippet { get; set; }
            public string updated_time { get; set; } // DateTime object
            public int message_count { get; set; }
            public int unread_count { get; set; }
            public Participants participants { get; set; }
            public Participants senders { get; set; }
            public string can_reply { get; set; }
            public string is_subscribed { get; set; }
        }
        public class Comment
        {
            public Comment(JToken jData)
            {
                try { id = jData["id"].ToString().Replace("\"", ""); }
                catch (Exception) {  /* id always exists */ }
                try { attachment = jData["attachment"].ToString().Replace("\"", ""); }
                catch (Exception) {  /* attachment doesn't exist */ }
                try { can_comment = jData["can_comment"].ToString().Replace("\"", ""); }
                catch (Exception) {  /* can_comment doesn't exist */ }
                try { can_like = jData["can_like"].ToString().Replace("\"", ""); }
                catch (Exception) {  /* can_like doesn't exist */ }
                try { can_like = jData["can_like"].ToString().Replace("\"", ""); }
                catch (Exception) {  /* can_like doesn't exist */ }
                try { can_reply_privately = jData["can_reply_privately"].ToString().Replace("\"", ""); }
                catch (Exception) {  /* can_reply_privately doesn't exist */ }
                try { comment_count = (int)jData["comment_count"]; }
                catch (Exception) {  /* comment_count doesn't exist */ }
                try { created_time = jData["created_time"].ToString().Replace("\"", ""); }
                catch (Exception) {  /* created_time doesn't exist */ }
                try { from_id = jData["from"]["id"].ToString().Replace("\"", ""); }
                catch (Exception) {  /* from.id doesn't exist */ }
                try { from_name = jData["from"]["name"].ToString().Replace("\"", ""); }
                catch (Exception) {  /* from.name doesn't exist */ }
                try { like_count = (int)jData["like_count"]; }
                catch (Exception) {  /* like_count doesn't exist */ }
                try { message = jData["message"].ToString().Replace("\"", ""); }
                catch (Exception) {  /* message doesn't exist */ }
                // set parent
                // set conversation
            }
            public string id { get; set; }
            public string attachment { get; set; }
            public string can_comment { get; set; }
            public string can_like { get; set; }
            public string can_reply_privately { get; set; }
            public int comment_count { get; set; }
            public string created_time { get; set; } // DateTime object
            public string from_id { get; set; }
            public string from_name { get; set; }
            public int like_count { get; set; }
            public string message { get; set; }
            public Comment parent { get; set; }
            public Conversation private_reply_conversation { get; set; }
        }
        public class Page
        {
            public Page(JToken jData)
            {
                try { id = jData["id"].ToString().Replace("\"", ""); }
                catch (Exception) {  /* no id */ }
                try { name = jData["name"].ToString().Replace("\"", ""); }
                catch (Exception) {  /* no name */ }
                try { access_token = jData["access_token"].ToString().Replace("\"", ""); }
                catch (Exception) {  /* no access_token */ }
            }
            public string id { get; set; }
            public string name { get; set; }
            public string access_token { get; set; }

        }
    }
}
