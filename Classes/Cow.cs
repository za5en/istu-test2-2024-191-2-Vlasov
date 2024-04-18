namespace TestProject1;

public class Cow
{
    public CowType Type { get; set; }
    public CowCond Condition { get; set; }
    public CowLocation Location { get; set; }
    public CowHealth Health { get; set; }
    public bool InQueue { get; set; }
    public bool IsHungry { get; set; }
    public bool IsThirsty { get; set; }

    public Cow()
    {
        Type = CowType.Cow;
        Condition = CowCond.Milking;
        Location = CowLocation.MilkingHall;
        Health = CowHealth.Healthy;
        InQueue = false;
        IsHungry = false;
        IsThirsty = false;
    }
    
    public Cow(CowType type, CowCond condition, CowLocation location, CowHealth health, bool inQueue, bool isHungry, bool isThirsty)
    {
        Type = type;
        Condition = condition;
        Location = location;
        Health = health;
        InQueue = inQueue;
        IsHungry = isHungry;
        IsThirsty = isThirsty;
    }
    
    public CowLocation Move(CowLocation location)
    {
        Location = location;
        return Location;
    }
    
    public CowCond CondChange(CowCond condition)
    {
        Condition = condition;
        return Condition;
    }
    
    public CowHealth HealthChange(CowHealth health)
    {
        Health = health;
        return Health;
    }
    
    public bool Waiting()
    {
        InQueue = !InQueue;
        return InQueue;
    }
    
    public Product? GiveMilk()
    {
        if (Type == CowType.Cow)
        {
            Condition = CowCond.Milking;
            var milk = new Product(isObtained: true, inStock: true, isStored: true, isSent: false, isSpoiled: false);
            IsHungry = true;
            IsThirsty = true;
            return milk;
        }
        return null;
    }
    
    public bool Drink(Water water)
    {
        if (!IsThirsty) return IsThirsty;
        Condition = CowCond.Drinking;
        water.InStock = false;
        IsThirsty = false;
        return IsThirsty;
    }
    
    public bool Eat(Food food)
    {
        if (!IsHungry) return IsHungry;
        Condition = CowCond.Eating;
        food.InStock = false;
        IsHungry = false;
        return IsHungry;
    }
    
    public CowCond WakeUp()
    {
        if (Condition == CowCond.Sleeping)
        {
            Condition = CowCond.Living;
        }
        return Condition;
    }
    
    public CowCond Sleep()
    {
        if (Condition != CowCond.Sleeping)
        {
            Condition = CowCond.Sleeping;
        }
        return Condition;
    }
}