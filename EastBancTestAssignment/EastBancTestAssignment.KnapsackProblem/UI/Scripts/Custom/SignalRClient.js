$(function () {
    var $table = $("table");

    var hub = $.connection.progressHub;

    // Create a function that the hub can call to report progress.
    hub.client.reportProgress = function (id, progress) {
        $table
            .find("tbody").find('#' + id).find("#percantage").text(progress + "%");
    };

    hub.client.reportComplete = function (id, bestItemPrice, percantage, status) {
        var $tr = $table
            .find("tbody").find('#' + id);

        $tr.find("#price").text(bestItemPrice);
        $tr.find("#percantage").text(percantage + "%");
        $tr.find("#staus").text(status);
    };

    // Start the connection.
    $.connection.hub.start().done(function () {

    });
});


