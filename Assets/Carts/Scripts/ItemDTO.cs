public class ItemDTO
{
    // Product Atttribute
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public bool IsAvailable { get; set; }

    // Price
    public double Price { get; set; }
    public bool IsFixedPrice { get; set; }

    // Discount
    public double DiscountPercent { get; set; }
    public double DiscountPrice { get; set; }

    // Catergory Attribute
    public CategoryDTO Category { get; set; }
}

public class CategoryDTO
{
    public int Code { get; set; }
    public string Name { get; set; }
}


