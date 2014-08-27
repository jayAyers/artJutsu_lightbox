<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<!--
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
-->

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>[aJ] Lightbox</title>
  <link rel="StyleSheet" href="Stylesheet.css" />
  <link rel="StyleSheet" href="Search.css" />
  <link rel="StyleSheet" href="template.css" />

  <script src="jpi/jpi.js"></script>    
  <script>jpi.startJPI('');</script>

  <script src='js/ajaxupload.min.js'></script>

  <!----->
  <script src='User/JScript.js'></script>
  <link rel="Stylesheet" href="User/StyleSheet.css" />
  <!----->

  <script src="clicks.js"></script>
  <script src="functions.js"></script>
  <script src="template.js"></script>
  <script src="ready.js"></script>

  <script src="sketch.js"></script>

  <link rel="Stylesheet" href="ajIM/Stylesheet.css" />
  <script src="ajIM/JScript.js"></script>

  <!--profile-->
  <script src="panels/profile/JScript.js"></script>
</head>
<body>
  <a id='artJutsu' href='http://artjutsu.com/' target="_blank">artJutsu <span id='lightbox_txt'>lightbox</span></a>
  <a id='artJutsuBlog' href='http://artjutsu.com/blog' target="_blank">blog</a>
  
  <img id='lb_load' src="image/icons/loadingGif.gif" />

  <div id='lb_List_wrap'></div> 
   
  <div id='wrap'>  
  <!--<div id='loadCanvas'>LOAD</div>-->
   <div id='ajMenu' class='lb_List lb1'>
    <div id='home_prompt'>
     <div id='home_title'>login to save</div>
     <div id='home_prompt_arrow_border'></div>
     <div id='home_prompt_arrow'></div>     
    </div>
    <img class='panelSwap lb_ListItem profile' src='image/icons/home.png' />
    <img class='panelSwap lb_ListItem lightbox' src='image/icons/lightbox.png'>
     <div class='lb_List lb1 aj_submenu'>
      <img class='panelSwap lb_ListItem search' src='image/icons/search.png' />
      <img class='panelSwap lb_ListItem gallery' src='image/icons/gallery.png' />
     </div>    
    </img>
    
    <!--<div class='panelSwap lb_ListItem blog'>B</div>-->
   </div>
   <img id='aj_menu_icon' src='image/icons/menu.png' />

   <div class='lb_Display lb1'>
    <div class='lightbox' id='lightbox'></div>
    <div class='search'>
      <div class='searchList sl1'>
       <div class='panelSwap searchListItem search'></div>
       <div class='cleaner'></div>
      </div>
      <div class='searchDisplay sl1'>
       <div class='search'>
         <input id='webSearch_TB' />
         <div id='webSearch_Btn'>search</div>
         <div class='cleaner'></div>
         <div id='webSearch_Result'></div>
         <div id='webSearch_Xtra'>
           <b>Draw over pictures and videos from the web!</b><br /><br />

           Lets get started!<br /><br />
           Crawling for a picture is easy!<br /><br />
           
           <b>1)</b> Copy a url from the link you'd like to crawl.<br /><br />
           <b>2)</b> Paste it in the search bar on the left.<br /><br />
           <b>3)</b> Click the "search" button.<br /><br />
           <b>4)</b> Select the image you'd like to draw.<br /><br /><br /><br />
           
           <a href='http://www.wacom.com/' target="_blank" title='wacom tablets'>
            <img style='margin-left: 12px; width: 125px; border: 2px solid #708090;' src='http://adsoftheworld.com/sites/default/files/wacom_pen_aotw.jpg' />
            <div style='margin-left: 7px'>wacom tablets starting at $99</div>
           </a>

           <!--
           How to use this thing...<br /><br />
           1) Crawl by Web url...<br /><br /><br />
           2) Crawl by Youtube url...<br /><br /><br />

           Users also searched other links...<br /><br /><br /><br />
           A) this<br /><br />
           B) that<br /><br /><br /><br />

           <a href='http://megaman.capcom.com/' target="_blank">
            <img style='margin-left: 12px; width: 125px; border: 1px solid #708090;' src='http://kobun20.interordi.com/wp-content/uploads/2010/06/MegaMan_4.jpg' />
           </a>
           -->
         </div>
         <div class='cleaner'></div>
       </div>
      </div>
      <div class='cleaner'></div>
      <!--
      
      -->
    </div>
    <div class='gallery'><div style='margin-top: 250px; text-align: center; font-size: 30px; font-weight: bolder; color: #708090;'>lightbox gallery coming soon</div></div>
    <div class='profile'></div>
    <div class='blog'>
      <!--<script type="text/javascript" src="http://artjutsu.tumblr.com/js"></script>-->    
    </div>
   </div><!--end lb_Display-->
  </div><!--end wrap-->
  
  <div id='update_pic_wrap'></div>
</body>
</html>
