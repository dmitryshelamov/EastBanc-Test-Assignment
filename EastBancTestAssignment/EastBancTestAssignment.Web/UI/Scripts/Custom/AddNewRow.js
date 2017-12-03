// Add row the table
//        $('#buttonAddRow').on('click', function() {
//            $('#tableItem > tbody').append(' <tr> <th> last </th> <th> last </th> <th> last </th> </tr>');
//        });

$(document).ready(function () {
    var $tableItem = $("#table-item");
    var $buttomAddRow = $("#button-add-row");
    var $buttonDeleteRow = $tableItem.find("tbody > tr button");

    $buttomAddRow.on("click",
        function() {
            var newIndex = $tableItem.find("tbody > tr").length - 1;
//        alert("NewIdndex: " + newIndex);

            $tableItem
                .find("tbody > .template")
                .clone()
                .removeClass("template")
                .addClass()
                .find(".name-input").attr("name", "Items[" + newIndex + "].Name").end()
                .find(".item-delete > .text-center button").attr("id", "row-number-" + newIndex).end()
                .appendTo($tableItem.find("tbody"));
        });
});


//  user even delegate, because we create rows dynamically
$(document).on("click", "tbody > tr button", function () {
    $(this).closest("tr").remove();
    return false;
});

//$buttonDeleteRow.on("click",
//    function () {
//        $(this).closest("tr").remove();
//    });