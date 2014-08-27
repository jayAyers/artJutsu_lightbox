function loadDailyDrawing() {
    //daily pic
    var id = '';

    if (getParameterByName('lightbox') != '') { id = getParameterByName('lightbox'); }

    $('.searchDisplay.sl1 > .search').append(jqAJAX('Default.aspx/getAutoDraw', [id]));
    $('.searchDisplay.sl1 > .search').children('.searchPic').trigger('click').remove();
    $('.searchDisplay.sl1 > .search').children('.searchYoutube').trigger('click').remove();
}

function chkNuPic() {
    var list = [''];
    $('#update_pic_wrap').html(jqAJAX('Default.aspx/chkNuPic', list)).fadeIn(4000);
    $('#update_pic').fadeOut(4000);
}//end chkNuPic()

$('#update_pic').live('click', function () { });

function searchLightbox() {//https://www.youtube.com/watch?v=Xv1LFjApbKk
    $('#webSearch_Result').show();
    $('#lightbox').hide();

    var html = '';
    var thisURL = '';

    ///search by param
    if (getParameterByName('q') != '') { 
        if (qFlag) { $('#webSearch_TB').val(getParameterByName('q')); qFlag = false; }
    }
    ///

    if ($('#webSearch_TB').val() != '') { html = searchByTB(); }
    else { html = jqAJAX('Default.aspx/searchLightbox', [$('#webSearch_TB').val()]); }

    //html += "<div id='close_webSearch'>x</div>";
    //html = '<div class="select_how_to">2) Select a Picture to Draw!</div>' + html;

    var reportNoPic = jqAJAX('Default.aspx/cantFind', ['searchPic']);


    html += "<div class='cleaner'></div>" + reportNoPic;

    $('#webSearch_Result').html(html);
    //$('#webSearch_report_noPic').trigger('click');
    //$('#webSearch_report_info').hide();
    //$('#webSearch_report_info').trigger('click');

    //this is a lame loop, but it works
    var searchCount = 0;

    $('#webSearch_Result').children().each(function () {
        if ($(this).attr('src') != undefined) { searchCount++; }
    });

    if (searchCount == 1) {
        $('#webSearch_Result').children().each(function () { $(this).trigger('click'); });
    }

    
}//end searchLightbox()

