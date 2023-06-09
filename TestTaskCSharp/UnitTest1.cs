using NUnit.Framework;
using VerifyProcess;

namespace TestTaskCSharp
{
    [TestFixture]
    public class Tests
    {
        [TestCase("notepad", "1", "1")] // usual test
        [TestCase("notepad", "1", "0")] // continous check
        [TestCase("notepad", "0", "0")] // instant terminate
        public void Test1(string process, string lifetime, string frequency)
        {
            //Process.Start(process + ".exe"); //This is optional
            var retMessage = ProcessExecutor.Executor(process, lifetime, frequency);
            Assert.That(ProcessExeReturns.The_process_was_terminated_because_it_exceeded_the_allowed_time, Is.EqualTo(retMessage));
        }

        //what if the lifetime is 1 minute and the application checks the process after 2 minutes? Should the app let the process run more than is specified or it should stop the process, ignoring the user request?
        //if the task specified this option, we would only set a more frequent (0s) check on back-end and ignore the user input
    }
}