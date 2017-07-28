/// <reference path="../jquery-1.12.1.js" />
/// <reference path="../underscore.js" />
/// <reference path="~/Views/SmsGateWay/Change.cshtml" />

function HideAllAlerts()
{
    $(divSuccess).addClass("collapse");
    $(divError).addClass("collapse");
}

$(".pill").click(function(){
    HideAllAlerts();
});


function postToController(element, postUrl, captionHeaderMsg)
{
    //How can I get form data with JavaScript/jQuery?
    //http://stackoverflow.com/questions/2276463/how-can-i-get-form-data-with-javascript-jquery

    //event.target always refers to the element that triggered the event
    var currentform = element.form;
    var formdata = $(currentform).serializeArray();
    //"smsGateWayProxy.UserName"
    //"smsGateWayProxy.Pasword"
    //"Index"
    //divSuccess
    // create Json object for the controler
    HideAllAlerts();
    var t = _.findWhere(formdata, { name: "proxyname" });
    var controllerdata =
        {
            proxyName: _.findWhere(formdata, {name:"proxyname"}).value,
            userName: _.findWhere(formdata, { name: "smsGateWayProxy.UserName" }).value,
            password: _.findWhere(formdata, { name: "smsGateWayProxy.Pasword" }).value
        }
    
    
    $.post(postUrl, controllerdata)
    .done(function (data) {
        if (data.Error==true)
        {
            $(divError).removeClass("collapse");
            $(spanErrorMesage).text(data.Message);
        }
        else {

            $(divSuccess).removeClass("collapse");
            $(spanCaption).text(captionHeaderMsg);
            $(spanAvailableCredits).text(data.Message);
        }
    })
    .fail(
        function(xhr, status, error)
        {
            $(divError).removeClass("collapse");
            $(spanErrorMesage).text(error);
        }
    );
    
    return false;

}


$(".buttonSave").click(function () {
    return postToController(this, "/SmsGateWay/Save", "Save Success!");
});

$(".buttonTest").click(function () {
    return postToController(this, "/SmsGateWay/TestProxy", "Test Success!");
});
