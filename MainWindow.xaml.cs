using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms; // Requires WindowsForms integration or Microsoft.Win32

namespace UnityCNEasyTool
{
    public class GameInfo
    {
        public string Name { get; set; }
        public string Key { get; set; }
    }

    public partial class MainWindow : Window
    {
        private List<string> _targetFiles = new List<string>();
        private bool _isFolderMode = false;

        public MainWindow()
        {
            InitializeComponent();
            LoadGames();
        }

        private void LoadGames()
        {
            var games = new List<GameInfo>
            {
                new GameInfo { Name = "Punishing: Gray Raven", Key = "5265736F" },
                new GameInfo { Name = "Custom Game...", Key = "" }
            };
            GameComboBox.ItemsSource = games;
            GameComboBox.SelectedIndex = 0;
        }

        private void GameComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (GameComboBox.SelectedItem is GameInfo game)
            {
                KeyTextBox.Text = game.Key;
            }
        }

        private void Window_DragOver(object sender, System.Windows.DragEventArgs e)
        {
            if (e.Data.GetDataPresent(System.Windows.DataFormats.FileDrop))
                e.Effects = System.Windows.DragDropEffects.Copy;
            else
                e.Effects = System.Windows.DragDropEffects.None;
            e.Handled = true;
        }

        private void Window_Drop(object sender, System.Windows.DragEventArgs e)
        {
            if (e.Data.GetDataPresent(System.Windows.DataFormats.FileDrop))
            {
                string[] paths = (string[])e.Data.GetData(System.Windows.DataFormats.FileDrop);
                ProcessSelectedPaths(paths);
            }
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            // Simple file dialog
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Multiselect = true;
            dlg.Filter = "AssetBundle files (*.bundle;*.unity3d;*.assetbundle)|*.bundle;*.unity3d;*.assetbundle|All files (*.*)|*.*";
            if (dlg.ShowDialog() == true)
            {
                ProcessSelectedPaths(dlg.FileNames);
            }
        }

        private void BrowseOutput_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    OutputPathTextBox.Text = dialog.SelectedPath;
                }
            }
        }

        private void ProcessSelectedPaths(string[] paths)
        {
            _targetFiles.Clear();
            _isFolderMode = false;

            if (paths.Length == 1 && Directory.Exists(paths[0]))
            {
                _isFolderMode = true;
                _targetFiles.Add(paths[0]);
                SelectedFilesText.Text = $"Selected Folder: {Path.GetFileName(paths[0])}";
            }
            else
            {
                _targetFiles.AddRange(paths.Where(File.Exists));
                SelectedFilesText.Text = $"Selected: {_targetFiles.Count} files";
            }
        }

        private void Log(string message)
        {
            Dispatcher.Invoke(() =>
            {
                LogTextBox.AppendText(message + Environment.NewLine);
                LogTextBox.ScrollToEnd();
            });
        }

        private async void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (_targetFiles.Count == 0)
            {
                System.Windows.MessageBox.Show("Please select a file or folder first.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(KeyTextBox.Text))
            {
                System.Windows.MessageBox.Show("Please enter the Game Key.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            StartButton.IsEnabled = false;
            LogTextBox.Clear();
            WorkProgressBar.Value = 0;
            string key = KeyTextBox.Text.Trim();
            bool isEncrypt = EncryptRadio.IsChecked == true;
            bool customOutput = CustomFolderRadio.IsChecked == true;
            string outputDir = OutputPathTextBox.Text;

            string exePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tools", "UnityCN-Helper.exe");
            if (!File.Exists(exePath))
            {
                Log($"ERROR: Cannot find UnityCN-Helper.exe at {exePath}");
                StartButton.IsEnabled = true;
                return;
            }

            Log($"Starting Process (Mode: {(isEncrypt ? "Encrypt" : "Decrypt")})");

            int total = _isFolderMode ? Directory.GetFiles(_targetFiles[0], "*.*", SearchOption.AllDirectories).Length : _targetFiles.Count;
            int current = 0;

            await Task.Run(() =>
            {
                if (_isFolderMode)
                {
                    string inputPath = _targetFiles[0];
                    string outputPath = customOutput && !string.IsNullOrWhiteSpace(outputDir) ? outputDir : inputPath + "_output";
                    if (!Directory.Exists(outputPath)) Directory.CreateDirectory(outputPath);
                    
                    RunProcess(exePath, inputPath, outputPath, key, isEncrypt, true);
                    current = total;
                    Dispatcher.Invoke(() => WorkProgressBar.Value = 100);
                }
                else
                {
                    foreach (var file in _targetFiles)
                    {
                        string outPath = file;
                        if (customOutput && !string.IsNullOrWhiteSpace(outputDir))
                        {
                            outPath = Path.Combine(outputDir, Path.GetFileName(file));
                        }
                        else
                        {
                            outPath = file + (isEncrypt ? ".enc" : ".dec");
                        }

                        RunProcess(exePath, file, outPath, key, isEncrypt, false);
                        
                        current++;
                        Dispatcher.Invoke(() =>
                        {
                            WorkProgressBar.Value = (current / (double)total) * 100;
                            StatusText.Text = $"Status: {current}/{total} processed";
                        });
                    }
                }
            });

            Log("✅ Operation completed!");
            StatusText.Text = "Status: Done";
            StartButton.IsEnabled = true;
        }

        private void RunProcess(string exePath, string input, string output, string key, bool isEncrypt, bool isFolder)
        {
            string mode = isEncrypt ? "-e" : "-d";
            string folderFlag = isFolder ? "-f" : "";
            string args = $"-i \"{input}\" -o \"{output}\" {mode} -k {key} {folderFlag}";

            Log($"> UnityCN-Helper {args}");

            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = exePath,
                Arguments = args,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true,
                WorkingDirectory = Path.GetDirectoryName(exePath)
            };

            using (Process process = Process.Start(psi))
            {
                process.OutputDataReceived += (s, e) => { if (e.Data != null) Log(e.Data); };
                process.ErrorDataReceived += (s, e) => { if (e.Data != null) Log("ERR: " + e.Data); };
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
                process.WaitForExit();
            }
        }
    }
}