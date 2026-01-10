using System.Windows;
using System.Windows.Media;
using WinTrek.Core.Services;

namespace WinTrek.UI.Views;

public partial class StatusReportWindow : Window
{
    public StatusReportWindow(SystemStatusResult statusResult)
    {
        InitializeComponent();

        var displayItems = statusResult.Systems.Select(s => new SystemStatusDisplayItem
        {
            Name = s.Name,
            IsOperational = s.IsOperational,
            DamageLevel = s.DamageLevel,
            StatusText = s.IsOperational ? "OK" : "DAMAGED",
            HealthPercent = CalculateHealthPercent(s.DamageLevel),
            StatusColor = GetStatusColor(s.IsOperational, s.DamageLevel)
        }).ToList();

        SystemsList.ItemsSource = displayItems;

        var damagedCount = statusResult.Systems.Count(s => !s.IsOperational);
        DamageSummary.Text = damagedCount == 0
            ? "All systems operational."
            : $"{damagedCount} system(s) require repair. Dock at starbase to repair.";
    }

    private int CalculateHealthPercent(int damageLevel)
    {
        // DamageLevel is typically positive when damaged, 0 or negative when operational
        // Higher damage = lower health
        if (damageLevel <= 0)
            return 100;

        // Scale damage to percentage (assuming max damage around 50)
        var health = Math.Max(0, 100 - (damageLevel * 2));
        return health;
    }

    private SolidColorBrush GetStatusColor(bool isOperational, int damageLevel)
    {
        if (isOperational && damageLevel <= 0)
            return new SolidColorBrush(Colors.LimeGreen);

        if (isOperational && damageLevel > 0)
            return new SolidColorBrush(Colors.Yellow);

        return new SolidColorBrush(Colors.Red);
    }

    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}

public class SystemStatusDisplayItem
{
    public string Name { get; set; } = string.Empty;
    public bool IsOperational { get; set; }
    public int DamageLevel { get; set; }
    public string StatusText { get; set; } = string.Empty;
    public int HealthPercent { get; set; }
    public SolidColorBrush StatusColor { get; set; } = new SolidColorBrush(Colors.White);
}
