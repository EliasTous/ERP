net use \\192.168.1.10  /user:administrator p@ssw0rds
xcopy *.aspx \\192.168.1.10\aionhr\ /Y
xcopy Reports\*.aspx \\192.168.1.10\aionhr\Reports /Y
xcopy EmployeePages\*.aspx \\192.168.1.10\aionhr\EmployeePages /Y
xcopy EmployeePages\App_LocalResources\*.resx \\192.168.1.10\aionhr\EmployeePages\App_LocalResources /Y
xcopy Reports\App_LocalResources\*.resx \\192.168.1.10\aionhr\Reports\App_LocalResources /Y

xcopy Reports\Controls\*.ascx \\192.168.1.10\aionhr\Reports\Controls /Y
xcopy Reports\Controls\App_LocalResources\*.resx \\192.168.1.10\aionhr\Reports\Controls\App_LocalResources /Y
xcopy bin\AionHR.Infrastructure.dll \\192.168.1.10\\aionhr\bin\ /Y
xcopy bin\AionHR.Model.dll \\192.168.1.10\aionhr\bin\ /Y
xcopy bin\AionHR.Repository.WebService.dll \\192.168.1.10\aionhr\bin\ /Y
xcopy bin\AionHR.Services.dll \\192.168.1.10\aionhr\bin\ /Y
xcopy bin\AionHR.Web.UI.Forms.dll \\192.168.1.10\aionhr\bin\ /Y
xcopy bin\Reports.dll \\192.168.1.10\aionhr\bin\ /Y
xcopy bin\ar\*.* \\192.168.1.10\aionhr\bin\ar\*.* /Y
xcopy bin\en\*.* \\192.168.1.10\aionhr\bin\en\*.* /Y
xcopy Scripts\*.* \\192.168.1.10\aionhr\Scripts\ /Y
xcopy Controls\*.ascx \\192.168.1.10\aionhr\Controls\*.ascx /Y
xcopy Controls\App_LocalResources\*.resx \\192.168.1.10\aionhr\Controls\App_LocalResources\*.resx /Y
xcopy CSS\*.css \\192.168.1.10\aionhr\CSS\ /Y
xcopy App_GlobalResources\*.resx \\192.168.1.10\aionhr\App_GlobalResources\*.resx  /Y
xcopy App_LocalResources\*.resx \\192.168.1.10\aionhr\App_LocalResources\*.resx  /Y