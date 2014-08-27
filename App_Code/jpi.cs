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

    public static string getStringBetween(string fullText, string txt_A, int txt_A_length, string txt_B)
    {
        int txt_index_A = 0;
        int txt_index_B = 0;

        if (fullText.Contains(txt_A))
        {
            txt_index_A = fullText.IndexOf(txt_A) + txt_A_length; //txt_A_length == not including txt_A for the upcoming substring

            if (fullText.Substring(txt_index_A).Contains(txt_B))
            {
                txt_index_B = fullText.Substring(txt_index_A).IndexOf(txt_B);
                fullText = fullText.Substring(txt_index_A, txt_index_B);
            }
            else { fullText = ""; }
        }
        else { fullText = ""; }

        return fullText;
    }//end getStringBetween()

    public static string getStringBetween(string fullText, string txt_A, string txt_B)
    {//overloaded function
        return getStringBetween(fullText, txt_A, 0, txt_B);
    }
}