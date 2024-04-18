namespace TestProject1;

public class Food
{
    public bool InStock { get; set; }
    public bool IsOrdered { get; set; }
    public bool IsTransported { get; set; }
    public FoodType Type { get; set; }
    public FoodLocation Location { get; set; }
    public bool ReadyToUse { get; set; }

    public Food(bool inStock, bool isOrdered, bool isTransported, FoodType type, FoodLocation location, bool readyToUse)
    {
        InStock = inStock;
        IsOrdered = isOrdered;
        IsTransported = isTransported;
        Type = type;
        Location = location;
        ReadyToUse = readyToUse;
    }
}