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
            int lifetime = Convert.ToInt32(time);
            int frequency = Convert.ToInt32(freq);
            DateTime startTime = DateTime.Now;

            while (true)
            {
                if (ProcessExists(process))
                {
                    startTime = DateTime.Now;
                    break;
                }
                Thread.Sleep(frequency * 60000);
            }

            DateTime endTime = startTime.AddMinutes(lifetime);

            while (true)
            {
                if (ProcessExists(process) && DateTime.Now < endTime)
                {
                    Thread.Sleep(frequency * 60000);
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
