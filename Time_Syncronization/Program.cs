using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Time_Syncronization
{
    public struct SYSTEMTIME
    {
        public ushort wYear;
        public ushort wMonth;
        public ushort wDayOfWeek;
        public ushort wDay;
        public ushort wHour;
        public ushort wMinute;
        public ushort wSecond;
        public ushort wMilliseconds;
    }
    class Program
    {

        [DllImport("kernel32.dll", SetLastError = true)]
        public extern static uint SetSystemTime(ref SYSTEMTIME lpSystemTime);

        static void Main(string[] args)
        {
            System.DateTime dtDateTime;
            try
            {
                if (args != null && args.Length > 1)
                {
                    dtDateTime = Convert.ToDateTime(args[0] + " " + args[1]); // 서버로 부터 받아온 시간.yyyy-mm-dd hh:mm:ss

                    // 우리나라의 표준 시간대로 변경하기 위해서
                    dtDateTime = dtDateTime.AddHours(-9);



                    // 컴퓨터의 시간을 변경한다.
                    SYSTEMTIME ServerTime = new SYSTEMTIME();

                    ServerTime.wYear = (ushort)dtDateTime.Year;
                    ServerTime.wMonth = (ushort)dtDateTime.Month;
                    ServerTime.wDay = (ushort)dtDateTime.Day;
                    ServerTime.wHour = (ushort)dtDateTime.Hour;
                    ServerTime.wMinute = (ushort)dtDateTime.Minute;
                    ServerTime.wSecond = (ushort)dtDateTime.Second;
                    //ServerTime.wMilliseconds = (ushort)dtDateTime.Millisecond;

                    //ServerTime.wHour = (ushort)(ServerTime.wHour + 1 % 24);// Set the system clock ahead one hour. 
                    var status = SetSystemTime(ref ServerTime);
                }
            }
            catch (FormatException)
            {
                return;
            }
            catch (Exception)
            {
                return;
            }
        }
    }
}
