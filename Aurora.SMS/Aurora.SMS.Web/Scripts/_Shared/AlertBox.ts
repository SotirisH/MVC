/// <reference path="../typings/jquery/jquery.d.ts" />
/// <reference path="../typings/bootstrap/index.d.ts" />
/// <reference path="global.ts" />
/// <reference path="global-variables.ts" />




enum AlertType {
    Info = 1,
    Success,
    Warning,
    Error
}
/**
 * Options for the alertBox
 */
class AlertOptions {
    static DivAlert: JQuery = $("#customAlertBox");;
    /** Alert type */
    AlertType: AlertType;
    /** The header message. If the AlertType=Error and no string is defined then has value of 'The following errors have occured:<hr/>'  */
    HeaderMessage?: string;
    MessageDetails?: string;
    /** Defines if the global $ProgressModalWindow will be closed before the message appears. Default=True */
    CloseGlobalModal?: boolean = true;
    /** Defines the element that will host the Alert. By default is modal */
    HostElement?: Element = undefined;
    /** If it is different that zero the window will fade out in FadeOutTimeSpan ms */
    FadeOutTimeSpan?: number;
    /** If true the window can be closed. Default=True */
    ShowCloseButton?: boolean = true;
    /** If IsExpandable then the window can be collapsed. Default=True */
    IsExpandable?: boolean = true;
}

function T(a: AlertOptions)
{ }

/**
 * Initializes a alert div by removing the classes
 * @param divname
 */
function InitAlertDiv(divAlert: JQuery = $("#customAlertBox")): void {
    divAlert.hide();
    $("#divcollapseError").collapse("show");
    divAlert.removeClass("alert-danger");
    divAlert.removeClass("alert-success");
    divAlert.removeClass("alert-warning");
    divAlert.removeClass("alert-info");
    $("#iconInfo").removeClass("fa-exclamation-circle")
    $("#iconInfo").removeClass("fa-info-circle")
    $("#iconInfo").removeClass("fa-exclamation-triangle");
    $("#iconInfo").removeClass("fa-warning");
    $ProgressModalWindow.modal("hide");
}

/**
 * Displays a message into the specificDiv
 * @alertOptions The options of the message
 */
function DisplayMessage(alertOptions: AlertOptions): void {
    InitAlertDiv();
    if (alertOptions.AlertType == AlertType.Error && alertOptions.HeaderMessage == undefined) {
        alertOptions.HeaderMessage = "The following errors have occured:<hr/>";
    }

    if (alertOptions.CloseGlobalModal == undefined || alertOptions.CloseGlobalModal) {
        $ProgressModalWindow.modal("hide");
    }

    $("#iconInfo").removeClass("fa-exclamation-circle")
    $("#iconInfo").removeClass("fa-info-circle")
    $("#iconInfo").removeClass("fa-exclamation-triangle");
    $("#iconInfo").removeClass("fa-warning");

    AlertOptions.DivAlert.removeClass("alert-success");
    AlertOptions.DivAlert.removeClass("alert-danger");
    AlertOptions.DivAlert.removeClass("alert-warning");
    AlertOptions.DivAlert.removeClass("alert-info");

    // find the label
    var label = AlertOptions.DivAlert.find("#labelMsg");

    if (alertOptions.MessageDetails == undefined || alertOptions.MessageDetails.length == 0) {
        label.html('');
    }
    else {
        label.html(alertOptions.MessageDetails);
    }

    $("#msgHeader").html(alertOptions.HeaderMessage);

    switch (alertOptions.AlertType) {
        case AlertType.Success:
            $("#iconInfo").addClass("fa-info-circle");
            AlertOptions.DivAlert.addClass("alert-success");
            break;
        case AlertType.Error:
            $("#iconInfo").addClass("fa-exclamation-circle");
            AlertOptions.DivAlert.addClass("alert-danger");
            break;
        case AlertType.Warning:
            $("#iconInfo").addClass("fa-exclamation-triangle");
            AlertOptions.DivAlert.addClass("alert-warning");
            break;
        case AlertType.Info:
            $("#iconInfo").addClass("fa-info-circle");
            AlertOptions.DivAlert.addClass("alert-info");
            break;
        default:
            break;
    }

    if (alertOptions.FadeOutTimeSpan == undefined || alertOptions.FadeOutTimeSpan == 0) {
        AlertOptions.DivAlert.fadeIn("fast");
    }
    else {
        AlertOptions.DivAlert.fadeIn("slow").fadeOut(alertOptions.FadeOutTimeSpan);
    }
}

