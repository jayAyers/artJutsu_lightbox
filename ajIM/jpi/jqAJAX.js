jqAJAX_Load();

    function jqAJAX(url, list) {
    jqAJAX_DebugAdd(url);
	//jqAJAX_Load('pre');

        var HTML = "";
        var jsonText = JSON.stringify({ list: list });

        $.ajax({
            type: "POST",
            url: url,
            data: jsonText,
            dataType: "json",
            async: false,
            contentType: "application/json; charset=utf-8",
            success: function (msg) { HTML = msg.d; } // jqAJAX_Load('post'); }
        }); //end ajax

        return HTML;
    } //end jqAJAX
	
	function jqAJAX_Load(loader)
	{
	   if(loader == undefined)
	   {
	    var div = "<div id='jqLoad'>"
	           + "<div id='jqLoad_Pre'></div>"
			   + "<div id='jqLoad_Post'></div>"
			   + "<div class='cleaner'></div>"
			   + "</div>";
	    var style = "<style>"
	             + "#jqLoad{margin-left: -10px; top: 0; position: fixed; width: 100%; height: 2px; border: 1px solid transparent;}"
				 + "#jqLoad_Pre{height: 2px; background-color: #E9967A; float: left;}"
				 + "#jqLoad_Post{height: 2px; background-color: #528B8B; float: left;}"
				 + "</style>";
	   }
	   if(loader == 'pre'){ 
	     $(document).ready(function(){
		     $('#jqLoad').show();
	         $('#jqLoad_Pre').animate({width: "50%"}, 1000);
		 });
	   }
	   if(loader == 'post'){ 
	     $(document).ready(function(){
	         $('#jqLoad_Post').animate({width: "50%"}, 1000); 	
             $('#jqLoad').fadeOut(1500);
		 });
	   }
	   
	   $(document).ready(function () { $('head').append(style); $('body').append(div); });	   
	}//end jqAJAX_load()

    function jqAJAX_Debug() {
        var div = "<div id='jqSource'>"
                +  "<div id='jqSource_head'>jqAJAX calls</div>"
                +  "<div id='jqSource_content'></div>"
                + "</div>";

        var style = "<style>"
                  + " #jqSource{width: 225px; height: 200px; border: 2px solid #8B2323; position: fixed; top: 0; background-color: white;"
                  +  "          }"
                  +  "#jqSource_head{padding-top: 3px; height: 25px; border-bottom: 1px solid #8B2323; background-color: #FFF8DC;"
                  +  "color: #8B2323; font-weight: bolder; text-align: center;}"
                  + "#jqSource_content{height: 170px; overflow: scroll;}"

                  + ".jqs_contentItem{margin: 6px 0 0 12px; width: 195px; border-bottom: 1px solid #708090;"
                  + " color: #8B2323; font-size: 14px; font-weight: bolder;}"

                  + ".selected_jqs_contentItem{background-color: #FFF68F;}"

                  +  "::-webkit-scrollbar { width: 8px; }"
                  +  "::-webkit-scrollbar-track { -webkit-box-shadow: inset 0 0 6px rgba(0,0,0,0.3); background-color: #F0F8FF; }"
                  +  "::-webkit-scrollbar-thumb { -webkit-box-shadow: inset 0 0 6px rgba(0,0,0,0.5); background-color: #EE3B3B; }"
                  + "</style>";

        $(document).ready(function () { $('head').append(style); $('body').append(div); });
    } //end jqAJAX_Source()

    function jqAJAX_DebugAdd(url) {
        $('.selected_jqs_contentItem').removeClass('selected_jqs_contentItem');

        $('#jqSource_content').append("<div class='jqs_contentItem selected_jqs_contentItem'>" + url + "</div>");
        //$('#jqSource_content').animate({ scrollTop: $("#jqSource_content")[0].scrollHeight }, 1000);
    }//end showURL()

	$(document).ready(function(){
	  //capture DIV clicks//////////////
	  $('div').live('click', function(e)
	  {
	     var thisID      = $(this).attr('id');
		 var thisCLASS_S = $(this).attr('class').split(' ');
		 var thisClass   = '';		 
		 
		 for(var i = 0; i < thisCLASS_S.length; i++) { thisClass += '.' + thisCLASS_S[i] + ', '; }
         //if(thisClass.substr(0, thisClass.length - 2) == '.'){ thisClass = ' '; }		 
		 
		 var elem = "<div class='jqs_contentItem selected_jqs_contentItem'>";
		 
		    if(thisID != ''){ elem += "#" + thisID }
			if(thisID != '' && thisClass != ''){ elem += ", "; }
			elem += thisClass.substr(0, thisClass.length - 2);		 
		    elem += "</div>";
			
            e.stopPropagation();
	     if(thisID != '' && thisClass != ''){ $('#jqSource_content').append(elem); }
	  });//end div click	
	});//end ready
/////////////////////////////////////////////