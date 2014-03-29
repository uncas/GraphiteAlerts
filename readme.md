Graphite Alerts
===============

Using ASP.NET MVC4.

Features
--------
- Define alerts based on graphite monitors.
- Display alerts in web page, with critical on top.
- Read alert definitions from configurable folders.
- Refresh of all alerts, using ajax refresh.

Ideas
-----
- Collapse on name parts.
- Server side caching?
- Only refresh alerts when they're expected to change (use history!). Still with possibility to force refresh of all.
- Refresh alerts individually, using ajax refresh.
- Floating judgement of alerts: Instead of Ok vs Warning vs Critical, indicate a critical limit, and a factor for warnings.