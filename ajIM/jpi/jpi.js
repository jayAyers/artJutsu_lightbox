/**************************************
       JPI DOCUMENTATION
 --------------------------------------
 Author: Jay Ayers
 Web   : http://www.artJutsu.com/jpi
 Date  : 12/23/2013
 --------------------------------------

 --------------------------------------
     GETTING STARTED HTML
 --------------------------------------
  <script src="jpi/jpi.js"></script>    
  <script>jpi.startJPI('');</script> 
 **************************************/


/******************************************/
  function JPI() { }; var jpi = new JPI();
/******************************************/

JPI.prototype.startJPI = function (path) {
    //load jpi components
    document.write('<script src="' + path + 'jpi/jquery-1.4.4.min.js"></script>');
    document.write('<script src="' + path + 'jpi/DC/json2.js"></script>');
    document.write('<script src="' + path + 'jpi/panelSwap.js"></script>');
    document.write('<script src="' + path + 'jpi/jqAJAX.js"></script>');
    document.write('<script src="' + path + 'jpi/goodies.js"></script>');

    //load style
    document.write('<style>'
             + '.cleaner{clear: left; line-height: 0; height: 0;}'
             + 'body{-webkit-touch-callout: none; -webkit-user-select: none;'
             + '-khtml-user-select: none; -moz-user-select: none;'
             + '-ms-user-select: none; user-select: none;}'
             + '</style>');
};

JPI.prototype.loadSource = function (source) {
    //loads js and css cnnected to a specific module
        $('#source').append("<link rel='StyleSheet' href='" + source + "StyleSheet.css' />");
        $('#source').append("<script src='" + source + "JScript.js'></script>");
}
