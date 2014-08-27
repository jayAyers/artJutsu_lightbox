<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <link rel="StyleSheet" href="Stylesheet.css" />

  <script src="jpi/jpi.js"></script>    
  <script>jpi.startJPI('');</script>
  <script src="JScript.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div id='wrap'>
    

    </div>
    <div id='userID'></div>

    #imConvo
    <div id='convo_1' class='ajIM'>
      <div class='ajIM_preview'>preview</div>
      <div class='ajIM_convo'></div>
      <div class='ajIM_textWrap'>
       <input class='ajIM_send_txt' />
       <div class='ajIM_send_btn'>send</div>
       <div class='cleaner'></div>
      </div>
    </div>
    #endimConvo
    </form>
</body>
</html>
