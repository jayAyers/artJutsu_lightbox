using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Net;
using System.IO;

using System.Web.Services;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Response.Write(crawlImage("http://lookbook.nu/look/6013663-Iamvibes-Vest-Crystal-Collective-By-Black-Angel", "crawlPic"));  
        //Response.Write(crawlImage("http://instagram.com/p/mL91sHC8YV/", "crawlPic"));
        //Response.Write(crawlImage("http://www.trendyblendy.com/products/cappuccino-run-colorblock-sweater-1", "crawlPic"));

        //Response.Write(
        //crawlMeta("http://lookbook.nu/look/6212077-Am-Vibes-Dress-Stockings-Spikes-Sneakers-Chet-Faker-Talk-Is-Cheap", "og:description"));

        //Response.Write(crawlByTag("http://tarasov.deviantart.com/art/--410253220", "title"));
        //Response.Write(crawlByTag("http://8wayrun.com/threads/megaman-vs-sonic.18003/", "title"));

        string url = @"
<html>
 <head></head>
 <body>
  <iframe id='photoset_iframe_92549394632' class='photoset' scrolling='no' frameborder='0' height='-10' width='100%' style='border:0px; background-color:transparent; overflow:hidden;' src='http://supersonicart.com/post/92549394632/photoset_iframe/supersonicart/tumblr_n94ksd8giU1qz9v0t/0/false'>
 </body>
