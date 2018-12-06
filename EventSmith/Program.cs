using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace 定时事件
{
    class Program
    {
        private static DateTime[] dt;
        private static string[] mt=new string[3];
        private static string[] ut = new string[3];
        private static DealEvent dl;
        private static System.Timers.Timer aTimer;
        static void Main(string[] args)
        {
            dl = new DealEvent();
            SentMsg sm = new SentMsg();
            SendWeChat sw = new SendWeChat();

            dl.dealMsg += sm.SentOndMsg;
            dl.dealMsg += sw.SentOndWeChat;

            dt = new DateTime[3];
            dt[0] = DateTime.Now.AddSeconds(5);
            dt[1] = DateTime.Now.AddSeconds(10);
            dt[2] = DateTime.Now.AddSeconds(15);

            mt[0] = "123"; mt[1] = "456"; mt[2] = "123";
            ut[0] = "啦啦"; ut[1] = "卡卡"; ut[2] = "嘻嘻";

            aTimer = new System.Timers.Timer(10000);

            //注册计时器的事件
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);

            //设置时间间隔为1秒（1000毫秒），覆盖构造函数设置的间隔
            aTimer.Interval = 1000;

            //设置是执行一次（false）还是一直执行(true)，默认为true
            aTimer.AutoReset = true;

            //开始计时
            aTimer.Enabled = true;
            Console.WriteLine("按任意键退出程序。");
            Console.ReadLine();
        }

        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            if (dt.Length != 0)
            {
                bool reslut;
                dl.IsEqual(dt[0], e.SignalTime, mt[0], ut[0],out reslut);
                if (reslut)
                {
                    List<DateTime> list = dt.ToList();
                    list.RemoveAt(0);
                    dt = list.ToArray();
                    List<string> list1 = mt.ToList();
                    list1.RemoveAt(0);
                    mt = list1.ToArray();
                    List<string> list2 = ut.ToList();
                    list2.RemoveAt(0);
                    ut = list2.ToArray();
                }
            }
            else { Console.WriteLine("没数据啦"); }
        }
    }
}
