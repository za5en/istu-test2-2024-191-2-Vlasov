namespace TestProject1;

public class Product
{
    public bool IsObtained { get; set; }
    public bool InStock { get; set; }
    public bool IsStored { get; set; }
    public bool IsSent { get; set; }
    public bool IsSpoiled { get; set; }
    
    public Product()
    {
        IsObtained = true;
        InStock = true;
        IsStored = true;
        IsSent = false;
        IsSpoiled = false;
    }
    
    public Product(bool isObtained, bool inStock, bool isStored, bool isSent, bool isSpoiled)
    {
        IsObtained = isObtained;
        InStock = inStock;
        IsStored = isStored;
        IsSent = isSent;
        IsSpoiled = isSpoiled;
    }
}