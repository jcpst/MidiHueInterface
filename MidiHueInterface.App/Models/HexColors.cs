namespace MidiHueInterface.App.Models;

public static class HexColors
{
    public const string Red = "#FF0000";

    public const string Green = "#00FF00";

    public const string Blue = "#0000FF";

    public const string White = "#FFFFFF";

    public const string Black = "#000000";

    public const string Yellow = "#FFFF00";

    public const string Orange = "#FFA500";

    public const string Purple = "#800080";

    public const string Cyan = "#00FFFF";

    public const string Magenta = "#FF00FF";

    public const string Lime = "#00FF00";

    public const string Pink = "#FFC0CB";

    /// <summary>
    /// An off-white color that's not too harsh for a live show
    /// </summary>
    public const string OffWhite = "#F5F5F5";

    public static string[] All = [Red, Green, Blue, White, Black, Yellow, Orange, Purple, Cyan, Magenta, Lime, Pink];

    public static string Random() => All[new Random().Next(All.Length)];

    public static string GetColorByReadableName(string colorName = OffWhite) => colorName switch
    {
        nameof(Red) => Red,
        nameof(Green) => Green,
        nameof(Blue) => Blue,
        nameof(Black) => Black,
        nameof(Yellow) => Yellow,
        nameof(Orange) => Orange,
        nameof(Purple) => Purple,
        nameof(Cyan) => Cyan,
        nameof(Magenta) => Magenta,
        nameof(Lime) => Lime,
        nameof(Pink) => Pink,
        nameof(White) => White,
        _ => OffWhite
    };
}