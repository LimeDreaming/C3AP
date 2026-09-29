using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace C3AP_Client
{
    public static class DuckstationHook
    {
        public const string EUGame = "SCES-01420";
        public const string USGame = "SCUS-94244";
        public static bool checkGameFileRegion(string gamePath, out string detectedGame) 
        {
            detectedGame = "Unknown";

            try
            {
                int bufferSize = 2 * 1024 * 1024;
                byte[] buffer = new byte[bufferSize];

                using (FileStream fs = new FileStream(gamePath, FileMode.Open, FileAccess.Read))
                {
                    int bytesRead = fs.Read(buffer, 0, bufferSize);
                    string content = Encoding.ASCII.GetString(buffer, 0, bytesRead);

                    Match match = Regex.Match(content, @"(SCUS|SCES)[-_]\d{3}[_.]?\d{2}", RegexOptions.IgnoreCase);

                    if (match.Success)
                    {
                        detectedGame = match.Value.Replace("_", "-").Replace(".", "").ToUpper();

                        if (detectedGame == EUGame || detectedGame == USGame)
                        {
                            Properties.Settings.Default.fileRegion = detectedGame;
                            Properties.Settings.Default.Save();
                            return true;
                        }
                        else
                        {
                            ClientMessageBox.Show("Please select Crash Bandicooot 3", "Wrong Game!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ClientMessageBox.Show($"Error by reading file: {ex.Message}", "Error");
            }

            return false;
        }
        public static bool searchDucktationFile()
        {
            using (OpenFileDialog duckFileDialog = new OpenFileDialog())
            {
                duckFileDialog.Title = "Duckstation install folder";
                duckFileDialog.Filter = "Duckstation EXE (*.exe)|*.exe";
                duckFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);

                if(duckFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string duckFilePath = duckFileDialog.FileName;

                    Properties.Settings.Default.duckPath = duckFilePath;
                    Properties.Settings.Default.Save();
                    //searchGameFile();
                    return true;

                }
            }
            return false;
        }

        public static bool searchGameFile() 
        {
            using (OpenFileDialog gameFileDialog = new OpenFileDialog())
            {
                gameFileDialog.Title = "Crash Bandicoot 3 Game File";
                gameFileDialog.Filter = "PlayStation File (*.bin)|*.bin";
                gameFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);

                if (gameFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if(checkGameFileRegion(gameFileDialog.FileName,out string game))
                    {

                    }
                    string gameFilePath = gameFileDialog.FileName;
                    Properties.Settings.Default.gamePath = gameFilePath;
                    Properties.Settings.Default.Save();
                    return true;
                }
            }
            return false;
        }

        public static void startDucktation(string duckExe, string romFile) 
        {
            try
            {
                if (!File.Exists(duckExe))
                {
                    ClientMessageBox.Show("DuckStation was not found at the specified path.", "Error");
                    return;
                }

                if (!File.Exists(romFile))
                {
                    ClientMessageBox.Show("The Game file wasn't found!", "Error");
                    return;
                }

                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = duckExe,
                    Arguments = $"\"{romFile}\"",
                    UseShellExecute = true
                };

                Process.Start(startInfo);
            }
            catch (Exception ex)
            {
                ClientMessageBox.Show($"Error at starting from DuckStation: {ex.Message}", "Error");
            }
        }
    }
}
