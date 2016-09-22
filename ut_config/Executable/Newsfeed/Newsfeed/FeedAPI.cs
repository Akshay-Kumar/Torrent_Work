using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Collections;
using System.Diagnostics;
using System.Data;
using System.Data.SqlClient;
namespace Newsfeed
{
    class FeedAPI
    {
        public static List<feeds> NewsFlash = new List<feeds>();
        private static string[] readFeedsFromFile()
        {
            string[] chunks = null;
            if (System.IO.File.Exists("config.ini"))
            {
                System.IO.StreamReader r = new System.IO.StreamReader("config.ini");
                try
                {
                    string line = r.ReadLine();
                    chunks = line.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception : " + e.Message);
                }
                finally
                {
                    r.Close();
                }
            }
            return chunks;
        }
        private static void writeFeedsToFile(List<feeds> feedList)
        {
            System.IO.StreamWriter w = new System.IO.StreamWriter("config.ini");
            try
            {
                string txt = string.Empty;
                foreach (feeds f in feedList)
                {
                    txt += f.GuId + ";";
                }
                txt = txt.Substring(0, txt.Length - 1);
                w.Write(txt);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception : " + e.Message);
            }
            finally
            {
                w.Close();
            }
        }
        public static List<feeds> getNewsFeeds()
        {
            List<feeds> feedList = new List<feeds>();
            try
            {
                System.Net.WebClient objClient = new System.Net.WebClient();
                feeds feedData = null;
                string response = string.Empty;
                string title = string.Empty;
                string nr = string.Empty;
                string guid = string.Empty;
                string pubDate = string.Empty;
                string link = string.Empty;
                //Creating a new xml document
                XmlDocument doc = new XmlDocument();

                //reading data and converting to string
                response = Encoding.UTF8.GetString(objClient.DownloadData(@"http://feeds.feedburner.com/ndtvnews-latest?format=xml"));
                /*
                response = response.Replace(
                     @"<rss xmlns:feedburner=""http://rssnamespace.org/feedburner/ext/1.0"" version=""2.0"">
                    <channel>
                       <title>NDTV News  - Latest</title>
                       <link>http://www.ndtv.com/</link>
                       <lastBuildDate>June 26, 2016 04:26 PM</lastBuildDate>
                       <newsAppVersion>0.6</newsAppVersion>
                       <timezone>GMT+05:30</timezone>
                       <recordcount>200</recordcount>
                       <atom10:link xmlns:atom10=""http://www.w3.org/2005/Atom"" rel=""self"" type=""application/rss+xml"" 
                    href=""http://feeds.feedburner.com/ndtvnews-latest"" /><feedburner:info 
                    uri=""ndtvnews-latest"" /><atom10:link xmlns:atom10=""http://www.w3.org/2005/Atom"" 
                    rel=""hub"" href=""http://pubsubhubbub.appspot.com/"" />
                    <feedburner:browserFriendly></feedburner:browserFriendly>", @"<feed>");
                response = response.Replace(@"</channel></rss>", @"</feed>");
                 */

                //response = response.Replace(@"<rss xmlns:feedburner=""http://rssnamespace.org/feedburner/ext/1.0"" version=""2.0"">", @"<rss>");
                //loading into an XML so we can get information easily
                doc.LoadXml(response);

                //nr of emails
                //nr = doc.SelectSingleNode(@"/rss/fullcount").InnerText;
                //Reading the title and the summary for every email
                foreach (XmlNode node in doc.SelectNodes(@"/rss/channel/item"))
                {
                    title = node.SelectSingleNode("title").InnerText;
                    pubDate = node.SelectSingleNode("pubDate").InnerText;
                    link = node.SelectSingleNode("link").InnerText;
                    guid = node.SelectSingleNode("guid").InnerText;
                    feedData = new feeds();
                    feedData.Title = title;
                    feedData.GuId = guid;
                    feedData.PubDate = formatDate(pubDate);
                    feedData.Link = link;
                    feedList.Add(feedData);
                }
                feedList.Sort();
                writeFeedsToFile(feedList);
            }
            catch (Exception exe)
            {
                Console.WriteLine(String.Format("Check your network connection : {0}", exe.Message));
            }
            return feedList;
        }
        private static DateTime formatDate(string date)
        {
            //Sun, 26 Jun 2016 16:05:27 +0530
            string[] chunks = date.Split(' ');
            DateTime dateTime = new DateTime(
                Convert.ToInt32(chunks[3].Trim()),
                Convert.ToInt32(getMonth(chunks[2].Trim())),
                Convert.ToInt32(chunks[1].Trim()),
                Convert.ToInt32(chunks[4].Split(':')[0].Trim()),
                Convert.ToInt32(chunks[4].Split(':')[1].Trim()),
                Convert.ToInt32(chunks[4].Split(':')[2].Trim())
                );
            return dateTime;
        }
        private static int getMonth(string month)
        {
            int rMon = -1;
            switch (month)
            {
                case "Jan": rMon = 1;
                break;
                case "Feb": rMon = 2;
                break;
                case "Mar": rMon = 3;
                break;
                case "Apr": rMon = 4;
                break;
                case "May": rMon = 5;
                break;
                case "Jun": rMon = 6;
                break;
                case "Jul": rMon = 7;
                break;
                case "Aug": rMon = 8;
                break;
                case "Sep": rMon = 9;
                break;
                case "Oct": rMon = 10;
                break;
                case "Nov": rMon = 11;
                break;
                case "Dec": rMon = 12;
                break;
            }
            return rMon;
        }
        private static string formatLink(string link, string title)
        {
            return @"<a href=" + link + @">" + title + "</a>";
        }
        private static void StartProcess(string sUrl)
        {
            ProcessStartInfo sInfo = new ProcessStartInfo(sUrl);
            Process.Start(sInfo);
        }

