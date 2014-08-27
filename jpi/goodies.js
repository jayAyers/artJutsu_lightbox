//$(document).ready(function () {

/*****************************
          GOODIES!!!  
I'm going to start putting all
the good stuff I find here.
Enjoy ~Jay
*****************************/


//function showMe()
//{
//alert('hi');
//}

//get request.querystring
//var acc = getParameterByName('acc');

function inArray(value, array, val_substr) 
{//returns true or false if value is inside array
    var is_inArray = false;

    value = value.substr(value.length - val_substr);

    for (var i = 0; i < array.length; i++) { if (value == array[i]) { is_inArray = true; } }

    return is_inArray;
}//end inArray()

function getParameterByName(name) //Jay
{//http://stackoverflow.com/questions/1018855/finding-elements-with-text-using-jquery
     name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
     var regexS = "[\\?&]" + name + "=([^&#]*)";
     var regex = new RegExp(regexS);
     var results = regex.exec(window.location.search);
   
     if(results == null)
      return "";
     else
      return decodeURIComponent(results[1].replace(/\+/g, " "));
 } //end function*/

 function ytVidId(url) {//http://stackoverflow.com/questions/2964678/jquery-youtube-url-validation-with-regex
     var p = /^(?:https?:\/\/)?(?:www\.)?(?:youtu\.be\/|youtube\.com\/(?:embed\/|v\/|watch\?v=|watch\?.+&v=))((\w|-){11})(?:\S+)?$/;
     return (url.match(p)) ? RegExp.$1 : false;
 }

 function validURL(url) {//http://www.roseindia.net/answers/viewqa/JavaScriptQuestions/25837-javascript-regex-validate-url.html
     var pattern = /(ftp|http|https):\/\/(\w+:{0,1}\w*@)?(\S+)(:[0-9]+)?(\/|\/([\w#!:.?+=&%@!\-\/]))?/;
     if (pattern.test(url)) { return true; }
     
     return false;
 } //end validURL()

 function swapSourcePic($$, parentClass) {
     /**************************************************************
     This function assumes that the targeted image has a parent with an id that is an image
     example : <div id="http://www.artjutsu.com/smile.png"><img class='swapMe' src='http://www.artjutsu.com/yuk.png' /></div>

     use the following to start this function
     var $$ = $(this); swapSourcePic($$, 'portfolio_pic_wrap');
     **************************************************************/
     var p_src = $$.parent().attr('id');
     var src = $$.attr('src');
     if ($$.parent().hasClass(parentClass)) {
         $$.attr('src', p_src);
         $$.parent().attr('id', src);
     }
 } //end swapSourcePic()

/*
			 http://stackoverflow.com/questions/1018855/finding-elements-with-text-using-jquery
			 jQuery.expr[':'].hasText = function(element, index) {
     // if there is only one child, and it is a text node
     if (element.childNodes.length == 1 && element.firstChild.nodeType == 3) {
        return jQuery.trim(element.innerHTML).length > 0;
     }
     return false;
};
var thisText = $('#someDiv :H + E').get();
			 
			 alert(thisText);
			 */
			 
			 /*
			 var getIDs = '';
			 
			 $("span[id^='dg_summary__']").each(function(){			 
			   if($(this).attr('id').substr($(this).attr('id').length - 7) == 'lblQnty'){ 			    
			    getIDs += '#' + $(this).attr('id') + " ";
				
				alert($(this).html());
				
			   }//end if			   
			 });//end span
			 
			 */

//alert('goodies');
//});//end ready