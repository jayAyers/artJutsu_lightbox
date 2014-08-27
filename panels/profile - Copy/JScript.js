var profilePath = '';
var profileUID = '';

$(document).ready(function () { startProfile('#wrap', ''); });

function startProfile(wrap, path) {
    //globals
    profilePath = path;

    //load
    $(wrap).append('<link rel="Stylesheet" href="' + profilePath + 'StyleSheet.css" />');
    
    //////////////////////////
    var list = [profileUID];
    $(wrap).append(jqAJAX(profilePath + 'Default.aspx/startProfile', list));
}

