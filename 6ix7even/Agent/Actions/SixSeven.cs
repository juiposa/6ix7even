using _6ix7even.Agent.Enums;

namespace _6ix7even.Agent.Actions;

public static class SixSeven
{
    private static readonly PlayerMarker.SignEnum sixMk = PlayerMarker.SignEnum.Attack6;
    private static readonly PlayerMarker.SignEnum sevenMk = PlayerMarker.SignEnum.Attack7;
    private static readonly PlayerMarker.SignEnum triangle = PlayerMarker.SignEnum.Triangle;

    public static void Invoke(ulong target, MarkEntity marker)
    {
        marker.InvokeSeries([sixMk, sevenMk, triangle, triangle], target);
    }
}