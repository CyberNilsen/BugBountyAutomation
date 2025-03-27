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

}