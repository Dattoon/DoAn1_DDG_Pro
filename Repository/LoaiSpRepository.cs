using DoAn1_DDG_Pro.Models;
namespace DoAn1_DDG_Pro.Repository
{
    public class LoaiSpRepository : ILoaiSpRepository
    {
        private readonly ShopDdgContext _context;
        public LoaiSpRepository (ShopDdgContext context)
        {
            _context = context;
        }
        public ProductType Add(ProductType productType)
        {
             _context.Add(productType);
             _context.SaveChanges();
            return productType;
        }
        
        public IEnumerable<ProductType> GetAll()
        {
            return _context.ProductTypes;
        }
        

        public ProductType Update(ProductType productType)
        {
            _context.Update(productType);
            _context.SaveChanges ();
            return productType;
        }

        public ProductType Delete(string typeId)
        {
            throw new NotImplementedException();
        }

        public ProductType Get(string typeId)
        {
            return _context.ProductTypes.Find(typeId);
        }

    }
}
