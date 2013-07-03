// -----------------------------------------------------------------------
// <copyright file="KillFile.cs">
// Copyright Edward Wilde (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace core.net.tests.Util
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.InteropServices;

    public static class FileUtils
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool CloseHandle(IntPtr hObject);

        public static void CloseFileHandle(string fileName)
        {
            var handle = GetFileHandle(Process.GetCurrentProcess().ProcessName, fileName);
            if (handle != null)
            {
                Console.Write("Attempting to close handle for file: {0}", handle.Item2);
                var result = CloseHandle(new IntPtr(handle.Item1));
                Console.Write("Result {0} whilst close file handle for {1}", result, handle.Item2);
            }
        }

        public static Tuple<int, string> GetFileHandle(string processName, string fileName)
        {
            return GetFileHandles(processName).FirstOrDefault(item => item.Item2.Contains(fileName));
        }

        public static List<Tuple<int, string>> GetFileHandles(string processName)
        {
            Process handleProcess = new Process();
            handleProcess.StartInfo.FileName = "handle.exe";
            handleProcess.StartInfo.UseShellExecute = false;
            handleProcess.StartInfo.CreateNoWindow = true;
            handleProcess.StartInfo.RedirectStandardOutput = true;

            handleProcess.StartInfo.Arguments = "-p " + processName;
            handleProcess.Start();

            var streamReader = handleProcess.StandardOutput;
            var outputText = streamReader.ReadToEnd();
            handleProcess.WaitForExit();


            var lines = outputText.Split(new[] { System.Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            var fileEntries = new List<Tuple<int, string>>();
            foreach (var line in lines)
            {
                if (line.Contains("File"))
                {
                    var fileName = line.Substring(line.IndexOf(')') + 1).Trim();
                    var hexHandle = line.Substring(0, line.IndexOf(':')).Trim();
                    int handle = int.Parse(hexHandle, System.Globalization.NumberStyles.HexNumber);
                    fileEntries.Add(new Tuple<int, string>(handle, fileName));
                }
            }

            return fileEntries;
        }
    }
}