
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.Services;

using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using System.IO;

//http://www.colorpicker.com/
//http://www.somacon.com/p142.php


public partial class _Default : System.Web.UI.Page
{
    //static char S222 = (char)222; //"'þ"
    //static char S216 = (char)216; //Ø
    static char c254 = (char)254; //"'þ"

    protected void Page_Load(object sender, EventArgs e)
    {
        Session["naughty"] = "nice";
        //Session["naughty"] = "naughty";
        //Session["naughty"] = "filthy";

        //Session["UID"] = "";
        if (Session["UID"] == null) { Session["UID"] = ""; }
        //Response.Write(Session["UID"]);

        /*
        DateTime date = DateTime.Now;
        Response.Write(date.ToString() + "<br />");
        TimeSpan time = new TimeSpan(-1, 0, -22, 0);
        DateTime combined = date.Add(time);
        Response.Write(combined.ToString());   
        */
    }

    #region webMethods


    [WebMethod]
    public static string cantFind(List<string> list)
    {
        string html = "";

        if (list[0] == "searchPic")
        {
            html = @"
<div id='webSearch_report_noPic'>I can't find my pic -_-</div>
<div id='webSearch_report_info' style='margin: 12px 0 0 35px;'>
 Don't fret. We're here to help.<br />

<span style='font-size: 12px;'>
  We all want to learn how to draw like our favorite artists, so artJutsu is constantly trying to improve. 
  <br />To help us, we need your help.

  <br /><br />We have the link, now we just need the pic (or a description of it).
</span>

<br /><textarea id='webSearch_cantFind_TB' style='margin-top: 15px; width: 550px; max-width: 550px; height: 125px; max-height: 125px;'></textarea>
<div id='webSearch_cantFind_submit' 
 style='margin: 15px 435px; padding: 5px 0 8px; width: 100px; border: 1px solid #708090; text-align: center; font-weight: bolder; font-size: 16px;
        color: #708090; box-shadow: 3px 3px 3px #ccc; cursor: pointer;'
>submit</div>

</div>
";
        }

        return html;
    }//end cantFind()

    [WebMethod]
    public static string cantFind_submit(List<string> list)
    {//[0]searchType, [1] search_tb, [2] pic_info

        string description = "";

        if (list[0] == "searchPic") { description = String.Format("searchTB: {0}<br />description: {1}", list[1], list[2]); }

        return cantFind_submit(list[0], description);

    }//end cantFind_submit()


