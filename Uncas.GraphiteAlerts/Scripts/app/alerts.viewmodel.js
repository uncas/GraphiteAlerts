window.alertsApp.alertsViewModel = (function(ko, datacontext) {
    /// <field name="alerts" value="[new datacontext.alerts()]"></field>
    var alerts = ko.observableArray();
    var error = ko.observable();

    var search = function () {
        datacontext.getAlerts(alerts, error);
    };

    var searchRepeatedly = function () {
        search();
        setTimeout(searchRepeatedly, 60 * 1000);
    };

    searchRepeatedly();

    return {
        alerts: alerts,
        error: error,
        search: search
    };

})(ko, alertsApp.datacontext);

// Initiate the Knockout bindings
ko.applyBindings(window.alertsApp.alertsViewModel);