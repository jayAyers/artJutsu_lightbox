//WOW!
//http://soulwire.github.io/sketch.js/


//globals
var qFlag = true;var startFlag = false;

var lb_url = "og:url";

//may add more...
//alert(jqAJAX('Default.aspx/getUID', ['']));
//starter
$(document).ready(function () {
    //end prompts
    $('#home_prompt').hide();
    //////

//lightbox menu icon
//$('.panelSwap.lb_ListItem.lightbox, .aj_submenu').hide();

    if (getParameterByName('qLB') != '') { $('#webSearch_TB').val(getParameterByName('qLB')); $('#webSearch_Btn').trigger('click'); }
    else { } //loadDailyDrawing(); }

    //$('#webSearch_TB').val('https://www.youtube.com/watch?v=Xv1LFjApbKk');
    //$('#webSearch_TB').val('http://86bb71d19d3bcb79effc-d9e6924a0395cb1b5b9f03b7640d26eb.r91.cf1.rackcdn.com/wp-content/uploads/2013/04/sonic-and-mega-man-in-comic-book-crossover.jpg');
    //$('#webSearch_TB').val('http://8wayrun.com/threads/megaman-vs-sonic.18003/');
    //$('#webSearch_TB').val('http://danluvisiart.deviantart.com/art/THE-STREETS-Chapter-1-457224890');
    //$('#webSearch_TB').val('http://lilclaw.deviantart.com/art/--410253220');
    //$('#webSearch_TB').val('http://www.deviantart.com/art/Wolverine-Print-456947988');
    //$('#webSearch_TB').val('http://www.deviantart.com/art/Gaia-1162014-427262419');
    //$('#webSearch_TB').val('http://www.deviantart.com/art/In-Teals-and-Drizzles-443707005');
    //$('#webSearch_TB').val('http://www.deviantart.com/art/N-O-T-O-R-I-O-U-S-354083760');
    //$('#webSearch_TB').val('');

    //$('.lb_List.lb1 > .lightbox').trigger('click');
    //$('.lb_List.lb1 > .search').trigger('click');
    $('.lb_List.lb1 > .profile').trigger('click');

    $('.searchList.sl1 > .search').trigger('click');
    //$('#webSearch_Btn').trigger('click');
    //$('#searchPic0').trigger('click');

    //setup other parts
    startProfile('.lb_Display.lb1 > .profile', 'panels/profile/');
    //startProfile('.lb_Display.lb1 > .profile', 'panels/search/');
    //startProfile('.lb_Display.lb1 > .profile', 'panels/blog/');

    $('#lb_load').hide();

});                      // $('#clicky').trigger('click'); });
//setInterval(function () { chkNuPic() }, 30000);
//stop interval...
//http://stackoverflow.com/questions/109086/stop-setinterval-call-in-javascript

//clicks
$('#webSearch_Btn').live('click', function () {
    if ($('#webSearch_TB').val() != '') {
        $('#lb_load').show();

        setTimeout(function () {
            jqAJAX('Default.aspx/storeURL', [$('#webSearch_TB').val()]);

            lb_url = $('#webSearch_TB').val();
            searchLightbox();
            $('#lb_load').fadeOut();
        }, 1000);
    } //end if
});

$('#close_webSearch').live('click', function () {
    $('#webSearch_Results').hide();
    $('#lightbox').show();
});

$('.searchPic, .searchYoutube').live('click', function () {
    var $$ = $(this);
    $('#lb_load').show();

    setTimeout(function () {
        //$('#aj_menu_icon').trigger('click');
        $('.panelSwap.lb_ListItem.lightbox, .aj_submenu').fadeIn(); //will do an append later on...

        loadLightbox($$);

        $('#floatToolbar').trigger('click');

        $('#lb_load').fadeOut();
    }, 1000);
});


//tools
$('.lightbox_tool_icon').live('click', function () {
    //$('.lightbox_tool_icon').css('background-color', 'red'); // ('box-shadow', '5px 5px 5px #708090');

    if ($(this).next().is(":visible")) { $(this).next().hide(); }
    else { $('.lightbox_tool_palette').hide(); $(this).next().show(); }
});

//setInterval(function () { $('#saveCanvas').trigger('click') }, 1000);

