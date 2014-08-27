<%@ Page Language="C#" AutoEventWireup="true" CodeFile="uploadProfilePic.aspx.cs" Inherits="uploadProfilePic" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <style>
      #aj_options_info_pic{width: 150px; height: 175px; border: 1px solid black;}
      #aj_options_info_upload{margin: 15px 0 0 25px; width: 100px; height: 22px; max-height: 55px; border: 1px solid #708090; text-align: center; 
                              cursor: pointer; color: #708090; border-radius: 2px;}
    </style>

    <script src='../../jpi/jquery-1.4.4.min.js'></script>
    <script src='../../jpi/jqAJAX.js'></script>
    <script src='../../js/ajaxupload.min.js'></script>
    

    <script>
        $(document).ready(function () {
            $("#aj_options_info_upload").ajaxUpload({
                url: "uploadProfilePic.aspx",
                name: "file",
                onSubmit: function () {
                    $('#aj_options_info_upload').html('Uploading ... ');
                }
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <asp:Panel ID='user_profile_frame' runat="server"></asp:Panel>
    </div>
    </form>
</body>
</html>
