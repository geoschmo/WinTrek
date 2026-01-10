namespace WinTrek.Core;

/// <summary>
/// Static game data including quadrant names and display strings.
/// </summary>
public static class GameData
{
    /// <summary>
    /// Star Trek-themed names for the 64 quadrants in the galaxy.
    /// </summary>
    public static readonly string[] QuadrantNames =
    {
        "Aaamazzara",
        "Altair IV",
        "Aurelia",
        "Bajor",
        "Benthos",
        "Borg Prime",
        "Cait",
        "Cardassia Prime",
        "Cygnia Minor",
        "Daran V",
        "Duronom",
        "Dytallix B",
        "Efros",
        "El-Adrel IV",
        "Epsilon Caneris III",
        "Ferenginar",
        "Finnea Prime",
        "Freehaven",
        "Gagarin IV",
        "Gamma Trianguli VI",
        "Genesis",
        "H'atoria",
        "Holberg 917-G",
        "Hurkos III",
        "Iconia",
        "Ivor Prime",
        "Iyaar",
        "Janus VI",
        "Jouret IV",
        "Juhraya",
        "Kabrel I",
        "Kelva",
        "Ktaris",
        "Ligillium",
        "Loval",
        "Lyshan",
        "Magus III",
        "Matalas",
        "Mudd",
        "Nausicaa",
        "New Bajor",
        "Nova Kron",
        "Ogat",
        "Orion III",
        "Oshionion Prime",
        "Pegos Minor",
        "P'Jem",
        "Praxillus",
        "Qo'noS",
        "Quadra Sigma III",
        "Quazulu VIII",
        "Rakosa V",
        "Rigel VII",
        "Risa",
        "Romulus",
        "Rura Penthe",
        "Sauria",
        "Sigma Draconis",
        "Spica",
        "Talos IV",
        "Tau Alpha C",
        "Ti'Acor",
        "Udala Prime",
        "Ultima Thule",
        "Uxal",
        "Vacca VI",
        "Volan II",
        "Vulcan",
        "Wadi",
        "Wolf 359",
        "Wysanti",
        "Xanthras III",
        "Xendi Sabu",
        "Xindus",
        "Yadalla Prime",
        "Yadera II",
        "Yridian",
        "Zalkon",
        "Zeta Alpha II",
        "Zytchin III"
    };

    /// <summary>
    /// ASCII art title screen.
    /// </summary>
    public static readonly string[] TitleArt =
    {
        @"       ______ _______ ______ ______    _______ ______  ______ __  __ ",
        @"      / __  //__  __// __  // __  /   /__  __// __  / / ____// / / /",
        @"     / / /_/   / /  / /_/ // /_/ /      / /  / /_/ / / /__  / // /",
        @"     _\ \     / /  / __  //   __/      / /  /   __/ / __ / /   / ",
        @"   / /_/ /   / /  / / / // /\ \       / /  / /\ \  / /___ / /\ \",
        @"  /_____/   /_/  /_/ /_//_/  \_\     /_/  /_/  \_\/_____//_/  \_\",
        @"",
        @"                   ________________        _",
        @"                   \__(=======/_=_/____.--'-`--.___",
        @"                              \ \   `,--,-.___.----'",
        @"                            .--`\\--'../",
        @"                           '---._____.|]"
    };

    /// <summary>
    /// Sector display characters for different content types.
    /// </summary>
    public static class SectorSymbols
    {
        public const string Empty = "   ";
        public const string Enterprise = "<*>";
        public const string Klingon = "+++";
        public const string Star = " * ";
        public const string Starbase = ">!<";
    }
}
