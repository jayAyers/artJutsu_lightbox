//color
$('#drawItPaletteErase').live('click', function () {
            $(this).css({ boxShadow: '0px 0px 0px #A52A2A' });
            $(this).children().trigger('click');
        });

$('.drawItPalettePicker').live('click', function(){
   $('#drawItPaletteErase').css({ boxShadow: '3px 3px 3px #A52A2A' });

   $('#drawItPaletteMarker').trigger('click');
   $(this).children().trigger('click');
   
   changeBGColor($(this).css('background-color'));
});

$('.drawItPaletteSize').live('click', function () {
    $('.selecteddrawItPaletteSize').removeClass('selecteddrawItPaletteSize');
    $(this).addClass('selecteddrawItPaletteSize');

    $('#drawItPaletteColor').html($(this).attr('class').split(' ')[1].substr(3) + 'px');

    $(this).children().trigger('click');
});

$('#drawItPaletteClear').live('click', function () { $('#lightbox_pic_viewer').attr('src', $('#lightbox_pic_viewer').attr('src')); });

function changeBGColor(color)
{
  $('#drawItPaletteColor').css('background-color', color);
  
  if(color == 'rgb(255, 255, 0)' || color == 'rgb(255, 255, 255)'){$('#drawItPaletteColor').css('color', '#708090');}
  else{$('#drawItPaletteColor').css('color', 'white');}
} //end changeBGColor

//pen
$('.penOpacity').live('click', function () {
    //#drawItPaletteColor
    //#lightbox_canvas
    var thisOpacity = $('#drawItPaletteColor').css('opacity') * 10;

    if ($(this).attr('id') == 'penOpacity_Left') { thisOpacity--; }
    if ($(this).attr('id') == 'penOpacity_Right') { thisOpacity++; }

    if (thisOpacity > 10) { thisOpacity = 10; }
    if (thisOpacity < 0) { thisOpacity = 1; }

    thisOpacity /= 10;
    
    $('#drawItPaletteColor').css('opacity', thisOpacity);
    $('#lightbox_canvas').css('opacity', thisOpacity);
});

//hue
$('.hue_selector').live('click', function () {
    var hueNum = $('#hue_info').html().split('%')[0]/100; //parseFloat($('#hue_info').html());
    
    if ($(this).attr('id') == 'hue_left') { hueNum = hueNum - .1; } // Math.round(hueNum - .1); }
    if ($(this).attr('id') == 'hue_right') { hueNum = hueNum + .1; } // Math.round(hueNum + .1); }

    hueNum = Math.round(hueNum * 100) / 100;

    if (hueNum > -0.1 && hueNum < 1.1) {
        $('#lightbox_screen').css('opacity', hueNum);
        $('#hue_info').html(hueNum * 100 + '%');
    }
});

$('.hue_color_selector').live('click', function () {
    $('.selected_hue_color_selector').removeClass('selected_hue_color_selector');
    $(this).addClass('selected_hue_color_selector');

    $('#lightbox_screen ').css('background-color', $(this).css('background-color'));
});


//grid
$('#grid_eye').live('click', function () {
    $('.gridBox').toggle();
    //if ($(this).html() == 'show') { $('#lightbox_grid').show(); $(this).html('hide'); }
    //else if ($(this).html() == 'hide') { $('#lightbox_grid').hide(); $(this).html('show'); }
    /*
    var gridBox = "";
    var gridHeight = $('#lightbox_screen').height() / 5;
    for (var i = 0; i < gridHeight; i++) { gridBox += "<div class='gridBox'></div>"; }
    $('#lightbox_grid').html(gridBox + "<div class='cleaner'></div>");
    */
    //for now
    //later do a conversion
});

//background sight
$('#bgSight').live('click', function () {

    if ($(this).attr('src') == 'image/icons/eyeOPEN.png') { $(this).attr('src', 'image/icons/eyeCLOSE.png'); }
    else { $(this).attr('src', 'image/icons/eyeOPEN.png'); }

    $('#lightbox_pic_viewer').toggle();
});


//tutorial
$('#tutorial_eye').live('click', function () {
    if ($(this).attr('src') == 'image/icons/eyeOPEN.png') { $(this).attr('src', 'image/icons/eyeCLOSE.png'); }
    else { $(this).attr('src', 'image/icons/eyeOPEN.png'); }

    $('#lightbox_tutorial').fadeToggle();
});

