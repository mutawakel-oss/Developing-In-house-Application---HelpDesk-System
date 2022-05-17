using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
public partial class frmSubCategoryReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ReportDocument rpt = null;
        rpt = CreateCrystalReportDocument();
        CrystalReportViewer1.ReportSource = rpt;
    }

    private ReportDocument CreateCrystalReportDocument()
    {
        ReportDocument rpt = new ReportDocument();
        string reportPath = Server.MapPath("rptInvSubCategory.rpt");
        rpt.Load(reportPath);
        ConnectionInfo connectionInfo = new ConnectionInfo();
        connectionInfo.DatabaseName = "HelpDesk";
        connectionInfo.UserID = "appuser";
        connectionInfo.Password = "appuser";
        Tables tables = rpt.Database.Tables;

        foreach (CrystalDecisions.CrystalReports.Engine.Table table in tables)
        {
            TableLogOnInfo tableLogonInfo = table.LogOnInfo;
            tableLogonInfo.ConnectionInfo = connectionInfo;
            table.ApplyLogOnInfo(tableLogonInfo);
        }
        return rpt;
    }
}
