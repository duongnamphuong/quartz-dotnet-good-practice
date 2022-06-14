using Schedule.SingletonClasses;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quartz_dotnet_practice
{
    class Program
    {
        static void Main(string[] args)
        {
            Settings.InitSetting.Instance.InitTokenCleaningCron(ConfigurationManager.AppSettings["TokenCleaningCron"]);
            DoSomethingJobWorker.Instance.Initialize();
            DoSomethingJobWorker.Instance.Start();
            Console.WriteLine("Scheduler started. Press Enter to shut it down.");
            Console.ReadLine();
            DoSomethingJobWorker.Instance.Shutdown();
            Console.WriteLine("Scheduler shutted down. Press Enter to exit.");
            Console.ReadLine();
        }
    }
}
