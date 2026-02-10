using System.Collections.ObjectModel;

namespace CrossfireCrosshair.Models;

public sealed class AppSettings : ObservableObject
{
    private bool _overlayEnabled = true;
    private int _selectedProfileIndex;
    private int _targetMonitorIndex;
    private HotkeyBinding _toggleOverlayHotkey = HotkeyBinding.DefaultToggle();
    private HotkeyBinding _cycleProfileHotkey = HotkeyBinding.DefaultCycle();

    public ObservableCollection<CrosshairProfile> Profiles { get; set; } = [];

    public bool OverlayEnabled
    {
        get => _overlayEnabled;
        set => SetProperty(ref _overlayEnabled, value);
    }

    public int SelectedProfileIndex
    {
        get => _selectedProfileIndex;
        set => SetProperty(ref _selectedProfileIndex, value);
    }

    public int TargetMonitorIndex
    {
        get => _targetMonitorIndex;
        set => SetProperty(ref _targetMonitorIndex, value);
    }

    public HotkeyBinding ToggleOverlayHotkey
    {
        get => _toggleOverlayHotkey;
        set => SetProperty(ref _toggleOverlayHotkey, value);
    }

    public HotkeyBinding CycleProfileHotkey
    {
        get => _cycleProfileHotkey;
        set => SetProperty(ref _cycleProfileHotkey, value);
    }
}
