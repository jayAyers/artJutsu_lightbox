var profilePath = '';
var profileUID = '';

//$(document).ready(function () { startProfile('#wrap', ''); });

function startProfile(wrap, path) {
    //globals
    profilePath = path;
    profileUID = jqAJAX('Default.aspx/getUID', ['']);
    
    $(wrap).html('<link rel="Stylesheet" href="' + profilePath + 'StyleSheet.css" />');


    if (!parseInt(profileUID)) 
    {
        $(wrap).append(user_startLogin());
        $('.loginList.ll1 > .login').trigger('click');
    }
    else 
    {
        var list = [profileUID];
        $(wrap).append(jqAJAX(profilePath + 'Default.aspx/startProfile', list));

        getPortfolio();
        $('#profile_options').html(jqAJAX(profilePath + 'Default.aspx/getOptions', ['']));

        //$('.profileMenuList.pml1 > .portfolio').trigger('click');
        $('.profileMenuList.pml1 > .friends').trigger('click');     
        //$('.profileMenuList.pml1 > .options').trigger('click');


        ////other stuff
        //click facebook
        $('#ajSocial_2').trigger('click');
    }
}//end startProfile()

//clicks
$('.portfolio_pic_add_submit').live('click', function () {
    //http://stackoverflow.com/questions/1531093/how-to-get-current-date-in-javascript

    //alert($(this).parent().html());
    //alert($(this).parent().children('.searchPic').attr('id').substr(9));
    var thisID = $(this).parent().children('.searchPic').attr('id').substr(9);

    var day = prompt('What day would you like for this pic?', '');
    if (day != null && day != '') {
        jqAJAX(profilePath + 'Default.aspx/addDailies', [thisID, day]);
        $(this).removeClass('portfolio_pic_add_submit').html('pending at: ' + day);
    }

});

$('#saveCanvas').live('click', function () { getPortfolio(); });
$('#profile_Logout').live('click', function () { user_Logout(); });

/*********
  options
 *********/
$('.aj_options_info_show_email').live('click', function () {
    $('.aj_options_info_show_email').removeClass('selected_option');
    $(this).addClass('selected_option');

    //if ($(this).html() == 'sure') { }
    //if ($(this).html() == 'nah') { }

    //    alert('do something');
    //jqAJAX(profilePath + 'Default.aspx/', [$(this).html()]);

});
$('.aj_options_info_send_email').live('click', function () {
    $('.aj_options_info_send_email').removeClass('selected_option');
    $(this).addClass('selected_option');
    //    alert('do something');

    //if ($(this).html() == 'do it') { }
    //if ($(this).html() == 'noO') { }

    //jqAJAX(profilePath + 'Default.aspx/', [$(this).html()]);
});


$('#aj_options_info_upload').live('click', function () { alert('do something'); });
$('#aj_options_location').live('change', function () {
    var thisVal = 'image/city/' + $(this).val() + '.png';

    $('#aj_options_location_pic').attr('src', thisVal);


});

$('#aj_options_info_submit').live('click', function () {

    var firstName = $('#aj_options_info_firstName').val();
    var lastName = $('#aj_options_info_lastName').val();
    var month = $('#aj_options_info_birth_month').val();
    var day = $('#aj_options_info_birth_day').val();
    var year = $('#aj_options_info_birth_year').val();
    var location = $('#aj_options_location').val();
    var email = $('#aj_options_info_email').val();
    var perm_show_email = $('.aj_options_info_show_email.selected_option').html();
    var perm_send_email = $('.aj_options_info_send_email.selected_option').html();

    var DOB = month + '/' + day + '/' + year;
    DOB = '1/1/2001';

    var list = [firstName, lastName, DOB, location, email, perm_show_email, perm_send_email];

    $(this).html(jqAJAX(profilePath + 'Default.aspx/setUserInfo', list));

    var $$ = $(this);
    setTimeout(function () { $$.html('submit'); }, 1000);
});


$('#addAvatarURL_go').live('click', function () { change_Avatar_Banner('avatar', $('#addAvatarURL').val()); });
$('#addBannerURL_go').live('click', function () { change_Avatar_Banner('banner', $('#addBannerURL').val()); });

$('.avatarPic').live('click', function () { change_Avatar_Banner('avatar', $(this).attr('src')); });
$('.bannerPic').live('click', function () { change_Avatar_Banner('banner', $(this).attr('src')); });

$('.profileMenuList.pml1 > .friends').live('click', function () {
    $('#profile_friends').html(jqAJAX(profilePath + 'Default.aspx/startFriends', ['']));

    $('.aj_friend_post_comment_wrap').each(function () {
        //if ($(this).children().length == 1) { $(this).hide(); }
    });
});

//functions
function change_Avatar_Banner(type, src) {
    if (src != '') {
        if (type == 'avatar') {
            jqAJAX(profilePath + 'Default.aspx/changeAvatar', [src]);
            $('#profilePic').attr('src', src);

            $('#addAvatarURL_message').html('Avatar Changed!!!').fadeIn();
            setTimeout(function () { $('#addAvatarURL_message').fadeOut('2000'); }, 1000);
            $('#addAvatarURL').val('');
        }

        if (type == 'banner') {
            jqAJAX(profilePath + 'Default.aspx/changeBanner', [src]);
            $('#profileTop').css('background-image', 'url(' + src + ')');

            $('#addBannerURL_message').html('Banner Changed!!!').fadeIn();
            setTimeout(function () { $('#addBannerURL_message').fadeOut('2000'); }, 1000);
            $('#addBannerURL').val('');
        }
    } //end if
} //end change_Avatar_Banner()