        public static void feedScanner()
        {
            if (Program.global == 0)
            {
                Program.global++;
            }
            else
            {
                System.Threading.Thread.Sleep(1000000);
            }
            string[] feedsArray = null;
            try
            {
                if ((feedsArray = readFeedsFromFile()) != null)
                {
                    if (feedsArray.Length > 0)
                    {
                        string[] oldFeeds = feedsArray;
                        feeds[] newFeeds = FeedAPI.getNewsFeeds().ToArray<feeds>();
                        for (int i = 0; i < newFeeds.Length; i++)
                        {
                            if (oldFeeds.Contains(newFeeds[i].GuId))
                                continue;
                            if (!NewsFlash.Contains(newFeeds[i]))
                            {
                                NewsFlash.Add(newFeeds[i]);
                                ExecuteCommand(@"C:\Users\AkshayKumar\Desktop\Torrent_Work\ut_config\feeds.bat", newFeeds[i].Title, newFeeds[i].Link, newFeeds[i].Title);
                            }
                        }
                    }
                }
                else
                {
                    List<feeds> newsFeeds = FeedAPI.getNewsFeeds();
                    writeFeedsToFile(newsFeeds);
                }
                feedScanner();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.Red;
            }
        }

        static void ExecuteCommand(string fileName,params string[] args)
        {
            int exitCode;
            ProcessStartInfo processInfo;
            Process process;
            StringBuilder arguments = new StringBuilder();
            foreach (string a in args)
            {
                arguments.Append('"' + a + '"');
                arguments.Append(" ");
            }
            processInfo = new ProcessStartInfo();
            processInfo.FileName = fileName;
            processInfo.Arguments = arguments.ToString().Trim();
            processInfo.CreateNoWindow = true;
            processInfo.UseShellExecute = false;
            // *** Redirect the output ***
            processInfo.RedirectStandardError = true;
            processInfo.RedirectStandardOutput = true;

            process = Process.Start(processInfo);
            process.WaitForExit();

            // *** Read the streams ***
            // Warning: This approach can lead to deadlocks, see Edit #2
            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();

            exitCode = process.ExitCode;
            if (!String.IsNullOrEmpty(output))
                Console.ForegroundColor = ConsoleColor.Green;
            if(!String.IsNullOrEmpty(error))
                Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("output>>" + (String.IsNullOrEmpty(output) ? "(none)" : output));
            Console.WriteLine("error>>" + (String.IsNullOrEmpty(error) ? "(none)" : error));
            Console.WriteLine("ExitCode: " + exitCode.ToString(), "ExecuteCommand");
            process.Close();
        }

    }

    class feeds : IComparable<feeds>,IEquatable<feeds>
    {
        string link;

        public string Link
        {
            get { return link; }
            set { link = value; }
        }
        string title;

        public string Title
        {
            get { return title; }
            set { title = value; }
        }
     
        DateTime pubDate;

        public DateTime PubDate
        {
            get { return pubDate; }
            set { pubDate = value; }
        }
        string guId;

        public string GuId
        {
            get { return guId; }
            set { guId = value; }
        }

        public int CompareTo(feeds feed)
        {
            return feed.pubDate.CompareTo(this.pubDate);
        }
        public bool Equals(feeds other)
        {
            // Would still want to check for null etc. first.
            return this.guId == other.guId;
        }
    }
}

