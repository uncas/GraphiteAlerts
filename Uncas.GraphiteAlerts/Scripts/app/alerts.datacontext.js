window.alertsApp = window.alertsApp || {};

window.alertsApp.datacontext = (function() {

    var datacontext = {
        getAlerts: getAlerts
    };

    return datacontext;

    function updateStatus(status) {
        var updatingStatus = $("footer p span#updatingStatus");
        updatingStatus.text(status);
    }

    function updateInfo() {
        var today = new Date();
        var $lastUpdated = $("footer p span#lastUpdated");
        $lastUpdated.text("Last updated " + today.toLocaleString() + ".");
    }

    function toggleUpdate(enabled) {
        var $updateNow = $("footer p a");
        var $updateNowShadow = $("footer p span#updateNowShadow");
        if (enabled) {
            $updateNow.show();
            $updateNowShadow.hide();
        } else {
            $updateNow.hide();
            $updateNowShadow.show();
        }
    }

    function getAlerts(alertsObservable, errorObservable) {
        toggleUpdate(false);
        return ajaxRequest("get", alertsUrl())
            .done(getSucceeded)
            .fail(getFailed);

        function updateData(data) {
            var mappedAlerts = $.map(data, function(list) { return new createAlert(list); });
            alertsObservable(mappedAlerts);
        }

        function getSucceeded(data) {
            updateData(data);
            updateInfo();
            updateStatus("");
            localStorage.setItem('alerts', JSON.stringify(data));
            toggleUpdate(true);
        }

        function getFailed() {
            errorObservable("Error retrieving alerts.");
            var retrievedObject = localStorage.getItem('alerts');
            var alerts = JSON.parse(retrievedObject);
            if (alerts) updateData(alerts);
            updateInfo();
            updateStatus("Error while updating!");
            toggleUpdate(true);
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