// See https://aka.ms/new-console-template for more information
using System.Diagnostics;
using VerifyProcess;

Console.WriteLine("Hello, World!");

var x = ProcessExecutor.Executor("notepad", 1, 1);
Console.WriteLine(x);
Console.ReadLine();