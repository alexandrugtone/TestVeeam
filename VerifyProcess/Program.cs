// See https://aka.ms/new-console-template for more information
using VerifyProcess;

var commands = Environment.GetCommandLineArgs();

Thread secondThread = new Thread(FirstExecutor);
secondThread.Start();

ConsoleKeyInfo cki;
Console.WriteLine("Press Q to stop the application if needed!");
cki = Console.ReadKey();
if (cki.Key == ConsoleKey.Q)
    Environment.Exit(0);

void FirstExecutor()
{
    var x = ProcessExecutor.Executor(commands[0], commands[1], commands[2]);
    Console.WriteLine(x);
}