using System.Linq;
using System.Threading.Tasks;
using basurapp.api.Helpers;
using basurapp.api.Models;
using Microsoft.EntityFrameworkCore;

namespace basurapp.api.Data
{
    public class RecyclerRepository : IRecyclerRepository
    {
        private readonly DataContext context;

        public RecyclerRepository(DataContext dataContext)
        {
            context = dataContext;
        }

        public void Add<T>(T entity) where T : class
        {
            context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            context.Remove(entity);
        }

        public async Task<bool> saveAll()
        {
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<BasurappUser> getBasurappUserById(int idUser)
        {
            var basurappUser = await context.BasurappUser.FirstOrDefaultAsync(C => C.id == idUser);
            return basurappUser;        
        }

        public async Task<Product> registerProduct(Product product)
        {
            await context.Product.AddAsync(product);
            await context.SaveChangesAsync();
            return product;
        }

        public async Task<Delivery> registerDelivery(Delivery delivery)
        {
            await context.Delivery.AddAsync(delivery);                        
            await context.SaveChangesAsync();
            var deliveryComplete = await context.Delivery.Include(u=> u.basurappUserContributor).Include(u=> u.basurappUserRecolector).FirstOrDefaultAsync(C => C.id == delivery.id);
            return deliveryComplete;        
        }

        public async Task<Delivery> getDeliveryById(int idDelivery)
        {
            var delivery = await context.Delivery.Include(u=> u.basurappUserContributor).Include(u=> u.basurappUserRecolector).FirstOrDefaultAsync(C => C.id == idDelivery);
            return delivery;        
        }

        public async Task<PagedList<Delivery>> getDeliveriesByRecolectorId(UserParams userParams, int idRecolector)
        {
            var deliveriesByRecolector = context.Delivery.Where(r => r.basurappUserRecolector.id == idRecolector).Include(u=> u.basurappUserRecolector).Include(u=> u.basurappUserContributor).OrderByDescending(d => d.id).AsQueryable();
            return await PagedList<Delivery>.createAsync(deliveriesByRecolector, userParams.pageNumber, userParams.pageSize);
        }
        
        public async Task<PagedList<Delivery>> getDeliveriesByContributorId(UserParams userParams,int idContributor)
        {
            var deliveriesByContributor = context.Delivery.Where(r => r.basurappUserContributor.id == idContributor).Include(u=> u.basurappUserContributor).Include(u=> u.basurappUserRecolector).OrderByDescending(d => d.id).AsQueryable();
            return await PagedList<Delivery>.createAsync(deliveriesByContributor, userParams.pageNumber, userParams.pageSize);
        }

        public async Task<PagedList<Delivery>> getFreeDeliveries(UserParams userParams)
        {
            var deliveries = context.Delivery.Include(u=> u.basurappUserContributor).OrderByDescending(d => d.id).AsQueryable().Where(l=>l.deliveryState==0);
            return await PagedList<Delivery>.createAsync(deliveries, userParams.pageNumber, userParams.pageSize);
        }

        public async Task<Delivery> getInProgressDeliveriesByUserID(int idRecolector)
        {                        
            var delivery = await context.Delivery.Where(r => r.basurappUserRecolector.id == idRecolector).Include(u=> u.basurappUserContributor).Include(u=> u.basurappUserRecolector).FirstOrDefaultAsync(D => D.deliveryState == 1);
            return delivery;  
        }

        public async Task<PagedList<Product>> getProducts(UserParams userParams)
        {
            var products = context.Product.Include(u=> u.creator).OrderByDescending(p => p.id).AsQueryable();
            return await PagedList<Product>.createAsync(products, userParams.pageNumber, userParams.pageSize);
        }
    }
}