function getPortfolio() {
    $('#profile_portfolio').html(jqAJAX(profilePath + 'Default.aspx/getPortfolio', ['']));

} //end getPortfolio()


/**********************
   FRIENDS
 **********************/
//$('.aj_top_friends_pic').live('click', function () { alert($(this).parent().attr('id').substr(5)); });
$('.aj_top_friends_status').live('click', function () {
    if ($(this).html() == 'add') {
        jqAJAX(profilePath + 'Default.aspx/requestFriend', [$(this).parent().attr('id').substr(5)]);
        $(this).html('pending').removeClass('aj_top_friends_status_add').addClass('aj_top_friends_status_pending');
    } //end if
});

$('.aj_friend_request_accept').live('click', function () { acceptFriend($(this), 'friend'); });
$('.aj_friend_request_reject').live('click', function () { acceptFriend($(this), ''); });

function acceptFriend($$, status) {
    jqAJAX(profilePath + 'Default.aspx/acceptFriend', [$$.parent().attr('id').substr(9), status]);
    $$.parent().fadeOut();

    $('#my_friend_wrap').html(jqAJAX(profilePath + 'Default.aspx/getMyFriends', ['']));
}//end acceptFriend

/*****************************
  social
 *****************************/
$('.aj_social_ListItem').live('click', function () {
    var socialID = $(this).attr('id').substr(9);
    $('#aj_social_Display').html(jqAJAX(profilePath + 'Default.aspx/getMySocialInfo', [socialID, 'get']));

    $('#aj_social_display_link_edit').hide();
});

$('.aj_social_display_share').live('click', function () {
    var socialID = $(this).attr('id').substr(21);

    var thisText = $('#aj_social_display_link_write_TB').val();
    if (thisText == undefined) { thisText = ''; }

    var toShare = '';
    var toEdit = '';

    if ($(this).html() == 'share')
    { $(this).html('unShare'); toShare = 'yes'; $('#ajSocial_' + socialID).addClass('aj_social_added'); }
    else if ($(this).html() == 'unShare') { $(this).html('share'); toShare = 'no'; $('#ajSocial_' + socialID).removeClass('aj_social_added'); }
    else if ($(this).html() == 'save') { $('#ajSocial_' + socialID).addClass('aj_social_added'); toShare = 'yes'; toEdit = 'edit'; $(this).html('unShare'); }


    $('#aj_social_display_link_write_saved').html(jqAJAX(profilePath + 'Default.aspx/getSocialLink', [socialID, thisText, toShare, toEdit]));
    $('#aj_social_display_link_write').hide();
});

$('#aj_social_display_web_link_edit').live('click', function () {
    //alert('1');
    $('#aj_social_display_link_write').show();
    $('#aj_social_display_link_write_saved').html('');

    //alert('2');
    var socialID = '';
    
    try{ socialID = $(this).parent().parent().children('.aj_social_display_share').attr('id').substr(21);
    //alert('3');
    $('#aj_social_display_link_write').html(jqAJAX(profilePath + 'Default.aspx/getMySocialInfo', [socialID, 'edit']));
    }catch(e){}

    $('.aj_social_display_share').html('save');
});

 //end social

$('#aj_write_friends_cancel').live('click', function () { $('#aj_write_friends_TA').val(''); });

$('.aj_friend_post_TB_click').live('click', function () {
    var list = [$(this).parent().parent().parent().attr('id').substr(4)];

    $(this).parent().parent().children('.aj_friend_post_comment_user_wrap').html(jqAJAX(profilePath + 'Default.aspx/getTB', list));
    $(this).parent().parent().children('.aj_friend_post_comment_user_wrap').children('.aj_friend_post_TA').focus();
    $(this).hide();
});

$('.aj_friend_post_comment_cancel, .aj_friend_post_comment_submit').live('click', function () {
    $(this).parent().children('.aj_friend_post_TB_click').show();
    $(this).parent().parent().children('.aj_friend_post_comment_user_starter').html("<div class='aj_friend_post_TB_click'></div>");

    /////////////////
    var thisPost = $(this).parent().children('.aj_friend_post_TA').val();
    if ($(this).hasClass('aj_friend_post_comment_submit') && thisPost != '') {
        var list = [$(this).attr('id').substr(10), thisPost];
        $(this).parent().parent().children('.aj_friend_post_comment_previous_post').append(jqAJAX(profilePath + 'Default.aspx/submitFriendPost', list));
    }
    /////////////////

    $(this).parent().html('');
});

$('.aj_friend_post_fave').live('click', function () {
    var thisSrc = $(this).attr('src').split('/')[2];
    var toAdd = '';

    if (thisSrc == 'fave.png') { $(this).attr('src', 'image/portfolio/fave_selected.png'); toAdd = 'plus'; }
    if (thisSrc == 'fave_selected.png') { $(this).attr('src', 'image/portfolio/fave.png'); toAdd = 'minus'; }

    $(this).parent().children('.aj_friend_post_fave_count').html(jqAJAX(profilePath + 'Default.aspx/addFave', [toAdd]));
});



$('#aj_search_friends_Btn').live('click', function () {
    $('#aj_search_friends_results').html(jqAJAX(profilePath + 'Default.aspx/searchFriends', [$('#aj_search_friends_TB').val()]));
});

$(document).keydown(function (e) {
    if (e.which == 13) {
        if ($('#aj_search_friends_TB').is(":focus")) { $('#aj_search_friends_Btn').trigger('click'); }
        
        if ($('#addBannerURL').is(":focus")) { change_Avatar_Banner('banner', $('#addBannerURL').val()); }
        if ($('#addAvatarURL').is(":focus")) { change_Avatar_Banner('avatar', $('#addAvatarURL').val()); }
    } //end if		
});	

