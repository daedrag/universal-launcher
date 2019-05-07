using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLoader.Info
{
    internal class ProcessInfo
    {
        public string ProcessName { get; set; }
        public int PID { get; set; }
        public string CommandLine { get; set; }
        public string UserName { get; set; }
        public string UserDomain { get; set; }
        public string User
        {
            get
            {
                if (string.IsNullOrEmpty(UserName))
                {
                    return "";
                }
                if (string.IsNullOrEmpty(UserDomain))
                {
                    return UserName;
                }
                return string.Format("{0}\\{1}", UserDomain, UserName);
            }
        }
    }
}
