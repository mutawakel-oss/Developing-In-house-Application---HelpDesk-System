using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.Odbc;

/// <summary>
/// Summary description for tech
/// </summary>
public class task
{
    private    int  intTaskId = 0;//This variable will hold the id of the task
    private    int intTaskPrioriy = 0;//This variable will hold the technician name
    OdbcDataReader reader = null;//This is the database reader
    DataTable availableTechTbl1 = new DataTable();//This table will hold the data of all pc technicians
    
    public task()
	{
       
	}
    public void setTaskId(int  taskId) { intTaskId = taskId; }
    public int getTaskId() { return intTaskId; }
    public void setTaskPriority(int taskPriority) { intTaskPrioriy = taskPriority; }
    public int getTaskPriority() { return intTaskPrioriy; }
    public void  mGetMostPriority(string techinicanID,string strTechnicianBuilding)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to get the highest priority task and delet it and assign it
        /// to the pc technician.
        /// Author: mutawakelm
        /// Date :15/06/2008 03:55:42 PM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            DataTable queueTable = new DataTable();//This table will hold the requests in the "request_queue" table
            queueTable.Columns.Add();//This colomn will hold request id
            queueTable.Columns.Add();//This colomn will hold request priority
            queueTable.Columns.Add();//This colomn will hold request creation date            
            availableTechTbl1.Columns.Add();//This column will hold the id of the technician
            availableTechTbl1.Columns.Add();//This column will hold the token value which become '1' if the last task was assigned to him
            bool breakSearch = false;
            //The following code will be used to get the data for all the pc technicians
            string strAllTechQuery = "SELECT id,token FROM t_users WHERE  user_group='2' and building_code="+strTechnicianBuilding;
            reader = GeneralClass.Program.gRetrieveRecord(strAllTechQuery);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    availableTechTbl1.Rows.Add(reader["id"].ToString(), reader["token"].ToString());
                }
                reader.Close();
            }
            else
                reader.Close();
            //The following query will be used to get the requests in the waiting queue
            string strQueueQuery = "SELECT     TOP (100) PERCENT dbo.t_request_queue.req_id, dbo.t_requests.priority, dbo.t_requests.created_date" +
                                    " FROM         dbo.t_requests INNER JOIN" +
                                    " dbo.t_request_queue ON dbo.t_requests.id = dbo.t_request_queue.req_id WHERE dbo.t_request_queue.building_code="+strTechnicianBuilding +
                                    " ORDER BY dbo.t_requests.priority, dbo.t_requests.created_date";
            reader = GeneralClass.Program.gRetrieveRecord(strQueueQuery);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    queueTable.Rows.Add(reader["req_id"].ToString(), reader["priority"].ToString(), reader["created_date"].ToString());
                }
                reader.Close();
            }
            else reader.Close();
            //The following code will be used to choose the highest priority task
            string strHighPrID="";//This variable will hold the id of the highest priority request ID
            if (queueTable.Rows.Count > 0)
            {
                for (int j = 0; j < 4; j++)//This loop will iterate for the four priority types "low,normal,medium,critical".
                {
                    if (j == 0)//case of "high" priority
                    {
                        for (int i = 0; i < queueTable.Rows.Count; i++)
                        {
                            if (queueTable.Rows[i][1].ToString() == "High")
                            {
                                strHighPrID=queueTable.Rows[i][0].ToString();
                                breakSearch=true;
                                break;
                            }

                        }
                    }
                    else
                    if (j == 1)
                    {
                        for (int i = 0; i < queueTable.Rows.Count; i++)
                        {
                             if (queueTable.Rows[i][1].ToString() == "Medium")
                            {
                                strHighPrID=queueTable.Rows[i][0].ToString();
                                breakSearch=true;
                                break;
                            }
                        }
                    }
                    else
                    if (j == 2)
                    {
                        for (int i = 0; i < queueTable.Rows.Count; i++)
                        {
                            if (queueTable.Rows[i][1].ToString() == "Normal")
                            {
                                strHighPrID=queueTable.Rows[i][0].ToString();
                                breakSearch=true;
                                break;
                            }
                        }
                    }
                     else
                    if (j == 3)
                    {
                        for (int i = 0; i < queueTable.Rows.Count; i++)
                        {
                            if (queueTable.Rows[i][1].ToString() == "Low")
                            {
                                strHighPrID = queueTable.Rows[i][0].ToString();
                                breakSearch=true;
                                break;
                            }
                        }
                    }
                    if (breakSearch == true)
                        break;
                }
                //The following code will be used to assign the highest priorty task to the technician
                        
                        GeneralClass.Program.Add("request_id", strHighPrID.ToString(), "I");
                        GeneralClass.Program.Add("assigned_to", techinicanID, "S");

                        int intReturnID2 = GeneralClass.Program.InsertRecordStatement("t_assignment");
                        //The following code will be used to insert the mobile
                       
                        //The following code will be used to update the status of the selected pc techinican
                        GeneralClass.Program.Add("status", "2", "I");
                        GeneralClass.Program.UpdateRecordStatement("t_users", "id","'"+ techinicanID+"'");
                        //The following call will be used to assign the token to the technician
                        mAssignToken(techinicanID);//This call to assign token to the technician
                        //The following code will be used to update the status of the request in "t_requests"
                        GeneralClass.Program.Add("status_id", "2", "I");
                        GeneralClass.Program.UpdateRecordStatement("t_requests", "id",strHighPrID);
                        //The following code will be used to delete a row
                        GeneralClass.Program.gDeleteRecord("DELETE FROM t_request_queue WHERE req_id =" + strHighPrID);

                        
            }



        }
        catch (OdbcException exp)
        {
            if (reader != null)
                reader.Close();
        }
    }
    
    public void mAssignToken(string techId)
    {
        try
        {
            for (int i = 0; i < availableTechTbl1.Rows.Count; i++)
            {
                if (availableTechTbl1.Rows[i][0].ToString() != techId)
                {
                    GeneralClass.Program.Add("token", "0", "I");

                    int returnId = GeneralClass.Program.UpdateRecordStatement("t_users", "id", "'" + availableTechTbl1.Rows[i][0].ToString() + "'");
                }
                else
                    if (availableTechTbl1.Rows[i][0].ToString() == techId)
                    {
                        GeneralClass.Program.Add("token", "1", "I");
                        GeneralClass.Program.UpdateRecordStatement("t_users", "id", "'" + techId + "'");
                    }
            }

        }
        catch (Exception exp)
        {
        }
    }
}
