(function(ko, datacontext) {
    datacontext.Alert = alert;

    function getCssClass(level) {
        if (level == "Critical")
            return "alert-danger";
        if (level == "Warning")
            return "alert-warning";
        return "";
    }

    function alert(data) {
        var self = this;
        data = data || {};

        self.name = data.Name;
        self.level = data.Level;
        self.comments = data.Comments;
        self.chartUrl = data.ChartUrl;
        self.timestamp = data.Timestamp;
        self.cssClass = getCssClass(data.Level);
        self.dashboardUrl = data.DashboardUrl;
        self.dashboardText = "";
        if (self.dashboardUrl)
            self.dashboardText = "Dashboard";

        self.toJson = function() { return ko.toJSON(self); };
    }

})(ko, alertsApp.datacontext);