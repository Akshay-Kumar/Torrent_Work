using System;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using System.Threading;

namespace ut_amc_automation
{
    class Program
    {
        static void Main(string[] args)
        {
            new CustomeFilebot().StartProcess(args);
        }
        
    }
}
