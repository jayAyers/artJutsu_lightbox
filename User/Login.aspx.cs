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

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    #region webMethod
    [WebMethod]
    public static string getLoginWindow(List<string> list)
    {
        string html = @"
<div id='loginWindow'>
 <div id='loginPicWrap'>
  <div id='login_artJutsu'>artJutsu</div>
  <div id='loginWrap'>
   <div class='loginList ll1'>
    <div class='panelSwap loginListItem login'>login</div>
    <!--<div class='panelSwap loginListItem forgot'>forgot</div>-->
    <div class='panelSwap loginListItem join loginListItem_last'>join</div>
    <div class='cleaner'></div>
   </div>
   <div class='loginDisplay ll1'>
  {0}
   </div>
  </div> 
 </div>
</div>";

        string login_in  = @"
<div class='login'>
  <div class='login_label'>username</div>
 <input id='login_username' class='login_TB' />
  <div class='login_label'>password</div>
 <input id='login_password' class='login_TB' type='password' />
 <div id='login_submit'>login</div>
 <div id='login_error'></div>
</div>
";
        login_in += @"
<div class='join'>
  <div class='login_label'>username</div>
 <input id='register_username' class='login_TB' />
  <div class='login_label'>password</div>
 <input id='register_password' class='login_TB' type='password' />
  <div class='login_label'>confirm password</div>
 <input id='register_password_confirm' class='login_TB' type='password' />
 <div id='register_submit'>register</div>
 <div id='register_error'></div>
</div>
";
        html = string.Format(html, login_in);

        return html;
    }//end getLoginWindow()

    [WebMethod]
    public static string tryLogin(List<string> list)
    {//[0] username, [1] password

        string isLogin = "false";

        HttpContext.Current.Session["UID"] = tryLogin(list[0], list[1]);
        if ((string)HttpContext.Current.Session["UID"] != "false") { isLogin = "true"; }

        return isLogin;
    }//end tryLogin()

    [WebMethod]
    public static string logout(List<string> list)
    {
        HttpContext.Current.Session["UID"] = "";

        return "logged out";
    }//end logout()

    [WebMethod]
    public static string chkUserExists(List<string> list)
    { return chkUserExists(list[0]); }

    [WebMethod]
    public static string registerUser(List<string> list)
    { return registerUser(list[0], list[1]); }
    #endregion

    #region functions
    public static string tryLogin(string userName, string passWord)
    {
        string isLogin = "false";

        string connStr = ConfigurationManager.ConnectionStrings["aJ_ConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connStr);
        SqlDataReader rdr = null;

        try
        {
            conn.Open();

            string query = @"select userID from Users where userName = @userName and password = @passWord";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add(new SqlParameter("@userName", SqlDbType.VarChar)).Value = userName;
            cmd.Parameters.Add(new SqlParameter("@passWord", SqlDbType.VarChar)).Value = passWord;

            rdr = cmd.ExecuteReader();

            while (rdr.Read()){ isLogin = rdr["userID"].ToString(); }
        }finally { rdr.Close(); conn.Close(); }

        return isLogin;
    }//end tryLogin()

    public static string chkUserExists(string userName)
    {
        string isUser = "false";

        string connStr = ConfigurationManager.ConnectionStrings["aJ_ConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connStr);
        SqlDataReader rdr = null;

        try
        {
            conn.Open();

            string query = @"select userID from Users where userName = @userName";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add(new SqlParameter("@userName", SqlDbType.VarChar)).Value = userName;
            rdr = cmd.ExecuteReader();

            while (rdr.Read()) { isUser = "true"; }
        }
        finally { rdr.Close(); conn.Close(); }

        return isUser;    
    }//end chkUserExist()

    public static string registerUser(string userName, string passWord)
    {
        string connStr = ConfigurationManager.ConnectionStrings["aJ_ConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connStr);
        SqlDataReader rdr = null;

        try
        {
            conn.Open();

            string query = @"
insert into Users (userName, password, dateJoined)
values (@userName, @password, @date)

select @@identity
";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add(new SqlParameter("@userName", SqlDbType.VarChar)).Value = userName;
            cmd.Parameters.Add(new SqlParameter("@passWord", SqlDbType.VarChar)).Value = passWord;
            cmd.Parameters.Add(new SqlParameter("@date", SqlDbType.VarChar)).Value = DateTime.Now;
            rdr = cmd.ExecuteReader();

            while (rdr.Read()) { HttpContext.Current.Session["UID"] = rdr[0].ToString(); }
        }
        finally { rdr.Close(); conn.Close(); }

        return "SUCCESS";
    }//end chkUserExist()
    #endregion
}