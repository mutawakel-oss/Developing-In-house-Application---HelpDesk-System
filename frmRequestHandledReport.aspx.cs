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
using CrystalDecisions.Reporting;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Web;
using CrystalDecisions.Shared;
using System.Data;
using System.Data.Odbc;


public partial class frmRequestHandledReport : System.Web.UI.Page
{
    public static ReportDocument rpt = null;
    DateTime dt1;
    DateTime dt2;
    string tmpDt;
    OdbcDataReader reader;
    public static ReportDocument techReport = null;
    public static ReportDocument ServiceRerport = null;
    public static ReportDocument techEvaluationReport = null;
    public static ReportDocument userRequestsReport = null;
    protected void Page_Load(object sender, EventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:The following function will be used to fill the page fields 
        /// Author: mutawakelm
        /// Date :24/05/2008 10:45:34 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        
        try
        {
            HyperLink LB1 = (HyperLink)this.Master.Page.Controls[0].Controls[3].FindControl("hlkLogin");
            if (Session.Count == 0)
            {
                Response.Redirect("error.aspx?error=Session Expired");
            }
            LB1.Text = "Log Out";       
            int creteriaCounter = 0;
            LoggedAs();
            this.CrystalReportViewer1.ReportSource = null;
            //ServiceRerport = CreateCrystalReportDocument("rptHandledRequest.rpt");
            
            
            //The following code to initialize the date fields
            //Format="dd/MMM/yyyy"
            //txtDeliveryDate.Text = "01/Jan/"+DateTime.Now.Year.ToString();
            //txtToDate.Text = DateTime.Now.Day.ToString()+"/"+DateTime.Now.Month.ToString()+"/"+DateTime.Now.Year.ToString() ;

            if (!IsPostBack)
            {
                int counter = 0;//This counter will hold the value of the current drop down list index
                string strTechnicianQuery = "SELECT id,full_name FROM t_users WHERE user_group='2'";
                reader = GeneralClass.Program.gRetrieveRecord(strTechnicianQuery);
                if (reader.HasRows)
                {
                    ddlTechnicianName.Items.Clear();
                    while (reader.Read())
                    {
                        ddlTechnicianName.Items.Add(reader["full_name"].ToString());
                        ddlTechnicianName.Items[counter].Value = reader["id"].ToString();
                        counter++;
                    }
                    reader.Close();
                }
                else reader.Close();
                ddlCategory.SelectedIndex = -1;
                //The following code will used to fill the evaluation category drop down list in the technician evaluation part
                string strCategories = "SELECT id,category FROM t_category";//This variable will hold the categories query
                reader = GeneralClass.Program.gRetrieveRecord(strCategories);
                if (reader.HasRows)
                {
                    
                    while (reader.Read())
                    {
                        ddlEvaluationCreteria.Items.Add(reader["category"].ToString());
                        ddlEvaluationCreteria.Items[creteriaCounter].Value = reader["id"].ToString();
                        creteriaCounter++;

                    } reader.Close();
                    ddlEvaluationCreteria.Items.Insert(ddlEvaluationCreteria.Items.Count, "All");
                    ddlEvaluationCreteria.Text = "All";
                    //ddlEvaluationCreteria.SelectedIndex = ddlEvaluationCreteria.Items.Count;
                }
                else reader.Close();
                //The follownig code will be used to fill the status drop down list in technician history option
                string strStatus = "SELECT status FROM t_status";//This variable will hold the status query
                reader = GeneralClass.Program.gRetrieveRecord(strStatus);
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        ddlCategory.Items.Add(reader["status"].ToString());
                        ddlCategoryStatus.Items.Add(reader["status"].ToString());
                    } reader.Close();
                    ddlCategoryStatus.Text = "Completed";
                    ddlCategory.Text = "Completed";
                    
                }
                else reader.Close();
                
            }
            else
            {

                if ((txtDeliveryDate.Text != null && txtDeliveryDate.Text != "") && (MyAccordion.SelectedIndex == 0))
                {

                    loadTechReport();
                    
                }
                else
        
            if((txtDeliveryDate.Text != null && txtDeliveryDate.Text != "")&&(MyAccordion.SelectedIndex==1))
                loadTechEvaluationReport();
            else                    
                if ((txtDeliveryDate.Text != null && txtDeliveryDate.Text != "") && (MyAccordion.SelectedIndex == 2))
                    LoadReport();
                else
                    if (MyAccordion.SelectedIndex == 3)
                     mLoadUsersReqReport();
                
            }  
            
        }
        catch (Exception exp)
        {
            Response.Write(exp.Message.ToString());
        }
        }
    protected void Page_UnLoad(object sender, EventArgs e)
    {

        this.CrystalReportViewer1.Dispose();
        this.CrystalReportViewer1 = null;
     
        GC.Collect();
    }
    protected void LoadReport()
    {
        //=====================================================//
        /// <summary>
        /// Description:
        /// Author: Olivery
        /// Date :29/04/2008 09:38:07 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {

            
            TextObject txtHandle;
            TextObject txtDates;
            // // rpt.FileName = Server.MapPath("rptHandledRequest");
            rpt = CreateCrystalReportDocument("rptHandledRequest.rpt");
            txtHandle = (TextObject)rpt.ReportDefinition.ReportObjects["txtSummary"];
            txtDates = (TextObject)rpt.ReportDefinition.ReportObjects["txtDateRange"];



            if (txtDeliveryDate.Text != null && txtDeliveryDate.Text != "")
            {
                if (txtToDate.Text != null && txtToDate.Text != "")
                {
                    dt2 = Convert.ToDateTime(String.Format("{0:dd/mm/yyyy}", txtToDate.Text.Trim()));

                }

                else
                {
                    dt2 = DateTime.Today;

                    //  string.Format("{0:yyyy-MM-dd}"

                    txtToDate.Text = String.Format("{0: dd/MMM/yyyy}", DateTime.Today);
                }

                tmpDt = String.Format("{0: dd/MMM/yyyy}", Convert.ToDateTime(txtToDate.Text).AddDays(1.0));

                dt1 = Convert.ToDateTime(String.Format("{0:dd/mm/yyyy}", txtDeliveryDate.Text.Trim()));
               rpt.SetParameterValue(0, dt1);
               rpt.SetParameterValue(1, dt2);
               CrystalReportViewer1.ReportSource = rpt;
               txtHandle.Text = "Requests Handled :       " + TotalRequestsHandled().ToString()+ "      Requests Resolved :     " + TotalRequestsResolved().ToString() + "       Unresolved Requests : " + TotalRequestsUnResolved().ToString();
               txtDates.Text = "Between " + txtDeliveryDate.Text + " and " + txtToDate.Text;
            
            }           
                        
        }
        catch (Exception LoadReport_Exp)
        {

        }
    }
    protected void LoadServiceReport(object sender, EventArgs e)
    {
        try
        {
            TextObject txtHandle;
            TextObject txtDates;

            if (!((string.IsNullOrEmpty(txtDeliveryDate.Text.ToString())) && (string.IsNullOrEmpty(txtToDate.Text.ToString()))))
            {
                rpt = CreateCrystalReportDocument("rptHandledRequest.rpt");
                txtHandle = (TextObject)rpt.ReportDefinition.ReportObjects["txtSummary"];
                txtDates = (TextObject)rpt.ReportDefinition.ReportObjects["txtDateRange"];



                
                    if (txtToDate.Text != null && txtToDate.Text != "")
                    {
                        dt2 = Convert.ToDateTime(String.Format("{0:dd/mm/yyyy}", txtToDate.Text.Trim()));

                    }

                    else
                    {
                        dt2 = DateTime.Today;
                        txtToDate.Text = String.Format("{0: dd/MMM/yyyy}", DateTime.Today);
                    }

                    tmpDt = String.Format("{0: dd/MMM/yyyy}", Convert.ToDateTime(txtToDate.Text).AddDays(1.0));

                    dt1 = Convert.ToDateTime(String.Format("{0:dd/mm/yyyy}", txtDeliveryDate.Text.Trim()));
                    rpt.SetParameterValue(0, dt1);
                    rpt.SetParameterValue(1, dt2);
                    CrystalReportViewer1.ReportSource = rpt;
                    txtHandle.Text = "Requests Handled :       " + TotalRequestsHandled().ToString() + "      Requests Resolved :     " + TotalRequestsResolved().ToString() + "       Unresolved Requests : " + TotalRequestsUnResolved().ToString();
                    txtDates.Text = "Between " + txtDeliveryDate.Text + " and " + txtToDate.Text;
                    this.lblServiceReportError.Visible = false;
                
            }
            else
            {
                this.lblServiceReportError.Visible = true;
                this.lblServiceReportError.Text = "Please select begining date and end date for the report";
            }
        }
        catch (Exception exp)
        {
            Response.Write(exp.Message.ToString());
        }
    }
    private ReportDocument CreateCrystalReportDocument(string ReportName)
    {

        try
        {
            //TextObject txtHandle;
            ReportDocument rpt = new ReportDocument();
            string reportPath = Server.MapPath(ReportName);
            rpt.Load(reportPath);
            ////txtHandle = (TextObject)rpt.ReportDefinition.ReportObjects["txtHandled"];
            ////txtHandle.Text = "test";

            ConnectionInfo connectionInfo = new ConnectionInfo();
            connectionInfo.DatabaseName = "HelpDesk";
            connectionInfo.UserID = "sa";
            connectionInfo.Password = "dbadmin";
            Tables tables = rpt.Database.Tables;

            foreach (CrystalDecisions.CrystalReports.Engine.Table table in tables)
            {
                TableLogOnInfo tableLogonInfo = table.LogOnInfo;
                tableLogonInfo.ConnectionInfo = connectionInfo;
                table.ApplyLogOnInfo(tableLogonInfo);
            }

            return rpt;
        }
        catch (Exception exp)
        {
            mRestartApplication();
            return null;
        }
    }
    protected void mRestartApplication()
    {
         System.Web.HttpRuntime.UnloadAppDomain();
         this.CrystalReportViewer1.Dispose();
        
            
            try
            {
                System.Threading.Thread.Sleep(1000);
                Response.Redirect("http://helpdesk.ksau-hs.edu.sa/helpdesk/frmRequestHandledReport.aspx");
            }
            catch
            {
            }
            finally
            {
                
            } 

       
    }
    protected void DisplayReport(object sender, EventArgs e)
    {
        //=====================================================//
        /// <summary>
        /// Description:
        /// Author: Olivery
        /// Date :30/04/2008 12:13:13 PM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        //ReportDocument rpt = null;
        //try
        //{
        //    if (txtDeliveryDate.Text != null && txtDeliveryDate.Text != "")
        //    {
        //        if (txtToDate.Text != null && txtToDate.Text != "")
        //            dt2 = Convert.ToDateTime(txtToDate.Text.Trim());
        //        else
        //            dt2 = DateTime.Today;

        //        //rpt = CreateCrystalReportDocument("rptHandledRequest.rpt");
        //        rpt = CreateCrystalReportDocument("rptRequestsHandled.rpt");
        //        dt1 = Convert.ToDateTime(txtDeliveryDate.Text.Trim());
             
        //        rpt.SetParameterValue(0, dt1);
        //        rpt.SetParameterValue(1, dt2);
        //        CrystalReportViewer1.ReportSource = rpt;
        //    }

            //dt1 = Convert.ToDateTime(txtDeliveryDate.Text);
            //dt2 = Convert.ToDateTime(txtToDate.Text);

            //rpt = CreateCrystalReportDocument("rptHandledRequest.rpt");
            // rpt.SetParameterValue(0, dt1);
            //// rpt.SetParameterValue("ToDate", dt2);

            //CrystalReportViewer1.ReportSource = rpt;

        //}
        //catch (Exception DisplayReport_Exp)
        //{

        //}    
    }
    protected void NewRequest(object sender, EventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:The following function will be used to assign the language of the session of new request page
        /// Author: mutawakelm
        /// Date :02/05/2008 09:29:35
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            //Response.Write(this.languageLabel.Text.ToString());
            Session["Language"] = "en-US";
            Response.Redirect("./frmNewRequest.aspx");

        }
        catch (Exception NewRequest_Exp)
        {

        }
    }
    protected void EditLogUserInfo(object sender, EventArgs e)
    {
        /// <summary>
        /// 	Description: Navigate to the Editor page of user info edit
        ///	
        ///
        /// 	Date:28/Aug/2007
        /// 	Author:Oliver
        /// 	Parameter:loginID
        ///		input:
        ///		output: 
        /// 	Example: 
        /// </summary>    

        if (null != Session["UserID"] && Session["UserID"].ToString() != "")
            Response.Redirect("frmEditLogUserInfo.aspx?logid=" + Session["UserID"].ToString() + "& backto=frmAdminRequestList.aspx");
    }    
    private int TotalRequestsHandled()
    {
        //=====================================================//
        /// <summary>
        /// Description:
        /// Author: Olivery
        /// Date :03/05/2008 03:27:16 PM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
       int RecNos=0;
       OdbcDataReader reader = null;
        try
        {
            reader = GeneralClass.Program.gDataReader("vw_requests_handled", "count(id)", "status_id<>0 AND assigned_date >= '" + txtDeliveryDate.Text + "' AND assigned_date <'" + tmpDt + "'");
           while (reader.Read())
           {
             if(Convert.ToInt32(reader[0])!=0)
               {
                   RecNos =Convert.ToInt32(reader[0]);
               }
           
           }
           reader.Close();
            return RecNos;
        }
        catch (Exception RequestSummary_Exp)
        {
            if (reader != null)
                reader.Close();         
            return RecNos;
        }
    }
    protected void mDisplayTechReport(object sender, EventArgs e)
    {
        try
        {
            loadTechReport();
        }
        catch (Exception exp)
        {
            Response.Write(exp.Message.ToString());
        }

       
    }
    protected void loadTechReport()
    {
        //=====================================================//
        /// <summary>
        /// Description:This function will be used to display the report of the selected technician in the drop down list
        /// Author: mutawakelm
        /// Date :14/05/2008 03:27:35 PM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            TextObject txtEmployeeName;
            TextObject txtStatus;
            TextObject txtCategoryTasksNo;
            TextObject txtFrom;
            TextObject txtTo;
            TextObject txtPrinter;//The full name of person who print the report
            string requestre_name = "";//This variable will hold the name of the requester
            string strDuration = "";//This variable will hold the duration of a task (from assignment date to completion date).
            techReport = CreateCrystalReportDocument("rptTechHistory.rpt");
            if (!((string.IsNullOrEmpty(txtDeliveryDate.Text.ToString())) && (string.IsNullOrEmpty(txtToDate.Text.ToString()))))
            {
                this.unloadMessage.Text = "";
                this.unloadMessage.Visible = false;
                DateTime fromDateTime = Convert.ToDateTime(txtDeliveryDate.Text.ToString());
                DateTime toDateTime = Convert.ToDateTime(txtToDate.Text.ToString());

                //The following table will hold the records of the selected technician
                DataTable tblTechnicianHistory = new DataTable();
                tblTechnicianHistory.Columns.Add("created_by");
                tblTechnicianHistory.Columns.Add("assign_date");
                tblTechnicianHistory.Columns.Add("complete_date");
                tblTechnicianHistory.Columns.Add("duration");
                tblTechnicianHistory.Columns.Add("rating", Type.GetType("System.Double"));
                tblTechnicianHistory.Columns.Add("category");
                tblTechnicianHistory.Columns.Add("id");//request id
                string strTechHistory = "SELECT     dbo.t_requests.requester_name,dbo.t_requests.id, dbo.t_requests.created_by, dbo.t_RequestServiceHistory.status, dbo.t_RequestServiceHistory.modified_date, dbo.t_assignment.assigned_date, " +
                          " dbo.t_category.Category, dbo.t_requests.requester_rate" +
                          " ,(select t_users.full_name from t_users where t_users.id=t_requests.created_by) FROM         dbo.t_users INNER JOIN" +
                          " dbo.t_RequestServiceHistory ON dbo.t_users.id = dbo.t_RequestServiceHistory.serviced_by INNER JOIN" +
                          " dbo.t_requests ON dbo.t_RequestServiceHistory.request_id = dbo.t_requests.id INNER JOIN" +
                          " dbo.t_assignment ON dbo.t_requests.id = dbo.t_assignment.request_id INNER JOIN" +
                          " dbo.t_category ON dbo.t_requests.category_id = dbo.t_category.id" +
                          " WHERE     (dbo.t_users.user_group = '2') AND (dbo.t_users.id = '" + ddlTechnicianName.SelectedValue.ToString() + "') AND (dbo.t_RequestServiceHistory.status = '" + ddlCategory.Text.ToString() + "')";//This query for tasks of a pc technician
                reader = GeneralClass.Program.gRetrieveRecord(strTechHistory);
                if (reader.HasRows)
                {
                    time_schedule t = new time_schedule();
                   
                    while (reader.Read())
                    {
                        DateTime startDate = Convert.ToDateTime(reader["assigned_date"].ToString());
                        DateTime endDate = Convert.ToDateTime(reader["modified_date"].ToString());
                        if ((startDate >= fromDateTime) && (startDate <= toDateTime))
                        {
                            t.setStartDate(startDate);
                            t.setEndDate(endDate);

                            System.TimeSpan difference = t.getTimeDifference(); 
                            //The following code will determine days 
                            string[] durationSplitter = difference.ToString().Split('.');
                            if (durationSplitter.Length == 2)
                                strDuration = durationSplitter[0] + " Days and " + durationSplitter[1];
                            else
                                if (durationSplitter.Length == 1)
                                    strDuration = difference.ToString();
                            requestre_name = reader["requester_name"].ToString();
                            tblTechnicianHistory.Rows.Add(reader[""].ToString(), reader["assigned_date"].ToString(), reader["modified_date"].ToString(), strDuration, double.Parse(reader["requester_rate"].ToString()), reader["Category"].ToString(), reader["id"].ToString());
                        }
                    }
                    reader.Close();
                }
                else reader.Close();
                //The following code will assign the datatable to the dataset
                techReport.SetDatabaseLogon("sa", "dbadmin");
                techReport.Database.Tables["tblTechHistory"].SetDataSource((DataTable)tblTechnicianHistory);//assign the resulted data table to the abstract table of the dataset
                techReport.Refresh();
                txtEmployeeName = (TextObject)techReport.ReportDefinition.ReportObjects["txtName"];
                txtEmployeeName.Text = ddlTechnicianName.SelectedItem.ToString();
                txtStatus = (TextObject)techReport.ReportDefinition.ReportObjects["txtReportStatus"];
                txtStatus.Text = "PC Technician "+ddlCategory.SelectedItem.ToString() + " Tasks Report";
                txtFrom = (TextObject)techReport.ReportDefinition.ReportObjects["txtDateFrom"];
                txtFrom.Text = txtDeliveryDate.Text.ToString();
                txtTo = (TextObject)techReport.ReportDefinition.ReportObjects["txtToDate"];
                txtTo.Text = txtToDate.Text.ToString();
                txtPrinter = (TextObject)techReport.ReportDefinition.ReportObjects["txtPrintedBy"];
                txtPrinter.Text = Session["UserFullName"].ToString();
                this.CrystalReportViewer1.ReportSource = techReport;//to assign the transcript report file to the form report viewer
            }
            else
            {
                this.unloadMessage.Visible = true;
                this.unloadMessage.Text = "Please select begining date and end date for the report";
            }
            
            

        }
        catch (Exception exp)
        {
            if (reader != null)
                reader.Close();
            //Response.Write(exp.Message.ToString());
        }
    }
    private int TotalRequestsResolved()
    {
        //=====================================================//
        /// <summary>
        /// Description:
        /// Author: Olivery
        /// Date :03/05/2008 03:27:16 PM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        int RecNos = 0;
        OdbcDataReader reader = null;
        try
        {


            reader = GeneralClass.Program.gDataReader("vw_requests_handled", "count(id)", "status_id=5 AND assigned_date >='" + txtDeliveryDate.Text + "' AND assigned_date < '" + tmpDt + "'");

            while (reader.Read())
            {
                if (Convert.ToInt32(reader[0]) != 0)
                {
                    RecNos = Convert.ToInt32(reader[0]);
                }

            }
            reader.Close();
            return RecNos;
        }
        catch (Exception RequestSummary_Exp)
        {
            if (reader != null)
                reader.Close();
            return RecNos;
        }
    }
    private int TotalRequestsUnResolved()
    {
        //=====================================================//
        /// <summary>
        /// Description:
        /// Author: Olivery
        /// Date :03/05/2008 03:27:16 PM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        int RecNos = 0;
        OdbcDataReader reader = null;
        try
        {
            reader = GeneralClass.Program.gDataReader("vw_requests_handled", "count(id)", "status_id<>5 AND assigned_date >= '" + txtDeliveryDate.Text + "' AND assigned_date <'" + tmpDt + "'");

            while (reader.Read())
            {
                if (Convert.ToInt32(reader[0]) != 0)
                {
                    RecNos = Convert.ToInt32(reader[0]);
                }

            }
            reader.Close();
            return RecNos;
        }
        catch (Exception RequestSummary_Exp)
        {
            if (reader != null)
                reader.Close();
            return RecNos;
        }
    }
    protected void LoggedAs()
    {
        /// <summary>
        /// 	Description: show the user group of the logged in user
        ///	
        ///
        /// 	Date:27/Aug/2007
        /// 	Author:Oliver
        /// 	Parameter:loginID
        ///		input:
        ///		output: 
        /// 	Example: 
        /// </summary>

        if (null != Session["UserFullName"] && Session["UserFullName"].ToString() != "")
            lblLogUser.Text = Session["UserFullName"].ToString();
        if (null != Session["Badge"] && Session["Badge"].ToString() != "")
            lblBadgeNo.Text = Session["Badge"].ToString();
        if (null != Session["Title"] && Session["Title"].ToString() != "")
            lblTitle.Text = Session["Title"].ToString();
        if (null != Session["Department"] && Session["Department"].ToString() != "")
            lblDepartment.Text = Session["Department"].ToString();

        Label LB = (Label)this.Master.Page.Controls[0].Controls[3].Controls[9].Controls[1];
        HyperLink Hlk = (HyperLink)this.Master.Page.Controls[0].Controls[3].Controls[5];

        if (null != Session["Admin"] && Session["Admin"].ToString() == "true")
        {
            LB.Text = "Administrator";
            Hlk.NavigateUrl = "frmAdminDefault.aspx";
        }
        else
        {
            if (null != Session["LocalAdmin"] && Session["LocalAdmin"].ToString() == "true")
            {
                LB.Text = "Local Admin";
                Hlk.NavigateUrl = "frmAdminDefault.aspx";
            }
            else
                if (null != Session["Technician"] && Session["Technician"].ToString() == "true")
                {
                    LB.Text = "PC Technician";
                    //Hlk.NavigateUrl = "frmTechRequestList.aspx?id=" + Session["UserID"].ToString();
                    Hlk.NavigateUrl = "frmTechnicianDefault.aspx";
                }
                else
                    if (null != Session["ITMember"] && Session["ITMember"].ToString() == "true")
                {
                    LB.Text = "User";
                    Hlk.NavigateUrl = "frmAdminDefault.aspx";
                }
                    else
                {
                    LB.Text = "User";
                    Hlk.NavigateUrl = "frmUserRequestList.aspx";
                }

        }
    }
    protected void m_evaluate_technicians(object sender, EventArgs e)
    {
        try
        {
            loadTechEvaluationReport();
        }
        catch (Exception exp)
        {
            Response.Write(exp.Message.ToString());
        }
      
    }
    protected void loadTechEvaluationReport()
    {
        //=====================================================//
        /// <summary>
        /// Description:This function will be used to evaluate pc technicians according to the selected creteria
        /// Author: mutawakelm
        /// Date :24/05/2008 10:54:07 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            techEvaluationReport = CreateCrystalReportDocument("rptTechEvaluation.rpt");
            ArrayList technicianNames = new ArrayList();

            DataTable techEvaluationTbl = new DataTable();
            techEvaluationTbl.Columns.Add("tech_name");//Technician name
            techEvaluationTbl.Columns.Add("creteria");//Evaluation criteria
            techEvaluationTbl.Columns.Add("tasks_no", Type.GetType("System.Double"));//tasks no
            techEvaluationTbl.Columns.Add("average_rating", Type.GetType("System.Double"));//Duration average
            techEvaluationTbl.Columns.Add("average_duration", Type.GetType("System.Double"));//Rating average
            int intTasksNo = 0;//This variable will hold the number of tasks
            int intTotalRating = 0;//This variable will hold the total number of rating
            double duration_average = 0.0;//This variable will hold the average of duration
            double rating_average = 0.0;//This variable will hold the average of rating
            int intRatedTasks = 0;//This variable will hold the number of tasks which were rated
            TextObject txtRptTitle;//This variable will hold the titile of the report
            TextObject txtFromDate;//This variable will hold the starting date of the report
            TextObject txtToDateLbl;//This variable will hold the ending date of the report
            TextObject txtPrinter;//This variable will hold the name of the user who print the report
            string strTechQuery = "SELECT id,full_name FROM t_users WHERE user_group='2'";//This query will select the names of pc technicians
            reader = GeneralClass.Program.gRetrieveRecord(strTechQuery);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    technicianNames.Add(reader["id"].ToString() + "|" + reader["full_name"].ToString());

                } reader.Close();
            }
            else reader.Close();
            //The following code will be used to 
            if (!((string.IsNullOrEmpty(txtDeliveryDate.Text.ToString())) && (string.IsNullOrEmpty(txtToDate.Text.ToString()))))
            {
                DateTime fromDateTime = Convert.ToDateTime(txtDeliveryDate.Text.ToString());
                DateTime toDateTime = Convert.ToDateTime(txtToDate.Text.ToString());
                this.lblTechEval.Text = "";
                //The following if condition will check if the category is specified or for all categories
                if (ddlEvaluationCreteria.SelectedItem.Text == "All")
                {
                    for (int i = 0; i < technicianNames.Count; i++)
                    {
                        intTasksNo = 0;
                        intTotalRating = 0;
                        System.TimeSpan wholeDuration = new TimeSpan(0);
                        string[] strTechSplitter = technicianNames[i].ToString().Split('|');
                        string strTechHistory = "SELECT     dbo.t_requests.requester_name,dbo.t_requests.id, dbo.t_requests.created_by, dbo.t_RequestServiceHistory.status, dbo.t_RequestServiceHistory.modified_date, dbo.t_assignment.assigned_date, " +
                              " dbo.t_category.Category, dbo.t_requests.requester_rate" +
                              " FROM         dbo.t_users INNER JOIN" +
                              " dbo.t_RequestServiceHistory ON dbo.t_users.id = dbo.t_RequestServiceHistory.serviced_by INNER JOIN" +
                              " dbo.t_requests ON dbo.t_RequestServiceHistory.request_id = dbo.t_requests.id INNER JOIN" +
                              " dbo.t_assignment ON dbo.t_requests.id = dbo.t_assignment.request_id INNER JOIN" +
                              " dbo.t_category ON dbo.t_requests.category_id = dbo.t_category.id" +
                              " WHERE     (dbo.t_users.user_group = '2') AND (dbo.t_users.id = '" + strTechSplitter[0] + "') AND  (dbo.t_RequestServiceHistory.status='" + ddlCategoryStatus.SelectedItem.Text.ToString() + "')";//This query for tasks of a pc technician
                        reader = GeneralClass.Program.gRetrieveRecord(strTechHistory);
                        if (reader.HasRows)
                        {
                            time_schedule t = new time_schedule();
                            while (reader.Read())
                            {
                                DateTime startDate = Convert.ToDateTime(reader["assigned_date"].ToString());
                                DateTime endDate = Convert.ToDateTime(reader["modified_date"].ToString());
                                if ((startDate >= fromDateTime) && (startDate <= toDateTime))
                                {
                                    t.setStartDate(startDate);
                                    t.setEndDate(endDate);
                                    System.TimeSpan difference = t.getTimeDifference();
                                    wholeDuration += difference;
                                    //The following code will determine days
                                    intTasksNo++;
                                    if (!string.IsNullOrEmpty(reader["requester_rate"].ToString()))
                                    {
                                        if (reader["requester_rate"].ToString() != "0")
                                        {
                                            intTotalRating += int.Parse(reader["requester_rate"].ToString());
                                            intRatedTasks++;
                                        }
                                    }
                                }


                            }
                            reader.Close();
                            //The following code will fill the table

                            duration_average = wholeDuration.TotalHours / intTasksNo;
                            if (intRatedTasks != 0)
                                rating_average = ((double.Parse(intTotalRating.ToString()) / double.Parse(intRatedTasks.ToString())) / 5) * 100;
                            else
                                rating_average = 0.0;
                            techEvaluationTbl.Rows.Add(strTechSplitter[1], ddlEvaluationCreteria.SelectedItem.Text.ToString(), double.Parse(intTasksNo.ToString()), double.Parse(duration_average.ToString()), double.Parse(rating_average.ToString()));
                        }
                        else reader.Close();
                    }
                }
                else
                {
                    for (int i = 0; i < technicianNames.Count; i++)
                    {
                        intTasksNo = 0;
                        intTotalRating = 0;
                        System.TimeSpan wholeDuration = new TimeSpan(0);
                        string[] strTechSplitter = technicianNames[i].ToString().Split('|');
                        string strTechHistory = "SELECT     dbo.t_requests.requester_name,dbo.t_requests.id, dbo.t_requests.created_by, dbo.t_RequestServiceHistory.status, dbo.t_RequestServiceHistory.modified_date, dbo.t_assignment.assigned_date, " +
                              " dbo.t_category.Category, dbo.t_requests.requester_rate" +
                              " FROM         dbo.t_users INNER JOIN" +
                              " dbo.t_RequestServiceHistory ON dbo.t_users.id = dbo.t_RequestServiceHistory.serviced_by INNER JOIN" +
                              " dbo.t_requests ON dbo.t_RequestServiceHistory.request_id = dbo.t_requests.id INNER JOIN" +
                              " dbo.t_assignment ON dbo.t_requests.id = dbo.t_assignment.request_id INNER JOIN" +
                              " dbo.t_category ON dbo.t_requests.category_id = dbo.t_category.id" +
                              " WHERE     (dbo.t_users.user_group = '2') AND (dbo.t_users.id = '" + strTechSplitter[0] + "') AND (dbo.t_requests.category_id = '" + ddlEvaluationCreteria.Text.ToString() + "') AND (dbo.t_RequestServiceHistory.status='" + ddlCategoryStatus.SelectedItem.Text.ToString() + "')";//This query for tasks of a pc technician
                        reader = GeneralClass.Program.gRetrieveRecord(strTechHistory);
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                DateTime startDate = Convert.ToDateTime(reader["assigned_date"].ToString());
                                DateTime endDate = Convert.ToDateTime(reader["modified_date"].ToString());
                                if ((startDate >= fromDateTime) && (startDate <= toDateTime))
                                {

                                    System.TimeSpan difference = endDate - startDate;
                                    wholeDuration += difference;
                                    //The following code will determine days
                                    intTasksNo++;
                                    if (!string.IsNullOrEmpty(reader["requester_rate"].ToString()))
                                    {
                                        if (reader["requester_rate"].ToString() != "0")
                                        {
                                            intTotalRating += int.Parse(reader["requester_rate"].ToString());
                                            intRatedTasks++;
                                        }
                                    }
                                }


                            }
                            reader.Close();
                            //The following code will fill the table

                            duration_average = wholeDuration.TotalHours / intTasksNo;
                            if (intRatedTasks != 0)
                                rating_average = ((double.Parse(intTotalRating.ToString()) / double.Parse(intRatedTasks.ToString())) / 5) * 100;
                            else
                                rating_average = 0.0;
                            techEvaluationTbl.Rows.Add(strTechSplitter[1], ddlEvaluationCreteria.SelectedItem.Text.ToString(), double.Parse(intTasksNo.ToString()), double.Parse(duration_average.ToString()), double.Parse(rating_average.ToString()));
                        }
                        else reader.Close();
                    }
                }
                //The following code will be used to assign the table to the report
                techEvaluationReport.SetDatabaseLogon("sa", "dbadmin");
                techEvaluationReport.Database.Tables["tblTechEvaluation"].SetDataSource((DataTable)techEvaluationTbl);//assign the resulted data table to the abstract table of the dataset
                techEvaluationReport.Refresh();
                txtRptTitle = (TextObject)techEvaluationReport.ReportDefinition.ReportObjects["txtRptTitle"];
                txtRptTitle.Text = "Technician Evaluation Report By (" + ddlEvaluationCreteria.SelectedItem.Text.ToString() + ") Completed Tasks";
                txtFromDate = (TextObject)techEvaluationReport.ReportDefinition.ReportObjects["txtFromDate"];
                txtFromDate.Text = txtDeliveryDate.Text.ToString();
                txtToDateLbl = (TextObject)techEvaluationReport.ReportDefinition.ReportObjects["txtToDate"];
                txtToDateLbl.Text = txtToDate.Text.ToString();
                txtPrinter = (TextObject)techEvaluationReport.ReportDefinition.ReportObjects["txtPrintedBy"];
               txtPrinter.Text = Session["UserFullName"].ToString();
                this.CrystalReportViewer1.ReportSource = techEvaluationReport;//to assign the transcript report file to the form report viewer
               
            }
            else
            {
                this.lblTechEval.Visible = true;
                this.lblTechEval.Text = "Please select begining date and end date for the report";
            }

        }
        catch (OdbcException m_evaluate_technicians_Exp)
        {
            if (reader != null)
                reader.Close();
            Response.Write(m_evaluate_technicians_Exp.Message.ToString());

        }
    }
    protected void mDeterminePerioud(object sender, EventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to determine the perioud of time that seleted.
        /// Author: mutawakelm
        /// Date :1/24/2009 2:25:15 PM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            DateTime dtToday = DateTime.Now;
            DateTime MonthStartDay = new DateTime(dtToday.Year, dtToday.Month, 1);//This varaiable represent the first day of the month
            DateTime MonthEndDay = new DateTime(dtToday.Year, dtToday.Month, 28);//This vriable represent the ending day of the month
            DateTime WeekStartDay=dtToday;
            DateTime WeekEndDay=dtToday;
            DateTime todayStartTime;
            DateTime todayEndTime;
            //The following condition will used to determine the start and end days of the current month
            if (ddlPerioud.Text == "month")
            {
                txtDeliveryDate.Text = MonthStartDay.ToString();
                if (dtToday < MonthEndDay)
                    txtToDate.Text = dtToday.ToString();
                else
                    txtToDate.Text = MonthEndDay.ToString();
                this.CrystalReportViewer1.ReportSource = null;
            }
            else
                //The following condition will used to determine the start and end days of the current week
                if (ddlPerioud.Text == "week")
                {
                    //The following segment of code will determine the first day of the week
                    for (int i = 0; i < 7; i++)
                    {
                        if (WeekStartDay.DayOfWeek.ToString() == "Saturday")
                        {
                            txtDeliveryDate.Text = WeekStartDay.ToString();
                            break;
                        }
                        else
                            WeekStartDay = WeekStartDay.AddDays(-1);
                    }
                    txtToDate.Text = dtToday.ToString();

                    this.CrystalReportViewer1.ReportSource = null;
                }
                else
                    if (ddlPerioud.Text == "today")
                    {
                        todayStartTime = new DateTime(dtToday.Year, dtToday.Month, dtToday.Day, 8, 0, 0);
                        todayEndTime = dtToday;
                        txtDeliveryDate.Text = todayStartTime.ToString();
                        txtToDate.Text = todayEndTime.ToString();
                        this.CrystalReportViewer1.ReportSource = null;
                    }

            
        }
        catch (Exception exp)
        {
            Response.Write(exp.Message.ToString());
        }
    }
    protected void mUsersReqReportLinkClicked(object sender , EventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to call the function which will fill the user request report " rptUsersRequest.rpt"
        /// Author: mutawakelm
        /// Date :1/25/2009 10:40:20 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            mLoadUsersReqReport();
        }
        catch (Exception exp)
        {
        }
      
    }
    protected void mLoadUsersReqReport()
    {
        //=====================================================//
        /// <summary>
        /// Description:This function will be used to display a report of users requests
        /// Author: mutawakelm
        /// Date :1/24/2009 4:32:19 PM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            TextObject txtFromDate;//This variable will hold the starting date of the report
            TextObject txtToDateLbl;//This variable will hold the ending date of the report
            TextObject txtPrinter;//The full name of person who print the report
            
            if (!((string.IsNullOrEmpty(txtDeliveryDate.Text.ToString())) && (string.IsNullOrEmpty(txtToDate.Text.ToString()))))
            {
                string strUsersReqQuery = "select count(t_requests.id) as 'orders_no',t_users.full_name from t_requests join t_users on t_requests.created_by=t_users.id where ((t_requests.created_date < '" + txtToDate.Text + "')AND (t_requests.created_date < '" + txtDeliveryDate.Text + "'))  group by t_users.full_name order by orders_no desc";
                DataTable userRequestDataTable = new DataTable();
                userRequestDataTable.Columns.Add("user_name");
                userRequestDataTable.Columns.Add("requests_no");
                reader = GeneralClass.Program.gRetrieveRecord(strUsersReqQuery);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        userRequestDataTable.Rows.Add(reader["full_name"].ToString(), reader["orders_no"].ToString());
                    }
                    reader.Close();
                }
                else reader.Close();
                //The following code will assign the data table to the dataset "usersRequestsDataset.xsd"

                userRequestsReport = CreateCrystalReportDocument("rptUsersRequest.rpt");
                userRequestsReport.SetDatabaseLogon("sa", "dbadmin");
                userRequestsReport.Database.Tables["userRequestTable"].SetDataSource((DataTable)userRequestDataTable);//assign the resulted data table to the abstract table of the dataset
                userRequestsReport.Refresh();
                txtFromDate = (TextObject)userRequestsReport.ReportDefinition.ReportObjects["txtFromDate"];
                txtFromDate.Text = txtDeliveryDate.Text.ToString();
                txtToDateLbl = (TextObject)userRequestsReport.ReportDefinition.ReportObjects["txtToDate"];
                txtToDateLbl.Text = txtToDate.Text.ToString();
                txtPrinter = (TextObject)userRequestsReport.ReportDefinition.ReportObjects["txtPrintedBy"];
                txtPrinter.Text = Session["UserFullName"].ToString();
                this.CrystalReportViewer1.ReportSource = userRequestsReport;//to assign the transcript report file to the form report viewer
                this.lblUserRequestReprotError.Visible = false;
            }
            else
            {
                this.lblUserRequestReprotError.Visible = true;
                this.lblUserRequestReprotError.Text = "Please select begining date and end date for the report";
            }
        }
        catch (Exception exp)
        {
            if (reader != null)
                reader.Close();
            Response.Write(exp.Message.ToString());
        }
    }
   
}