    public static string cantFind_submit(string searchType, string description)
    {
        string connStr = ConfigurationManager.ConnectionStrings["aJ_ConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connStr);

        try
        {
            conn.Open();
            string query = @"
IF NOT EXISTS (select description from aj_cantFind where search_type = @searchType and description = @description)
begin
  insert into aj_cantFind (search_type, description, userID, date)
  values (@searchType, @description, @uid, @date)             
end
            ";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add(new SqlParameter("@searchType", SqlDbType.VarChar)).Value = searchType;
            cmd.Parameters.Add(new SqlParameter("@description", SqlDbType.VarChar)).Value = description;
            cmd.Parameters.Add(new SqlParameter("@uid", SqlDbType.VarChar)).Value = "";
            cmd.Parameters.Add(new SqlParameter("@date", SqlDbType.VarChar)).Value = DateTime.Now.ToString();

            cmd.ExecuteNonQuery();
        }finally { conn.Close(); }

        return "Thanks!";
    }//end cantFind_submit()

    [WebMethod]
    public static string storeURL(List<string> list)
    { return storeURL(list[0]); }

    [WebMethod]
    public static string getUID(List<string> list)
    {
       // HttpContext.Current.Session["UID"] = "non";
 
        return (string)HttpContext.Current.Session["UID"];
    }//end getUID()

    [WebMethod]
    public static string getColor(List<string> list)
    {
        //jpi j = new jpi();
        //j.Delim = 'Þ';

        return getColor();// jpi.parseHTML("template.html", "color");       
    }//end getColor()

    [WebMethod]
    public static string getHue(List<string> list)
    { return jpi.parseHTML("template.html", "hue"); }

    [WebMethod]
    public static string getGrid(List<string> list)
    { return jpi.parseHTML("template.html", "grid"); }

    [WebMethod]
    public static string getBGSight(List<string> list)
    { return jpi.parseHTML("template.html", "bgSight"); }

    [WebMethod]
    public static string getSaveBtn(List<string> list)
    {
        return @"<div id='save_prompt'>
      <div id='save_title'>error</div>
      <div id='save_prompt_arrow_border'></div>
      <div id='save_prompt_arrow'></div>     
      </div>
      <img id='saveCanvas' src='image/icons/save.png' />";
    } // jpi.parseHTML("template.html", "saveCanvas"); }

    [WebMethod]
    public static string saveCanvasImage(List<string> list) 
    {//[0] image, [1] userID, [2] url, [3] title, [4] desc, [5] bg_src
        //http://stackoverflow.com/questions/15571022/how-to-find-reason-for-generic-gdi-error-when-saving-an-image
        //http://stackoverflow.com/questions/5400173/converting-a-base-64-string-to-an-image-and-saving-it

        string canvasPic = "tempPic" + list[1] + ".png";//can be random name

        canvasPic = list[1] + "_" + DateTime.Now.ToString().Replace(" ", "_").Replace("/", "_").Replace(":", "_") + ".png";
        
        System.Drawing.Image image;
        byte[] data = Convert.FromBase64String(list[0]);
        using (var stream = new MemoryStream(data, 0, data.Length)){ image = System.Drawing.Image.FromStream(stream); }
        image.Save(System.Web.HttpContext.Current.Server.MapPath(@"image\" + canvasPic));
        /////////////////////


        string imgSrc = "image";
        if (list[2].Contains("youtube.com")) 
        { 
            imgSrc = "youtube";
            list[5] = "http://img.youtube.com/vi/" + list[2].Split('?')[1].Substring(2) + "/3.jpg";
        }

        //(string title, string blab)
        return saveCanvasImage(canvasPic, list[1], imgSrc, list[2], list[3], list[4], list[5], list[0]);
    }//saveCanvasImage()

    [WebMethod]
    public static string getPalette(List<string> list)
    { return jpi.parseHTML("template.html", "tutorial"); }

    [WebMethod]
    public static string loadTutorialDisplay(List<string> list) 
    { 
       //if id exists, show tutorial/ else show "create tutorial" option

        string html = @"
<div class='tutorialDisplay td1'>
 <img class='1' src='image/tutorial/step 1.png' />
 <img class='2' src='image/tutorial/step 2.png' />
 <img class='3' src='image/tutorial/step 3.png' />
</div>
";
        return html;    
    }

    [WebMethod]
    public static string chkNuPic(List<string> list)
    {
        string html = searchLightbox("", "", "chkNuPic"); //@"<div id='update_pic'>" + DateTime.Now + " new drawings</div>";

        return html;
    }//end chkNuPic()

    [WebMethod]
    public static string getAutoDraw(List<string> list)
    {
        string html = "";

        if (list[0] != "") {  html = searchLightbox("", list[0], "dailyDraw"); }
        else { html = getDailyDraw(); }

        return html;
    }//end getAutoDraw()

    [WebMethod]
    public static string searchLightbox(List<string> list)
    {//[0] searchTB

        string html = "";

        html += searchLightbox(list[0], "", "");

        //html += @"<div id='close_webSearch'>x</div>";


        return html;
    }//end searchLightbox()

    [WebMethod]
    public static string getLightboxEntry(List<string> list)
    { return searchLightbox("", list[0], "searchByKey"); } //@"<img id='lightbox_pic_viewer' src='image/cigar.jpg' /></div>þ"; }

    [WebMethod]
    public static string getCanvasInfo(List<string> list)
    {//[0] key, [1] type

        return getCanvasInfo(list[0], list[1]);
    }
#endregion

    #region functions
    public static string getDailyDraw()
    {
        string id = "";

        string connStr = ConfigurationManager.ConnectionStrings["aJ_ConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connStr);
        SqlDataReader rdr = null;

        try
        {
            conn.Open();

            string query = @"select lightbox_entry_key from lightbox_dailies where date_released = @date";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add(new SqlParameter("@date", SqlDbType.VarChar)).Value = DateTime.Now;
            rdr = cmd.ExecuteReader();

            while (rdr.Read()){ id = rdr[0].ToString(); }
        }finally { rdr.Close(); conn.Close(); }

        return searchLightbox("", id, "dailyDraw");
    }//end getDailyDraw()

    public static string storeURL(string url)
    {
        string connStr = ConfigurationManager.ConnectionStrings["aJ_ConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connStr);

        try
        {
            conn.Open();

            string query = @"
insert into lightbox_url (url, uid)
values(@url, @uid)
";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add(new SqlParameter("@url", SqlDbType.VarChar)).Value = url;
            cmd.Parameters.Add(new SqlParameter("@uid", SqlDbType.VarChar)).Value = HttpContext.Current.Session["UID"];

            cmd.ExecuteNonQuery();
        }finally { conn.Close(); }

        return "Success";
    }//end storeURL()


    public static string getCanvasInfo(string key, string type)
    {
        string html = "";
        
        string connStr = ConfigurationManager.ConnectionStrings["aJ_ConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connStr);
        SqlDataReader rdr = null;

        try
        {
            conn.Open();

            string query = @"select * from lightbox_entry where lightbox_entry_key = @key";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add(new SqlParameter("@key", SqlDbType.VarChar)).Value = key;
            rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {

                if (type == "title")
                {
                    html = String.Format(@"
<div id='lb_titleWrap'>
<div id='lb_title'>{0}</div>
<div id='lb_desc'>{1}</div>
<a id='lb_url' href='' target='_blank'>{2}</a>
<div style='margin-left: 114px; font-size: 14px; font-weight: bolder; color: #8B2323;'>crawled from</div>
<div id='lb_url_B'><a href='{3}' target='_blank'>{4}</a></div>
</div>
", rdr["title"].ToString(), rdr["blab"].ToString(), "", rdr["link"].ToString(), rdr["link"].ToString());
                }

                if (type == "bg_src") { html = rdr["bg_src"].ToString(); }
                if (type == "image_src") { html = "image/" + rdr["image_src"].ToString(); }
            }//end while
        }finally { rdr.Close(); conn.Close(); }

       return html;
    }//end getCanvasInfo()


    public static string searchLightbox(string search, string searchID, string search_type) 
    { 
       //search = parseSearch()
       

        string html = "";

        string clpQuery = "select * from lightbox_entry";
        if (search_type == "searchByKey") 
        { clpQuery = "select image_src, src_type, bg_src from lightbox_entry where lightbox_entry_key = @key"; }
        if (search_type == "dailyDraw"){ clpQuery = "select src_type from lightbox_entry where lightbox_entry_key = @key"; }
        if (search_type == "chkNuPic") { clpQuery = "select * from lightbox_entry where date >= @date"; }

        DateTime date = DateTime.Now;
        TimeSpan time = new TimeSpan(0, 0, -15, 0); //15 minutes ago
        DateTime combined = date.Add(time);
        int picCount = 0;

        string connStr = ConfigurationManager.ConnectionStrings["aJ_ConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connStr);
        SqlDataReader rdr = null;

        try
        {
            conn.Open();
            
            SqlCommand cmd = new SqlCommand(clpQuery, conn);
            cmd.Parameters.Add(new SqlParameter("@key", SqlDbType.VarChar)).Value = searchID;
            cmd.Parameters.Add(new SqlParameter("@date", SqlDbType.VarChar)).Value = combined;
            rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                
                if(search_type == "searchByKey")
                {
                   if (rdr["src_type"].ToString() == "youtube")
                   {
                       var ytSrc = rdr["bg_src"].ToString().Split('/')[4];
                       html = String.Format("<iframe id='lightbox_pic_viewer' width='700' height='500' src='https://www.youtube.com/embed/{0}'"
                            + " frameborder='0' allowfullscreen></iframe>" + c254 + "37", ytSrc);
                   }
                   else { html = String.Format("<img id='lightbox_pic_viewer' src='image/{0}' /></div>" + c254, rdr["image_src"].ToString()); }                   
                }
                else if (search_type == "chkNuPic")
                {
                    picCount++;
                    if (rdr["src_type"].ToString() == "youtube")
                    {
                        html += String.Format("<img id='lb_entry_{0}' class='searchYouTube searchPic_mini' src='http://img.youtube.com/vi/{1}/3.jpg' />"
                            , rdr["lightbox_entry_key"].ToString(), rdr["image_src"].ToString());
                    }
                    else
                    {
                        html += String.Format("<img id='lb_entry_{0}' class='searchPic searchPic_mini' src='image/{1}' />"
                        , rdr["lightbox_entry_key"].ToString(), rdr["image_src"].ToString());
                    }                
                }
                else if (search_type == "dailyDraw")
                {
                    if (rdr["src_type"].ToString() == "youtube"){ html = "<img id='lb_entry_" + searchID + "' class='searchYoutube' />"; }
                    else { html = "<img id='lb_entry_" + searchID + "' class='searchPic' />"; }
                }
                else
                {
                    if (rdr["src_type"].ToString() == "youtube")
                    {
                        html += String.Format("<img id='lb_entry_{0}' class='searchYouTube' src='http://img.youtube.com/vi/{1}/3.jpg' />"
                        , rdr["lightbox_entry_key"].ToString(), rdr["image_src"].ToString());
                    }
                    else
                    {
                        html += String.Format("<img id='lb_entry_{0}' class='searchPic' src='image/{1}' />"
                        , rdr["lightbox_entry_key"].ToString(), rdr["image_src"].ToString());
                    }
                }
            }//end while
        }finally{ rdr.Close(); conn.Close(); }

/*
        html += @"
<img class='searchPic' src='image/cigar.jpg' />
<img class='searchPic' src='image/cigar.jpg' />
<img class='searchPic' src='image/cigar.jpg' />
";
*/
        if (search_type == "") { html += "<div class='cleaner'></div>"; }
        if (search_type == "chkNuPic" && picCount > 0)
        //{ html = "<div id='update_pic'>" + picCount.ToString() + "<br />" + html + "</div>"; }
        { html = "<div id='update_pic'>" + html + "</div>"; }

        return html;// +combined;
    }//end searchLightBox()

    public static string saveCanvasImage(string canvasPic, string UID, string srcType, string url, string title, string blab, string bg_src, string dataURL)
    { 
        string connStr = ConfigurationManager.ConnectionStrings["aJ_ConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connStr);

        try
        {
            conn.Open();
            string query = @"
IF EXISTS (select * from lightbox_entry where UID = @uid and link = @url and bg_src = @bg_src)
    update lightbox_entry 
    set image_src = @pic, title = @title, blab = @blab, bg_src = @bg_src, dataURL = @dataURL
    where UID = @uid and link = @url
ELSE
insert into lightbox_entry (link, image_src, date, status, src_type, UID, share, title, blab, bg_src, dataURL)
values (@url, @pic, GETDATE(), 'ACTIVE', @src_type, @UID, 'yes', @title, @blab, @bg_src, @dataURL)             
            ";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add(new SqlParameter("@url", SqlDbType.VarChar)).Value = url;
            cmd.Parameters.Add(new SqlParameter("@src_type", SqlDbType.VarChar)).Value = srcType;
            cmd.Parameters.Add(new SqlParameter("@pic", SqlDbType.VarChar)).Value = canvasPic;
            cmd.Parameters.Add(new SqlParameter("@UID", SqlDbType.VarChar)).Value = UID;
            cmd.Parameters.Add(new SqlParameter("@title", SqlDbType.VarChar)).Value = title;
            cmd.Parameters.Add(new SqlParameter("@blab", SqlDbType.VarChar)).Value = blab;
            cmd.Parameters.Add(new SqlParameter("@bg_src", SqlDbType.VarChar)).Value = bg_src;
            cmd.Parameters.Add(new SqlParameter("@dataURL", SqlDbType.VarChar)).Value = dataURL;

            cmd.ExecuteNonQuery();
        }
        finally { conn.Close(); }
    
       return "Saved!";
    }//end saveCanvasImage()

    public static string getColor()
    {
        return @"
   <div class='lightbox_tool_wrap'>
    <img id='floatToolbar' src='image/icons/pin_up.png' />
    <img class='lightbox_tool_icon' src='image/icons/palette.png' />
	<div id='canvas_palette' class='lightbox_tool_palette'>
	
	 <div id='canvas_info'>
	  <div id='drawItPaletteColor'>2px</div>
      <div id='penOpacityWrap'>
       <div id='penOpacity_Left' class='penOpacity'><</div>
       <div id='penOpacity_Right' class='penOpacity'>></div>
      </div>
	  <div id='drawItPaletteErase'>eraser<a href='#lightbox_canvas' data-tool='eraser'></a></div>
      <a id='drawItPaletteMarker' href='#lightbox_canvas' data-tool='marker'></a>
      <!--<div id='drawItPaletteClear'>clear</div>-->
	  <div class='cleaner'></div>
	 </div>
	
	 <div id='canvas_size'>
	  <div class='drawItPaletteSize dip1'><a href='#lightbox_canvas' data-size='1'></a></div>
      <div class='drawItPaletteSize dip2 selecteddrawItPaletteSize'><a href='#lightbox_canvas' data-size='2'></a></div>
      <div class='drawItPaletteSize dip3'><a href='#lightbox_canvas' data-size='3'></a></div>
	  <div class='drawItPaletteSize dip5'><a href='#lightbox_canvas' data-size='5'></a></div>
	  <div class='drawItPaletteSize dip8'><a href='#lightbox_canvas' data-size='8'></a></div>
	  <div class='drawItPaletteSize dip13'><a href='#lightbox_canvas' data-size='13'></a></div>
	  <div class='drawItPaletteSize dip21'><a href='#lightbox_canvas' data-size='21'></a></div>
	  <div class='drawItPaletteSize dip34'><a href='#lightbox_canvas' data-size='34'></a></div>
	  <div class='cleaner'></div>
	 </div>
	 <div id='canvas_picker'>
	  <div class='drawItPalettePicker' style='background-color: black;'><a href='#lightbox_canvas' data-color='black'></a></div>
      <div class='drawItPalettePicker white' style='background-color: white;'><a href='#lightbox_canvas' data-color='white'></a></div>
      <div class='drawItPalettePicker' style='background-color: gray;'><a href='#lightbox_canvas' data-color='gray'></a></div>
      <div class='drawItPalettePicker' style='background-color: red;'><a href='#lightbox_canvas' data-color='red'></a></div>
	  <div class='drawItPalettePicker' style='background-color: orange;'><a href='#lightbox_canvas' data-color='orange'></a></div>
      <div class='drawItPalettePicker white' style='background-color: yellow;'><a href='#lightbox_canvas' data-color='yellow'></a></div>
      <div class='drawItPalettePicker' style='background-color: green;'><a href='#lightbox_canvas' data-color='green'></a></div>
      <div class='drawItPalettePicker' style='background-color: #218868;'><a href='#lightbox_canvas' data-color='#218868'></a></div>
	  <div class='drawItPalettePicker' style='background-color: blue;'><a href='#lightbox_canvas' data-color='blue'></a></div>
      <div class='drawItPalettePicker white' style='background-color: purple;'><a href='#lightbox_canvas' data-color='purple'></a></div>
	  <div class='cleaner'></div>
	 </div>
	</div>
   </div>
";
    
    }
}
    #endregion