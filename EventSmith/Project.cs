using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;//定时处理需要引用

namespace DelegateUseInEvent
{
    /// <summary>
    /// 事件要是标示一个对象在一定条件下会运行的方法列表，其使用委托实现；
    /// 定时处理则是通过Timer的定时功能来实现在时间间隔内处理各种任务和方法的一种机制，其也采用委托实现。
    /// </summary>
    class Program1S
    {
        static void Main1(string[] args)
        {
            Console.WriteLine("当前托管线程ID为：" + Thread.CurrentThread.ManagedThreadId);
            EventEx();
            TimerEx();
            Console.ReadKey();//此处为了保证定时器能顺利执行，不然主线程运行结束则程序结束，定时器是不会运行的
        }

        #region 事件中的委托      
        /// <summary>
        /// 此处模拟一个烧水的过程，其中把需要烧的水定义为一个类（BoilingWater)，其中提供一个温度属性
        /// 随着温度的改变，如果达到100度的沸点，则触发定义的事件（WaterBoiled）。
        /// 而在实际执行时则给事件添加了两个模拟方法，一个模拟通知，一个模拟关闭烧水用电源
        /// </summary>
        static void EventEx()
        {
            BoilingWater etc = new BoilingWater();
            etc.WaterBoiled += etc_BoiledTest;//给类实例添加水沸腾的模拟方法
            etc.WaterBoiled += etc_BoiledTest2;//给类实例添加关闭电源的模拟方法
            for (int i = 98; i < 102; i++)//通过给对象温度赋值模拟烧水过程，
            {
                Console.WriteLine("给对象设置的温度为:" + i);
                etc.Temperature = i;
            }
        }

        //事件需调用方法一
        static void etc_BoiledTest(object o, EventArgs e)
        {
            BoilingWater SourceObject = (BoilingWater)o;
            Console.WriteLine("*********执行方法一*********");
            Console.WriteLine("通知：水已经沸腾！水的温度为：" + SourceObject.Temperature);
            Console.WriteLine("托管线程ID为：" + Thread.CurrentThread.ManagedThreadId);
        }

        //事件都调用方法二
        static void etc_BoiledTest2(object o, EventArgs e)
        {
            BoilingWater SourceObject = (BoilingWater)o;
            Console.WriteLine("*********执行方法二*********");
            Console.WriteLine("操作：关闭电源！水的温度为：" + SourceObject.Temperature);
        }

        //定义对象即其支持的事件
        class BoilingWater
        {
            public delegate void EventTest(object o, EventArgs e);
            public event EventTest WaterBoiled;//定义一个事件，
            private int _Temperature;
            public int Temperature//定义一个温度属性
            {
                get
                {
                    return _Temperature;
                }
                set
                {
                    _Temperature = value;
                    if (value == 100)//若水达到100度即沸腾则触发事件
                    {
                        OnEventTest();
                    }
                }
            }

            //触发事件的过程
            private void OnEventTest()
            {
                if (WaterBoiled != null)//触发事件时先判断是否为空
                {
                    WaterBoiled(this, new EventArgs());
                }
            }
        }
        #endregion

        #region 定时处理中的委托
        /// <summary>
        /// .NET的System.Threading中提供了定时器，并且提供了回调委托类型（TimerCallback），以实现定时处理
        /// </summary>
        static void TimerEx()
        {
            //TimerCallback是指向一个object类型参数的无返回值方法的委托，供定时器回调使用
            TimerCallback timerCallback = new TimerCallback(Method1);//把Method1的方法加入委托中
            timerCallback += o => { Console.WriteLine("定时调用方法：Lambda表达式方法"); };//把lambda表达式加入委托中
            timerCallback += delegate (object o)//把匿名方法加入到委托中
            {
                Console.WriteLine("定时调用方法：匿名方法");
            };

            //使用定时器定时调用委托中引用的方法
            Timer timer = new Timer(timerCallback,//参数定义定时时处理的方法或方法列表（以委托实现）
                null,                             //此参数定义传递给方法的object类型参数，用null表明不传递参数
                0,                                //此参数表明定时器开始的时间，0标示理解开始
                1000);                            //此参数标示定时处理的事件间隔，以毫秒为单位
        }

        //定义定时处理的方法
        static void Method1(object o)
        {
            Console.WriteLine("定时调用方法：Method1；执行时间：" + DateTime.Now.ToLongTimeString());
            Console.WriteLine("托管线程ID为：" + Thread.CurrentThread.ManagedThreadId);
        }
        #endregion
    }

}
