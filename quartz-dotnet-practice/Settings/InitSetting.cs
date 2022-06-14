using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Settings
{
    public class InitSetting
    {
        private static object syncRoot = new Object();
        private static volatile InitSetting instance;
        public static InitSetting Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new InitSetting();
                        }
                    }
                }
                return instance;
            }
        }
        public string TokenCleaningCron { get; private set; }
        public void InitTokenCleaningCron(string TokenCleaningCron)
        {
            this.TokenCleaningCron = TokenCleaningCron;
        }
    }
}
