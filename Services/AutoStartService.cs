using System.Diagnostics;
using System.Reflection;
using Microsoft.Win32;

namespace CrossfireCrosshair.Services;

public sealed class AutoStartService
{
    private const string RunKeyPath = @"Software\Microsoft\Windows\CurrentVersion\Run";
    private readonly string _valueName;

    public AutoStartService(string valueName)
    {
        _valueName = valueName;
    }

    public bool IsEnabled()
    {
        using RegistryKey? key = Registry.CurrentUser.OpenSubKey(RunKeyPath, false);
        string? value = key?.GetValue(_valueName) as string;
        return !string.IsNullOrWhiteSpace(value);
    }

    public bool TrySetEnabled(bool enabled, out string? errorMessage)
    {
        errorMessage = null;
        try
        {
            using RegistryKey? key = Registry.CurrentUser.CreateSubKey(RunKeyPath, true);
            if (key is null)
            {
                errorMessage = "Failed to open startup registry key.";
                return false;
            }

            if (enabled)
            {
                key.SetValue(_valueName, BuildLaunchCommand(), RegistryValueKind.String);
            }
            else
            {
                key.DeleteValue(_valueName, false);
            }

            return true;
        }
        catch (Exception ex)
        {
            errorMessage = ex.Message;
            return false;
        }
    }

    private static string BuildLaunchCommand()
    {
        string? processPath = Environment.ProcessPath;
        if (string.IsNullOrWhiteSpace(processPath))
        {
            processPath = Process.GetCurrentProcess().MainModule?.FileName;
        }

        if (string.IsNullOrWhiteSpace(processPath))
        {
            throw new InvalidOperationException("Unable to resolve process path for startup registration.");
        }

        if (processPath.EndsWith("dotnet.exe", StringComparison.OrdinalIgnoreCase))
        {
            string? entryAssembly = Assembly.GetEntryAssembly()?.Location;
            if (!string.IsNullOrWhiteSpace(entryAssembly))
            {
                return $"\"{processPath}\" \"{entryAssembly}\"";
            }
        }

        return $"\"{processPath}\"";
    }
}
