using System.Windows;

namespace CrossfireCrosshair;

public partial class ShareCodeInputDialog : Window
{
    public string ShareCode { get; private set; } = string.Empty;

    public ShareCodeInputDialog(string initialCode = "")
    {
        InitializeComponent();
        ShareCodeTextBox.Text = initialCode;
        ShareCodeTextBox.SelectAll();
    }

    private void Import_Click(object sender, RoutedEventArgs e)
    {
        ShareCode = ShareCodeTextBox.Text.Trim();
        if (string.IsNullOrWhiteSpace(ShareCode))
        {
            MessageBox.Show(this, "Please paste a share code first.", "Import Share Code", MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }

        DialogResult = true;
    }
}
