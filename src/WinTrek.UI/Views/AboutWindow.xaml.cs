using System.IO;
using System.Text.RegularExpressions;
using System.Windows;

namespace WinTrek.UI.Views;

public partial class AboutWindow : Window
{
    public AboutWindow()
    {
        InitializeComponent();
        LoadReadmeContent();
    }

    private void LoadReadmeContent()
    {
        try
        {
            // Try to find README.md relative to the application
            var appDir = AppDomain.CurrentDomain.BaseDirectory;
            var readmePath = FindReadme(appDir);

            if (readmePath != null && File.Exists(readmePath))
            {
                var content = File.ReadAllText(readmePath);
                // Remove screenshot/image markdown lines
                content = Regex.Replace(content, @"!\[.*?\]\(.*?\)\r?\n?", "");
                AboutContent.Text = content;
            }
            else
            {
                AboutContent.Text = GetFallbackContent();
            }
        }
        catch
        {
            AboutContent.Text = GetFallbackContent();
        }
    }

    private string? FindReadme(string startDir)
    {
        var dir = new DirectoryInfo(startDir);

        // Walk up the directory tree looking for README.md
        while (dir != null)
        {
            var readmePath = Path.Combine(dir.FullName, "README.md");
            if (File.Exists(readmePath))
                return readmePath;
            dir = dir.Parent;
        }

        return null;
    }

    private string GetFallbackContent()
    {
        return @"# WinTrek

A remake of the classic Star Trek text-based strategy game, built with WPF and .NET 8. Features an LCARS-inspired interface paying homage to Star Trek: The Next Generation's iconic computer displays.

## About the Game

Take command of the USS Enterprise and defend the Federation! Your mission: destroy all Klingon warships within a limited number of stardates. Navigate through an 8x8 galaxy of quadrants, each containing its own 8x8 sector grid. Dock at starbases to replenish energy and repair damage. Use phasers for close combat and photon torpedoes for precision strikes.

### Features

- LCARS-Style Interface - Purple and orange themed UI inspired by Star Trek: TNG computer displays
- Strategic Gameplay - Manage energy between shields, weapons, and warp drive
- Galaxy Exploration - 64 uniquely named quadrants to explore
- Combat Systems - Phasers (automatic targeting) and photon torpedoes (manual aiming)
- Ship Systems - Damage and repair mechanics for 7 different ship systems
- Sensor Arrays - Short-range and long-range scanning capabilities
- Save/Load - Save your game progress and continue later

## Acknowledgments

- Mike Mayfield - Creator of the original Star Trek game (1971)
- David Ahl & Bob Leedom - Super Star Trek (1978)
- Gene Roddenberry - Creator of Star Trek
- Michael Okuda - Designer of the LCARS interface for Star Trek: TNG

## License

This is a fan project created for educational purposes. Star Trek and related marks are trademarks of Paramount Pictures / CBS Studios.";
    }

    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}
