﻿$(document).ready(function() {
    $(".js-delete-row").click(function(e) {
        var tr = $(e.target).parents("tr");

        tr.find("#staus").text("Deleting");

        $.ajax({
                url: "/api/backpack/" + tr.attr("id"),
                method: "DELETE"
            })
            .done(function() {
                tr.fadeOut(function() {
                    $(this).remove();
                });
            })
            .fail(function() {
                alert("Something failed!");
            });
    });
});