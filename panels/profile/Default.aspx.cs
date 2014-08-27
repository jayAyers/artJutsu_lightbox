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

public partial class panels_profile_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    { }

    /***************
         PROFILE
     ***************/
    //http://www.iconarchive.com/
    //https://www.iconfinder.com/

    public static string getInfo()
    {

        /*
         string connStr = ConfigurationManager.ConnectionStrings["aJ_ConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connStr);
        SqlDataReader rdr = null;

        try
        {
            conn.Open();
            string query = @"select id, image from aj_social_list where id > 1";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add(new SqlParameter("@uid", SqlDbType.VarChar)).Value = HttpContext.Current.Session["UID"];
            rdr = cmd.ExecuteReader();

            string socialLink = " <img id='ajSocial_{0}' class='aj_social_ListItem {1}' src='image/social/{2}' />";
            while (rdr.Read()) {
                html += String.Format(socialLink, rdr[0].ToString(), "", rdr[1].ToString());
            }
        }
        finally { rdr.Close(); conn.Close(); }
         */

        string html = "";

        string header = @"
<br />
<div class='aj_options_header'>user info</div>
<div class='aj_options_header_text'>Connect with other members on artJutsu! If you'd like, you may omit any information. 
We recommend including at least your email just incase you need to reset your password.</div> 
";
        string optionsA = "<iframe id='aj_options_info_upload_frame' src='panels/profile/uploadProfilePic.aspx' frameBorder='0'>upload</iframe>";
        string optionsB = @"
<div class='aj_options_row'>
 <div class='aj_options_info'>first name</div> 
 <div class='aj_options_info'>last name</div>
 <div class='cleaner'></div>
</div>
<div class='aj_options_row'>
 <input id='aj_options_info_firstName' value='{1}' />
 <input id='aj_options_info_lastName' value='{2}' />
 <div class='cleaner'></div>
</div>

<div class='aj_options_row'>
 <div id='aj_options_info_month' class='aj_options_info'>month</div> 
 <div id='aj_options_info_day' class='aj_options_info' style='width: 55px;'>day</div>
 <div id='aj_options_info_year' class='aj_options_info' style='width: 55px;'>year</div>
 <div class='cleaner'></div>
</div>
<div class='aj_options_row'>
 <input id='aj_options_info_birth_month' value='{3}' />
 <input id='aj_options_info_birth_day' value='{4}' />
 <input id='aj_options_info_birth_year' value='{5}' />
</div>

<div class='aj_options_row'>
 <div class='aj_options_info'>location</div> 
</div>

<div class='aj_options_row'>
 <br />
{6}
 <img id='aj_options_location_pic' src='image/city/dc.png' />
 <select id='aj_options_location'>
  <option value='dc'>Washington DC</option>
  <option value='london'>London</option>
  <option value='nyc'>New York</option>
 </select>
 <br />(choose a location closest to you.)
</div>
";

        string optionsC = @"
<div class='aj_options_row'>
 <div class='aj_options_info'>email</div>
 <input id='aj_options_info_email' value='{7}' />
 <br />
 <div class='aj_options_info_text'>Sends new password to your email if I forget my current password.</div>
 <br /><br />{8}
   <div class='aj_options_info_text'>Make my email available to friends on artJutsu?</div>
    <div class='aj_options_info_show_email selected_option'>sure</div>
    <div class='aj_options_info_show_email'>nah</div>
   <div class='cleaner'></div>
 <br /><br />{9}
   <div class='aj_options_info_text'>Send me an email on artJutsu news and comments?</div>
    <div class='aj_options_info_send_email selected_option'>do it</div>
    <div class='aj_options_info_send_email'>noO</div>
   <div class='cleaner'></div>
</div>
";

        html = String.Format(@"
<div class='aj_options_info_A'>{0}</div>
<div class='aj_options_info_B'>{1}</div>
<div class='aj_options_info_C'>{2}</div>
<div class='cleaner'></div>
<div id='aj_options_info_submit'>submit</div>
<br /><br /><br /><br />
", optionsA, optionsB, optionsC);

        html = header + html;

        html = getUserInfo(html);

        //social
        html += getSocial();

//        html += "<br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />";
        return html;    
    }//end getInfo()
    /***************
       END PROFILE
     ***************/

    [WebMethod]
    public static string uploadProfilePic(List<string> list)
    {
        return "DFD";    
    }//end uploadProfilePic()

    [WebMethod]
    public static string addDailies(List<string> list)
    { return addDailies(list[0], list[1]); }

    public static string addDailies(string id, string day)
    {
        string connStr = ConfigurationManager.ConnectionStrings["aJ_ConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connStr);

        try
        {
            conn.Open();
            string query = "insert into lightbox_dailies values (@id, @day)";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.VarChar)).Value = id;
            cmd.Parameters.Add(new SqlParameter("@day", SqlDbType.VarChar)).Value = day;

            cmd.ExecuteNonQuery();
        }
        finally { conn.Close(); }

        return "success";
    }//end addDailies()

    [WebMethod]
    public static string startProfile(List<string> list)
    {//[0] uid
        string html = "";



        html = startProfile();

        html += @"
<div id='profile_wrap'>
 <div class='profileMenuDisplay pml1'>
  <div id='profile_portfolio' class='portfolio'></div>
  <div id='profile_friends' class='friends'></div>
  <div id='profile_options' class='options'>options</div>
 </div>
</div>
";

        //        html += @"<div id='profile_portfolio'></div>";

        return html;
    }//end startProfile()

    [WebMethod]
    public static string getPortfolio(List<string> list)
    { return getPortfolio(); }


    [WebMethod]
    public static string getOptions(List<string> list)
    { return getOptions(); }

    [WebMethod]
    public static string changeAvatar(List<string> list)
    { return changeAvatar(list[0]); }

    [WebMethod]
    public static string changeBanner(List<string> list)
    { return changeBanner(list[0]); }

    [WebMethod]
    public static string startFriends(List<string> list)
    { return startFriends(); }

    /*********
      FRIENDS
     *********/

    [WebMethod]
    public static string getFriends(List<string> list)
    { return getFriends(); }

    [WebMethod]
    public static string addFave(List<string> list)
    {
        int faveCount = 0;

        if (list[0] == "plus") { faveCount++; }
        if (list[0] == "minus") { faveCount--; }
    
        return faveCount.ToString();
    }//end addFave()

    [WebMethod]
    public static string searchFriends(List<string> list)
    { return searchFriends(list[0]); }

    [WebMethod]
    public static string submitFriendPost(List<string> list)
    {
        return submitFriendPost(list[0], list[1]);
    }//end submitFriendPost()


    [WebMethod]
    public static string getTB(List<string> list)
    {//[0] postNum
        string html = String.Format(@"
  <img class='aj_friend_post_comment_avatar' src='http://static.giantbomb.com/uploads/original/11/119311/1649084-kirby.jpg' />
  <textarea class='aj_friend_post_TA'></textarea>
  <div id='submitPost{0}' class='aj_friend_post_comment_submit'>submit</div>
  <div class='aj_friend_post_comment_cancel'>cancel</div>
  <div class='cleaner'></div>
", list[0]);

        return html;
    }//end getTB()

    [WebMethod]
    public static string requestFriend(List<string> list)
    { return requestFriend(list[0]); }

    [WebMethod]
    public static string acceptFriend(List<string> list)
    { return acceptFriend(list[0], list[1]); }

    [WebMethod]
    public static string getMyFriends(List<string> list)
    { return getMyFriends(); }

    [WebMethod]
    public static string getMySocialInfo(List<string> list)
    { return getMySocialInfo(list[0], list[1]); }

    [WebMethod]
    public static string getSocialLink(List<string> list)
    {//[0] socialID, [1] mySocialLink, [2] toShare
        return getSocialLink(list[0], list[1], list[2], list[3]);
    }//end getSocialLink()

    [WebMethod]
    public static string setUserInfo(List<string> list)
    { return setUserInfo(list[0], list[1], list[2], list[3], list[4], list[5], list[6]); }


    public static string submitFriendPost(string id, string post)
    {
        string html = "";

        //html = @"<div style='border: 1px solid black;'>" + post.Replace("\n", "<br />") + "</div>";

        html = String.Format(@"
<div style='margin-top: 10px; border: 1px solid black;'>
 <img style='width: 35px; height: 35px; float: left;' src='image/profile/yuk.jpg' />
 <div style='float: left; font-size: 14px;'>
 name<br />
 date<br />
 {0}
 </div> 
 <div class='cleaner'></div>
</div>", post.Replace("\n", "<br />"));

        return html;
    }//end submitFriendPost()


    public static string getUserInfo(string html)
    {
            string userPic = "";
            string firstName = "";
            string lastName = "";
            string month = "";
            string day = "";
            string year = "";
            string location = "";
            string email = "";
            string perm_email_show = "";
            string perm_email_send = "";

        string connStr = ConfigurationManager.ConnectionStrings["aJ_ConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connStr);
        SqlDataReader rdr = null;

        try
        {
            conn.Open();
            string query = "select * from Users_info where userID = @uid";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add(new SqlParameter("@uid", SqlDbType.VarChar)).Value = HttpContext.Current.Session["UID"];
            rdr = cmd.ExecuteReader();

  

            while (rdr.Read()) { 
                //DOB convert

                userPic = rdr["user_pic"].ToString();////////////////////////////////////////
                firstName = rdr["firstName"].ToString();
                lastName = rdr["lastName"].ToString();
                month = "February";
                day = "23";
                year = "1984";
                location = "";////////////////////////////////////////
                email = rdr["email"].ToString();
                perm_email_show = "";////////////////////////////////////////
                perm_email_send = "";////////////////////////////////////////
            }
        }
        finally { rdr.Close(); conn.Close(); }

        //////////////////////////////////////
        html = String.Format(html, userPic, firstName, lastName, month, day, year, location, email, perm_email_show, perm_email_send);
        //////////////////////////////////////

        return html;
    }//end getUserInfo()

    public static string setUserInfo(string firstName, string lastName, string DOB, string location
        , string email, string perm_show_email, string perm_send_email)
    { 
        string connStr = ConfigurationManager.ConnectionStrings["aJ_ConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connStr);

        try
        {
            conn.Open();
            string query = @"
IF EXISTS (select id from Users_info where userID = @uid)
 BEGIN
  update Users_info
  set firstName = @firstName, lastName = @lastName, DOB = @DOB, location = @location
, email = @email, perm_show_email = @perm_show_email, perm_send_email = @perm_send_email
  where userID = @uid
 END
ELSE
 BEGIN
  insert into Users_info (userID, firstName, lastName, DOB, location, email, perm_show_email, perm_send_email)
  values (@uid, @firstName, @lastName, @DOB, @location, @email, @perm_show_email, @perm_send_email)
 END
";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add(new SqlParameter("@firstName", SqlDbType.VarChar)).Value = firstName;
            cmd.Parameters.Add(new SqlParameter("@lastName", SqlDbType.VarChar)).Value = lastName;
            cmd.Parameters.Add(new SqlParameter("@DOB", SqlDbType.VarChar)).Value = DOB;
            cmd.Parameters.Add(new SqlParameter("@location", SqlDbType.VarChar)).Value = location;
            cmd.Parameters.Add(new SqlParameter("@email", SqlDbType.VarChar)).Value = email;
            cmd.Parameters.Add(new SqlParameter("@perm_show_email", SqlDbType.VarChar)).Value = perm_show_email;
            cmd.Parameters.Add(new SqlParameter("@perm_send_email", SqlDbType.VarChar)).Value = perm_send_email;
            cmd.Parameters.Add(new SqlParameter("@uid", SqlDbType.VarChar)).Value = HttpContext.Current.Session["UID"];

            cmd.ExecuteNonQuery();
        }
        finally { conn.Close(); }

       return "done";
    }//end setUserInfo()

    public static string getSocialLink(string socialID, string mySocialLink, string toShare, string toEdit)
    {
        string html = "";

        string connStr = ConfigurationManager.ConnectionStrings["aJ_ConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connStr);
        SqlDataReader rdr = null;

        try
        {
            conn.Open();
            string query = @"
IF EXISTS (select id from aj_social_user where aj_social_list_id = @socialID and userID = @uid)
 BEGIN
  update aj_social_user set share = @toShare 
  where aj_social_list_id = @socialID and userID = @uid
  
  select asl.link, asu.user_link
  from aj_social_list asl
   inner join aj_social_user asu
    on asl.id = asu.aj_social_list_id
  where asl.id = @socialID and userID = @uid
 END
ELSE
 BEGIN
  insert into aj_social_user(aj_social_list_id, userID, user_link, share)
  values(@socialID, @uid, @mySocialLink, 'yes')

  select link from aj_social_list where id = @socialID 
 END
";

            if (toEdit == "edit") { query = @"
 BEGIN
  update aj_social_user set user_link = @mySocialLink 
  where aj_social_list_id = @socialID and userID = @uid
  
  select asl.link, asu.user_link
  from aj_social_list asl
   inner join aj_social_user asu
    on asl.id = asu.aj_social_list_id
  where asl.id = @socialID
 END
"; }

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add(new SqlParameter("@socialID", SqlDbType.VarChar)).Value = socialID;
            cmd.Parameters.Add(new SqlParameter("@uid", SqlDbType.VarChar)).Value = HttpContext.Current.Session["UID"];
            cmd.Parameters.Add(new SqlParameter("@mySocialLink", SqlDbType.VarChar)).Value = mySocialLink;
            cmd.Parameters.Add(new SqlParameter("@toShare", SqlDbType.VarChar)).Value = toShare;
            rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                if (mySocialLink != "")
                {
                    html = String.Format(rdr["link"].ToString(), mySocialLink);
                    html = String.Format(@"<a id='aj_social_display_web_link' href='http://{0}' target='_blank'>{1}</a>", html, html);
                }
                else
                {//if share is hit
                    html = String.Format(rdr["link"].ToString(), rdr["user_link"].ToString());
                    html = String.Format("<a id='aj_social_display_web_link' href='http://{0}' target='_blank'>{1}</a>", html, html);
                }
            }//end while
        }finally{ rdr.Close(); conn.Close(); }


        return html + "<div id='aj_social_display_web_link_edit'>edit</div><div class='cleaner'></div>";
    }//end getSocialLink()

    public static string getMySocialInfo(string socialID, string isEdit)
    {
        string html = @"
  <img id='aj_social_display_image' src='image/social/{0}' />
  <div id='aj_social_display_title'>{1}</div>
  <div id='aj_social_display_link_write'>{2}</div>
  <div id='aj_social_display_link_write_saved'></div>
  <div id='aj_social_display_link_edit'>edit</div>
  <div class='cleaner'></div>

  <div id='aj_social_display_id_{3}' class='aj_social_display_share'>{4}</div>
";

        string connStr = ConfigurationManager.ConnectionStrings["aJ_ConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connStr);
        SqlDataReader rdr = null;

        try
        {
            conn.Open();
            string query = @"
select asl.image, asl.name, asl.link, asu.user_link, asu.share
from aj_social_list asl
left join aj_social_user asu
  on (asl.id = asu.aj_social_list_id and asu.userID = @uid)
where asl.id = @socialID";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add(new SqlParameter("@socialID", SqlDbType.VarChar)).Value = socialID;
            cmd.Parameters.Add(new SqlParameter("@uid", SqlDbType.VarChar)).Value = HttpContext.Current.Session["UID"]; 
            rdr = cmd.ExecuteReader();

            string toShare = "share";
            while (rdr.Read())
            {
                string link = String.Format(rdr[2].ToString(), "<input id='aj_social_display_link_write_TB' />");

                if (rdr[3].ToString().Length > 0)
                {
                    string socialLink = String.Format(rdr[2].ToString(), rdr[3].ToString());
                    link = String.Format(@"<a id='aj_social_display_web_link' href='http://{0}' target='_blank'>{1}</a>
<div id='aj_social_display_web_link_edit'>edit</div><div class='cleaner'></div>", socialLink, socialLink); 
                }

                if (rdr[4].ToString() == "yes") { toShare = "unShare"; }
                //

                html = String.Format(html, rdr[0].ToString(), rdr[1].ToString(), link, socialID, toShare);

                if (isEdit == "edit") 
                {
                       link = String.Format(rdr[2].ToString(), "<input id='aj_social_display_link_write_TB' value={0} />");
                       link = String.Format(link, rdr[3].ToString());

                       html = "<div id='aj_social_display_link_write'>{0}</div>";
                       html = String.Format(html, link);
                }//end if
            }
        }
        finally { rdr.Close(); conn.Close(); }

        return html;    
    }//end getMySocialInfo()

    public static string getSocial()
    {
        string html = "";

        string connStr = ConfigurationManager.ConnectionStrings["aJ_ConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connStr);
        SqlDataReader rdr = null;

        try
        {
            conn.Open();
            string query = @"
select asl.id, asl.image, asu.id, asu.user_link, asu.share
from aj_social_list asl
 left join aj_social_user asu
  on (asl.id = asu.aj_social_list_id and asu.userID = @uid)
where asl.id > 1";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add(new SqlParameter("@uid", SqlDbType.VarChar)).Value = HttpContext.Current.Session["UID"];
            rdr = cmd.ExecuteReader();

            string socialLink = " <img id='ajSocial_{0}' class='aj_social_ListItem {1}' src='image/social/{2}' />";
            while (rdr.Read()) {
                string added = "";
                if (rdr[4].ToString() == "yes") { added = "aj_social_added"; }

                html += String.Format(socialLink, rdr[0].ToString(), added, rdr[1].ToString());
            }
        }
        finally { rdr.Close(); conn.Close(); }

        string header = @"
<div class='aj_options_header'>social</div>
<div class='aj_options_header_text'>Add your favorite social networks so others so other can discover more of your work!
<br />We'll add some more networks soon.
</div> 
";

        html = "<div id='aj_social_List'>" + html + "</div>"
             + "<div id='aj_social_Display'></div>"
             + "<div class='cleaner'></div>";

        return header + html;    
    }//end getSocial()

    public static string acceptFriend(string id, string status)
    {
        string connStr = ConfigurationManager.ConnectionStrings["aJ_ConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connStr);

        try
        {
            conn.Open();
            string query = "update aj_friends set status = @status, date = @date where id = @id";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add(new SqlParameter("@status", SqlDbType.VarChar)).Value = status;
            cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.VarChar)).Value = id;
            cmd.Parameters.Add(new SqlParameter("@date", SqlDbType.VarChar)).Value = DateTime.Now;

            cmd.ExecuteNonQuery();
        }
        finally { conn.Close(); }

        return "success!!!";
    }//end acceptFriend()

    public static string requestFriend(string friendID)
    {

        string connStr = ConfigurationManager.ConnectionStrings["aJ_ConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connStr);

        try
        {
            conn.Open();

/*********************************************
  notes: 
  A) userID = @uid and friendID = @friendID
   where I have requested this_friend

  B) userID = @friendID and friendID = @uid
   where this_friend has requested me
*********************************************/
            string query = @"
--A
if exists (select id from aj_friends where userID = @uid and friendID = @friendID)
 update aj_friends set status = 'pending' where userID = @uid and friendID = @friendID

--B
if exists (select id from aj_friends where userID = @friendID and friendID = @uid)
 begin
  delete from aj_friends where userID = @friendID and friendID = @uid
  insert into aj_friends (userID, friendID, status) values(@uid, @friendID, 'pending')
 end
else
 insert into aj_friends (userID, friendID, status) values(@uid, @friendID, 'pending')
";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add(new SqlParameter("@friendID", SqlDbType.VarChar)).Value = friendID;
            cmd.Parameters.Add(new SqlParameter("@uid", SqlDbType.VarChar)).Value = HttpContext.Current.Session["UID"];

            cmd.ExecuteNonQuery();
        }
        finally { conn.Close(); }

        return "pending";
    }//end requestFriend()

    public static List<string> getMyFriendsList()
    {
        List<string> list = new List<string>();

        string connStr = ConfigurationManager.ConnectionStrings["aJ_ConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connStr);
        SqlDataReader rdr = null;

        try
        {
            conn.Open();
        string query = @"
        select friendID from aj_friends
where userID = @uid and status = 'friend'
union
select userID from aj_friends
where friendID = @uid and status = 'friend'
";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add(new SqlParameter("@uid", SqlDbType.VarChar)).Value = HttpContext.Current.Session["UID"];
            rdr = cmd.ExecuteReader();

            while (rdr.Read()) { list.Add(rdr[0].ToString()); }
        }finally{ rdr.Close(); conn.Close();}

        return list;
    }//end list

    public static string searchFriends(string searchTerm)
    {
        string html = "";

        string connStr = ConfigurationManager.ConnectionStrings["aJ_ConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connStr);
        SqlDataReader rdr = null;

        try
        {
            conn.Open();

            string query = @"
Select top 15 u.userID, u.userPic, aj_f.[status], u.userName, aj_f.userID
From Users u
 Left Join aj_friends aj_f
  on 
  (
  (aj_f.userID = @uid and u.userID = aj_f.friendID) 
  or 
  (aj_f.friendID = @uid and u.userID = aj_f.userID)
  )
Where 
 (u.userName like '%%' and u.userID <> @uid and u.status = 'active') 
";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add(new SqlParameter("@searchTerm", SqlDbType.VarChar)).Value = searchTerm + "%";
            cmd.Parameters.Add(new SqlParameter("@uid", SqlDbType.VarChar)).Value = HttpContext.Current.Session["UID"];
            rdr = cmd.ExecuteReader();

            string status = "";

            while (rdr.Read()) {
                
                //status
                 if (rdr[2].ToString() == "blocked") { status = "blocked"; }
                 if (rdr[2].ToString() == "friend") { status = "friend"; }
                 if (rdr[2].ToString() == "pending") { status = "pending"; }
                 
                 //if (rdr[2].ToString() == "pending/blocked") { status = "pending"; } //if I request friendship, but was blocked
                 if (rdr[2].ToString() == "pending/blocked")
                 {
                     if(rdr[4].ToString() == (string)HttpContext.Current.Session["UID"]){ status = "blocked"; }//if I blocked friendship
                     else{ status = "pending"; }//if I request friendship, but was blocked
                 } 
                 if (rdr[2].ToString() == "") { status = "add"; }
                //end status

                //remove friends from search
               //if (rdr[3].ToString() == HttpContext.Current.Session["UID"] && status == "friend"){ status = "friend_no_show"; }
                //end remove

                if (status != "blocked")
                {
                    html += String.Format(@"
<div id='ajUSR{0}' class='aj_top_friends_pic_wrap'>
 <a href='?user={1}' target='_blank'>
<img class='aj_top_friends_pic' src='{2}' />
</a>"
    , rdr[0].ToString(), rdr[3].ToString(), rdr[1].ToString());

                    if (status != "friend") {
                        html += String.Format("<div class='aj_top_friends_status aj_top_friends_status_{0}'>{1}</div>", status, status);
                    }

                    html += "</div>";
                }//end if            
            }//end while
        }
        finally { rdr.Close(); conn.Close(); }

        return html + "<div class='cleaner'></div>";
    }//searchFriends()

    public static string startFriends() 
    {
        string html = "";

//        html = "<div style='color: #708090; font-size: 30px; font-weight: bolder; text-align: center;'>Friend's List Coming Soon</div>";

        //lets just work on the posts

        string post = "";
/*
        string prevComments = @"
<div class='aj_friend_post_TB_click aj_friend_post_TB_click_big'></div>
"; //prevComments = "";

        post = String.Format(@"
<div id='post1' class='aj_friend_post'>
 <div class='aj_friend_post_banner_wrap'>
  <img class='aj_friend_post_banner' src='http://www.pivotalkids.com/speed_racer.JPG' />
  <img class='aj_friend_post_avatar' src='http://upload.wikimedia.org/wikipedia/en/2/25/Speed_Racer_promotional_image.jpg' />
  <div class='aj_friend_post_name'>sreyaNotfilc</div>
  <div class='aj_friend_post_date'>July 5, 2014</div>
 </div> 
 <div class='aj_friend_post_txt'>Hey guys. Here's the pictuers that I drew! Check it out. Tutorial included!</div>
 <img class='aj_friend_post_photo' src='http://artjutsu.com/lightbox/image/1_6_30_2014_7_29_52_PM.png' />
  
 <div class='aj_friend_post_TB_wrap'>
  <div class='aj_friend_post_fave_count'></div>
  <img class='aj_friend_post_fave' src='image/portfolio/fave.png' />
  <!--<div class='aj_friend_post_TB_click'></div>-->
  <div class='cleaner'></div>
 </div>
 <div class='aj_friend_post_comment_wrap'>
  {0}
  <div class='aj_friend_post_comment_user_wrap'></div>
 </div>
</div>
", prevComments);

        post += @"
<div class='aj_friend_post'>
 <div class='aj_friend_post_banner_wrap'>
  <img class='aj_friend_post_banner' src='http://www.relaxorium.com/villains/_nnef020.JPG' />
  <img class='aj_friend_post_avatar' src='image/profile/nephlite.jpg' />
 </div>
</div>
";
        post += @"
<div class='aj_friend_post'>
 <div class='aj_friend_post_banner_wrap'>
  <img class='aj_friend_post_banner' src='http://www.relaxorium.com/villains/_nnef020.JPG' />
  <img class='aj_friend_post_avatar' src='image/profile/nephlite.jpg' />
 </div>
</div>
";*/

        post = getFriendsPost();

        /////////////////////////////////
        string menu = String.Format(@"
<div id='aj_friend_menu_wrap'>
 <div id='aj_friend_menu'>{0}</div>
</div>", getMenu());

        /////////////////////////////////


        html = menu + "<div id='aj_friend_post_wrap'>" + post + "</div>" + "<div class='cleaner'></div>";
        html = menu + post + "<div class='cleaner'></div>";
        
//social shit
        html += @"
<a href='https://www.facebook.com/sharer/sharer.php?u=example.org' target='_blank'>
  Share on Facebook
</a>
";
//end social

        return html;    
    }//end startFriends()

    public static string getFriendsPost()
    {
        string post = "";
        /*
        string prevComments = @"
<div class='aj_friend_post_TB_click aj_friend_post_TB_click_big'></div>
"; //prevComments = "";
        */

        string myTB = @"
  <img class='aj_friend_post_comment_avatar' src='http://static.giantbomb.com/uploads/original/11/119311/1649084-kirby.jpg' />
  <textarea class='aj_friend_post_TA'></textarea>
  <div class='aj_friend_post_comment_submit'>submit</div>
  <div class='aj_friend_post_comment_cancel'>cancel</div>
  <div class='cleaner'></div>
";

        myTB = "<div class='aj_friend_post_TB_click'></div>";


        string friendsList = retFriendsListQuery();

        string connStr = ConfigurationManager.ConnectionStrings["aJ_ConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connStr);
        SqlDataReader rdr = null;

        try
        {
            conn.Open();

            string query = @"
select u.top_pic, u.userPic, u.userName 
from Users u
where u.userID in " + friendsList;

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add(new SqlParameter("@friendIDs", SqlDbType.VarChar)).Value = friendsList;
            rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                post += String.Format(@"
<div id='post1' class='aj_friend_post'>
 <div class='aj_friend_post_banner_wrap'>
  <img class='aj_friend_post_banner' src='{0}' />
  <img class='aj_friend_post_avatar' src='{1}' />
  <div class='aj_friend_post_name'>{2}</div>
  <div class='aj_friend_post_date'>July 5, 2014</div>
 </div> 
 <div class='aj_friend_post_txt'>Hey guys. Here's the picture that I drew! Check it out. Tutorial included!</div>
 <img class='aj_friend_post_photo' src='http://artjutsu.com/lightbox/image/1_6_30_2014_7_29_52_PM.png' />
  
 <div class='aj_friend_post_TB_wrap'>
  <div class='aj_friend_post_fave_count'></div>
  <img class='aj_friend_post_fave' src='image/portfolio/fave.png' />
  <!--<div class='aj_friend_post_TB_click'></div>-->
  <div class='cleaner'></div>
 </div>
 <div class='aj_friend_post_comment_wrap'>  
  <div class='aj_friend_post_comment_previous_post'>previous</div>
  <div class='aj_friend_post_comment_user_wrap'></div>
  <div class='aj_friend_post_comment_user_starter'>{3}</div>
<div class='cleaner'></div>
 </div>
</div>", rdr[0].ToString(), rdr[1].ToString(), rdr[2].ToString(), myTB);
            }//end while
        }
        finally { rdr.Close(); conn.Close(); }

        return post;
    }//end getFriendsPost()

    public static string retFriendsListQuery()
    { 
        string friendsList = "(";
        foreach (string l in getMyFriendsList()){ friendsList += "'" + l + "',"; }
        friendsList = friendsList.Substring(0, friendsList.Length - 1) + ")";

        if (friendsList == ")") { friendsList = "('')"; }

        return friendsList;
    }//end retFriendsListQuery()

    public static string getMenu()
    {
        string html = "";

        //http://iconmonstr.com/g/?icon=iconmonstr-pencil-8-icon.png
        //http://iconmonstr.com/g/?icon=iconmonstr-photo-camera-4-icon.png
        //http://iconmonstr.com/g/?icon=iconmonstr-video-camera-6-icon.png

        string writeFriends = @"
<div id='aj_write_friends'>
 <img id='aj_write_friends_avatar' src='http://static.giantbomb.com/uploads/original/11/119311/1649084-kirby.jpg' />
 <textarea id='aj_write_friends_TA' placeholder='Write something to a friend...'></textarea>
 <div id='aj_write_friends_submit'>submit</div>
 <div id='aj_write_friends_cancel'>cancel</div>
 <div class='cleaner'></div>
</div>
";
        string writeFriends_Options = @"
<div id='aj_write_friends_options'>
 <div id='aj_write_friends_options_text' class='aj_write_friends_options'>
  <img src='image/icons/text.png' />
  <div>text</div>
 </div>
 <div id='aj_write_friends_options_photo' class='aj_write_friends_options'>
  <img src='image/icons/photo.png' />
  <div>photo</div>
 </div>
 <div id='aj_write_friends_options_camera' class='aj_write_friends_options'>
  <img src='image/icons/camera.png' />
  <div>camera</div>
 </div>
 <div id='aj_write_friends_options_link' class='aj_write_friends_options'>
  <img src='image/icons/link.png' />
  <div>link</div>
 </div>
 <div class='cleaner'></div>
</div>
";
        //writeFriends += writeFriends_Options;
        //writeFriends += "<div id='aj_write_friends_who'>who would you like to write to</div>";

        string searchFriends = @"
<div id='aj_search_friends'>
 <input id='aj_search_friends_TB' />
 <img id='aj_search_friends_Btn' src='image/icons/search.png' />
 <div class='cleaner'></div>

 <div id='aj_search_friends_results'>
  
 </div>
</div>
";

        string friendRequest = String.Format("<div id='aj_friend_request'>{0}</div>", getFriendRequest());

        string topFriends = String.Format(@"
<div id='aj_top_friends'>
 <div>my friends</div>
 <div id='my_friend_wrap'>{0}</div>
<div class='cleaner'></div>
</div>
", getMyFriends());

        //searchFriends = string.Format(searchFriends, topFriends);

        html = writeFriends + searchFriends + topFriends + friendRequest;

        return html;    
    }//end getMenu()

    public static string getMyFriends()
    {
        string html = "";

        string connStr = ConfigurationManager.ConnectionStrings["aJ_ConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connStr);
        SqlDataReader rdr = null;

        try
        {
            conn.Open();

            string query = @"
select u.userName, u.userPic 
from aj_friends aj_f
 inner join Users u
  on aj_f.friendID = u.userID
where aj_f.userID = @uid and aj_f.status = 'friend'

union

select u.userName, u.userPic 
from aj_friends aj_f
 inner join Users u
  on aj_f.userID = u.userID
where aj_f.friendID = @uid and aj_f.status = 'friend'
";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add(new SqlParameter("@uid", SqlDbType.VarChar)).Value = (string)HttpContext.Current.Session["UID"];
            rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {

                html += String.Format(@"
<a class='aj_top_friends_pic_a' href='?user={0}' target='_blank'>
 <img class='aj_top_friends_pic' src='{1}' />
</a>", rdr[0].ToString(), rdr[1].ToString());

            }
        }finally { rdr.Close(); conn.Close(); }

        return html;
    }

    public static string getFriendRequest()
    {
        string html = "";

        string connStr = ConfigurationManager.ConnectionStrings["aJ_ConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connStr);
        SqlDataReader rdr = null;

        try
        {
            conn.Open();

            string query = @"
Select aj_f.[id], u.userPic, u.userName
From aj_friends aj_f 
 Inner Join Users u
  on aj_f.userID = u.userID
Where aj_f.friendID = @uid and aj_f.status = 'pending'";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add(new SqlParameter("@uid", SqlDbType.VarChar)).Value = (string)HttpContext.Current.Session["UID"];
            rdr = cmd.ExecuteReader();
            
            while (rdr.Read())
            {
                html += String.Format(@"
<div id='friendReq{0}' class='aj_friend_request_wrap'>
 <img class='aj_friend_request_pic' src ='{1}' />
 <div class='aj_friend_request_name'>{2}</div>
 <div class='aj_friend_request_accept'>accept</div>
 <div class='aj_friend_request_reject'>reject</div>
 <div class='cleaner'></div>
</div>", rdr[0].ToString(), rdr[1].ToString(), rdr[2].ToString());  // rdr[0].ToString() + "<br />";
            }
        }
        finally { rdr.Close(); conn.Close(); }

        return html;    
    }//end getFriendRequest()


    public static string startProfile()
    {
        string html = "";

        string connStr = ConfigurationManager.ConnectionStrings["aJ_ConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connStr);
        SqlDataReader rdr = null;

        try
        {
            conn.Open();

            string query = @"Select * from Users where userID = @uid";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add(new SqlParameter("@uid", SqlDbType.VarChar)).Value = (string)HttpContext.Current.Session["UID"];
            rdr = cmd.ExecuteReader();

            string topPic = "";
            string userPic = "";

            while (rdr.Read())
            {
                userPic = rdr["userPic"].ToString(); topPic = rdr["top_pic"].ToString();
                if ((string)HttpContext.Current.Session["naughty"] == "naughty") { userPic = rdr["userPic_R"].ToString(); topPic = rdr["top_pic_R"].ToString(); }
                if ((string)HttpContext.Current.Session["naughty"] == "filthy") { userPic = rdr["userPic_XXX"].ToString(); topPic = rdr["top_pic_XXX"].ToString(); }

                html = string.Format(@"
<div id='profileTop' style='background-image: url({0})'></div>

<div id='profileMenu'>
  <img id='profilePic' src='{1}' />
  <div id='profileName'>{2}</div>
  <div id='profile_logout'>logout</div>

  <div class='profileMenuList pml1'>
   <img class='panelSwap profileMenuListItem portfolio' src='image/portfolio/pencil.png' />
   <img class='panelSwap profileMenuListItem friends' src='image/portfolio/drop.png' />
   <img class='panelSwap profileMenuListItem options' src='image/portfolio/gear.png' />
   <div class='cleaner'></div>
  </div>
  <div id='profileDisplay_1' class='profileMenuDisplay pml1'>
   <div class='portfolio'>portfolio</div>
   <div class='friends'>friends</div>
   <div class='options'>settings</div>
  </div>
</div>
", topPic, userPic, rdr["username"].ToString());

            }
        }
        finally { rdr.Close(); conn.Close(); }



        return html;
    }//end startProfile()

    public static string getPortfolio()
    {
        string html = "";

        string connStr = ConfigurationManager.ConnectionStrings["aJ_ConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connStr);
        SqlDataReader rdr = null;

        try
        {
            conn.Open();

            string query = @"
Select 
 le.src_type, le.title, le.bg_src, le.lightbox_entry_key, le.image_src, le.date
, ld.date_released
from
 lightbox_entry le
  left join lightbox_dailies ld
   on le.lightbox_entry_key = ld.lightbox_entry_key
where
 uid = @uid and status = 'active' order by lightbox_entry_key desc
";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add(new SqlParameter("@uid", SqlDbType.VarChar)).Value = (string)HttpContext.Current.Session["UID"];
            rdr = cmd.ExecuteReader();

            //DateTime thisDate = new DateTime();

            string title = "";
            string classType = "";

            string inDailies = "";

            while (rdr.Read())
            {
                if ((string)HttpContext.Current.Session["UID"] == "1")
                {
                    inDailies = "<div class='portfolio_pic_add portfolio_pic_add_submit'>+</div>";
                    if (rdr[6].ToString().Length > 0) { inDailies = "<div class='portfolio_pic_add'>" + rdr[6].ToString() + "</div>"; }
                }

                if (rdr["src_type"].ToString() == "image") { classType = "class='searchPic'"; }
                if (rdr["src_type"].ToString() == "youtube") { classType = "class='searchYoutube'"; }

                title = rdr["title"].ToString();
                if (title.Length > 17) { title = title.Substring(0, 17); }

                html += String.Format("<div id='" + rdr["bg_src"].ToString() + "' class='portfolio_pic_wrap'>"
                       + "<img id='lb_entry_" + rdr["lightbox_entry_key"].ToString() + "' "
                       + classType + " src='image/" + rdr["image_src"].ToString() + "'>"
                       + "<div class='portfolio_pic_title'>" + title + "</div>"
                       + "<div class='portfolio_pic_date'>" + DateTime.Parse(rdr["date"].ToString()).ToString("MM/dd/yyyy") + "</div>"
                      + "{0}</div>", inDailies);
            }
        }
        finally { rdr.Close(); conn.Close(); }

        return html + "<div class='cleaner'></div>";
    }//end getPortfolio()

    public static string getFriends()
    {
        string html = "";

        html = "";

        return html;
    }//end getFriends()

    public static string getOptions()
    {
        string html = "";

        string info = getInfo();

        //avatar
        string avatar = getAvatar();

        //banner
        string banner = getBanner();// "this isthe banner";

        html = info + avatar + "<br /><br />" + banner;

        html += @"
<br /><br /><br />
<div class='aj_options_header'>radio</div>
<div class='aj_options_header_text'>Here is your radio!</div> 
<a id='onlineRadioLink' href='http://radiotuna.com/iPhone'>iphone radio</a><script type='text/javascript' src='http://radiotuna.com/OnlineRadioPlayer/EmbedRadio?playerParams=%7B%22styleSelection0%22%3A46%2C%22styleSelection1%22%3A253%2C%22styleSelection2%22%3A58%2C%22textColor%22%3A0%2C%22backgroundColor%22%3A15592941%2C%22buttonColor%22%3A16744626%2C%22glowColor%22%3A16744626%2C%22playerSize%22%3A240%2C%22playerType%22%3A%22style%22%7D&width=240&height=292'></script>
<iframe src='https://www.siriusxm.com/player/' style='width: 655px; height: 375px;' frameborder='0'></iframe>

<br /><br />
<script src='https://apis.google.com/js/platform.js'></script>
<div class='g-hangout' data-render='createhangout'></div>

<!--<iframe width='200' height='257' src='http://memegenerator.net/Woohyun-Kate-Sirius/embed' frameborder='0' />-->
";

        return html;
    }//end getOptions()

    public static string getBanner()
    {
        string banner = @"
<!--<div style='margin: 0 0 15px 90px; color: #708090; font-size: 22px; font-weight: bolder; text-decoration:underline;'>banners</div>-->
<br /><br /><br />
<div class='aj_options_header'>banner</div>
<div class='aj_options_header_text'>Select a banner or enter an image URL from the web!</div> 

<input id='addBannerURL' />
<div id='addBannerURL_go'>go</div>
<div id='addBannerURL_message'></div>
<div class='cleaner'></div>

<div id='bannerWrap'>
 <img class='bannerPic' src='http://fc09.deviantart.net/fs70/i/2010/038/6/6/Kirby_wallpaper_by_ninjin_x.jpg' />
 <img class='bannerPic' src='http://www.pivotalkids.com/speed_racer.JPG' />
 <img class='bannerPic' src='http://www.relaxorium.com/villains/_nnef020.JPG' />

 <img class='bannerPic' src='http://www.toonbarn.com/wordpress/wp-content/uploads/2012/02/Make-your-own-Domo-animations.jpg' />
 <img class='bannerPic' src='http://37.media.tumblr.com/tumblr_m7owtcMcTt1rac84ko1_500.png' />
 <img class='bannerPic' src='http://pmcvariety.files.wordpress.com/2013/10/mighty-morphin-power-rangers.jpg?w=670&h=377&crop=1' />
 
 <img class='bannerPic' src='http://hdwallphotos.com/wp-content/uploads/2014/03/Happy-Face-Smiley-Wallpaper-High-Definition.jpg' />
 <img class='bannerPic' src='http://fc00.deviantart.net/fs71/f/2012/021/7/a/i__m_not_afraid_by_vonman-d4n3pul.png' />
 <img class='bannerPic' src='http://www-inst.eecs.berkeley.edu/~cs188/fa07/projects/multiagent/pacman_multi_agent.png' />

 <img class='bannerPic' src=    'http://fc04.deviantart.net/fs71/f/2013/058/3/7/super_monopoly_by_jonizaak-d5vyix6.png' />
 <img class='bannerPic' src='http://www.impatientoptimists.org/~/media/Blog/Images/BlogPosts/Home%20Page%20Features/C/CJ%20CO/cmp014asst%20612x4436f4446e15454e33897db6fa72433f28png_png_autocropped.jpg' />
 <img class='bannerPic' src='http://cdn.filmschoolrejects.com/images/ourgang.jpg' />

 <img class='bannerPic' src='http://cdn.denofgeek.us/sites/denofgeekus/files/sailor-moon-1_0.jpg' />
 <img class='bannerPic' src='http://www.di-o-matic.com/images/header/spongebob01.gif' />
 <img class='bannerPic' src='http://images5.alphacoders.com/442/442433.jpg' />

 <img class='bannerPic' src='http://antiekllc.com/wp-content/uploads/2014/06/squidward-house-4.jpg'  />
 <img class='bannerPic' src='http://p1.la-img.com/1011/22618/7894324_2_l.jpg' />
 <img class='bannerPic' src='http://www.internationalhero.co.uk/s2/sailscou.jpg' />
 <div class='cleaner'></div>
</div>
";


        return banner;    
    }//end getBanner()

    public static string changeBanner(string imgSrc)
    {
        string html = "";

        string connStr = ConfigurationManager.ConnectionStrings["aJ_ConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connStr);

        try
        {
            conn.Open();

            string query = "update Users set top_Pic = @imgSrc where userID = @uid";
            if ((string)HttpContext.Current.Session["naughty"] == "naughty") { query = "update Users set top_Pic_R = @imgSrc where userID = @uid"; }
            if ((string)HttpContext.Current.Session["naughty"] == "filthy") { query = "update Users set top_Pic_XXX = @imgSrc where userID = @uid"; }

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add(new SqlParameter("@uid", SqlDbType.VarChar)).Value = (string)HttpContext.Current.Session["UID"];
            cmd.Parameters.Add(new SqlParameter("@imgSrc", SqlDbType.VarChar)).Value = imgSrc;//.Split('/')[2];

            cmd.ExecuteNonQuery();
        }
        finally { conn.Close(); }

        return "success";
    }//end changeAvatar()

    public static string changeAvatar(string imgSrc)
    {
        string html = "";

        string connStr = ConfigurationManager.ConnectionStrings["aJ_ConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connStr);

        try
        {
            conn.Open();

            string query = "update Users set userPic = @imgSrc where userID = @uid";
            if ((string)HttpContext.Current.Session["naughty"] == "naughty") { query = "update Users set userPic_R = @imgSrc where userID = @uid"; }
            if ((string)HttpContext.Current.Session["naughty"] == "filthy") { query = "update Users set userPic_XXX = @imgSrc where userID = @uid"; }

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add(new SqlParameter("@uid", SqlDbType.VarChar)).Value = (string)HttpContext.Current.Session["UID"];
            cmd.Parameters.Add(new SqlParameter("@imgSrc", SqlDbType.VarChar)).Value = imgSrc;//.Split('/')[2];

            cmd.ExecuteNonQuery();
        }
        finally { conn.Close(); }

        return "success";
    }//end changeAvatar()

    public static string getAvatar()
    {
        string avatar =
        @"
<!--<div style='margin: 0 0 15px 90px; color: #708090; font-size: 22px; font-weight: bolder; text-decoration:underline;'>avatars</div>-->
<br /><br /><br /><br />
<div class='aj_options_header'>avatars</div>
<div class='aj_options_header_text'>Select an avatar or enter an image URL from the web!</div> 


<input id='addAvatarURL' />
<div id='addAvatarURL_go'>go</div>
<div id='addAvatarURL_message'></div>
<div class='cleaner'></div>

<div id='avatarWrap'>
 <!--<img class='avatarPic' src='image/profile/smiley.jpg' />-->
 <img class='avatarPic' src='image/profile/smiley_org.jpg' />
 <img class='avatarPic' src='image/profile/sad.jpg' />
 <img class='avatarPic' src='image/profile/yuk.jpg' />
 <!--<img class='avatarPic' src='image/profile/awesome.jpg' />-->
 <img class='avatarPic' src='image/profile/pringles.jpg' />
 <img class='avatarPic' src='image/profile/national.jpg' />
 <img class='avatarPic' src='http://1.bp.blogspot.com/-gIdmEQxzA0s/TrmOt1WMFWI/AAAAAAAAAYQ/KIrOVLtalFk/s1600/utz+girl.jpg' />

 <img class='avatarPic' src='http://www.moonlightsoldiers.com/dub/images/dub_queen-beryl2.jpg' />
 <!--<img class='avatarPic' src='http://images5.fanpop.com/image/photos/29000000/Queen-Beryl-power-rangers-and-sailor-moon-29034057-467-356.jpg' />-->
 <img class='avatarPic' src='image/profile/alfred.jpg' />
 <img class='avatarPic' src='image/profile/alfalfa.png' />
 <img class='avatarPic' src='image/profile/nephlite.jpg' />
 <!--<img class='avatarPic' src='image/profile/wednesday.gif' />-->
 <img class='avatarPic' src='http://wp.streetwise.co/wp-content/uploads/2011/11/logo-mr-monopoly.jpg' />
 <img class='avatarPic' src='http://upload.wikimedia.org/wikipedia/en/2/25/Speed_Racer_promotional_image.jpg' />

 <img class='avatarPic' src='image/profile/pacman.png' />
 <img class='avatarPic' src='http://static.giantbomb.com/uploads/original/11/119311/1649084-kirby.jpg' />
 <img class='avatarPic' src='http://fc01.deviantart.net/fs28/i/2008/164/e/7/domo_kun_wallpaper__by_lucy_loo.jpg' />
 <img class='avatarPic' src='http://gamesdreams.com/attachment.php?attachmentid=7184&d=1349727849' />
 <img class='avatarPic' src='http://fitnessmonster.files.wordpress.com/2012/07/cookie_monster_by_neorame-d4yb0b5.png' />
 <img class='avatarPic' src='http://static.squarespace.com/static/50d327aae4b03be70f395b21/t/51d7d405e4b0c6a357c3f66e/1373099026909/Red%20Ranger%20Helmet.png' />

 <img class='avatarPic' src='http://a1.s6img.com/cdn/0007/p/1076962_6758993_lz.jpg' />
 <img class='avatarPic' src='http://img3.wikia.nocookie.net/__cb20100724183918/spongebob/images/3/33/Patrick_Star.svg' />
 <img class='avatarPic' src='http://img4.wikia.nocookie.net/__cb20110911114021/spongebob/images/e/e2/Squidward_Design_2.jpg' />
 <img class='avatarPic' src='http://www.polyvore.com/cgi/img-thing?.out=jpg&size=l&tid=28184877' />
 <img class='avatarPic' src='http://www.teesforall.com/images/Adventure_Time_Finn_Standing_Smile_Blue_Shirt_POP.jpg' />
 <img class='avatarPic' src='http://www.cartoonnetworkhq.net/sites/www.cartoonnetworkhq.net/files/new-character-image/jake.png?1250623753' />
";
 

        if ((string)HttpContext.Current.Session["naughty"] == "naughty" || (string)HttpContext.Current.Session["naughty"] == "filthy")
        {
            avatar += @"
 <!--fun-->
 <img class='avatarPic' src='http://37.media.tumblr.com/55bd6aceadd6726506db171b02c285f7/tumblr_mq42u0l2Pv1rc0xd0o1_400.gif' />
 <img class='avatarPic' src='http://i.imgur.com/rCWvV3R.gif' />
 <img class='avatarPic' src='http://ilarge.listal.com/image/5922754/968full-jennifer-love-hewitt.jpg' />
 <img class='avatarPic' src='http://coltmonday.files.wordpress.com/2010/10/jennifer-love-hewitt-bouncy-boobs-mopping-up.gif?w=525' />
 <img class='avatarPic' src='http://media.tumblr.com/tumblr_lo3un0GcOf1qav7zl.gif' />
 <img class='avatarPic' src='http://bouncebreak.com/gif/bounce/jessica-biel-boob-check.gif' />

  <!--Alison Brie!!-->
  <img class='avatarPic' src='http://www.humortrend.com/wp-content/uploads/images/gif/1/hotgifs317.gif' />   
  <img class='avatarPic' src='http://cdn.rsvlts.com/wp-content/uploads/2012/12/Allison-Brie-GIF-20.gif' />
  <img class='avatarPic' src='http://wac.450f.edgecastcdn.net/80450F/screencrush.com/files/2013/02/brie-cleavage.gif' />
  <img class='avatarPic' src='http://i.imgur.com/RLCrEZd.gif' />
  <img class='avatarPic' src='http://wac.450f.edgecastcdn.net/80450F/screencrush.com/files/2013/02/brie-bust-open.gif' />
  <img class='avatarPic' src='http://cdn.rsvlts.com/wp-content/uploads/2012/12/Allison-Brie-GIF-14.gif' />

  <!--Adrianne Ho!!-->
  <img class='avatarPic' src='http://madein1987.com/wp-content/uploads/2013/04/GQ-Editorial-Adrianne-Ho-For-Unis-By-Jake-Davis-00.jpg' />   
  <img class='avatarPic' src='http://streetweardeals.com/wp-content/uploads/2014/04/adrienne-ho-in-supreme-x-nike-collab-3.jpg' />
  <img class='avatarPic' src='http://the305.com/blog/wp-content/uploads/2013/05/tumblr_mlej0lanNz1qchuiio1_500.jpg' />
  <img class='avatarPic' src='http://iv1.lisimg.com/image/5447592/600full-adrianne-ho.jpg' />
  <img class='avatarPic' src='http://www.highsnobiety.com/files/2012/08/adrianne-ho-boast-8.jpg' />
  <img class='avatarPic' src='http://www.sweatthestyle.com/wp-content/uploads/2013/09/tumblr_mptw03v7mZ1rxv9sco4_1280.gif' />";
        }

        if ((string)HttpContext.Current.Session["naughty"] == "filthy")
        {
            avatar += @"
 <!--UMMM-->
<!--http://i.imgur.com/mcl0TI1.gif-->
  <img class='avatarPic' src='http://img0.joyreactor.com/pics/post/boobs-tits-gif-woman-978042.gif' />
  <img class='avatarPic' src='http://www.studiojezebel.info/wp-content/uploads/2013/10/Blowjob-GIF.gif' />
  <img class='avatarPic' src='http://images1.sex.com/images/pinporn/2013/05/30/236/2795200-amazing-anal-money-shot-gif-image-with-a-beautiful-brunette.gif' />
  <img class='avatarPic' src='http://mixedsluts.com/wp-content/uploads/2013/01/photo-Blowjob-GIF-392066637.gif' />
  <img class='avatarPic' src='http://i56.tinypic.com/2hhg1g5.gif' />
  <img class='avatarPic' src='http://i.imgur.com/Aiyxvz4.gif' />

  <img class='avatarPic' src='http://31.media.tumblr.com/471440884a785885e63601a305582d77/tumblr_mxngv1XZMv1s18wxno1_1280.gif' />
  <img class='avatarPic' src='http://1.bp.blogspot.com/-35gijq2DRG8/U1j0SINX6qI/AAAAAAAAU_g/aZ9cEkMtVU0/s1600/big-niples.gif' />
  <img class='avatarPic' src='http://www.primehentai.com/wp-content/uploads/2014/03/busty-hentai-gifs-19.gif' />
  <img class='avatarPic' src='http://4.bp.blogspot.com/-PGmdTQtB7ro/U1j0Uotn2KI/AAAAAAAAVAE/QqNAXOT2mxw/s1600/hentai-gif.gif' />
  <img class='avatarPic' src='http://37.media.tumblr.com/tumblr_lihav2mREP1qg32w0o1_500.gif' />
  <img class='avatarPic' src='http://37.media.tumblr.com/db54dffd4ec4d19f9adca0f6600e1d7f/tumblr_mpzdqhT4xf1s92di0o1_500.gif' />
<!--
  <img class='avatarPic' src='http://31.media.tumblr.com/tumblr_m6t1r6o3IA1r3vk7qo1_500.gif' />
  <img class='avatarPic' src='http://firegoon.com/u/a6gh1k.gif' />
  <img class='avatarPic' src='http://www.llamarepublik.com/hashbrowns/var/resizes/Hentai-Gifs/girl%20getting%20fucked%20by%20dick%20hentai%20gif%20uncensored.gif?m=1371205211' />
  <img class='avatarPic' src='http://38.media.tumblr.com/tumblr_m9ucfuwlWb1rdz9cno1_500.gif' />
  <img class='avatarPic' src='http://31.media.tumblr.com/tumblr_m9ooh0vFAX1rat3p6o1_500.gif' />
  <img class='avatarPic' src='http://31.media.tumblr.com/tumblr_m3eyr0Ws7N1r4eqz3o1_500.gif' />
-->
<!--
  <img class='avatarPic' src='http://38.media.tumblr.com/tumblr_m4axcs88pd1rwtt7po1_500.gif' />
  <img class='avatarPic' src='http://fap.to/images/full/44/103/1032333619.gif' />
  <img class='avatarPic' src='http://fap.to/images/full/44/111/1119444099.gif' />
  <img class='avatarPic' src='http://fap.to/images/full/44/123/1238974466.gif' />
  <img class='avatarPic' src='http://fap.to/images/full/44/138/138504871.gif' />
  <img class='avatarPic' src='http://fap.to/images/full/44/121/1214964030.gif' />
-->
  <!--
   <img class='avatarPic' src='http://thegifsgonewild.com/wp-content/uploads/Yurizan-Beltran-titty-fuck-cumshot.gif' />
   <img class='avatarPic' src='http://xxgifs.com/picture/25137-25137_hottest_masturbation_hot_pussy_ever_seen' />
   <img class='avatarPic' src='http://xxgifs.com/picture/24831-24831_awesome_funny_party_animated_picture/tags/hentai' />
   <img class='avatarPic' src='http://xxgifs.com/picture/24213-24213_amazing_bj_anime_porn_animation/tags/hentai' />
   <img class='avatarPic' src='http://xxgifs.com/picture/23473-23473_awesome_head_anime_porn_photo/tags/hentai' />
   <img class='avatarPic' src='http://xxgifs.com/picture/22274-22274_amazing_lesbian_teen_in_amazing_anal_solo_animation/tags/hentai' />
   <img class='avatarPic' src='http://xxgifs.com/picture/22094-22094_hot_anime_porn_picture_featuring_lovely_butt/tags/hentai' />
   <img class='avatarPic' src='http://xxgifs.com/picture/22091-22091_gorgeous_big_tits_in_a_amazing_anime_porn_animated_picture/tags/hentai' />
   <img class='avatarPic' src='http://xxgifs.com/picture/16275-hentai_sexy_girl_with_big_bouncy_tits_/tags/hentai' />
   <img class='avatarPic' src='http://xxgifs.com/picture/25130-25130_photo' />
  -->
 <!--ummm-->";


        }

        avatar += "<div class='cleaner'></div></div>";

        return avatar;
    
    }//getAvatar()
}