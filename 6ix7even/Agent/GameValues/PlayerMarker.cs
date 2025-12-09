namespace _6ix7even.Agent.Enums;

public static class PlayerMarker
{
    public const string Attack1 = "attack1";
    public const string Attack2 = "attack2";
    public const string Attack3 = "attack3";
    public const string Attack4 = "attack4";
    public const string Attack5 = "attack5";
    public const string Attack6 = "attack6";
    public const string Attack7 = "attack7";
    public const string Attack8 = "attack8";

    public const string Bind1 = "bind1";
    public const string Bind2 = "bind2";
    public const string Bind3 = "bind3";
    public const string Bind4 = "bind4";

    public const string Ignore1 = "ignore1";
    public const string Ignore2 = "ignore2";

    public const string Square   = "square";
    public const string Circle   = "circle";
    public const string Plus     = "plus";
    public const string Triangle = "triangle";
    
    public const string Off = "off";
    
    public enum SignEnum
    {
        None,
        Attack1, Attack2, Attack3, Attack4, Attack5,
        Attack6, Attack7, Attack8, 
        Bind1, Bind2, Bind3,
        Ignore1, Ignore2,
        Square, Circle, Plus, Triangle,
        AttackNext, BindNext, IgnoreNext,
    }
    
    public static int GetSignIndex(SignEnum sign)
    {
        switch (sign)
        {
            default:
            case SignEnum.None: return 0;
            case SignEnum.Attack1: return 1;
            case SignEnum.Attack2: return 2;
            case SignEnum.Attack3: return 3;
            case SignEnum.Attack4: return 4;
            case SignEnum.Attack5: return 5;
            case SignEnum.Bind1: return 6;
            case SignEnum.Bind2: return 7;
            case SignEnum.Bind3: return 8;
            case SignEnum.Ignore1: return 9;
            case SignEnum.Ignore2: return 10;
            case SignEnum.Square: return 11;
            case SignEnum.Circle: return 12;
            case SignEnum.Plus: return 13;
            case SignEnum.Triangle: return 14;
            case SignEnum.Attack6: return 15;
            case SignEnum.Attack7: return 16;
            case SignEnum.Attack8: return 17;
        }
    }
}

