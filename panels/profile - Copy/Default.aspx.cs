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
    {                                                  }

    [WebMethod]
    public static string startProfile(List<string> list)
    {

        return @"
this is the profile<br />
     here are my friends<br />
     here are my favorite pics<br />
     here are my work...
";
    }//end startProfile()
}