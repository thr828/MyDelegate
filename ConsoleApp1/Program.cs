using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Main Begin current thread {Thread.CurrentThread.ManagedThreadId}");
            //DoSomeThing();
            Action<string> action = new Action<string>(DoSomeThing);
            //action.Invoke();  委托的同步调用
            AsyncCallback asyncCallback = (t) => 
            {
                Console.WriteLine(t.AsyncState);
                Console.WriteLine($"this is callback current thread {Thread.CurrentThread.ManagedThreadId}");
            };
            IAsyncResult ar= action.BeginInvoke("", asyncCallback, "hi");//委托的异步调用
            //while (!ar.IsCompleted)   第一种
            //{
            //    Thread.Sleep(100);
            //    Console.WriteLine($"************等待******{ Thread.CurrentThread.ManagedThreadId}");
            //}
            //ar.AsyncWaitHandle.WaitOne();  第二种
            //ar.AsyncWaitHandle.WaitOne(-1);
            //ar.AsyncWaitHandle.WaitOne(200);
            action.EndInvoke(ar);//第三种


            {
                Func<string, int> func = (t) => {
                    Console.WriteLine(t);
                    return DateTime.Now.Year;
                };
                //AsyncCallback callback = (t) => {
                //    int res1 = func.EndInvoke(t);
                //    Console.WriteLine(res1);
                //};

                IAsyncResult ar1= func.BeginInvoke("begin", null, null);
                int res = func.EndInvoke(ar1);
                Console.WriteLine(res);

            }



            Console.WriteLine($"Main End current thread {Thread.CurrentThread.ManagedThreadId}");
            Console.ReadKey();

        }

        private static void DoSomeThing(string name)
        {
            Console.WriteLine($"DoSomeThing Begin current thread {Thread.CurrentThread.ManagedThreadId}");
            long res = 0;
            for (int i = 0; i < 1000000000; i++)
            {
                res += i;
            }
            Console.WriteLine($"DoSomeThing End current thread {Thread.CurrentThread.ManagedThreadId}");
        }
    }
}
