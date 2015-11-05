$(document).ready(function () {
    // MAKE FORMS DRAGGABLE
    $(".modal-dialog").draggable();

    // MANIPULATE ELEMENT
    var manipulateElementByDropDownMenu = function (dropDownElementSelected, elementToManipulate) {
        if (dropDownElementSelected.val() === "1") {
            elementToManipulate.show();
        } else {
            elementToManipulate.val(null);
            elementToManipulate.hide();
        }
        console.log(elementToManipulate.val());
    }

    // LISTENER FOR DROPDOWN MENUS
    $(".contest-strategy").change(function () {
        var inputId = this.id;

        switch (inputId) {
            case "contest-reward-strategy":
                manipulateElementByDropDownMenu($(this), $("#contest-reward-top-field"));
                break;
            case "contest-deadline-strategy":
                manipulateElementByDropDownMenu($(this), $("#contest-deadline-additional-field"));
                break;
            case "contest-voting-strategy":
                manipulateElementByDropDownMenu($(this), $("#contest-voting-additional-field"));
                break;
            case "contest-participation-strategy":
                manipulateElementByDropDownMenu($(this), $("#contest-participation-additional-field"));
                break;
        }
    });

    // ADD COMMITTEE LISTENER
    $("#add-committee").click(function () {
        $("#contest-voting-additional-field")
            .append("<input type='text' name='Committee[]' class='form-control' placeholder='Username'>");
    });

    // REMOVE COMMITTEE LISTENER
    $("#remove-committee").click(function () {
        if ($("#contest-voting-additional-field").children().length > 4) {
            $("#contest-voting-additional-field").children().last().remove();
        };
    });

    // ADD PARTICIPANT LISTENER
    $("#add-participant").click(function () {
        $("#contest-participation-additional-field")
            .append("<input type='text' name='AllowedParticipants[]' class='form-control' placeholder='Username'>");
    });

    // REMOVE PARTICIPANT LISTENER
    $("#remove-participant").click(function () {
        if ($("#contest-participation-additional-field").children().length > 4) {
            $("#contest-participation-additional-field").children().last().remove();
        };
    });
});