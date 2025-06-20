using Service.DTOs.CategoryDTOs;
using Service.DTOs.DiscountDTOs;
using Service.DTOs.ProductDTOs;
using Service.DTOs.ProductFeatureDTOs;

public class ProductDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string Brand { get; set; }
    public int StockQuantity { get; set; }
    public DateTime CreatedAt { get; set; }
    public int CategoryId { get; set; }
    public CategoryDTO Category { get; set; }

    public ICollection<ProductFeatureDTO> ProductFeatures { get; set; }
    public ICollection<DiscountDTO> Discounts { get; set; }
    public ICollection<ProductImageDTO> ProductImages { get; set; }
}
