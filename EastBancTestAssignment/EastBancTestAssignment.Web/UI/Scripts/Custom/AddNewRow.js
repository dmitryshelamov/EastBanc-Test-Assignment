// Add row the table
//        $('#buttonAddRow').on('click', function() {
//            $('#tableItem > tbody').append(' <tr> <th> last </th> <th> last </th> <th> last </th> </tr>');
//        });

$(document).ready(function () {
    var $tableItem = $("#table-item");
    var $buttomAddRow = $("#button-add-row");

    $buttomAddRow.on("click",
        function() {
            var newIndex = $tableItem.find("tbody > tr").length - 1;
//        alert("NewIdndex: " + newIndex);

            $tableItem
                .find("tbody > .template")
                .clone()
                .removeClass("template")
                .addClass("data-row")
                .find(".row-number").text(newIndex).end()
                .find(".name-input").attr("name", "Items[" + newIndex + "].Name").end()
                .find(".price-input").attr("name", "Items[" + newIndex + "].Price").end()
                .find(".weight-input").attr("name", "Items[" + newIndex + "].Weight").end()
                .find(".item-delete > .text-center button").attr("id", "row-number-" + newIndex).end()
                .appendTo($tableItem.find("tbody"));
        });
});


//  user event delegate, because we create rows dynamically
$(document).on("click", "tbody > tr button", function () {
    var $tableItem = $("#table-item");

    
    $(this).closest("tr").remove();

    var itemIndex = 0;
    $tableItem.find("tbody > .data-row").each(function (index) {
        var thisRow = $(this);

        thisRow.find(".row-number").text(index);
        thisRow.find(".item-name").find("input").attr("name", "Items[" + index + "].Name");
        thisRow.find(".item-price").find("input").attr("name", "Items[" + index + "].Price");
        thisRow.find(".item-weight").find("input").attr("name", "Items[" + itemIndex + "].Weight");

        itemIndex++;
    });

    return false;
});