$(function () {
    var $table = $("table");

    var hub = $.connection.progressHub;

    // Create a function that the hub can call to report progress.
    hub.client.reportProgress = function (id, progress) {
        $table
            .find("tbody").find('#' + id).find("#percantage").text(progress + "%");
    };

    hub.client.reportComplete = function (id, name, bestItemPrice, percantage, status) {
        updateProgress(id, name, bestItemPrice, percantage, status);
    };

    hub.client.ReportProgressExtended = function(progressList) {
        for (var i = 0; i < progressList.length; ++i) {
            if (progressList[i].Name === "Complete") {
                updateProgress(progressList[i].Id,
                    progressList[i].Name,
                    progressList[i].BestItemSetPrice,
                    progressList[i].Progress,
                    progressList[i].Status);
            };
        };
    };

    setTimeout(
        function () {
            hub.server.requestUpdate();
        }, 2000);

    // Start the connection.
    $.connection.hub.start().done(function () {
//        hub.server.requestUpdate();
    });

    function updateProgress(id, name, bestItemPrice, percantage, status) {
        var $tr = $table
            .find("tbody").find('#' + id);

        $tr.find("#name").find("span").replaceWith('<a href="Backpack/Details/' + id + '">' + name + '</a>');
        $tr.find("#price").text(bestItemPrice);
        $tr.find("#percantage").text(percantage + "%");
        $tr.find("#staus").text(status);
    }
});


