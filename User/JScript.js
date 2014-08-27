//globals

//clicks
$('#login_submit').live('click', function () {
    var list = [$('#login_username').val(), $('#login_password').val()];
    if (jqAJAX('User/Login.aspx/tryLogin', list) == "false") 
    { $('#login_error').html('* cannot login<br />* username may not exist<br />* password may not match username'); }
    else { login_logout('login'); }
});

$('#register_submit').live('click', function () {
    var no_error = true;
    var thisError = '';

    $('#register_username_error').html('');
    $('#register_password_error').html('');

    //check name exists
    if (jqAJAX('User/Login.aspx/chkUserExists', [$('#register_username').val()]) == "true")
    { thisError += '* username already exists<br />'; no_error = false; }

    //check passwords matchup
    if ($('#register_password').val() != $('#register_password_confirm').val())
    { thisError += '* passwords do not match<br />'; no_error = false; }
    else {
        if ($('#register_password').val().length < 10) { thisError += '* password must be 10+ characters long<br />'; no_error = false; }
    }

    if (no_error) {
        var list = [$('#register_username').val(), $('#register_password').val()];
        jqAJAX('User/Login.aspx/registerUser', list);
        login_logout('login');
    }
    else { $('#register_error').html(thisError); }

});

//functions
function login_logout(type) {
    $('#lb_load').show();

    setTimeout(function () {
        if (type == 'logout') { jqAJAX('User/Login.aspx/logout', ['']); }
        startProfile('.lb_Display.lb1 > .profile', 'panels/profile/');
        $('#lb_load').fadeOut();
    }, 1000);

}

function user_startLogin() {
    return jqAJAX('User/Login.aspx/getLoginWindow', ['']);
}

function user_Logout() { login_logout('logout'); }

//keydown
    $(document).keydown(function (e) {
        if (e.which == 13) {
            if ($('#login_password').is(":focus")) { $('#login_submit').trigger('click'); }
        } //end if		
    });	