$(document).ready(function () {
    /* 2011
    FILES NEEDED FOR psLoad/psRemove
    ---------------------------------
    JQuery Library
    jqAJAX.js
    mpSwitchBoard.js

    TEST CASE
    -------------
    <!--panelSwap-->
    <div class='psList lst1'>
    <div class='panelSwap psListItem A'>click A</div>
    <div class='panelSwap psListItem B'>click B</div>
    <div class='panelSwap psListItem C'>click C</div>
    </div><!--end psList-->
    
    <div class='psDisplay lst1'>
    <div class='A'>A</div>
    <div class='B psLoad'>B</div>
    <div class='C psLoad psLoadRemove'>C</div>
    </div><!--end psDisplay-->   
    */

    $('.panelSwap').live("click", function () {
        var thisParent = $(this).parent().attr('class').split(' ');
        var thisClass = $(this).attr('class').split(' ');

        var thisDisplay = '.' + thisParent[0].substr(0, thisParent[0].length - 4) + 'Display';
        var childDisplay = '.' + thisClass[2] + 'Display';

        var selectedKey = 'selected' + thisParent[0] + 'Item';
        var childKey = 'selected' + thisClass[2] + 'Display';

        changeSelectedIcon(thisParent[0], thisParent[1], selectedKey);

        $(this).addClass(selectedKey);

        hideChildNonSelectedDisplay(childDisplay, childKey);

        changeSelectedDisplay(thisDisplay, thisParent[1], thisClass[2]);
    });

    $('.panelSwap_focus').live("click", function () {
        var thisParent = $(this).parent().attr('class').split(' ');
        var thisClass = $(this).attr('class').split(' ');

        var selectedKey = 'selected' + thisParent[0] + 'Item';

        changeSelectedIcon(thisParent[0], thisParent[1], selectedKey);
        $(this).addClass(selectedKey);

        $(panelSwapFocus[1]).hide();

        panelSwapFocus[1] = '.' + thisClass[2].substr(10);

        $(panelSwapFocus[1]).show();
    });
}); //end ready

$('.panelSwapNext, .panelSwapFirst').live('click', function () {
    var thisParentParent = '.' + $(this).parent().parent().attr('class').split(' ')[1];
    var thisParentNum = '.' + (parseInt($(this).parent().attr('class').split(' ')[0]) + 1);

    if ($(this).hasClass('panelSwapFirst')) { thisParentNum = '.1'; }
    $(thisParentParent + ' > ' + thisParentNum).trigger('click');


});

    /*--------------------
    functions
    ====================*/
function hideChildNonSelectedDisplay(Display, selectedDisplay) {
    $(document).ready(function () {
        $(Display).children().each(function () {
            if ($(this).hasClass(selectedDisplay)) {
                $(this).show();
            } else { $(this).hide(); }
        });
    }); //end ready
} //end function

function changeSelectedIcon(parentClass, filterClass, selectedKey) {
    $(document).ready(function () {
        parentClass = '.' + parentClass;
        filterClass = '.' + filterClass;

        $(parentClass).filter(filterClass).children().each(function () {
            if ($(this).hasClass(selectedKey)) {
                $(this).removeClass(selectedKey);
            }
        });
    }); //end ready
} //end function

    function changeSelectedDisplay(display, filterClass, myClass) {
        $(document).ready(function () {
            if (filterClass.length > 0) {
                filterClass = '.' + filterClass;

                $(display).filter(filterClass).children().each(function () {
                    if ($(this).hasClass(myClass)) {
                        $(this).show(); $(this).addClass('selected' + display.substr(1));
                        /*******************
                        PSLOAD
                        *******************/
                        if ($(this).hasClass('psLoad') || $(this).hasClass('psLoadRemove')) {//jqAJAX loading Panel
                            psGetSet($(this));

                            if ($(this).hasClass('psLoadRemove')) {//this panel no longer loads new panel
                                $(this).removeClass('psLoad'); $(this).removeClass('psLoadRemove');
                            } //end if
                        } //end if
                        /*******************
                        end PSLOAD
                        *******************/
                    } else { $(this).hide(); $(this).removeClass('selected' + display.substr(1)); }
                });



            }

            else {
                $(display).children().each(function () {
                    if ($(this).hasClass(myClass)) {
                        $(this).show(); $(this).addClass('selected' + display.substr(1));
                    } else { $(this).hide(); $(this).removeClass('selected' + display.substr(1)); }
                });
            }
        });         //end ready
    } //end changeSelectedDisplay()

