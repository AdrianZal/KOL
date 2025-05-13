using Tutorial9.Model;

namespace Tutorial9.Services;

public interface IDbService
{
    Task<DeliveryDTO> GetDeliveries(int id);
}