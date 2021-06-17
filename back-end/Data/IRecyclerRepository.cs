using System.Threading.Tasks;
using basurapp.api.Helpers;
using basurapp.api.Models;

namespace basurapp.api.Data
{
    public interface IRecyclerRepository
    {
        Task<bool> saveAll();
        Task<BasurappUser> getBasurappUserById(int idUser);
        Task<Delivery> registerDelivery(Delivery delivery);
        Task<Delivery> getDeliveryById(int idDelivery);
        Task<PagedList<Delivery>> getDeliveriesByContributorId(UserParams userParams, int idContributor);
        Task<PagedList<Delivery>> getDeliveriesByRecolectorId(UserParams userParams, int idRecolector);
        Task<PagedList<Delivery>> getFreeDeliveries(UserParams userParams);
        Task<Delivery> getInProgressDeliveriesByUserID(int idContributor);
        Task<Product> registerProduct(Product product);
        Task<PagedList<Product>> getProducts(UserParams userParams);
    }
}