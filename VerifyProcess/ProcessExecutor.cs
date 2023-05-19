using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VerifyProcess
{
    public enum ProcessExeReturns
    {
        The_process_was_terminated_unexpectedly_,
        The_process_was_terminated_because_it_exceeded_the_allowed_time,
    }
    public class ProcessExecutor
    {
        public static string[] ArgumentsChecker()
        {
            var commands = Environment.GetCommandLineArgs();
            bool stopper = true;
            do
            {
                if (commands.Length < 4 || commands.Length > 4)
                {
                    Console.WriteLine("Invalid number of arguments, please try again using this format:" +
                        " processName allowedTime frequencyCheck .");
                    commands = InputReader();
                }
                else
                {
                    bool firstCheck = int.TryParse(commands[2], out int value1);
                    bool secondCheck = int.TryParse(commands[3], out int value2);
                    if (firstCheck == true && secondCheck == true)
                        stopper = false;
                    else
                    {
                        Console.WriteLine("Last two parameters must be valid numbers! Please retry:");
                        commands = InputReader();
                    }
                }
            }
            while (stopper);

            return commands;
        }

        public static string[] InputReader()
        {
            var input = "x ";
            input += Console.ReadLine();
            string[] splitInput = input.Split(' ');
            return splitInput;
        }

        public static bool ProcessExists(string process)
        {
            Process[] procList = Process.GetProcessesByName(process);
            if (procList.Length > 0)
                return true;
            return false;
        }

        public static void KillProcess(string process)
        {
            Process[] procList = Process.GetProcessesByName(process);
            foreach (Process proc in procList)
                proc.Kill();
        }

        public static ProcessExeReturns Executor(string process, string time, string freq)
        {
            var lifetime = int.Parse(time);
            var frequency = int.Parse(freq);
            const int milliseconds = 60000;
            DateTime startTime = DateTime.Now;

            while (true)
            {
                if (ProcessExists(process))
                {
                    startTime = DateTime.Now;
                    break;
                }
                Thread.Sleep(frequency * milliseconds);
            }

            DateTime endTime = startTime.AddMinutes(lifetime);

            while (true)
            {
                if (ProcessExists(process) && DateTime.Now < endTime)
                {
                    Thread.Sleep(frequency * milliseconds);
                    continue;
                }
                else if (!ProcessExists(process))
                {
                    return ProcessExeReturns.The_process_was_terminated_unexpectedly_;
                }
                else
                {
                    KillProcess(process);
                    return ProcessExeReturns.The_process_was_terminated_because_it_exceeded_the_allowed_time;
                }
            }
        }
    }
}
