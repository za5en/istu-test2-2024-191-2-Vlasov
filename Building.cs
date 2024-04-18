﻿namespace TestProject1;

public class Building
{
    public BuildingType Type { get; set; }
    public bool ServiceRequired { get; set; }
    public bool IsClean { get; set; }

    public Building(BuildingType type, bool serviceRequired, bool isClean)
    {
        Type = type;
        ServiceRequired = serviceRequired;
        IsClean = isClean;
    }
    
    public bool ServicePointCheck()
    {
        ServiceRequired = !ServiceRequired;
        return ServiceRequired;
    }
    
    public bool CowMove(Cow cow)
    {
        cow.Location = Type switch
        {
            BuildingType.MilkingHall => CowLocation.MilkingHall,
            BuildingType.Barn => CowLocation.Stall,
            _ => cow.Location
        };
        return true;
    }
}