/*----------------------------------------*/
/*                                        */
/* Helper.cs                              */
/*                                        */
/* WISE (C) Shoji Urashita                */
/*----------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using System.Globalization;

namespace Search
{
    public static class Helper
    {
        public static Regex extractUrl1 = new Regex(@"[&?](?:q|url)=([^&]+)", RegexOptions.Compiled);
        public static Regex extractUrl2 = new Regex("<a [^<]*(?<href>href=\"[^\"]+\")[^<]*>", RegexOptions.Compiled);

        public static string GetGoogleSearchResultHtlm(string kensakuword)
        {
            //StringBuilder sb = new StringBuilder("http://www.google.com/search?q=");

            StringBuilder sb = new StringBuilder("http://www.google.com/search?hl=ja&num=100&q=");
            sb.Append(HttpUtility.UrlEncode(kensakuword).ToString());

            WebClient webClient = new WebClient();
            //webClient.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.11 (KHTML, like Gecko) Chrome/23.0.1271.97 Safari/537.11 AlexaToolbar/alxg-3.1");
            //webClient.Encoding = System.Text.Encoding.UTF8;
            return webClient.DownloadString(sb.ToString());
        }

        public static List<String> ParseGoogleSearchResultHtml(string html, string siteURL, ref string keywordPosition, ref string searchVolumn, ref string googleURL, ref string googleTitle)
        {

            List<String> searchResults = new List<string>();

            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);

            var div = doc.DocumentNode.SelectSingleNode("//div[@id='resultStats']");
            string text = div.InnerHtml.ToString();
            var matches = Regex.Matches(text, @"([0-9,]+)");
            searchVolumn = matches[0].Groups[1].Value;

            /*
            var nodes = (from node in doc.DocumentNode.SelectNodes("//a")
                         let href = node.Attributes["href"]
                         where null != href
                         where href.Value.Contains("/url?") || href.Value.Contains("?url=")
                         select href.Value).ToList();
            */

            // get search result URLs
            var xpath = "//li[contains(concat(' ',@class,' '),' g ')]" + "/h3[contains(concat(' ',@class,' '),' r ')]" + "/a/@href";
            var nodes = doc.DocumentNode.SelectNodes(xpath);


            int num = 1;
            Boolean isFirst = true;
            if (nodes == null)
            {
                return null;
            }
            foreach (var node in nodes)
            {
                var match = extractUrl1.Match(node.OuterHtml);
                string checkURL = HttpUtility.UrlDecode(match.Groups[1].Value);

                if (checkURL.StartsWith("http://webcache.googleusercontent.com/search") || checkURL.StartsWith("/settings/ads/preferences"))
                {
                    continue;
                }

                Uri u1 = new Uri(siteURL);
                IdnMapping idn = new IdnMapping();
                string punny = idn.GetAscii(u1.DnsSafeHost);
                if (checkURL.StartsWith("http://" + punny))
                {
                    if (isFirst)
                    {
                        isFirst = false;
                        keywordPosition = num.ToString();
                        googleURL = checkURL;
                        googleTitle = node.InnerText;
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
                num++;

                searchResults.Add(checkURL);
            }

            return searchResults;
        }


        internal static string GetYahooSearchResultHtlm(string kensakuword)
        {
            StringBuilder sb = new StringBuilder("http://search.yahoo.co.jp/search?&n=100&p=");

            //sb = new StringBuilder("http://search.yahoo.co.jp/search?p=geek%A4%CA%A4%DA%A1%BC%A4%B8&fr=top&src=top");
            sb.Append(HttpUtility.UrlEncode(kensakuword).ToString());

            WebClient webClient = new WebClient();
            webClient.Encoding = System.Text.Encoding.UTF8;
            return webClient.DownloadString(sb.ToString());
        }

        internal static List<string> ParseYahooSearchResultHtml(string html, string siteURL, ref string keywordPosition, ref string searchVolumn, ref string yahooURL, ref string yahooTitle)
        {
            List<String> searchResults = new List<string>();

            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);

            var div = doc.DocumentNode.SelectSingleNode("//div[@id='bd']");
            string text = div.InnerHtml.ToString();
            var matches = Regex.Matches(text, @"約([0-9,]+)");
            searchVolumn = matches[0].Groups[1].Value;

            /*
            var nodes = (from node in doc.DocumentNode.SelectNodes("//a")
                         let href = node.Attributes["href"]
                         where null != href
                         where href.Value.Contains("http://")
                         select href.Value).ToList();
            */

            // get search result URLs
            var xpath = "//a";
            var nodes = doc.DocumentNode.SelectNodes(xpath);

            int num = 1;
            Boolean isFirst = true;
            foreach (var node in nodes)
            {
                var match = extractUrl2.Match(node.OuterHtml);
                string checkURL = HttpUtility.UrlDecode(match.Groups[1].Value);
                checkURL = HttpUtility.HtmlDecode(match.Groups[1].Value);

                checkURL = checkURL.Replace("href=", ""); // Remove htref=
                checkURL = checkURL.Replace("\"", "");    // Remove back slash

                var sibl = node.NextSibling;

                if (checkURL.StartsWith("http://www.yahoo.co.jp") || checkURL.StartsWith("http://search.yahoo.co.jp") || node.NextSibling == null)
                {
                    continue;
                }

                if (!(node.NextSibling.Name.Contains("div")))
                {
                    continue;
                }

                if (checkURL.StartsWith(siteURL))
                {
                    if (isFirst)
                    {
                        isFirst = false;
                        keywordPosition = num.ToString();
                        yahooURL = checkURL;
                        yahooTitle = node.InnerText;
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
                num++;

                searchResults.Add(checkURL);
            }

            return searchResults;
        }

        internal static string GetBingSearchResultHtlm(string kensakuword)
        {
            StringBuilder sb = new StringBuilder("http://www.bing.com/search?filt=lf&q=");

            sb.Append(HttpUtility.UrlEncode(kensakuword).ToString());
            //sb.Append("&lf=1"); //Only Japanese

            WebClient webClient = new WebClient();
            webClient.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.11 (KHTML, like Gecko) Chrome/23.0.1271.97 Safari/537.11 AlexaToolbar/alxg-3.1");
            webClient.Encoding = System.Text.Encoding.UTF8;
            return webClient.DownloadString(sb.ToString());
        }

        internal static List<string> ParseBingSearchResultHtml(string html, string siteURL, ref string keywordPosition, ref string searchVolumn, ref string bingURL, ref string bingTitle)
        {
            List<String> searchResults = new List<string>();

            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);

            var div = doc.DocumentNode.SelectSingleNode("//span [@class='sb_count']");
            string text = div.InnerHtml.ToString();
            var matches = Regex.Matches(text, @"([0-9,]+)");
            searchVolumn = matches[0].Groups[1].Value;

            /*
            var nodes = (from node in doc.DocumentNode.SelectNodes("//a")
                         let href = node.Attributes["href"]
                         where null != href
                         where href.Value.Contains("http://")
                         select href.Value).ToList();
            */
            // get search result URLs
            var xpath = "//a";
            var nodes = doc.DocumentNode.SelectNodes(xpath);

            int num = 1;
            Boolean isFirst = true;
            foreach (var node in nodes)
            {
                var match = extractUrl2.Match(node.OuterHtml);
                string checkURL = HttpUtility.UrlDecode(match.Groups[1].Value);
                checkURL = HttpUtility.HtmlDecode(match.Groups[1].Value);

                checkURL = checkURL.Replace("href=", ""); // Remove htref=
                checkURL = checkURL.Replace("\"", "");    // Remove back slash

                if (!checkURL.StartsWith("http") || checkURL.StartsWith("http://www.bingshopping.jp/price/keyword_search") || checkURL.StartsWith("http://www.microsofttranslator.com/bv.aspx"))
                {
                    continue;
                }

                if (checkURL.StartsWith(siteURL))
                {
                    if (isFirst)
                    {
                        isFirst = false;
                        keywordPosition = num.ToString();
                        bingURL = checkURL;
                        bingTitle = node.InnerText;
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
                num++;

                searchResults.Add(checkURL);
            }

            return searchResults;
        }


    }
}
