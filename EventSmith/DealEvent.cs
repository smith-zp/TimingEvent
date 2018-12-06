using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 定时事件
{
    public class DealEvent
    {
        //Action<string> methodCall = (x) => { x += "haha"; MessageBox.Show(x); };
        //public event Action<string> BoilerEventLog;
        public event Action<string, string> dealMsg;

        public void IsEqual(DateTime zreoDate, DateTime nowDate, string uid, string msg,out bool reslut)
        {
            reslut = false;
            DateTime _zreoDate = new DateTime(zreoDate.Year, zreoDate.Month, zreoDate.Day, zreoDate.Hour, zreoDate.Minute, zreoDate.Second);
            DateTime _nowDate = new DateTime(nowDate.Year, nowDate.Month, nowDate.Day, nowDate.Hour, nowDate.Minute, nowDate.Second);

            if (_zreoDate == _nowDate)
            {
                reslut = true;
                Console.WriteLine("id： {0}", uid);
                dealMsg?.Invoke(uid, msg);
            }

        }
    }
}
