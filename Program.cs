using System;
using System.Management;
using Microsoft.Win32;


class Program
{
    static void Main(string[] args)
    {
        var searcher = new ManagementObjectSearcher("select * from Win32_CacheMemory");
        Console.WriteLine("Memorias cache " + searcher.Get().Count);
        Console.WriteLine();

        Console.WriteLine("Buscando por valores Cache do seu processador");
        List<uint> lCache = new List<uint>();
        foreach (ManagementObject obj in searcher.Get())
        {
            lCache.Add((uint)obj["InstalledSize"]);
            Console.WriteLine("Cache Type: " + obj["Purpose"]);
            Console.WriteLine("Cache Size: " + obj["InstalledSize"]);
        }

        uint keyValue1 = lCache[1];
        string keyName1 = "SecondLevelDataCache";

        uint keyValue2 = lCache[2];
        string keyName2 = "ThirdLevelDataCache";

        Console.WriteLine();
        Console.WriteLine("Inserindo os Valores");
        RegistryKey key = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\Session Manager\\Memory Management", true);
        key.SetValue(keyName1, keyValue1, RegistryValueKind.DWord);
        key.SetValue(keyName2, keyValue2, RegistryValueKind.DWord);
        key.Close();

        Console.WriteLine("Terminado.");
        Console.ReadLine();

        /*
            Caminho Regedit = Computador\HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\Memory Management

            SecondLevelDataCache
            ThirdLevelDataCache
        */
    }
}
