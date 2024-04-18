namespace TestProject1;

public class Equipment
{
    public EqType Type { get; set; }
    public bool IsBroken { get; set; }
    public bool IsUsing { get; set; }
    public bool ServiceRequired { get; set; }
    public int UsageTime { get; set; }
    public bool IsClean { get; set; }

    public Equipment(EqType type)
    {
        Type = type;
        IsBroken = false;
        IsUsing = false;
        ServiceRequired = false;
        UsageTime = 50;
        IsClean = true;
    }
    
    public Equipment(EqType type, bool isBroken, bool isUsing, bool serviceRequired, int usageTime, bool isClean)
    {
        Type = type;
        IsBroken = isBroken;
        IsUsing = isUsing;
        ServiceRequired = serviceRequired;
        UsageTime = usageTime;
        IsClean = isClean;
    }
}