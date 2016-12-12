/// <reference path="../jquery-1.12.1.js" />
/// <reference path="~/SMSTemplate/CreateEdit.cshtml" />
 



$(function ()
{
    //Set the innerHtml of the divTemplateText by reading the content of the hidden textareaTemplateText
    var to = $("#textareaTemplateText").val();
    $("#divTemplateText").html($("#textareaTemplateText").val())
    // Select all elements with a 'data-templateField' attribute
    var elements = document.querySelectorAll('[data-templatefield]');
    $(elements).click(function ()
    {
        // get the value
        var templateFieldName = this.dataset.templatefield;
        var codeElement = "<div class='alert alert-dismissible alert-success' contenteditable='false' style='display:inline-block'>" +
                            "<button type='button' class='close' data-dismiss='alert'>&times;</button>" +
                            "<span>" + templateFieldName  + "</span>" +
                            "</div>"
        pasteHtmlAtCaret(codeElement);
    });
});

$("#formTemplate").submit(function () {
    var t = $("#divTemplateText").html();
    $("#textareaTemplateText").text($("#divTemplateText").html());
});


// http://stackoverflow.com/questions/6690752/insert-html-at-caret-in-a-contenteditable-div
function pasteHtmlAtCaret(html) {
    var sel, range;
    if (window.getSelection) {
        // IE9 and non-IE
        sel = window.getSelection();
        if (sel.getRangeAt && sel.rangeCount) {
            range = sel.getRangeAt(0);
            range.deleteContents();

            // Range.createContextualFragment() would be useful here but is
            // only relatively recently standardized and is not supported in
            // some browsers (IE9, for one)
            var el = document.createElement("div");
            el.innerHTML = html;
            var frag = document.createDocumentFragment(), node, lastNode;
            while ( (node = el.firstChild) ) {
                lastNode = frag.appendChild(node);
            }
            range.insertNode(frag);

            // Preserve the selection
            if (lastNode) {
                range = range.cloneRange();
                range.setStartAfter(lastNode);
                range.collapse(true);
                sel.removeAllRanges();
                sel.addRange(range);
            }
        }
    } else if (document.selection && document.selection.type != "Control") {
        // IE < 9
        document.selection.createRange().pasteHTML(html);
    }
}