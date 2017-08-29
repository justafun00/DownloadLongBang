using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Common
{
    /// <summary>
    /// 线程池类
    /// </summary>
    public class ThreadPool
    {
        public int ConCurrentThread { get; set; }//线程池允许的总线程数
        public int Times { get; set; }//检查可使用线程的时间

        public int currentThread;//已使用线程数
        private Thread objThread = null;//新线程
        public ThreadPool(int conCurrentThread, int times)
        {
            this.ConCurrentThread = conCurrentThread;//初始化总线程数
            this.Times = times;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cycles"></param>
        /// <param name="executeMethod"></param>
        /// <param name="callBack"></param>
        public void PushThreadPool<T>(int cycles, Func<int, T> executeMethod, AsyncCallback callBack)
        {
            currentThread = 0;
            this.objThread = new Thread(() =>
            {
                for (int i = 0; i < cycles; i++)
                {
                    //如果并发线程数使用完了，则进入循环阻塞线程
                    while (currentThread == this.ConCurrentThread)
                    {
                        Thread.Sleep(this.Times);//触发间隔
                    }

                    //如果当前使用的并发线程数小于标准线程数，则继续执行
                    currentThread++;

                    executeMethod.BeginInvoke(i, callBack, null);

                }
            });
            objThread.IsBackground = true;
            objThread.Name = "MainThreadPool";
            objThread.Start();
        }

        /// <summary>
        /// 将要执行的方法推送到线程池中
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cycles">执行次数</param>
        /// <param name="executeMethod"></param>
        /// <param name="callBack"></param>
        public void PushThreadPool(int cycles, Action<int, Common.ThreadPool> executeMethod, AsyncCallback callBack)
        {
            for (int i = 0; i < cycles; i++)
            {
                //如果并发线程数使用完了，则进入循环阻塞线程
                while (currentThread == this.ConCurrentThread)
                {
                    Thread.Sleep(this.Times);//触发间隔
                }

                //如果当前使用的并发线程数小于标准线程数，则继续执行
                currentThread++;

                executeMethod.BeginInvoke(i,this, callBack, null);

            }
        }

        /// <summary>
        /// 中止线程池中的线程
        /// </summary>
        /// <returns></returns>
        public bool KillThread()
        {
            if (this.objThread != null && this.objThread.ThreadState != ThreadState.Stopped)
            {
                this.objThread.Abort();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
