﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Filecopy
{
    class Program
    {
        static void Main()
        {
            string[] args = { @"C:\Users\AkshayKumar\Desktop\Torrent_Work\ut_config\Executable\Filecopy\Filecopy\a.txt",
                              @"C:\Users\AkshayKumar\Desktop\Torrent_Work\ut_config\Executable\Filecopy\Filecopy\Program2.cs"};
            char[] buffer = new char[1024];
            FileStream file = new FileStream(args[0].ToString(), FileMode.Open);
            StreamReader reader = new StreamReader(file);
            FileStream dest = new FileStream(args[1].ToString(), FileMode.Create);
            StreamWriter writer = new StreamWriter(dest);
            while (reader.Peek()>=0)
            {
                reader.Read(buffer, 0, 1024);
                writer.Write(buffer);
            }
        }
    }
}
