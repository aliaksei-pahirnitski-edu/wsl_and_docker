// See https://aka.ms/new-console-template for more information
using System;
using System.Diagnostics;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using ConsoleAppWsl;

// init
Console.OutputEncoding = Encoding.UTF8;
Console.WriteLine("Hello, World! Привет мир! " + DateTime.Now);
Console.WriteLine("OS=" + Environment.OSVersion);

// 0
Demo(Showcases.DisplayOSInfo_and_User);
// 1
Demo(Showcases.CurrentDirectory);
// 1b
Demo(Showcases.Environment_Get_and_Set);

// 2a: File not exists
// Demo(() => Showcases.File_Exists("unicorn%_x?*"));
// 2b: File Exists, mounted from win
// Demo(() => Showcases.File_Exists("ConsoleAppWsl.dll"));
// 2c: File Exists, mounted from win - case insensitive!!
Demo(() => Showcases.File_Exists("ConSOleappWsl.dll"));
// 2d: File Exists, mounted from ubuntu 
Demo(() => Showcases.File_Exists("/home/rootalex/alexfolder/file1.txt"));
// 2e: File mounted from ubuntu, case sensitive, so not exists!
Demo(() => Showcases.File_Exists("/home/rootalex/alexfolder/FilE1.txt"));
// 2d: File Exists, mounted from ubuntu, using ~
//Demo(() => Showcases.File_Exists("~/alexfolder/file1.txt"));


// 3a: Win Dir not exists
Demo(() => Showcases.Directory_Exists("c:/temp"));
// 3b: Dir ~ => false
// Demo(() => Showcases.Directory_Exists("~"));
// 3c: Dir not exists
// Demo(() => Showcases.Directory_Exists("/temp/abc"));
// 3d: Dir exists
Demo(() => Showcases.Directory_Exists("/home/rootalex/alexfolder/"));
// 3e: Dir exists (no matter ends with / or not)
// Demo(() => Showcases.Directory_Exists("/home/rootalex/alexfolder"));
// 3f: Dir not exists due to backslashes
// Demo(() => Showcases.Directory_Exists("\\home\\rootalex\\alexfolder"));
// 3g: Dir not exists due to case sensitive
// Demo(() => Showcases.Directory_Exists("/home/rootAlex/AlexFolder"));

// 4
// Demo(() => Showcases.Read_Write_Delete("/home/rootalex/alexfolder/"));


// 5
Demo(Showcases.Request_Mine_IP);

// 6
Demo(Showcases.Network);


// 7. Process
// Demo(() => Showcases.Process_Output("dir")); // only in linux
Demo(() => Showcases.Process_Output("whoami"));



// Demo(Showcases.DemoException);
Console.WriteLine("---------------------------------------------");


// finished
Console.WriteLine("Finishing ☺ ");
Thread.Sleep(1500);
Console.WriteLine("finished! ¤");


// local function
static void Demo(Action action, [CallerArgumentExpression(nameof(action))] string? argText = null)
{
    Stopwatch sw = Stopwatch.StartNew();
    try
    {
        Console.WriteLine();
        Console.WriteLine($"------- Starting: {argText} -------");
        action?.Invoke();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error happened: {ex.Message}");
        Console.WriteLine(ex);
    }
    finally
    {
        sw.Stop();
        Console.WriteLine($"Finished demo for: {argText} in {sw.ElapsedMilliseconds} ms");
    }
}