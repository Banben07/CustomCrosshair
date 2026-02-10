using System.IO;
using System.Text.Json;
using CrossfireCrosshair.Models;

namespace CrossfireCrosshair.Services;

public sealed class SettingsService
{
    private readonly string _settingsDirectory;
    private readonly string _settingsFile;
    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        WriteIndented = true,
        PropertyNameCaseInsensitive = true
    };

    public SettingsService()
    {
        _settingsDirectory = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "CrossfireCrosshair");
        _settingsFile = Path.Combine(_settingsDirectory, "settings.json");
    }

    public AppSettings Load()
    {
        try
        {
            if (!File.Exists(_settingsFile))
            {
                return CreateDefaultSettings();
            }

            string raw = File.ReadAllText(_settingsFile);
            AppSettings? deserialized = JsonSerializer.Deserialize<AppSettings>(raw, _jsonOptions);
            if (deserialized is null)
            {
                return CreateDefaultSettings();
            }

            Normalize(deserialized);
            return deserialized;
        }
        catch
        {
            return CreateDefaultSettings();
        }
    }

    public void Save(AppSettings settings)
    {
        Directory.CreateDirectory(_settingsDirectory);
        Normalize(settings);
        string raw = JsonSerializer.Serialize(settings, _jsonOptions);
        File.WriteAllText(_settingsFile, raw);
    }

    private static AppSettings CreateDefaultSettings()
    {
        AppSettings settings = new()
        {
            OverlayEnabled = true,
            SelectedProfileIndex = 0,
            TargetMonitorIndex = 0,
            ToggleOverlayHotkey = HotkeyBinding.DefaultToggle(),
            CycleProfileHotkey = HotkeyBinding.DefaultCycle(),
            Profiles = [.. ProfileFactory.CreatePresetPack()]
        };

        return settings;
    }

    private static void Normalize(AppSettings settings)
    {
        settings.Profiles ??= [];
        if (settings.Profiles.Count == 0)
        {
            settings.Profiles = [.. ProfileFactory.CreatePresetPack()];
        }

        settings.ToggleOverlayHotkey ??= HotkeyBinding.DefaultToggle();
        settings.CycleProfileHotkey ??= HotkeyBinding.DefaultCycle();
        settings.SelectedProfileIndex = Math.Clamp(settings.SelectedProfileIndex, 0, settings.Profiles.Count - 1);
        settings.TargetMonitorIndex = Math.Max(0, settings.TargetMonitorIndex);
    }
}
