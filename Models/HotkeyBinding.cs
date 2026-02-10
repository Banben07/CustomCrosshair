using System.Windows.Input;

namespace CrossfireCrosshair.Models;

public sealed class HotkeyBinding : ObservableObject
{
    private bool _ctrl = true;
    private bool _alt = true;
    private bool _shift;
    private bool _win;
    private Key _key = Key.X;

    public bool Ctrl
    {
        get => _ctrl;
        set => SetProperty(ref _ctrl, value);
    }

    public bool Alt
    {
        get => _alt;
        set => SetProperty(ref _alt, value);
    }

    public bool Shift
    {
        get => _shift;
        set => SetProperty(ref _shift, value);
    }

    public bool Win
    {
        get => _win;
        set => SetProperty(ref _win, value);
    }

    public Key Key
    {
        get => _key;
        set => SetProperty(ref _key, value);
    }

    public uint ToNativeModifiers()
    {
        uint modifiers = 0;
        if (Alt)
        {
            modifiers |= Services.NativeMethods.MOD_ALT;
        }

        if (Ctrl)
        {
            modifiers |= Services.NativeMethods.MOD_CONTROL;
        }

        if (Shift)
        {
            modifiers |= Services.NativeMethods.MOD_SHIFT;
        }

        if (Win)
        {
            modifiers |= Services.NativeMethods.MOD_WIN;
        }

        return modifiers;
    }

    public uint ToVirtualKeyCode()
    {
        return (uint)KeyInterop.VirtualKeyFromKey(Key);
    }

    public string ToDisplayString()
    {
        List<string> parts = [];
        if (Ctrl)
        {
            parts.Add("Ctrl");
        }

        if (Alt)
        {
            parts.Add("Alt");
        }

        if (Shift)
        {
            parts.Add("Shift");
        }

        if (Win)
        {
            parts.Add("Win");
        }

        parts.Add(Key.ToString());
        return string.Join(" + ", parts);
    }

    public static HotkeyBinding DefaultToggle()
    {
        return new HotkeyBinding
        {
            Ctrl = true,
            Alt = true,
            Shift = false,
            Win = false,
            Key = Key.X
        };
    }

    public static HotkeyBinding DefaultCycle()
    {
        return new HotkeyBinding
        {
            Ctrl = true,
            Alt = true,
            Shift = false,
            Win = false,
            Key = Key.C
        };
    }
}
