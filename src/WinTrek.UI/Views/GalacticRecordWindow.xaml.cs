using System.Windows;
using System.Windows.Media;
using WinTrek.Core.Services;

namespace WinTrek.UI.Views;

public partial class GalacticRecordWindow : Window
{
    public GalacticRecordWindow(GalacticRecordResult recordResult)
    {
        InitializeComponent();

        if (recordResult.Success)
        {
            var displayItems = recordResult.Quadrants.Select(q => new GalacticRecordDisplayItem
            {
                DisplayCode = q.IsScanned ? q.ScanCode : "???",
                Coordinates = q.Position.ToDisplayString(),
                CodeColor = GetCodeColor(q, recordResult.CurrentPosition),
                BackgroundColor = GetBackgroundColor(q, recordResult.CurrentPosition),
                IsCurrentPosition = q.Position.Equals(recordResult.CurrentPosition)
            }).ToList();

            GalaxyGrid.ItemsSource = displayItems;
        }
    }

    private SolidColorBrush GetCodeColor(QuadrantScanData data, WinTrek.Core.Models.Position currentPos)
    {
        if (!data.IsScanned)
            return new SolidColorBrush(Colors.DimGray);

        if (data.Position.Equals(currentPos))
            return new SolidColorBrush(Colors.Cyan);

        if (data.KlingonCount > 0)
            return new SolidColorBrush(Colors.Red);

        if (data.HasStarbase)
            return new SolidColorBrush(Colors.LimeGreen);

        return new SolidColorBrush(Colors.White);
    }

    private SolidColorBrush GetBackgroundColor(QuadrantScanData data, WinTrek.Core.Models.Position currentPos)
    {
        if (data.Position.Equals(currentPos))
            return new SolidColorBrush(Color.FromRgb(0x10, 0x30, 0x50)); // Highlighted background

        return new SolidColorBrush(Color.FromRgb(0x0a, 0x0a, 0x15)); // Normal background
    }

    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}

public class GalacticRecordDisplayItem
{
    public string DisplayCode { get; set; } = string.Empty;
    public string Coordinates { get; set; } = string.Empty;
    public SolidColorBrush CodeColor { get; set; } = new SolidColorBrush(Colors.White);
    public SolidColorBrush BackgroundColor { get; set; } = new SolidColorBrush(Colors.Black);
    public bool IsCurrentPosition { get; set; }
}
