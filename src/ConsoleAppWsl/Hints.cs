using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace ConsoleAppWsl
{
    public static class Hints
    {
        // path with ~ (like ~/alexfolder/file1.txt) doesn't work implicitely
        public static string ExpandTilda(string path)
        {
            var expandedPath = path
              .Replace("~", Environment.GetFolderPath(Environment.SpecialFolder.UserProfile))
              .Replace("//", "/");
            return expandedPath;
        }

        public static void File_Or_FileInfo_Exists(string path)
        {
            // doesn't throw exception, just true or false
            // and also for Directory and DirectoryInfo
        }

        public static void Machine_Name_Same_as_on_Win(string path)
        {
            // same LAPTOP-A1FDEMNE
        }

        public static void LogicalDrives(string path)
        {
            // in ubuntu crazy amount of logical drives
            //  [ /mnt/wsl ; /usr/lib/wsl/drivers ; / ; /mnt/wslg ; /mnt/wslg/distro ; /usr/lib/wsl/lib ;
            //    /init ; /dev ; /sys ; /proc ; /dev/pts ; /run ; /run/lock ; /run/shm ; /dev/shm ; /run/user ;
            //    /proc/sys/fs/binfmt_misc ; /sys/fs/cgroup ; /sys/fs/cgroup/unified ; /sys/fs/cgroup/cpuset ; /sys/fs/cgroup/cpu ; /sys/fs/cgroup/cpuacct ;
            //    /sys/fs/cgroup/blkio ; /sys/fs/cgroup/memory ; /sys/fs/cgroup/devices ; /sys/fs/cgroup/freezer ; /sys/fs/cgroup/net_cls ;
            //    /sys/fs/cgroup/perf_event ; /sys/fs/cgroup/net_prio ; /sys/fs/cgroup/hugetlb ; /sys/fs/cgroup/pids ; /sys/fs/cgroup/rdma ;
            //    /sys/fs/cgroup/misc ; /mnt/wslg/versions.txt ; /mnt/wslg/doc ; /tmp/.X11-unix ;
            //    /mnt/c ; /mnt/d ; /mnt/g ;
            //    /run/user ; /sys/fs/cgroup/systemd ; /dev/hugepages ; /dev/mqueue ; /sys/kernel/debug ; /sys/kernel/tracing ; /sys/fs/fuse/connections ;
            //    /snap ; /snap/bare/5 ; /snap/core22/1033 ; /snap/core20/2105 ; /snap/core22/864 ; /snap/dotnet-sdk/233 ; /snap/gtk-common-themes/1535 ;
            //    /snap/snapd/20290 ; /snap/snapd/20671 ; /snap/ubuntu-desktop-installer/1280 ; /snap/ubuntu-desktop-installer/1282]
            var arrLogicalDrives = Environment.GetLogicalDrives();
            var strLogicalDrives = arrLogicalDrives switch
            {
                null => "logical drives are null",
                [] => "no logical drives",
                [string oneDrive] => $"one logical drive [{oneDrive}]",
                [..] drives => $"several logical drives: {string.Join(" ; ", drives)}"
            };
            Console.WriteLine($"Environment.GetLogicalDrives=[{strLogicalDrives}]");
        }

        public static void DirectoryInfo_caches(string path)
        {
            // Directory[alexfolder] has 3 files and 1 sub - directories
            // DirectoryInfo after has 4 files and 2 sub - directories
            // Directory[alexfolder] has 3 files and 1 sub - directories
        }

        public static void Network_different_in_linux()
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
                Console.WriteLine($"GetIPProperties=[{first.GetIPProperties()}]");
                Console.WriteLine($"GetIPStatistics=[{first.GetIPStatistics()}]");
                Console.WriteLine($"GetIPv4Statistics=[{first.GetIPv4Statistics()}]");
            }
        }
    }
}
