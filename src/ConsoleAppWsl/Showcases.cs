using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppWsl
{
    /*
     * 0. OS Name and version 
     * 1. Current Directory
     * 2. File: Exists
     * 3. Directory: Exists
     * 4. FileInfo: Exists
     * 5. DirectoryInfo: Exists
     * 6. File: Read count bytes
     * 7. Directory: Count inner files (with .ttt and tttt.ttt)
     * 8. Write File "Hallo txt with date"
     * 
     * 9. Network: Download url, for example get IP
     * 10. Get Host Name
     * 11. Get External IP address
     * 12. Get current time
     * 13. NetworkInterface.GetIsNetworkAvailable 
     * 14. Exception
     * 
     * 15. Run process, example "whoami", "cowsay", "ls", tail file and get process output to stdout
     * 16. Console ReadLine 
     * 17. File extensions ??? Variations: mounted windows and inside linus VM!!!! todo !!!
     * 
     * 18. Current user
     * 19. Environment
     */
    internal static class Showcases
    {
        // 0. OS Info: Unix 5.15.133.1 vs Microsoft Windows NT 10.0.19045.0
        // 0a MachineName=LAPTOP-A1FDEMNE (both win and ubuntu)
        // 0b UserName=rootalex vs ; UserInteractive=True; UserDomainName=LAPTOP-A1FDEMNE;
        // 0c Environment.GetLogicalDrives=[several logical drives: /mnt/wsl ; /usr/lib/wsl/drivers ; / ; /mnt/wslg ; /mnt/wslg/distro ; /usr/lib/wsl/lib ; /init ; /dev ; /sys ; /proc ; /dev/pts ; /run ; /run/lock ; /run/shm ; /dev/shm ; /run/user ; /proc/sys/fs/binfmt_misc ; /sys/fs/cgroup ; /sys/fs/cgroup/unified ; /sys/fs/cgroup/cpuset ; /sys/fs/cgroup/cpu ; /sys/fs/cgroup/cpuacct ; /sys/fs/cgroup/blkio ; /sys/fs/cgroup/memory ; /sys/fs/cgroup/devices ; /sys/fs/cgroup/freezer ; /sys/fs/cgroup/net_cls ; /sys/fs/cgroup/perf_event ; /sys/fs/cgroup/net_prio ; /sys/fs/cgroup/hugetlb ; /sys/fs/cgroup/pids ; /sys/fs/cgroup/rdma ; /sys/fs/cgroup/misc ; /mnt/wslg/versions.txt ; /mnt/wslg/doc ; /tmp/.X11-unix ; /mnt/c ; /mnt/d ; /mnt/g ; /run/user ; /sys/fs/cgroup/systemd ; /dev/hugepages ; /dev/mqueue ; /sys/kernel/debug ; /sys/kernel/tracing ; /sys/fs/fuse/connections ; /snap ; /snap/bare/5 ; /snap/core22/1033 ; /snap/core20/2105 ; /snap/core22/864 ; /snap/dotnet-sdk/233 ; /snap/gtk-common-themes/1535 ; /snap/snapd/20290 ; /snap/snapd/20671 ; /snap/ubuntu-desktop-installer/1280 ; /snap/ubuntu-desktop-installer/1282]
        public static void DisplayOSInfo_and_User()
        {
            PlatformID platformId = Environment.OSVersion.Platform;
            string servicePack = Environment.OSVersion.ServicePack;
            var version = Environment.OSVersion.Version;
            Console.WriteLine("OS=" + Environment.OSVersion);            
            Console.WriteLine($"platformId=[{platformId}] servicePack=[{servicePack}] version=[{version}]");
            Console.WriteLine("Environment.MachineName=" + Environment.MachineName);
            Console.WriteLine("Environment.UserName=" + Environment.UserName);
            Console.WriteLine("Environment.UserInteractive=" + Environment.UserInteractive);
            Console.WriteLine("Environment.UserDomainName=" + Environment.UserDomainName);

            var arrLogicalDrives = Environment.GetLogicalDrives();
            var strLogicalDrives = arrLogicalDrives switch {
                null => "logical drives are null",
                [] => "no logical drives",
                [string oneDrive] => $"one logical drive [{oneDrive}]",
                [..] drives => $"several logical drives: {string.Join(" ; ", drives)}"
            };
            Console.WriteLine($"Environment.GetLogicalDrives=[{strLogicalDrives}]");
        }

        // 1. Current Directory
        // 1.1 UserProfile=[/home/rootalex] + CommonApplicationData=[/usr/share] + LocalApplicationData=[/home/rootalex/.local/share]
        public static void CurrentDirectory()
        {
            Console.WriteLine("Environment.CurrentDirectory=" + Environment.CurrentDirectory);

            Console.WriteLine("SpecialFolders:");
            Console.WriteLine($"ApplicationData=[{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}]");
            Console.WriteLine($"CommonApplicationData=[{Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)}]"); // (/usr/share)
            Console.WriteLine($"LocalApplicationData=[{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}]");
            Console.WriteLine($"CommonDocuments=[{Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments)}]");
            Console.WriteLine($"CommonPrograms=[{Environment.GetFolderPath(Environment.SpecialFolder.CommonPrograms)}]");
            Console.WriteLine($"MyDocuments=[{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}]");
            Console.WriteLine($"CommonProgramFiles=[{Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFiles)}]");
            Console.WriteLine($"Desktop=[{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}]");
            Console.WriteLine($"History=[{Environment.GetFolderPath(Environment.SpecialFolder.History)}]");
            Console.WriteLine($"UserProfile=[{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}]"); // for ~ (/home/rootalex) vs (C:\Users\aliaksei.pahirnitski)
            Console.WriteLine($"System=[{Environment.GetFolderPath(Environment.SpecialFolder.System)}]");
            Console.WriteLine($"Windows=[{Environment.GetFolderPath(Environment.SpecialFolder.Windows)}]");
        }

        // 1b. Environment
        public static void Environment_Get_and_Set()
        {            
            const string CEnv_ALEXENV = "ALEXENV";
            var before = Environment.GetEnvironmentVariable(CEnv_ALEXENV);
            var after = "ms" + DateTime.Now.Microsecond;
            Environment.SetEnvironmentVariable(CEnv_ALEXENV, after);
            var test = Environment.GetEnvironmentVariable(CEnv_ALEXENV);
            Debug.Assert(after == test, $"values not equal: {after} vs {test}");
            Console.WriteLine($"Environment[{CEnv_ALEXENV}] changed from [{before}] to [{after}]");
        }

        // 2. File: Exists or not
        public static void File_Exists(string fileName)
        {
            Console.WriteLine("File.Exists=" + File.Exists(fileName));
            Console.WriteLine("FileInfo.Exists=" + new FileInfo(fileName).Exists);
        }

        // 3. Directory: Exists or noit
        public static void Directory_Exists(string dirName)
        {
            Console.WriteLine("Directory.Exists=" + Directory.Exists(dirName));
            Console.WriteLine("DirectoryInfo=" + new DirectoryInfo(dirName).Exists);
        }

        // 3. Directory: Exists or noit
        public static void Process_Output(string processToStart)
        {
            var processInfo = new ProcessStartInfo(processToStart)
            {
                RedirectStandardOutput = true,
                // UseShellExecute = true,
            };
            var process = Process.Start(processInfo);

            process.WaitForExit();

            var outputResult = process.StandardOutput.ReadToEnd();

            Console.WriteLine($"process [{processToStart}] StandardOutput=[{outputResult}]");
        }


        // 4. Read / write / delete files and folders
        public static void Read_Write_Delete(string dirName)
        {
            if (!Directory.Exists(dirName))
            {
                Directory.CreateDirectory(dirName);
            }

            // test 1. Does DirectoryInfo knows that files or sub-directories created?
            var diBefore = new DirectoryInfo(dirName);
            var summaryBefore = $"Directory [{diBefore.Name}] has {diBefore.GetFiles().Length} files and {diBefore.GetDirectories().Length} sub-directories";
            Console.WriteLine(summaryBefore);

            // create sub dir
            var newSubDirName = $"dir {DateTime.Now:MMddhhmmssff} rnd{Random.Shared.Next(1,99)}";
            var newSubDirFullName = Path.Combine(dirName, newSubDirName);
            Directory.CreateDirectory(newSubDirFullName);

            // create a file 
            var newFileFullPath = Path.Combine(dirName, Guid.NewGuid() + ".test");
            using(var writer = new StreamWriter(newFileFullPath))
            {
                writer.WriteLine($"Hallo at {DateTime.Now}");
                writer.WriteLine($"Rand {Random.Shared.Next(1, 9_999_999)}");
            }

            var testAfter = $"DirectoryInfo after has {diBefore.GetFiles().Length} files and {diBefore.GetDirectories().Length} sub-directories";
            Console.WriteLine(testAfter);
            var summaryAfter = $"Real After has {Directory.GetFiles(dirName).Length} files and {Directory.GetDirectories(dirName).Length} sub-directories";
            Console.WriteLine(summaryBefore);

            using (var reader = new StreamReader(newFileFullPath))
            {
                var totalLength = reader.ReadToEnd().Length;
                var bytesCount = new FileInfo(newFileFullPath).Length;
                Console.WriteLine($"totalLength={totalLength} and bytesCount={bytesCount}");
                // Debug.Assert(totalLength == )
            }

            // delete 
            Directory.Delete(newSubDirFullName, true);
            File.Delete(newFileFullPath);
            Console.WriteLine("....Deleted!....");

            var summaryAfterDelete = $"After Delete: {Directory.GetFiles(dirName).Length} files and {Directory.GetDirectories(dirName).Length} sub-directories";
            Console.WriteLine(summaryAfterDelete);
        }

        // 5. 
        public static void Request_Mine_IP()
        {
            var httpClient = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "http://ifconfig.me/ip"); // also ipinfo.io/ip
            var response = httpClient.Send(request);
            Console.WriteLine($"Response={response}");
            Console.WriteLine($"Http Status: {response.StatusCode}");            
            var text = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            Console.WriteLine($"result IP: {text}");
        }

        // 6. Network
        public static void Network()
        {
            bool isAvailable = NetworkInterface.GetIsNetworkAvailable();
            var listNetworks = NetworkInterface.GetAllNetworkInterfaces();
            // [0].GetPhysicalAddress().;
            Console.WriteLine($"Network: isAvailable=[{isAvailable}] NetworkInterfaces Count=[{listNetworks.Length}]");

            if (listNetworks.Length > 0)
            {
                var first = listNetworks[0];
                Console.WriteLine($"Example Network Interface: [{first}]");
                Console.WriteLine($"Example Network Interface: GetPhysicalAddress=[{first.GetPhysicalAddress()}] SupportsMulticast=[{first.SupportsMulticast}]");
                Console.WriteLine($"Example Network Interface: Id=[{first.Id}] Name=[{first.Name}] NetworkInterfaceType=[{first.NetworkInterfaceType}] Speed=[{first.Speed}] OperationalStatus=[{first.OperationalStatus}]");
                Console.WriteLine($"Description=[{first.Description}]");
                Console.WriteLine($"GetIPProperties=[{first.GetIPProperties()?.DnsAddresses}]");
                Console.WriteLine($"GetIPStatistics=[{first.GetIPStatistics()?.BytesSent}]");
                Console.WriteLine($"GetIPv4Statistics=[{first.GetIPv4Statistics()?.BytesReceived}]");
            }
        }


        //internal static int Demo(Action action, [CallerArgumentExpression(nameof(action))] string? argText = null)
        //{
        //    Stopwatch sw = Stopwatch.StartNew();
        //    try
        //    {
        //        Console.WriteLine($"Starting demo for: {argText}");

        //        action?.Invoke();
        //        sw.Stop();
        //        return 0;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error happened: {ex.Message}");
        //        Console.WriteLine(ex);
        //        return -1;
        //    }
        //    finally
        //    {
        //        Console.WriteLine($"Finished demo for: {argText} in {sw.ElapsedMilliseconds} ms");
        //    }
        //}


        public static void DemoException()
        {
            Console.WriteLine("DDDD");
            object obj = null;
            var hash = obj.GetHashCode();
            Console.WriteLine("Should print nothing as NRE exception should be thrown above");
        }


        public static void ShowException()
        {
            // 

            Console.WriteLine("AAAAAA");
        }
        public static void ShowOS()
        {
            
        }

        //public static void FileExists(string fileName)
        //{
        //    var exists = File.Exists(fileName);
        //    Console.WriteLine($"1:Exists={exists} for file [{fileName}]");

        //    FileInfo fileInfo = new FileInfo(fileName);
        //    Console.WriteLine($"2:fileInfo=[{fileInfo}]");

        //    Console.WriteLine($"3:fileInfo.Exists={fileInfo.Exists}");

        //    Console.WriteLine($"4:UnixFileMode={fileInfo.UnixFileMode}");

        //    Console.WriteLine($"5:ileInfo.DirectoryName=[{fileInfo.DirectoryName}]");

        //    Console.WriteLine($"6:fileInfo.Length={fileInfo.Length}");
        //}
        //}
    }
}
