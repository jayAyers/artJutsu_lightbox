using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.IO;

/// <summary>
/// Summary description for jpi
/// </summary>
public class jpi
{

	public jpi()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static char priv_delim = 'Þ';//yyyy'#';
    public char Delim
    {
        get{ return priv_delim; }
        set{ priv_delim = value; }
    }
    
    public static string parseHTML(string filename, string hash)
    {
        string html = "";

        try
        {
            using (StreamReader sr = new StreamReader(System.Web.HttpContext.Current.Server.MapPath(filename)))
            {
                String line = sr.ReadToEnd();
                html = line;

                //
                string[] html_split = html.Split('#');

                for (int i = 0; i < html_split.Length; i++)
                {
                    if (hash == html_split[i].Split('\n')[0].Trim())
                    { html = html_split[i].Substring(html_split[i].Split('\n')[0].Length); }
                }
            }//end using
        }
        catch (Exception e) { html = e.Message; }

        return html;
    }//end parseHTML()

    public static string parseHTML(string filename, string hash, string replaceOld, string replaceNew)
    {
        string html = parseHTML(filename, hash);
        if (replaceNew != "") { html = html.Replace(replaceOld, replaceNew); }

        return html;    
    }//end parseHTML()
}