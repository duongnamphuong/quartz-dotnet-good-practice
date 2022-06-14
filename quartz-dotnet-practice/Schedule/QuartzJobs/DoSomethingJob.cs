using LogUtil;
using Quartz;
using Schedule.SingletonClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Schedule.QuartzJobs
{
    class DoSomethingJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            int Counter = DoSomethingJobWorker.Instance.Counter++;
            Log4netLogger.Info(MethodBase.GetCurrentMethod().DeclaringType, $"[{Counter}]: [START] at {DateTime.UtcNow} UTC");
            var timeout = TimeSpan.FromMilliseconds(0);
            bool lockTaken = false;
            try
            {
                Monitor.TryEnter(DoSomethingJobWorker.Instance.ObjectToBeLockedDuringJobExecution, timeout, ref lockTaken);
                if (lockTaken)
                {
                    // Main work of the task.
                    Thread.Sleep(90000); // Simulation: Job processing takes longer than period.
                }
                else
                {
                    // If a new job execution occured while a previous one is still running
                    // (we know that thanks to the locked object),
                    // new job won't do anything
                }
            }
            finally
            {
                if (lockTaken)
                {
                    // release the locked object
                    Monitor.Exit(DoSomethingJobWorker.Instance.ObjectToBeLockedDuringJobExecution);
                }
            }
            Log4netLogger.Warn(MethodBase.GetCurrentMethod().DeclaringType, $"[{Counter}]: [FINISH] at {DateTime.UtcNow} UTC\n");
        }
    }
}
