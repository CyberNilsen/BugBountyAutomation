using System.Diagnostics;
using System;
using System.IO;
using System.Windows;

namespace BugBountyAutomation;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    string pythonPath = "python";
    public MainWindow()
    {
        InitializeComponent();
    }

    private void RunDirsearch(object sender, RoutedEventArgs e)
    {
        string target = TargetInput.Text;
        if (string.IsNullOrWhiteSpace(target))
        {
            OutputBox.Text += "[-] Please enter a target URL.\n";
            return;
        }

        string scriptPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..","..", "..","Main", "dirsearch_scan.py");

        if (!File.Exists(scriptPath))
        {
            OutputBox.Text += $"[-] Error: {scriptPath} not found!\n";
            return;
        }

        ProcessStartInfo psi = new ProcessStartInfo
        {
            FileName = pythonPath,
            Arguments = $"\"{scriptPath}\" {target}",
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        Process process = new Process { StartInfo = psi };
        process.Start();
        string result = process.StandardOutput.ReadToEnd();
        process.WaitForExit();

        OutputBox.Text += result + "\n";
    }

    private void LaunchShellCommand(object sender, RoutedEventArgs e)
    {
        try
        {
            string targetUrl = TargetInput.Text.Trim();

            if (string.IsNullOrEmpty(targetUrl))
            {
                OutputBox.Text += "[-] Please enter a target URL.\n";
                return;
            }

            string threads = string.IsNullOrEmpty(ThreadsInput.Text.Trim()) ? "25" : ThreadsInput.Text.Trim();

            string appDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            string dirsearchPath = Path.Combine(appDirectory, "..", "..", "..", "Tools", "dirsearch");

            if (!Directory.Exists(dirsearchPath))
            {
                MessageBox.Show("dirsearch tool not found in the expected location.");
                return;
            }

            string command = $"python dirsearch.py -u {targetUrl} -t {threads}";

            ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                FileName = "cmd.exe",  
                Arguments = $"/C cd /d \"{dirsearchPath}\" && {command}", 
                RedirectStandardOutput = false,  
                UseShellExecute = true,  
                CreateNoWindow = false,  
            };

            Process process = new Process();
            process.StartInfo = startInfo;
            process.Start();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}");
        }
    }


}