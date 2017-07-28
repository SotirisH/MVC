

$(function () {
    $.get("/SmsGateWay/GetAvailableCredits",
        null,
        function (data) {
            $("#spanCredits").text(data);
            $()
            }
        );
});