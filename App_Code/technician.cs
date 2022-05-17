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
public class technician
{
    private    string intTechId = "";//This variable will hold the id of the technician
    private    string strTechName = "";//This variable will hold the technician name
    OdbcDataReader reader = null;//This is the database reader
    DataTable availableTechTbl = new DataTable();//This table will hold the data of available pc technicians
    DataTable availableTechTbl1 = new DataTable();//This table will hold the data of all pc technicians
    public technician()
	{
        intTechId = "";
        strTechName = "";
	}
    public void setTechId(string techId) { intTechId = techId; }
    public string getTechId() { return intTechId; }
    public void setTechName(string techName) { strTechName = techName; }
    public string getTechName() { return strTechName; }
    public string mCheckAvailability(string strBuildingCode)
    {

        //=====================================================//
        /// <summary>
        /// Description:The following function will be used to check the availability of
        /// a pc technician .
        /// Author: mutawakelm
        /// Date :14/06/2008 03:11:05 PM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        
        try
        {
            
            availableTechTbl.Columns.Add();//This column will hold the id of the technician
            availableTechTbl.Columns.Add();//This column will hold the token value which become '1' if the last task was assigned to him
            availableTechTbl1.Columns.Add();//This column will hold the id of the technician
            availableTechTbl1.Columns.Add();//This column will hold the token value which become '1' if the last task was assigned to him
            bool blAllZeroToken = true;
            bool founded = false;
            //The following code will be used to get the data for all the pc technicians
            string strAllTechQuery = "SELECT id,token FROM t_users WHERE  user_group='2' and building_code="+strBuildingCode;
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
             
            //The following code will be used to get the data for only available pc technicians
            string strTechQuery = "SELECT id,token FROM t_users WHERE status='1' and user_group='2' and building_code=" + strBuildingCode;
            reader = GeneralClass.Program.gRetrieveRecord(strTechQuery);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    availableTechTbl.Rows.Add(reader["id"].ToString(),reader["token"].ToString());
                }
                reader.Close();
                //The following code will process the technician table
                if (availableTechTbl.Rows.Count == 1)
                {
                    mAssignToken(availableTechTbl.Rows[0][0].ToString());//This call to assign token to the technician
                    return availableTechTbl.Rows[0][0].ToString();
                }
                else
                {
                    for (int i = 0; i < availableTechTbl.Rows.Count; i++)
                    {
                        if (availableTechTbl.Rows[i][1].ToString() == "1")
                        {
                            blAllZeroToken = false;
                            if (i == availableTechTbl.Rows.Count - 1)//case of last row in the table
                            {
                                mAssignToken(availableTechTbl.Rows[0][0].ToString());//This call to assign token to the technician
                                return availableTechTbl.Rows[0][0].ToString();
                            }
                            else
                            {
                                mAssignToken(availableTechTbl.Rows[i+1][0].ToString());//This call to assign token to the technician
                                return availableTechTbl.Rows[i+1][0].ToString();
                            }
                        }
                    }
                    if (blAllZeroToken == true)//The case of all available technicians have zero token
                    {
                       for (int j = 0; j < availableTechTbl1.Rows.Count; j++)
                        {
                            if (availableTechTbl1.Rows[j][1].ToString() == "1")
                            {
                                if (j == availableTechTbl1.Rows.Count - 1)
                                {
                                    
                                    mAssignToken(availableTechTbl.Rows[0][0].ToString());//This call to assign token to the technician
                                    return availableTechTbl.Rows[0][0].ToString();
                                }
                                else
                                {
                                    for (int z = j + 1; z < availableTechTbl1.Rows.Count; z++)
                                    {
                                        string nextTech = availableTechTbl1.Rows[j + 1][0].ToString();
                                        for (int s = 0; s < availableTechTbl.Rows.Count; s++)
                                        {
                                            if((availableTechTbl.Rows[s][0].ToString() == nextTech)&&(founded==false))
                                            {
                                                founded = true;
                                                mAssignToken(availableTechTbl.Rows[s][0].ToString());//This call to assign token to the technician
                                                return availableTechTbl.Rows[s][0].ToString();
                                            }
                                        }
                                       
                                    }
                                    if (founded == false)
                                    {
                                        mAssignToken(availableTechTbl.Rows[0][0].ToString());//This call to assign token to the technician
                                        return availableTechTbl.Rows[0][0].ToString();
                                    }
                                    
                                }
                            }
                            
                        }
                    }
                    return null;
                }
            }
            else
            {
                reader.Close();
                return null;
            }
        }
        catch (OdbcException exp)
        {
            if (reader != null)
                reader.Close();
            return null;
            
        }
    }
    public void mAssignToken(string techId)
    {
        try
        {
            for(int i=0;i<availableTechTbl1.Rows.Count;i++)
            {
                if(availableTechTbl1.Rows[i][0].ToString()!=techId)
                {
                    GeneralClass.Program.Add("token","0","I");
                    
                    int returnId =GeneralClass.Program.UpdateRecordStatement("t_users", "id","'"+ availableTechTbl1.Rows[i][0].ToString()+"'");
                }
                else
                    if(availableTechTbl1.Rows[i][0].ToString()==techId)
                    {
                    GeneralClass.Program.Add("token","1","I");
                    GeneralClass.Program.UpdateRecordStatement("t_users","id","'"+techId+"'");
                    }
            }

        }catch(Exception exp)
        {
        }
    }
}
