function isValidEmailAddress(emailAddress) {

    var pattern = new RegExp(/^(("[\w-\s]+")|([\w-]+(?:\.[\w-]+)*)|("[\w-\s]+")([\w-]+(?:\.[\w-]+)*))(@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$)|(@\[?((25[0-5]\.|2[0-4][0-9]\.|1[0-9]{2}\.|[0-9]{1,2}\.))((25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\.){2}(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\]?$)/i);
    return pattern.test(emailAddress);
};

function isNullOrEmpty(value) {
    if (value == null || value == "" || value == undefined) 
        return false;
    else
        return true;
}

function setNullIdInvalid(value) {
    if (value == null || value == "" || value == undefined || value == "undefined")
        return null;
    else
        return value;
}

function resetprocess(){
    $("#isErrorMsg").hide();
    $("#isSuccessMsg").hide();
    $("#content-page").show();
    $("#loading").hide();
}

function Loadingprocess() {
    $scope.process = {};
    $scope.process.isloading = true;
    $("#content-page").hide();
    $("#loading").show();
}

function AddProcessErrorValue (isErrorMsg, Msg, isloading) {
    if (isErrorMsg == true)
        $("#isErrorMsg").show();
    else
        $("#isErrorMsg").hide();

    $("#ErrorMsg").text(Msg);

    if (isloading == true)
        $("#loading").show();
    else
        $("#loading").hide();

    $("html, body").animate({ scrollTop: 0 }, "slow");
    $("#content-page").show();
    //$("#loading").hide();
}

function AddInquiryErrorValue (isErrorMsg, Msg, isloading) {
    if (isErrorMsg == true)
        $("#isErrorMsg").show();
    else
        $("#isErrorMsg").hide();

    $("#ErrorMsg").text(Msg);

    if (isloading == true)
        $("#loading").show();
    else
        $("#loading").hide();
	var heightValue =$("#isErrorMsg").offset().top - 100;
$("html, body").animate({ scrollTop: heightValue}, "slow");
    $("#content-page").show();
    
}
function AddInquirySuccessValue(isSuccessMsg, Msg, isloading) {

    if (isSuccessMsg == true)
        $("#isSuccessMsg").show();
    else
        $("#isSuccessMsg").hide();

    $("#SuccessMsg").text(Msg);

    if (isloading == true)
        $("#loading").show();
    else
        $("#loading").hide();
	var heightValue =$("#isErrorMsg").offset().top - 100;
$("html, body").animate({ scrollTop: heightValue}, "slow");
        $("#content-page").show();
}

function AddProcessSuccessValue(isSuccessMsg, Msg, isloading) {

    if (isSuccessMsg == true)
        $("#isSuccessMsg").show();
    else
        $("#isSuccessMsg").hide();

    $("#SuccessMsg").text(Msg);

    if (isloading == true)
        $("#loading").show();
    else
        $("#loading").hide();

    $("html, body").animate({ scrollTop: 0 }, "slow");
    $("#content-page").show();
}

function LoadPremissionPageSettings() {
    $("#List").show();
    $("#NonList").hide();
    $("#content-page").show();
    $("#loading").hide();
}

function LoadPremissionPageSettings() {
    $("#List").show();
    $("#NonList").hide();
    $("#content-page").show();
    $("#loading").hide();
}

function StartLoading() {
    $("#content-page").hide();
    $("#loading").show();

}

function StopLoading() {
    $("#content-page").show();
    $("#loading").hide();
}

function ShowList() {
    $("#List").show();
    $("#NonList").hide();

}

function HideList() {
    $("#List").hide();
    $("#NonList").show();
}

function LogOut() {
    var url = "/Common/Logout/";
    $.get(url).success(function (data, status, headers, config) {
        debugger;
        window.location.href = window.location.origin;
    }).error(function (data) {
        AddProcessErrorValue(true, 'Error has been occured , please contact administrator.!', false);
        window.location.href = window.location.origin;
    });
}

function ClearCookies() {
    var url = "/Common/Logout/";
    $.get(url).success(function (data, status, headers, config) {
    }).error(function (data) {
    });
}


