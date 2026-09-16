using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static C3AP_Client.Helpers.NativeMethods;

namespace C3AP_Client.Hook
{
    public static class Hook
    {
        public static IntPtr GetProcess()
        {
            Process[] processes = Process.GetProcessesByName("duckstation-qt-x64-ReleaseLTCG");

            if (processes.Length == 0)
            {
                return IntPtr.Zero;
            }
            Process proc = processes[0];

            if (proc != null)
            {
                IntPtr processHandle = OpenProcess(PROCESS_VM_READ | PROCESS_VM_WRITE | PROCESS_QUERY_INFORMATION | PROCESS_VM_OPERATION, false, proc.Id);
                return processHandle;
            }

            return IntPtr.Zero;
        }

        public static Process[] isProcessRunning()
        {
            return Process.GetProcessesByName("duckstation-qt-x64-ReleaseLTCG");
        }

        public static IntPtr FindRamBySignature(IntPtr hProcess)
        {
            // Die funktionierende Byte-Signatur, die den RAM-Anfang direkt lokalisiert
            byte[] signature = new byte[] { 0x63, 0x64, 0x72, 0x6f, 0x6d, 0x3a, 0x5c, 0x53, 0x43 };

            IntPtr currentAddress = IntPtr.Zero;
            MEMORY_BASIC_INFORMATION mbi;

            while (VirtualQueryEx(hProcess, currentAddress, out mbi, (uint)Marshal.SizeOf(typeof(MEMORY_BASIC_INFORMATION))) != 0)
            {
                if (mbi.State == MEM_COMMIT && (mbi.Protect == PAGE_READWRITE || mbi.Protect == PAGE_EXECUTE_READWRITE))
                {
                    long regionSize = mbi.RegionSize.ToInt64();
                    if (regionSize > 4096 && regionSize < 100 * 1024 * 1024)
                    {
                        byte[] buffer = new byte[regionSize];
                        IntPtr bytesRead;

                        if (ReadProcessMemory(hProcess, mbi.BaseAddress, buffer, buffer.Length, out bytesRead))
                        {
                            int bytesReadInt = (int)bytesRead;
                            long baseAddrVal = mbi.BaseAddress.ToInt64();

                            for (int i = 0; i <= bytesReadInt - signature.Length; i++)
                            {
                                bool found = true;
                                for (int j = 0; j < signature.Length; j++)
                                {
                                    if (buffer[i + j] != signature[j])
                                    {
                                        found = false;
                                        break;
                                    }
                                }

                                if (found)
                                {
                                    // Zurückrechnen zur echten PS1-RAM-Basis (Offset 0x69320)
                                    long ramBaseInt = baseAddrVal + i - 0xb8b0;
                                    return new IntPtr(ramBaseInt);
                                }
                            }
                        }
                    }
                }

                long nextAddress = mbi.BaseAddress.ToInt64() + mbi.RegionSize.ToInt64();
                if (nextAddress <= currentAddress.ToInt64()) break;
                currentAddress = new IntPtr(nextAddress);
            }

            return IntPtr.Zero;
        }

        public static string GetRegion(IntPtr ramBase, IntPtr targetProcess)
        {
            IntPtr bytesRead;
            byte[] regionBuffer = new byte[6];
            IntPtr targetAddress = IntPtr.Add(ramBase, 0xB8B9);
            bool success = ReadProcessMemory(targetProcess, targetAddress, regionBuffer, regionBuffer.Length, out bytesRead);
            if (success)
            {
                string region = System.Text.Encoding.ASCII.GetString(regionBuffer);
                if (region == "US_942")
                {
                    GameConfig.LoadUSAddresses();
                    return "US";
                }
                else
                {
                    GameConfig.LoadEUAddresses();
                    return "EU";
                }
            }
            return "Error";
        }
    }
}
