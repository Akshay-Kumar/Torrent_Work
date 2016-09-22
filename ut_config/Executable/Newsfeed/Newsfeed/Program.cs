using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newsfeed
{
    class Program
    {
        public static int global = 0;
        static void Main(string[] args)
        {
            System.Threading.Thread t = new System.Threading.Thread(Newsfeed.FeedAPI.feedScanner, 10000);
            t.Start();
        }
    }
}
