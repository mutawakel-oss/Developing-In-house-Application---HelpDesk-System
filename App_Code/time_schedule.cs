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
/// Summary description for time_schedule
/// </summary>
public class time_schedule
{
    private DateTime dtStartDate;
    private DateTime dtEndDate ;
    private TimeSpan startWorkingTime=new TimeSpan(8,0,0);
    private TimeSpan endWorkingTimes=new TimeSpan(17,0,0);
    OdbcDataReader reader = null;//This is the database reader
    private TimeSpan emptyTime = new TimeSpan(0, 0, 0);
	public time_schedule()
	{
		//
		// TODO: Add constructor logic here
		//
        dtStartDate = new DateTime();
        dtEndDate = new DateTime();
	}
    public void setStartDate(DateTime prStartDate) { dtStartDate = prStartDate; }
    public void setEndDate(DateTime prEndDate) { dtEndDate=prEndDate; }
    public TimeSpan getTimeDifference()
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to get the difference of working times according to 
        /// working times
        /// Author: mutawakelm
        /// Date :3/8/2009 3:08:26 PM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            TimeSpan assigningDayTimeSpan;
            TimeSpan complettionDayTimeSpan;
            DateTime tempStartDay;
            DateTime tempEndDay;
            int middleDays = 0;
            int middleDaysHours = 0;
            TimeSpan middleDayHoursTimeSpan;
            TimeSpan workingHoursTimeSpan;
            TimeSpan totalHoursTimeSpan;
            //The following function calls will reset the start date and end date
            mSetStartDate();
            mSetEndDate();
            //The following code will be used to calculate the time difference 
            if (dtStartDate.Date == dtEndDate.Date)
            {
                complettionDayTimeSpan = dtEndDate.TimeOfDay - dtStartDate.TimeOfDay;
                assigningDayTimeSpan = new TimeSpan(0, 0, 0);
            }
            else
            {
                assigningDayTimeSpan = endWorkingTimes - dtStartDate.TimeOfDay;
                complettionDayTimeSpan = dtEndDate.TimeOfDay - startWorkingTime;
            }
            //The following code will determine the middle days
            tempStartDay = dtStartDate.AddDays(1) ;
            tempEndDay=dtEndDate;
            while(tempStartDay.Date<tempEndDay.Date)
            {
                if ((tempStartDay.DayOfWeek.ToString() != "Thursday") && (tempStartDay.DayOfWeek.ToString() != "Friday"))
                    middleDays++;
                tempStartDay = tempStartDay.AddDays(1);
                
            }
            workingHoursTimeSpan = endWorkingTimes - startWorkingTime;
            middleDaysHours = middleDays * int.Parse(workingHoursTimeSpan.Hours.ToString());
            //The following code will calculate the total hours 
            middleDayHoursTimeSpan=new TimeSpan(middleDaysHours,0,0);
            totalHoursTimeSpan = assigningDayTimeSpan + complettionDayTimeSpan;
            totalHoursTimeSpan = totalHoursTimeSpan.Add(middleDayHoursTimeSpan);
            return totalHoursTimeSpan;
        }
        catch (Exception exp)
        {
            if (reader != null)
                reader.Close();
            return emptyTime;
        }
    }
    protected void mSetStartDate()
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to reset the start date if it is before or after working times
        /// Author: mutawakelm
        /// Date :3/8/2009 3:47:30 PM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            if (dtStartDate.DayOfWeek.ToString() == "Thursday")
            {
                dtStartDate = dtStartDate.AddDays(2);
                dtStartDate = new DateTime(dtStartDate.Year, dtStartDate.Month, dtStartDate.Day, startWorkingTime.Hours, startWorkingTime.Minutes, startWorkingTime.Seconds);
            }
            else
                if (dtStartDate.DayOfWeek.ToString() == "Friday")
                {
                    dtStartDate = dtStartDate.AddDays(1);
                    dtStartDate = new DateTime(dtStartDate.Year, dtStartDate.Month, dtStartDate.Day, startWorkingTime.Hours, startWorkingTime.Minutes, startWorkingTime.Seconds);
                }
                else

                    if (dtStartDate.TimeOfDay < startWorkingTime)
                        dtStartDate = new DateTime(dtStartDate.Year, dtStartDate.Month, dtStartDate.Day, startWorkingTime.Hours, startWorkingTime.Minutes, startWorkingTime.Seconds);
            else
                if (dtStartDate.TimeOfDay > endWorkingTimes)
                {
                    dtStartDate = new DateTime(dtStartDate.Year, dtStartDate.Month, dtStartDate.Day, startWorkingTime.Hours, startWorkingTime.Minutes, startWorkingTime.Seconds);
                    dtStartDate = dtStartDate.AddDays(1);
                }
        }
        catch (Exception exp)
        {
        }
    }
    protected void mSetEndDate()
    {
        //=====================================================//
        /// <summary>
        /// Description:This function will be used to reset the end date if it is before or after working times
        /// Author: mutawakelm
        /// Date :3/8/2009 3:47:30 PM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            if (dtEndDate.DayOfWeek.ToString() == "Thursday")
            {
                dtEndDate = dtEndDate.AddDays(-1);
                dtEndDate = new DateTime(dtEndDate.Year, dtEndDate.Month, dtEndDate.Day, endWorkingTimes.Hours, endWorkingTimes.Minutes, endWorkingTimes.Seconds);
            }
            else
                if (dtEndDate.DayOfWeek.ToString() == "Friday")
                {
                    dtEndDate = dtEndDate.AddDays(-2);
                    dtEndDate = new DateTime(dtEndDate.Year, dtEndDate.Month, dtEndDate.Day, endWorkingTimes.Hours, endWorkingTimes.Minutes, endWorkingTimes.Seconds);
                }
                else

            if (dtEndDate.TimeOfDay < startWorkingTime)
                dtEndDate = new DateTime(dtEndDate.Year, dtEndDate.Month, dtEndDate.Day, startWorkingTime.Hours, startWorkingTime.Minutes, startWorkingTime.Seconds);
            else
                if (dtEndDate.TimeOfDay > endWorkingTimes)
                {
                    dtEndDate = new DateTime(dtEndDate.Year, dtEndDate.Month, dtEndDate.Day, endWorkingTimes.Hours, endWorkingTimes.Minutes, endWorkingTimes.Seconds);
                    
                }
        }
        catch (Exception exp)
        {
        }
    }

}
