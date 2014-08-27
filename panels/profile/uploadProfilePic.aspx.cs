using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.IO;

using System.Drawing;
using System.Drawing.Drawing2D;

using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using System.Web.Services;

public partial class uploadProfilePic : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        string html = get_img_and_btn("");

        user_profile_frame.Controls.Add(new LiteralControl(html));


        HttpPostedFile hpfFile = Request.Files["file"];
        if (hpfFile != null && HttpContext.Current.Session["UID"].ToString().Length > 0)
        {
            if (Path.GetExtension(hpfFile.FileName).ToLower() == ".jpg"
                || Path.GetExtension(hpfFile.FileName).ToLower() == ".jpeg"
                || Path.GetExtension(hpfFile.FileName).ToLower() == ".png"
                || Path.GetExtension(hpfFile.FileName).ToLower() == ".gif")
            {
                string fileName = "../../image/profile_pic/" + hpfFile.FileName;
                hpfFile.SaveAs(Server.MapPath(fileName));

                updateProfilePic(hpfFile.FileName);
                user_profile_frame.Controls.Clear();
                user_profile_frame.Controls.Add(new LiteralControl(get_img_and_btn("")));
            }
        }//end if
    }//end Page_Load

    [WebMethod]
    public static string getSrc(List<string> list)
    { return get_img_and_btn("getSrc"); }

    public static string get_img_and_btn(string type)
    {
        string html = "";

        string connStr = ConfigurationManager.ConnectionStrings["aJ_ConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connStr);
        SqlDataReader rdr = null;

        try
        {
            conn.Open();
            string query = "select user_pic from Users_info where userID = @uid";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add(new SqlParameter("@uid", SqlDbType.VarChar)).Value = HttpContext.Current.Session["UID"];
            rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                html = String.Format(@"
     <img id='aj_options_info_pic' src='../../image/profile_pic/{0}' />
     <div id='aj_options_info_upload'>upload</div>
", rdr["user_pic"].ToString());

                if (type == "getSrc") { html = rdr["user_pic"].ToString(); }
            }
        }finally { rdr.Close(); conn.Close(); }

        return html;    
    }//end get_img_and_btn()

    public void updateProfilePic(string file)
    {

        string connStr = ConfigurationManager.ConnectionStrings["aJ_ConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connStr);

        try
        {
            conn.Open();

            string sql = "update Users_info set user_pic = @file where userID = @uid";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add(new SqlParameter("@file", SqlDbType.VarChar)).Value = file;
            cmd.Parameters.Add(new SqlParameter("@uid", SqlDbType.VarChar)).Value = HttpContext.Current.Session["UID"];

            cmd.ExecuteNonQuery();
        }
        finally { conn.Close(); }
    }//end updateProfilePic()
}