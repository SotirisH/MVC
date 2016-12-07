/// <reference path="../jquery-1.12.1.js" />

$(function () {
    $.get("/SmsGateWay/GetAvailableCredits",
        null,
        function (data) {
            $("#spanCredits").text(data);
            }
        );
});