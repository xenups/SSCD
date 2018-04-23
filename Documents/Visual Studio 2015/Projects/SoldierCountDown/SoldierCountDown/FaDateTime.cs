using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SoldierCountDown
{
    public class FaDate
    {
        public int year;
        public int month;
        public int day;
        public int monthsDay;
        public int today;
        public int thisMonth;
        public int thisYear;
        public int toEndMonth;
    }

    class FaDateTime
    {   
        public FaDateTime()
        {

        }
        public int getthisMonthsDay()
        {
            
            var persianCal = new System.Globalization.PersianCalendar();
             int monthsDay = persianCal.GetDaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
           
            return monthsDay; 
        }
         public FaDate getFaDateTime(string FaDate)
        {
            FaDate date = new FaDate();
            try {
                string[] finishFaArray = FaDate.Split('/');
                
                date.year = Convert.ToInt32(finishFaArray[0]);
                date.month = Convert.ToInt32(finishFaArray[1]);
                date.day = Convert.ToInt32(finishFaArray[2]);
               
                
                var persianCal = new System.Globalization.PersianCalendar();
                date.monthsDay = persianCal.GetDaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
                date.today = persianCal.GetDayOfMonth(DateTime.Now);
                date.thisMonth = persianCal.GetMonth(DateTime.Now);
                date.thisYear = persianCal.GetYear(DateTime.Now);
                if (date.year == persianCal.GetYear(DateTime.Now) && date.month == persianCal.GetMonth(DateTime.Now))
                {
                    date.toEndMonth = date.day - date.today;

                }
                else {
                    date.today = persianCal.GetDayOfMonth(DateTime.Now);
                    date.toEndMonth = date.monthsDay - date.today + date.day;
                }
                
                return date;
            }
            catch(Exception ex)
            {
                date.year = 0;
                date.month = 0;
                date.day = 0;
                date.monthsDay = 0;
                return date;
            }
        }
    }
}
