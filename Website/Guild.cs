using System;
using System.Net;
using System.Threading;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Pokemon
{
    public partial class Website
    {
        public delegate void GuildMembersReceived(List<CharInfo> members);

        public static void GuildMembers(string guildName, GuildMembersReceived callback)
        {
            string url = "http://www.Pokemon.com/community/?subtopic=guilds&page=view&GuildName=" + guildName;
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);

            request.BeginGetResponse(delegate(IAsyncResult ar)
            {
                string html = GetHTML(ar);

                callback(ParseGuildPage(html));
            }, request);
        }

        private static List<CharInfo> ParseGuildPage(string html)
        {
            List<CharInfo> members = new List<CharInfo>();

            MatchCollection matches = Regex.Matches(html, @""">([^<]+)</a>(\s\(([^\)]+)\))?</td>", RegexOptions.IgnoreCase);
            string guildName = Match(html, @"<h1>([^<]*)</h1>");

            foreach (Match m in matches)
            {
                members.Add(new CharInfo() 
                { 
                    Name = Prepare(m.Groups[1].Value),
                    GuildNickName = Prepare(m.Groups[3].Value), 
                    GuildName = guildName 
                });
            }
            return members;
        }
    }
}