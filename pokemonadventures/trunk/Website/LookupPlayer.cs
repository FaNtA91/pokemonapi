using System;
using System.Net;
using System.Threading;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Windows.Forms;

namespace Pokemon
{
    public partial class Website
    {
        public delegate void LookupReceived(CharInfo info);

        public static void LookupPlayer(string playername, LookupReceived callback)
        {
            playername = playername.Replace(' ', '+');
            playername = playername.Replace((char)0xA0, '+'); // Non-breaking space

            string url = "http://www.Pokemon.com/community/?subtopic=characters&name=" + playername;

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);

            request.BeginGetResponse(delegate(IAsyncResult ar)
            {
                string html = GetHTML(ar);

                callback(CharInfo.Parse(html));
            }, request);
        }

        public class CharInfo
        {
            public string Name;
            public string Sex;
            public string Profession;
            public int Level;
            public string World;
            public string Residence;
            //public string House;
            //public DateTime HousePaidUntil;
            public string GuildName;
            public string GuildTitle;
            //public DateTime LastLogin;
            public string Comment;
            public string AccountStatus;

            //public CharDeath[] Deaths;

            public string RealName;
            public string Location;
            //public DateTime Created;

            //public CharInfo[] Characters;

            //public string Status;

            //public DateTime GuildJoin;
            public string GuildNickName;

            public static CharInfo Parse(string html)
            {
                CharInfo i = new CharInfo();
                try
                {
                    i.Name = Match(html, @"Name:</td><td>([^<]*)</td>");
                    i.Sex = Match(html, @"Sex:</td><td>([^<]*)</td>");
                    i.Profession = Match(html, @"Profession:</td><td>([^<]*)</td>");
                    i.Level = int.Parse(Match(html, @"Level:</td><td>([^<]*)</td>"));
                    i.World = Match(html, @"World:</td><td>([^<]*)<\/td>");
                    i.Residence = Match(html, @"Residence:</td><td>([^<]*)</td>");
                    string guildDetails = Match(html, @"membership:</td><td>(.*?)</td>");
                    i.GuildTitle = Match(guildDetails, @"(.*) of the <a href");
                    i.GuildName = Match(guildDetails, @">([^<]*)</a>");

                    // Requires more complex parsing
                    //i.LastLogin = DateTime.Parse(HttpUtility.HtmlDecode(Regex.Match(html, @"Last Login:<\/TD><TD>([^<]*)<\/TD>").Groups[1].Value));
                    i.Comment = Match(html, @"Comment:</td><td>(.*?)</td>").Replace("<br />", string.Empty);
                    i.AccountStatus = Match(html, @"Account&#160;Status:</td><td>([^<]*)</td>");


                    i.RealName = Match(html, @"Real name:</td><td>([^<]*)</td>");
                    i.Location = Match(html, @"Location:</td><td>([^<]*)</td>");
                    // Requires more complex parsing
                    //i.Created = DateTime.Parse(HttpUtility.HtmlDecode(Regex.Match(html, @"Created:<\/TD><TD>([^<]*)<\/TD>").Groups[1].Value));
                    MatchCollection deaths = Regex.Matches(html, @"<tr bgcolor=(?:#D4C0A1|#F1E0C6)><td width=25%>(.*?)?</td><td>((?:Died|Killed) at Level ([^ ]*)|and) by (?:<[^>]*>)?([^<]*)", RegexOptions.Singleline);
                    // TODO finish this!
                }
                catch
                {
                    return i;
                }
                return i;
            }
        }

        public class CharDeath
        {
            public string CharName;
            public DateTime Time;
            public int AtLevel;
            public string[] KilledBy;
        }
    }
}