</html>
";

        //Response.Write(url);

        //Response.Write(crawlByTag(url, "iframe"));
        crawl_get_iframe("http://supersonicart.com/post/92549394632/phil-noto", "crawlPic");
        //Response.Write(crawlImage(url, "crawlPic"));
    }

    
    [WebMethod]
    public static string crawlImage(List<string> list)
    {//[0] url, [1] imgClass
        string html = crawl_get_iframe(list[0], list[1]);
        html += crawlImage(list[0], list[1]); 

        return html + "<div class='cleaner'></div>";
    }//end crawlImage()

    [WebMethod]
    public static string crawlMeta(List<string> list)
    {//[0] url, [1] contains
        return crawlMeta(list[0], list[1]);     
    }//end crawlMeta()

    [WebMethod]
    public static string crawlByTag(List<string> list)
    {//[0] url, [1] tagType
        return crawlByTag(list[0], list[1]);
    }//end crawlMeta()

    public static string crawl_get_iframe(string thisURL, string imgClass)
    {
        string html = "";

        string thisHTML = GetWebText(thisURL);
        //Response.Write(thisHTML.Substring(10000, 5000)); //how to programatically break this page apart???

        string[] splitThis = thisHTML.Split('\n');
        //Response.Write(splitThis.Length);
        //special rule for when ">" does not appear on same line
        List<string> list = new List<string>();
        string dilmThis = "";

        for (int i = 0; i < splitThis.Length - 1; i++)
        {
            for (int j = 0; j < 100; j++)
            {
                int dilm = j;// 0;
                //int dilm = 58;

                if (splitThis[i].Length > dilm)
                {
                    //start from 0 to 100 for now
                    //collect src in a list if src does not exist
                    //once collected "for" it to search for images

                    dilmThis = splitThis[i].Substring(dilm);

                    string txtA = "src=\"";
                    string txtB = "\"";

                    dilmThis = jpi.getStringBetween(dilmThis, txtA, 5, txtB);
                    if (dilmThis.Length > 0)
                    {                         
                        if (!list.Contains(dilmThis))
                        {
                            if (!dilmThis.Substring(dilmThis.Length - 4).Contains("."))//guessing if it has an extension //will review later
                            {
                                list.Add(dilmThis);
                                //Response.Write("<br />" + dilmThis + "<br />");
                            }
                        }
                    }//end if
                }//end for
            }//end for
        }//end for

        //Response.Write(list.Count);

        for (int i = 0; i < list.Count; i++)
        {
            html += crawlImage(list[i], imgClass);
        }
        //Response.Write(html);
        return html;
    }//end crawlByTag()

    public static string crawlByTag(string thisURL, string tag)
    {
        string html = "";

        string thisHTML = GetWebText(thisURL);
        string[] splitThis = thisHTML.Split('\n');

        //special rule for when ">" does not appear on same line
        List<string> list = new List<string>();

        for (int i = 0; i < splitThis.Length - 1; i++)
        {
             if (splitThis[i].Length > 6)
            {
                if (splitThis[i].Split('>')[0].ToLower().Trim() == "<" + tag) { html = splitThis[i].Split('>')[1].Split('<')[0].Trim(); }
            }//end if
        }//end for

        return html;
    }//end crawlByTag()

    public static string crawlMeta(string thisURL, string contains)
    {
        string html = "";

        string thisHTML = GetWebText(thisURL);  
        string[] splitThis = thisHTML.Split('\n');

        //special rule for when ">" does not appear on same line
        List<string> list = new List<string>();

        for (int i = 0; i < splitThis.Length - 1; i++)
        {
            if (splitThis[i].Length > 5)
            {
                if (splitThis[i].Split(' ')[0] == "<meta" && splitThis[i].Substring(splitThis[i].Length-1) != ">")
                {  list.Add(get_NL_meta(splitThis, i).Trim()); }
            }//end if
        }//end for

        string result = "";

        for (int i = 0; i < splitThis.Length - 1; i++)
        {
            result = GetStringInBetween("<meta ", @">", splitThis[i], false, false)[0];
            if (result.Length > 0) { list.Add(result.Trim()); }
        }//end for
        for (int i = 0; i < splitThis.Length - 1; i++)
        {
            result = GetStringInBetween("<meta ", @"/>", splitThis[i], false, false)[0];
            if (result.Length > 0) { list.Add(result.Trim()); }
        }//end for

        //all together now!
        for (int i = 0; i < list.Count; i++)
        {
            if(list[i].Contains(contains))
            {
                html = GetStringInBetween("content=\"", "\"", list[i], false, false)[0].Trim();
            }
        }

        return html;
    }//end crawlMeta

    public static string get_NL_meta(string[] arr, int index)
    {//meta data on new line        
        int i = index;
        string result = arr[i].Substring(5);

        bool is_end_meta = false; 

        while (i < arr.Length && !is_end_meta)
        {
            if (arr[i].Length > 1 && arr[i].Substring(arr[i].Length - 1) == ">")
            {
                is_end_meta = true;
                result += "<br />" + arr[i].Substring(0, arr[i].Length - 1).Trim();
            }        
            i++;
        }//end while

        return result;
    }//end get_NL_meta

    public static string crawlImage(string thisURL, string imgClass)
    {
        //returns crawlPic 
        string html = "";

        //string thisHTML = GetWebText("http://www.instintodevestir.com/2012/05/01/a-night-like-this/");
        //string thisHTML = GetWebText("http://lookbook.nu/look/6014165-Love-Nail-Tree-Even-Good-Men-Deserve-Little");
        //string thisHTML = GetWebText("http://lookbook.nu/look/6013663-Iamvibes-Vest-Crystal-Collective-By-Black-Angel");

        string thisHTML = GetWebText(thisURL);
        string[] splitThis = thisHTML.Split('\n');
        List<string> imgList = new List<string>();

        for (int i = 0; i < splitThis.Length - 1; i++)
        {
            string result = jpi.getStringBetween(splitThis[i], "<img", ">");

            string src_txtA = "src=\"";
            string src_txtB = "\"";

            if (result != "")
            {
                if (jpi.getStringBetween(result, src_txtA, 5, src_txtB).Length > 0) { imgList.Add(jpi.getStringBetween(result, src_txtA, 5, src_txtB)); }
            }
            else if (result == "")//get iframe 
            {//search by href="
                string txtA = "href=\"";
                string txtB = "\"";

                if (jpi.getStringBetween(splitThis[i], txtA, 6, txtB).Length > 0)
                { imgList.Add(jpi.getStringBetween(splitThis[i], txtA, 6, txtB)); }
            }
            else
            {//lookbook rule
                result = GetStringInBetween("data-url=\"", "\"", splitThis[i], false, false)[0];
                if (result != "") { imgList.Add(result.Split('?')[0]); }

                else 
                {//instagram rule
                    result = GetStringInBetween("<meta property=\"og:image\" content=\"", "\"", splitThis[i], false, false)[0];
                    if (result != "") { imgList.Add(result.Split('?')[0]); }
                }
            }
        }//end for


        //check for jpg, jpeg, bmp, png, gif
        string[] removePic = { "blank.gif", "rss.png", "twitter.png", "images/folder.png", "captcha/images/refresh.png" };

        for (int j = 0; j < imgList.Count; j++)
        {//http://2.bp.blogspot.com/-6pBoGJMOvVU/UFTvObf2_QI/AAAAAAAAA04/gVM7dUK-kEo/s1600/kiss-anime.jpeg
            //http://www.bloglovin.com/viewer?post=654007131&group=0&frame_type=b&blog=2342201&frame=1&click=0&user=0
            //Response.Write(imgList[j] + "<br />");
            if (imgList[j].EndsWith(".jpg".ToLower()) || imgList[j].EndsWith(".jpeg".ToLower())
                || imgList[j].EndsWith(".bmp".ToLower()) || imgList[j].EndsWith(".png".ToLower()) || imgList[j].EndsWith(".gif".ToLower()))
            {
                string checkThis = checkRemovePic(imgList[j], removePic, 0);
                if (checkThis == "success") 
                {
                    //Response.Write("<img src='" + imgList[j] + "' />");

                    //thisURL
                    if (thisURL.Substring(0, 7) == "http://" && imgList[j].Substring(0, 1) == "/")
                    { 
                        //Response.Write(thisURL.Split('/')[0] + "/" + thisURL.Split('/')[1] + "/" + thisURL.Split('/')[2] + "<br />");
                        imgList[j] = thisURL.Split('/')[0] + "/" + thisURL.Split('/')[1] + "/" + thisURL.Split('/')[2] + imgList[j];
                    }
                    html += String.Format("<img id='{0}' class='{1}' src='{2}' />", imgClass + j, imgClass, imgList[j]);
                }
            }
        }//end for

        return html;    
    }

    protected static string checkRemovePic(string content, string[] checkArr, int currCount)
    {
        string checker = "success";

        //Response.Write("checking..." + checkArr[currCount] + "<br />");

        if (content.EndsWith(checkArr[currCount]))
        { checker = checkArr[currCount]; }// "bingo"; }

        else {
            currCount++;

            if (currCount < checkArr.Length) { checker = checkRemovePic(content, checkArr, currCount); }
        }

        return checker;
    
    }//end checkRemovePic()

    protected static string GetWebText(string url)
    {//from www.thecodinghumanist.com/Content/HowToWriteAWebCrawlerInCSharp.aspx
        string htmlText = "";
        try
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);

            request.UserAgent = "A .NET Web Crawler";

            WebResponse response = request.GetResponse();

            Stream stream = response.GetResponseStream();

            StreamReader reader = new StreamReader(stream);

            htmlText = reader.ReadToEnd();
        }
        catch { }

        return htmlText;

    }

    public static string[] GetStringInBetween(string strBegin,
    string strEnd, string strSource,
    bool includeBegin, bool includeEnd)
    {
        //from www.mycsharpcorner.com/Post.aspx?postID=15

        string[] result = { "", "" };
        int iIndexOfBegin = strSource.IndexOf(strBegin);
        if (iIndexOfBegin != -1)
        {
            // include the Begin string if desired
            if (includeBegin)
                iIndexOfBegin -= strBegin.Length;
            strSource = strSource.Substring(iIndexOfBegin
                + strBegin.Length);
            int iEnd = strSource.IndexOf(strEnd);
            if (iEnd != -1)
            {
                // include the End string if desired
                if (includeEnd)
                    iEnd += strEnd.Length;
                result[0] = strSource.Substring(0, iEnd);
                // advance beyond this segment
                if (iEnd + strEnd.Length < strSource.Length)
                    result[1] = strSource.Substring(iEnd
                        + strEnd.Length);
            }
        }
        else
            // stay where we are
            result[1] = strSource;
        return result;
    }//end
}

        