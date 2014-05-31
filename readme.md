[![Build status](https://ci.appveyor.com/api/projects/status/e11nosa9brw5xm6w)](https://ci.appveyor.com/project/uncas/graphitealerts)

Graphite Alerts
===============

Using ASP.NET MVC4.

Features
--------
- Define alerts based on graphite monitors.
- Display alerts in web page, with critical on top.
- Read alert definitions from configurable folders.
- Refresh of all alerts, using ajax refresh.

How to set up
-------------
- Clone repository.
- The alerts are defined in json text files that should be located outside of repository,
- So choose a folder for alert definitions.
- Copy an example alert from Uncas.GraphiteAlerts.Tests\TestData\Alert.json,
- And modify with values for your needs.
- Change web.config app.setting AlertsFolder to point to the folder with alert definitions.
- Build calling build.ps1 or in Visual Studio.
- Run website from Visual Studio,
- Or mount subfolder with website Uncas.GraphiteAlerts in IIS,
- And open on the URL that you configured in IIS.

Ideas
-----
- Floating judgement of alerts: Instead of Ok vs Warning vs Critical, indicate a critical limit, and a factor for warnings.
- Collapse on name parts.
- Server side caching?
- Only refresh alerts when they're expected to change (use history!). Still with possibility to force refresh of all.
- Refresh alerts individually, using ajax refresh.
