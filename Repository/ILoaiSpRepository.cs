using DoAn1_DDG_Pro.Models;
namespace DoAn1_DDG_Pro.Repository
{
    public interface ILoaiSpRepository
    {
        ProductType Add(ProductType productType);
        ProductType Update(ProductType productType);
        ProductType Delete(String typeId);
        ProductType Get(String typeId);
        
        IEnumerable<ProductType> GetAll();

    }
}
