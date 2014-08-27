//globals
//var qFlag = true;var startFlag = false;
var userID = '';
var im_path = '';

//starter
//$(document).ready(function () { startIM('#wrap', '', '2'); });

function startIM(wrap, path, id) {
    im_path = path;
    $('#wrap').append(jqAJAX(im_path + 'Default.aspx/getIMWrap', ['']));
    //alert(jqAJAX(im_path + 'Default.aspx/getIMWrap', ['']));
    userID = prompt("Enter '2'", "2"); //id;

    $('#userID').html(userID);
    setInterval(function () { getIM('auto') }, 1000);

    $('.ajIM_preview').html("<img class='ajIM_prev_img' src='image/tempPic.png' />");

}

//clicks
$('.ajIM_send_btn').live('click', function () {
    var thisParent = $(this).parent().parent().attr('id').substr(6);
    var txt = $(this).parent().children('.ajIM_send_txt').val();

    var list = [thisParent, userID, txt];
    if (txt.length != 0) { jqAJAX(im_path + 'Default.aspx/insertIM', list); }

    //
    $(this).parent().children('.ajIM_send_txt').val('').focus();

});

//functions
function getIM(IM_type) {
//will update later
    var txt = $('.ajIM_convo').html();
    txt += jqAJAX(im_path + 'Default.aspx/getIM', [userID]);

    $('.ajIM_convo').html(txt);
    if (txt.length != 0) { $('.ajIM_convo').scrollTop(1000); }
    //can't scroll up after scroll down :(
    
    
    $('.ajIM_prev_img').attr('src', jqAJAX(im_path + 'Default.aspx/getImg', [userID]));
}//end getIM


//keydown
//http://stackoverflow.com/questions/10655202/detect-multiple-keys-on-single-keypress-event-on-jquery
//     var keyMap = {17: false, 49: false};
$(document).keydown(function (e) {
    if (e.which == 13) { $('.ajIM_send_btn').trigger('click'); }
});