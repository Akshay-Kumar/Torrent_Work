using System;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using System.Threading;

namespace ut_amc_automation
{
    class CustomeFilebot
    {
        private string arguments = string.Empty;
        private string command = string.Empty;
        private ErrorLoggerListener logger = null;
        public void StartProcess(string[] args)
        {
            if (args != null)
            {
                if (args.Length > 0)
                {
                    try
                    {
                        string workingDir = Path.GetDirectoryName(
                        Assembly.GetExecutingAssembly().Location);
                        string logFile = Path.Combine(workingDir, "amc-cmd.log");
                        string batchFile = Path.Combine(workingDir, args[0]);
                        logger = new ErrorLoggerListener(logFile);
                        for (int i = 1; i < args.Length; i++)
                        {
                            arguments += "\"" + NVL(args[i], "") + "\"";
                            arguments += " ";
                        }

                        command = batchFile + " " + arguments.Trim();
                        ProcessStartInfo startInfo = new ProcessStartInfo("cmd", "/c " + command);
                        //startInfo.Arguments = arguments.Trim();
                        startInfo.UseShellExecute = false;
                        startInfo.RedirectStandardOutput = true;
                        startInfo.RedirectStandardError = true;
                        startInfo.WorkingDirectory = workingDir;
                        //startInfo.CreateNoWindow = true;
                        Process process = new Process();
                        process.StartInfo = startInfo;
                        process.OutputDataReceived += CaptureOutput;
                        process.ErrorDataReceived += CaptureError;
                        process.Start();
                        process.BeginOutputReadLine();
                        process.BeginErrorReadLine();
                        process.WaitForExit();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                else
                {
                    ShowOutput("No arguments passed.", ConsoleColor.Red);
                }
            }
            else
            {
                ShowOutput("No arguments passed.", ConsoleColor.Red);
            }
        }
        private void CaptureOutput(object sender, DataReceivedEventArgs e)
        {
            ShowOutput(e.Data, ConsoleColor.Green);
        }

        private void CaptureError(object sender, DataReceivedEventArgs e)
        {
            ShowOutput(e.Data, ConsoleColor.Red);
        }

        private void ShowOutput(string data, ConsoleColor color)
        {
            if (data != null)
            {
                string consoleOutput = string.Format("{0}", data);
                ConsoleColor oldColor = Console.ForegroundColor;
                Console.ForegroundColor = color;
                Console.WriteLine(consoleOutput);
                Console.ForegroundColor = oldColor;
                logger.Log_Error(consoleOutput);
            }
        }

        private string NVL(string input, string replacement)
        {
            if (string.IsNullOrEmpty(input))
                return replacement;
            return input;
        }
    }
}
