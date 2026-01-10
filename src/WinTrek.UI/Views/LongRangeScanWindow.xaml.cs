using System.Windows;
using System.Windows.Media;
using WinTrek.Core.Services;

namespace WinTrek.UI.Views;

public partial class LongRangeScanWindow : Window
{
    public LongRangeScanWindow(LongRangeScanResult scanResult)
    {
        InitializeComponent();

        if (scanResult.Success)
        {
            var displayItems = scanResult.ScannedQuadrants.Select(q => new LrsScanDisplayItem
            {
                DisplayCode = q.IsValid ? q.ScanCode : "***",
                Coordinates = q.IsValid ? q.Position.ToDisplayString() : "---",
                CodeColor = GetColorBrush(q, scanResult.CenterPosition),
                IsCurrentPosition = q.IsValid && q.Position.Equals(scanResult.CenterPosition)
            }).ToList();

            ScanGrid.ItemsSource = displayItems;
        }
    }

    private SolidColorBrush GetColorBrush(QuadrantScanData data, WinTrek.Core.Models.Position centerPos)
    {
        if (!data.IsValid)
            return new SolidColorBrush(Colors.DarkGray);

        if (data.Position.Equals(centerPos))
            return new SolidColorBrush(Colors.Cyan);

        if (data.KlingonCount > 0)
            return new SolidColorBrush(Colors.Red);

        if (data.HasStarbase)
            return new SolidColorBrush(Colors.LimeGreen);

        return new SolidColorBrush(Colors.White);
    }

    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}

public class LrsScanDisplayItem
{
    public string DisplayCode { get; set; } = string.Empty;
    public string Coordinates { get; set; } = string.Empty;
    public SolidColorBrush CodeColor { get; set; } = new SolidColorBrush(Colors.White);
    public bool IsCurrentPosition { get; set; }
}
