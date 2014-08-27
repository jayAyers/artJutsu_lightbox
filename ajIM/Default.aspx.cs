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



public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    { }

    #region webMethods
    [WebMethod]
    public static string getStarted(List<string> list)
    {
        return list[0] + "go!";
    } //end getStarted()

    [WebMethod]
    public static string getImg(List<string> list)
    {//[0] userID

        string seeImg = "";
        if (list[0] == "1") { seeImg = "2"; }
        if (list[0] == "2") { seeImg = "1"; }

        return "image/tempPic" + seeImg + ".png";// "<img class='ajIM_prev_img' src='image/tempPic.png' />";
    } //end getStarted()

    [WebMethod]
    public static string getIMWrap(List<string> list)
    {
        return jpi.parseHTML("Default.aspx", "imConvo");
    }

    [WebMethod]
    public static string getIM(List<string> list)
    {  
        string text = getIM(list[0]);

        //updateIM(list[0]);

        return text;
    }

    [WebMethod]
    public static string insertIM(List<string> list)
    {//[0] convo, [1] userID, [2] text 
        
        return insertIM(list[0], list[1], list[2]); 
    
    }
    #endregion

    #region functions
    public static string getIM(string userID)
    {
        string html = "";
        string convo = "";
        string last_read = "";

        string connStr = ConfigurationManager.ConnectionStrings["aJ_ConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connStr);
        SqlDataReader rdr = null;

        try
        {
            conn.Open();
            string sql = @"
select ict.[key], ict.convo_key, ict.convo_text, ict.userID
from im_convo ic
 inner join im_convo_text ict
  on ic.[key] = ict.convo_key
 inner join im_convo_read icr
  on ic.[key] = icr.convo_key
where ic.status = 'active'
 and icr.userID = @userID
and ict.[key] > icr.im_convo_text_key  
";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add(new SqlParameter("@userID", SqlDbType.VarChar)).Value = userID;
            rdr = cmd.ExecuteReader();

            string currUser = "";

            while (rdr.Read()) {

                if (rdr["userID"].ToString() == userID) { html += "<div class='im_text_ME'>" + rdr["convo_text"].ToString() + "</div>"; }
                else{ html += "<div class='im_text_YOU'>" + rdr["convo_text"].ToString() + "</div>"; }

                last_read = rdr["key"].ToString();
                convo = rdr["convo_key"].ToString();
            }
        }finally { rdr.Close(); conn.Close(); }

        updateIM(userID, convo, last_read); //may need more

        return html;
    }

    public static string updateIM(string userID, string convo, string last_read)
    {
        string html = "";

        string connStr = ConfigurationManager.ConnectionStrings["aJ_ConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connStr);

        try
        {
            conn.Open();
            string sql = @"
update im_convo_read 
set im_convo_text_key = @last_read
where userID = @userID
 and convo_key = @convo
";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add(new SqlParameter("@last_read", SqlDbType.VarChar)).Value = last_read;
            cmd.Parameters.Add(new SqlParameter("@userID", SqlDbType.VarChar)).Value = userID;
            cmd.Parameters.Add(new SqlParameter("@convo", SqlDbType.VarChar)).Value = convo;

            cmd.ExecuteNonQuery();
        }finally { conn.Close(); }

        //html = "dud";
        return html;
    }

    public static string insertIM(string convo, string userID, string text)
    {
        string connStr = ConfigurationManager.ConnectionStrings["aJ_ConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connStr);
        
       
        try
        {
            conn.Open();
            string sql = @"insert into im_convo_text (convo_key, date, userID, convo_text) values (@convo, @date, @userID, @text)";
            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.Add(new SqlParameter("@convo", SqlDbType.VarChar)).Value = convo;
            cmd.Parameters.Add(new SqlParameter("@date", SqlDbType.VarChar)).Value = System.DateTime.Now;
            cmd.Parameters.Add(new SqlParameter("@userID", SqlDbType.VarChar)).Value = userID;
            cmd.Parameters.Add(new SqlParameter("@text", SqlDbType.VarChar)).Value = text;

            cmd.ExecuteNonQuery();
        }
        finally { conn.Close(); }

        return "success";
    }//end insertIM()
    #endregion
}