using System.Collections.Generic;
using System.Threading.Tasks;
using cspv3.Data;
using cspv3.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace cspv3.Services
{
    public class ProductOfferingService : IProductOffering
    {
        private ApplicationDbContext _dbContext;

        public ProductOfferingService(ApplicationDbContext Context)
        {
            _dbContext = Context;
        }

        public async Task<IEnumerable<ProductOffering>> GetProductOfferingsAsync()
        {
            return await _dbContext.Products.ToListAsync();
        }

        public IQueryable<ProductOffering> ProductOfferings(string productCategory)
        {
            // return _dbContext.Products.Where(i => i.category == productCategory).ToList();    

            var query = _dbContext.Products.Where(s => s.Name.Contains(productCategory));

            return query;
        }

        public IEnumerable<SubProductOffering> SubProductOfferings(string productCategory)
        {
            var query = _dbContext.SubProducts.Where(s => s.MeterCategory.Contains(productCategory)).ToList();

            return query;
        }
    }
}
