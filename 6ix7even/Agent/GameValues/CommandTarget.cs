namespace _6ix7even.Agent.Enums;

public static class CommandTarget
{   
    public const string Me = "me";
    public const string Target = "t";
    public const string TargetOfTarget = "tt";
    public const string Party1 = "1";
    public const string Party2 = "2";
    public const string Party3 = "3";
    public const string Party4 = "4";
    public const string Party5 = "5";
    public const string Party6 = "6";
    public const string Party7 = "7";
    public const string Party8 = "8";
    public const string Focus = "f";

    public static string GetTarget(string target)
    {
        return "<" +  target + ">";
    }
}