using System.Data;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using Tutorial9.Model;

namespace Tutorial9.Services;

public class DbService : IDbService
{
    private readonly string _connectionString;

    public DbService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Default") ?? string.Empty;
    }

    public async Task<DeliveryDTO> GetDeliveries(int id)
    {
        
        var delivery  = new DeliveryDTO();
        using (SqlConnection conn1 = new SqlConnection(_connectionString))
        using (SqlCommand cmd1 = new SqlCommand($"SELECT date FROM Delivery WHERE delivery_id = {id}", conn1))
        {
            await conn1.OpenAsync();
            using (var reader = await cmd1.ExecuteReaderAsync())
            {
                delivery.date = reader.GetDateTime(0);
            }
        }
        if (delivery.date == null) return null;
        
        using (SqlConnection conn2 = new SqlConnection(_connectionString))
        using (SqlCommand cmd2 = new SqlCommand($"SELECT * FROM Customer WHERE customer_id = (SELECT customer_id FROM Delivery WHERE delivery_id = {id})", conn2))
        {
            await conn2.OpenAsync();
            using (var reader = await cmd2.ExecuteReaderAsync())
            {
                delivery.customer.firstName = reader.GetString(1);
                delivery.customer.lastName = reader.GetString(2);
                delivery.customer.dateOfBirth = reader.GetDateTime(3);
            }
        }
        
        using (SqlConnection conn3 = new SqlConnection(_connectionString))
        using (SqlCommand cmd3 = new SqlCommand($"SELECT * FROM Driver WHERE driver_id = (SELECT driver_id FROM Delivery WHERE delivery_id = {id})", conn3))
        {
            await conn3.OpenAsync();
            using (var reader = await cmd3.ExecuteReaderAsync())
            {
                delivery.driver.firstName = reader.GetString(1);
                delivery.driver.lastName = reader.GetString(2);
                delivery.driver.licenceNumber = reader.GetString(3);
            }
        }
        
        using (SqlConnection conn4 = new SqlConnection(_connectionString))
        using (SqlCommand cmd4 = new SqlCommand($"SELECT * FROM Product_Delivery WHERE delivery_id = {id})", conn4))
        {
            await conn4.OpenAsync();
            using (var reader = await cmd4.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {   
                    delivery.products.Add(new Product()
                    {
                        amount = reader.GetInt32(2),
                    });
                }
            }
        }
        
        using (SqlConnection conn4 = new SqlConnection(_connectionString))
        using (SqlCommand cmd4 = new SqlCommand($"SELECT * FROM Product WHERE product_id = (SELECT product_id FROM Product_Delivery WHERE delivery_id = {id}))", conn4))
        {
            await conn4.OpenAsync();
            using (var reader = await cmd4.ExecuteReaderAsync())
            {
                int c = 0;
                while (await reader.ReadAsync())
                {
                    delivery.products[c].name = reader.GetString(1);
                    delivery.products[c].price = reader.GetDecimal(2);
                    c++;
                }
            }
        }
        
        return delivery;
    }
}
