window.alertsApp = window.alertsApp || {};

window.alertsApp.datacontext = (function () {

    var datacontext = {
        getAlerts: getAlerts
    };

    return datacontext;

    function getAlerts(alertsObservable, errorObservable) {
        return ajaxRequest("get", alertsUrl())
            .done(getSucceeded)
            .fail(getFailed);

        function getSucceeded(data) {
            var mappedAlerts = $.map(data, function(list) { return new createAlert(list); });
            alertsObservable(mappedAlerts);
        }

        function getFailed() {
            errorObservable("Error retrieving alerts.");
        }
    }

    function createAlert(data) {
        return new datacontext.Alert(data); // alert is injected by alerts.model.js
    }

    // Private

    function ajaxRequest(type, url, data, dataType) { // Ajax helper
        var options = {
            dataType: dataType || "json",
            contentType: "application/json",
            cache: false,
            type: type,
            data: data ? data.toJson() : null
        };
        var antiForgeryToken = $("#antiForgeryToken").val();
        if (antiForgeryToken) {
            options.headers = {
                'RequestVerificationToken': antiForgeryToken
            };
        }
        return $.ajax(url, options);
    }

    // routes

    function alertsUrl() {
        return "/api/alert/";
    }

})();