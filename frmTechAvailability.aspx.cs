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
using System.Data.Odbc;

public partial class _Default : System.Web.UI.Page
{
    OdbcDataReader reader = null;//This will hold the odbc data reader 

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            HyperLink LB1 = (HyperLink)this.Master.Page.Controls[0].Controls[3].FindControl("hlkLogin");
            DataTable technicianTable = new DataTable();//This data table will hold the information about pc technician and thier availability
            technicianTable.Columns.Add("ID");//This column will hold the technician ID
            technicianTable.Columns.Add("Full Name");//This column will hold the technician full name
            technicianTable.Columns.Add("status");//This column will hold the technician status
            technicianTable.Columns.Add("Building");//This column will hold the technician enrolled building
            if (Session.Count == 0)
            {
                Response.Redirect("error.aspx?error=Session Expired");
            }
            LB1.Text = "Log Out";       
            if (!IsPostBack)
            {
                try
                {
                    LoggedAs();
                    string technicianQuery = "SELECT u.id,u.full_name,u.status,b.building_name FROM t_users as u join t_buildingCode as b on u.building_code=b.building_id WHERE user_group='2'";
                    reader = GeneralClass.Program.gRetrieveRecord(technicianQuery);
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if (reader["status"].ToString() == "1")
                                technicianTable.Rows.Add(reader["id"].ToString(), reader["full_name"].ToString(), "Available", reader["building_name"].ToString());
                            else
                                if (reader["status"].ToString() == "2")
                                    technicianTable.Rows.Add(reader["id"].ToString(), reader["full_name"].ToString(), "Busy", reader["building_name"].ToString());
                                else
                                    if (reader["status"].ToString() == "3")
                                        technicianTable.Rows.Add(reader["id"].ToString(), reader["full_name"].ToString(), "Un available", reader["building_name"].ToString());
                        }
                        reader.Close();
                    }
                    else
                        reader.Close();


                    technicianGrid.DataSource = technicianTable;
                    technicianGrid.DataBind();

                    fillBuildingNamesDDL();
                    
                    

                }
                catch (Exception ex)
                {
                    Response.Redirect("error.aspx?error=" + ex.Message);
                }
                
            }
        }
        catch (Exception exp)
        {
        }

    }
    
    protected void fillBuildingNamesDDL()
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to fill the Building Names drop down list "ddlBuildingName"
        /// Author: mutawakelm
        /// Date :10/19/2010 3:12:18 PM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            string strBuildingNameQuery = "SELECT * FROM t_buildingCode";
            reader = GeneralClass.Program.gRetrieveRecord(strBuildingNameQuery);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ListItem building = new ListItem();
                    building.Text = reader["building_name"].ToString();
                    building.Value = reader["building_id"].ToString();
                    ddlBuilding.Items.Add(building);
                    
                }
                reader.Close();
            }
            else
                reader.Close();
        }
        catch (Exception exp)
        {
            if (reader != null)
                reader.Close();
        }
    }
  
    protected void technicianGrid_EditCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {

            string TechId = e.Item.Cells[2].Text.ToString();//This variable will hold the ID of the selected technician
            string TechStatus = e.Item.Cells[4].Text.ToString();//This variable will hold the status of the selected technician
            //The following condition statements will be used to change the status of the selected technician
            if (!string.IsNullOrEmpty(TechStatus))
            {
                if (TechStatus == "Available")
                {
                    GeneralClass.Program.Add("status", "2", "I");
                    GeneralClass.Program.UpdateRecordStatement("t_users", "id", "'" + TechId + "'");
                    mAddLog(Session["UserID"].ToString(), TechId, "Available", "Busy");
                }
                else
                    if (TechStatus == "Busy")
                    {
                        GeneralClass.Program.Add("status", "3", "I");
                        GeneralClass.Program.UpdateRecordStatement("t_users", "id", "'" + TechId + "'");
                        mAddLog(Session["UserID"].ToString(), TechId, "Busy", "Un Available");
                    }
                    else
                        if (TechStatus == "Un available")
                        {
                            GeneralClass.Program.Add("status", "1", "I");
                            GeneralClass.Program.UpdateRecordStatement("t_users", "id", "'" + TechId + "'");
                            mAddLog(Session["UserID"].ToString(), TechId, "Un Available","Available");
                        }
            }
            Response.Redirect("frmTechAvailability.aspx");
            
        }
        catch (Exception exp)
        {
            Response.Write(exp.Message.ToString());
        }
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
            
            Response.Redirect("./frmNewRequest.aspx");


        }
        catch (Exception NewRequest_Exp)
        {

        }
    }
    protected void mEnroll(object sender, EventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to enroll a building to the selected technicians.
        /// Author: mutawakelm
        /// Date :10/23/2010 11:02:59 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//

        try
        {
            foreach (DataGridItem dg in technicianGrid.Items)
            {
                CheckBox chk = (CheckBox)dg.FindControl("chkSelection");
                if (chk != null)
                {
                    if (chk.Checked == true)
                    {
                        GeneralClass.Program.Add("building_code",ddlBuilding.SelectedValue.ToString() , "I");
                        GeneralClass.Program.UpdateRecordStatement("t_users", "id", "'" + dg.Cells[2].Text + "'");
                        mAddLog(Session["UserID"].ToString(), dg.Cells[2].Text, dg.Cells[5].Text, ddlBuilding.SelectedItem.Text.ToString());
                        
                    }
                }
            }
            Response.Redirect("frmTechAvailability.aspx");

        }
        catch (Exception exp)
        {

        }
    }
    protected void mAddLog(string doneBy,string affectedBy,string previousStatus,string newStatus)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to add a log for any change of the technican status
        /// The situations of change are as following:
        /// 1- Change in the status.
        /// 2- Change the technician assigned building.
        /// Author: mutawakelm
        /// Date :10/23/2010 3:12:24 PM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            GeneralClass.Program.Add("done_by", doneBy, "S");
            GeneralClass.Program.Add("affected_by", affectedBy, "S");
            GeneralClass.Program.Add("previous_status", previousStatus, "S");
            GeneralClass.Program.Add("new_status", newStatus, "S");
            GeneralClass.Program.InsertRecordStatement("t_technician_status_log");
        }
        catch (Exception exp)
        {
        }

    }
    protected void LoggedAs()
    {
        /// <summary>
        /// 	Description: Set the home page 
        ///	
        ///
        /// 	Date:27/Aug/2007
        /// 	Author:Muta
        /// 	Parameter:loginID
        ///		input:
        ///		output: 
        /// 	Example: 
        /// </summary>
        /// 

      
        //The following code will be used to assign the home link
        HyperLink Hlk = (HyperLink)this.Master.Page.Controls[0].Controls[3].Controls[5];
        Hlk.NavigateUrl = "frmAdminDefault.aspx";

                   
        
    }
  
}