$('#saveCanvas').live('click', function (e) {
    //http://stackoverflow.com/questions/8126623/downloading-canvas-element-to-an-image

    //alert('hi');

    if (!parseInt(profileUID)) {
        $('#home_prompt').show();
        setTimeout(function () {
            $('#home_prompt').fadeOut('slow');
        }, 1000);
        e.stopPropogation();
    }
    else {
        var canvas = document.getElementById("lightbox_canvas");

        //http://img2.wikia.nocookie.net/__cb20130101205554/dragonball/es/images/0/07/Ilustraci%C3%B3n_de_Goku.png
        //////////////
        var img = canvas.toDataURL("image/png").substr(22);
        //$('#artJutsuBlog').append("<textarea>" + img + "<br /><br /><br />" + img.length + "</textarea>");


        ///////////////////////////////////////////
        var dilm = 100000;

        while (img.length > dilm) {

            alert('above dilm');

            alert(img.substr(0, dilm));
            img = img.substr(dilm);
        }

        alert(img);
        ///////////////////////////////////////////
        //for testing
        img = canvas.toDataURL("image/png").substr(22);


        //do that thing!
        //}
        //alert(img.substr(0, 100));
        //alert(img.substr(100, 100));
        //alert(img.substr(0, 200));


        /*
        what I need to do is save [img] and then create the image
        */



        //////////////


        if ($('#insertTitle').val() != '') { $('#insertTitle').hide(); $('#lb_title').html($('#insertTitle').val()); }

        var list = [img, profileUID, $('#lb_url_B').children().html(), $('#lb_title').html(), $('#lb_desc').html(), $('#lightbox_pic_viewer').attr('src')];


        //$('#lb_save').html(jqAJAX('Default.aspx/saveCanvasImage', list)).fadeIn(1000).fadeOut(3000);
        $('#save_title').html(jqAJAX('Default.aspx/saveCanvasImage', list)); //alert(jqAJAX('Default.aspx/saveCanvasImage', list));
        //$('#save_prompt_arrow, #save_prompt_arrow_border').hide();
        $('#save_prompt').fadeIn(1000).fadeOut(3000);
    } //end else
});

$('#aj_menu_icon').live('click', function () {//http://jsfiddle.net/SkiWether/KFmLv/
    $('.lb_List.lb1').toggle('slide');
    $('.lb_List.lb1>.aj_submenu').toggle('slide');
});

$('.lb_ListItem').live('click', function () {
    //$('.aj_submenu').hide();
    //$(this).children().show();
});

$('#webSearch_report_noPic').live('click', function () {
    $('#webSearch_cantFind_TB').val('');
    $('#webSearch_cantFind_submit').html('submit');

    $('#webSearch_report_info').fadeToggle();
});

$('#webSearch_cantFind_submit').live('click', function () {
    $('#lb_load').show();

    setTimeout(function () {
        var search_tb = $('#webSearch_TB').val();
        var pic_info = $('#webSearch_cantFind_TB').val();

        if (pic_info != 'abcd') { $('#webSearch_cantFind_submit').html(jqAJAX('Default.aspx/cantFind_submit', ['searchPic', search_tb, pic_info])); }

        $('#lb_load').fadeOut();
        $('#webSearch_report_info').fadeToggle();
    }, 500);
});


$('#floatToolbar').live('click', function () {
    var thisSrc = $(this).attr('src').split('/')[2];
    //alert(thisSrc);
    if (thisSrc == 'pin_up.png') {
        $(this).attr('src', 'image/icons/pin_down.png');
        $('#lightbox_tools').css('position', 'fixed');
    }
    if (thisSrc == 'pin_down.png') {
        $(this).attr('src', 'image/icons/pin_up.png');
        $('#lightbox_tools').css('position', 'static');
    }
});


//keydown, hover
$('.searchPic, .searchYoutube').live({//swap sources
    mouseenter: function () { var $$ = $(this); swapSourcePic($$, 'portfolio_pic_wrap'); },
    mouseleave: function () { var $$ = $(this); swapSourcePic($$, 'portfolio_pic_wrap'); }
});

$(document).keydown(function (e) {
    if (e.which == 13) {
        if ($('#webSearch_TB').is(":focus")) { $('#webSearch_Btn').trigger('click'); }
    } //end if		
});	
 
 