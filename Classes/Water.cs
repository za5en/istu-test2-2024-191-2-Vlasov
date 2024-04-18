namespace TestProject1;

public class Water
{
    public bool InStock { get; set; }
    public WaterLocation Location { get; set; }
    public bool ReadyToUse { get; set; }
    public Purpose Purpose { get; set; }

    public Water()
    {
        InStock = true;
        Location = WaterLocation.WateringMachine;
        ReadyToUse = true;
        Purpose = Purpose.Watering;
    }

    public Water(bool inStock, WaterLocation location, bool readyToUse, Purpose purpose)
    {
        InStock = inStock;
        Location = location;
        ReadyToUse = readyToUse;
        Purpose = purpose;
    }
}