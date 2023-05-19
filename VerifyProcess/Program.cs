// See https://aka.ms/new-console-template for more information
using System.ComponentModel.Design;
using VerifyProcess;

var logger = NLog.LogManager.GetCurrentClassLogger();
logger.Info("Test");
logger.Error("Test Error");
Thread secondThread = new Thread(FirstExecutor);
secondThread.Start();

ConsoleKeyInfo cki;
Console.WriteLine("Press Q to stop the application if needed!");
cki = Console.ReadKey();
if (cki.Key == ConsoleKey.Q)
    Environment.Exit(0);

void FirstExecutor()
{
    var commands = ProcessExecutor.ArgumentsChecker();
    
    logger.Info("App Started {arguments}", commands);

    var x = ProcessExecutor.Executor(commands[1], commands[2], commands[3]);
    Console.WriteLine(x);
}