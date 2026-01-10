using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using WinTrek.Core.Enums;

namespace WinTrek.UI.Converters;

/// <summary>
/// Converts SectorContent to a color for display.
/// </summary>
public class SectorContentToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is SectorContent content)
        {
            return content switch
            {
                SectorContent.Enterprise => new SolidColorBrush(Colors.Cyan),
                SectorContent.Klingon => new SolidColorBrush(Colors.Red),
                SectorContent.Star => new SolidColorBrush(Colors.Yellow),
                SectorContent.Starbase => new SolidColorBrush(Colors.LimeGreen),
                _ => new SolidColorBrush(Color.FromRgb(0x60, 0x60, 0x60)) // Dark gray for empty
            };
        }
        return new SolidColorBrush(Colors.Gray);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

/// <summary>
/// Converts alert condition to a color.
/// </summary>
public class ConditionToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string condition)
        {
            return condition.ToUpper() switch
            {
                "RED" => new SolidColorBrush(Colors.Red),
                "YELLOW" => new SolidColorBrush(Colors.Yellow),
                "GREEN" => new SolidColorBrush(Colors.LimeGreen),
                _ => new SolidColorBrush(Colors.White)
            };
        }
        return new SolidColorBrush(Colors.White);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

/// <summary>
/// Converts message type to a color.
/// </summary>
public class MessageTypeToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is WinTrek.Core.Events.MessageType type)
        {
            return type switch
            {
                WinTrek.Core.Events.MessageType.Alert => new SolidColorBrush(Colors.Red),
                WinTrek.Core.Events.MessageType.Warning => new SolidColorBrush(Colors.Orange),
                WinTrek.Core.Events.MessageType.Success => new SolidColorBrush(Colors.LimeGreen),
                WinTrek.Core.Events.MessageType.Damage => new SolidColorBrush(Colors.OrangeRed),
                WinTrek.Core.Events.MessageType.System => new SolidColorBrush(Colors.Cyan),
                _ => new SolidColorBrush(Colors.White)
            };
        }
        return new SolidColorBrush(Colors.White);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
