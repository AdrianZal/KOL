using System.ComponentModel.DataAnnotations;

namespace Tutorial9.Model;

public class DeliveryDTO
{
    public DateTime date { get; set; }
    public Customer customer { get; set; }
    public Driver driver { get; set; }
    
    public List<Product> products { get; set; }
}

public class Driver
{
    public string firstName { get; set; }   
    public string lastName { get; set; }
    public string licenceNumber { get; set; }    
}

public class Customer
{
    public string firstName { get; set; } 
    public string lastName { get; set; }
    public DateTime dateOfBirth { get; set; }   
} 

public class Product
{
    public string name { get; set; }   
    public decimal price { get; set; } 
    public int amount { get; set; }
} 
