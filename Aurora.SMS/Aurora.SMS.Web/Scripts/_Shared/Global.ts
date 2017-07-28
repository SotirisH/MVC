/// <reference path="../typings/jquery/jquery.d.ts" />
/// <reference path="../typings/bootstrap/index.d.ts" />
/// <reference path="../typings/moment/moment.d.ts" />
/// <reference path="global-variables.ts" />

interface FormData {
    name: string;
    value: string;
}

interface JsonResult {
    Success: boolean;

}

interface GenericJsonResult extends JsonResult {
    Data: any;
}

$(function (): void {
    $ProgressModalWindow = $("#divProgressModalWindow")

    BindDatetimepickerHandlers()
    //specify static for a backdrop which doesn't close the modal on click.
    // The modal should be initialized only once!
    $ProgressModalWindow.modal({
        backdrop: "static",
        keyboard: false,
        show: false
    })
})

function BindDatetimepickerHandlers(): void {
    //http://eonasdan.github.io/bootstrap-datetimepicker/
    //custom icons
    $(".date").datetimepicker(
        {
            format: "DD/MM/YYYY",
            icons: {
                time: "fa fa-clock-o",
                date: "fa fa-calendar",
                up: "fa fa-arrow-up",
                down: "fa fa-arrow-down"
            }
        })
}


function RenderDate(jsonDate) {
    var dateM = moment(jsonDate);
    var f = dateM.format("DD/MM/YYYY");
    //var date = new Date(parseInt(jsonDate.substr(6)));
    //var month = date.getMonth() + 1;
    //return date.getDate() + "/" + (month.length > 1 ? month : "0" + month) + "/" + date.getFullYear();
    return f;
}


function ConvertSecondsToString(elapsedTimeinSeconds: number): string {
    //https://stackoverflow.com/questions/1210701/compute-elapsed-time
    // Calculate the number of days left
    var days = Math.floor(elapsedTimeinSeconds / 86400);
    // After deducting the days calculate the number of hours left
    var hours = Math.floor(elapsedTimeinSeconds / 3600);
    // After days and hours , how many minutes are left
    var minutes = Math.floor((elapsedTimeinSeconds - (hours * 3600)) / 60);
    // Finally how many seconds left after removing days, hours and minutes.
    var secs = Math.floor((elapsedTimeinSeconds - (hours * 3600) - (minutes * 60)));
    return hours + "h:" + String(minutes) + "m:" + String(secs) + "s";
    //return _.string.pad(String(hours), 2, "0") + "h:" + _.string.pad(String(minutes), 2, "0") + "m:" + _.string.pad(String(secs), 2, "0") + "s";

}



/** Serializes form data into an oject
 * @param {JQuerySerializeArrayElement[]} formArray The form data that have been serialized using $form.serializeArray()
 * @returns {object} Returns an object with dynamic properties
 */
function objectifyForm(formArray: JQuerySerializeArrayElement[]): any {
    var returnArray: Object = {}


    for (var i = 0; i < formArray.length; i++) {

        returnArray[formArray[i].name] = formArray[i].value


    }
    return returnArray;

}



window.onerror = function (message: string, filename?: string, lineno?: number, colno?: number, error?: Error) {
    var errorMessage = message
    errorMessage += "<hr/>"
    errorMessage += "<br/>" + "filename:" + filename;
    errorMessage += "<br/>" + "line:" + lineno;
    errorMessage += "<br/>" + "column:" + colno;

    DisplayMessage({ AlertType: AlertType.Error, MessageDetails: errorMessage });
};
