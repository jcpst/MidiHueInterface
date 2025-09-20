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
    
    public static string[] All = [Red, Green, Blue, White, Black, Yellow, Orange, Purple, Cyan, Magenta, Lime, Pink];
    
    public static string Random() => All[new Random().Next(All.Length)];
}