function loadLightbox($$) {
    //alert($$.attr('id').substr(9));
    //alert($$.attr('src'));
    //$('#webSearch_Result').hidlee();
    $('#lightbox').show();
    $('.lb_List.lb1 > .lightbox').trigger('click');

    var yt_offset = '0';

    var view = '';


    var searchTitle = "";
    var searchDescription = "";
    var searchSiteName = "";
    var title = "";

    var canvasPic = "";
    var bgPic = ""; 
    /************************************************************/
    if ($$.attr('id').substr(0, 9) != 'lb_entry_') 
    {
        if ($$.hasClass('searchPic')) { view = "<img id='lightbox_pic_viewer' src='" + $$.attr('src') + "' /></div>"; } //yt_offset = ''; }
        if ($$.hasClass('searchYoutube')) {
            yt_offset = '37';
            view = '<iframe id="lightbox_pic_viewer" width="700" height="500" src="https://www.youtube.com/embed/'
                                               + $$.attr('id') + '" frameborder="0" allowfullscreen></iframe>';
            
        }

        searchTitle = jqAJAX('webCrawler/Default.aspx/crawlByTag', [lb_url, "title"]);
            searchDescription = jqAJAX('webCrawler/Default.aspx/crawlMeta', [lb_url, "og:description"]);
            //searchSiteName = jqAJAX('webCrawler/Default.aspx/crawlMeta', [lb_url, "og:site_name"]);

            if (searchTitle == '') { searchTitle = "<input id='insertTitle' placeholder='Enter A Title'></input>"; }

        title = "<div id='lb_titleWrap'>"
              + "<div id='lb_title'>" + searchTitle + "</div>"
              + "<div id='lb_desc'>" + searchDescription + "</div>"
              + "<a id='lb_url' href='" + lb_url + "' target='_blank'>" + searchSiteName + "</a>"
              + "crawled from<br />" + "<div id='lb_url_B'><a href='" + lb_url + "' target='_blank'>" + lb_url + "</a></div>"
              + "</div>";
    }
    else {
        var lightbox_entry = jqAJAX('Default.aspx/getLightboxEntry', [$$.attr('id').substr(9)]);
        //alert("þ".charCodeAt(0));//254
        //view = lightbox_entry.split('þ')[0];
        view = lightbox_entry.split(String.fromCharCode(254))[0];
        
        //view = "<img id='lightbox_pic_viewer' src='image/2_6_22_2014_11_15_57_AM.png' /></div>";
        //yt_offset = lightbox_entry.split('þ')[1];
        yt_offset = lightbox_entry.split(String.fromCharCode(254))[1];
        title = jqAJAX('Default.aspx/getCanvasInfo', [$$.attr('id').substr(9), 'title']);

        canvasPic = jqAJAX('Default.aspx/getCanvasInfo', [$$.attr('id').substr(9), 'image_src']);
        bgPic = jqAJAX('Default.aspx/getCanvasInfo', [$$.attr('id').substr(9), 'bg_src']);
        //alert(canvasPic);
        //alert($('#lightbox_pic_viewer').attr('src'));
        //$('#lightbox_pic_viewer').attr('src', jqAJAX('Default.aspx/getCanvasInfo', [$$.attr('id').substr(9), 'bg_src']));
        //alert(jqAJAX('Default.aspx/getCanvasInfo', [$$.attr('id').substr(9), 'bg_src']));
    }
    /************************************************************/
    //alert(view);

    var html = ""//title
             + "<div id='lightbox_pic'>" + view + "</div>"
             + "<div id='lightbox_screen'></div>"
             + "<div id='lightbox_tutorial'></div>"
             + "<div id='lightbox_grid'></div>"
			 + "<div id='lightbox_canvas_wrap'></div>"
    //+ "<div id='lightbox_btn'>l</div>"
             + "<div id='lightbox_tools'></div>";

    $('#lightbox').html(title + html);
    //alert(view);
    
    //image load
    $('#lightbox_pic_viewer').load(function () {
        if (yt_offset > -1) {
            var height = $$.height();
            $$.attr("height", height);

            $('#lightbox_pic').css('height', $('#lightbox_pic_viewer').height());

            //lightbox_screen css
            $('#lightbox_screen').css({
                'margin-top': '-' + ($('#lightbox_pic').height() + 2) + 'px',
                'width': $('#lightbox_pic').width() + 'px',
                'height': ($('#lightbox_pic').height() - yt_offset) + 'px'
            });

            //http://stackoverflow.com/questions/7348926/sketch-js-on-ipad
            //build canvas
            var canvasHtml = "<canvas id='lightbox_canvas' "
                  + "width='" + $('#lightbox_pic').width() + "' "
				  + "height='" + ($('#lightbox_pic').height() - yt_offset) + "'></canvas>";
            $('#lightbox_canvas_wrap').html(canvasHtml);
            $('#lightbox_canvas').css('margin-top', '-' + ($('#lightbox_pic').height() + 2) + 'px');

            //how to redraw???
            //may need to put a disclaimer indicating that this is a overlay
            $('#lightbox_canvas').css("background-image", "url('image/temppic1.png')");
            $('#lightbox_canvas').css("background-repeat", "no-repeat");

            /**********************/
            if ($$.attr('id').substr(0, 9) == 'lb_entry_') {
                //alert(bgPic); //HERE!!!
                $('#lightbox_canvas').css("background-image", "url('" + canvasPic + "')");
                if ($$.hasClass('searchPic')) { $('#lightbox_pic_viewer').attr('src', bgPic); }
            }
            /**********************/
            //how to redraw???

            $('#lightbox_canvas').sketch();

            //tutorial
            $('#lightbox_tutorial').css({
                'margin-top': '-' + ($('#lightbox_pic').height() + 2) + 'px',
                'width': $('#lightbox_pic').width() + 'px',
                'height': ($('#lightbox_pic').height() - yt_offset) + 'px'
            });
            //load tutorial
            var list = ['']; //tutorial id
            $('#lightbox_tutorial').html(jqAJAX('Default.aspx/loadTutorialDisplay', list));
            $('.td1 > .1').trigger('click');
            $('#lightbox_tutorial').hide();

            //grid
            $('#lightbox_grid').css({
                'margin-top': '-' + ($('#lightbox_pic').height() + 2) + 'px',
                'width': $('#lightbox_pic').width() + 'px',
                'height': ($('#lightbox_pic').height() - yt_offset) + 'px'
            });

            var gridBox = "";
            var gridHeight = $('#lightbox_screen').height() / 5;
            for (var i = 0; i < gridHeight; i++) { gridBox += "<div class='gridBox'></div>"; }
            $('#lightbox_grid').html(gridBox + "<div class='cleaner'></div>");
            $('.gridBox').hide();


            yt_offset = '-1';
        } //end if
    });       //end load


    //setup tools


    //colors
    var colors = "<div class='lightbox_tool_wrap'>"
	           + "<div class='lightbox_tool_icon'>C</div>"
			   + "<div class='lightbox_tool_palette'>"
			   + "tools<br />"
			   + "<div id='drawItPaletteErase'>eraser<a href='#lightbox_canvas' data-tool='eraser'></a></div>"
               + "<a id='drawItPaletteMarker' href='#lightbox_canvas' data-tool='marker'>Pen</a><br />"
			   + "<div class='cleaner'></div>"

			   + "size<br />"
			   + "<div class='drawItPaletteSize dip1'><a href='#lightbox_canvas' data-size='1'>1</a></div>"
               + "<div class='drawItPaletteSize dip2 selecteddrawItPaletteSize'><a href='#lightbox_canvas' data-size='2'>2</a></div>"
               + "<div class='drawItPaletteSize dip3'><a href='#lightbox_canvas' data-size='3'>3</a></div>"
			   + "<div class='cleaner'></div>"

	           + "color<br />"
			   + "<div class='drawItPalettePicker' style='background-color: black;'><a href='#lightbox_canvas' data-color='black'></a></div>"
               + "<div class='drawItPalettePicker white' style='background-color: white;'><a href='#lightbox_canvas' data-color='white'></a></div>"
               + "<div class='drawItPalettePicker' style='background-color: gray;'><a href='#lightbox_canvas' data-color='gray'></a></div>"
               + "<div class='drawItPalettePicker' style='background-color: red;'><a href='#lightbox_canvas' data-color='red'></a></div>"
			   + "<div class='cleaner'></div>"
			   + "</div>"
			   + "</div>";

    //http://localhost:51718/artJutsuMini/panels/show/drawIt/Default.aspx
    //
    var list = [''];
    var colors2 = jqAJAX('Default.aspx/getColor', list);
    var hue = jqAJAX('Default.aspx/getHue', list);
    var grid = jqAJAX('Default.aspx/getGrid', list);
    var bgSight = jqAJAX('Default.aspx/getBGSight', list);
    var save = jqAJAX('Default.aspx/getSaveBtn', list);
    var tutorial = jqAJAX('Default.aspx/getPalette', list);

    //alert(color2);
    //
    var lightboxTools = colors2 + tutorial + hue + grid + bgSight + save + "<div class='cleaner'></div>";
    $('#lightbox_tools').html(lightboxTools);
    $('#save_prompt').hide();
    //alert($('.dip3').html());//$('.dip3').trigger('click'); //pen thickness

    $('.lightbox_tool_palette').hide();
    //alert($('#lightbox_pic_viewer').attr('src'));
    //alert('draw!');

    if (getParameterByName('pic') != '') {
        $('#bgSight').trigger('click');
        $('#hue_left').trigger('click'); $('#hue_left').trigger('click'); $('#hue_left').trigger('click');
    }
}//end loadLightbox()

function searchByTB()
{
    var html = '';

    //youtube check (will modify later to include entire url check)
    var matches = $('#webSearch_TB').val().match(/watch\?v=([a-zA-Z0-9\-_]+)/);
    if (matches) {
        var src = $('#webSearch_TB').val().split('?')[1].substr(2);
        html = "<img id='" + src + "' class='searchYoutube' src='" + "http://img.youtube.com/vi/" + src + "/3.jpg' />";
    }
    else if (inArray($('#webSearch_TB').val(), ['.gif', '.png', '.jpg', '.bmp'], 4)) {
        html = "<img class='searchPic' src='" + $('#webSearch_TB').val() + "' />";
    }
    else {
        var list = [$('#webSearch_TB').val(), 'searchPic'];
        html = jqAJAX('webCrawler/Default.aspx/crawlImage', list);
    }

    //lb_url = jqAJAX('webCrawler/Default.aspx/crawlMeta', [$('#webSearch_TB').val(), "og:url"]);

    return html; //alert(html);
}//end searchByTB()