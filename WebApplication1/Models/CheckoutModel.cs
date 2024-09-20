using System.Collections.Generic;

namespace WebApplication1.Models
{
    public class CheckoutModel
    {
        public string Name { get; set; }         // Customer's name
        public string Address { get; set; }      // Shipping address
        public string Phone { get; set; }        // Contact phone number

        // List of cart items being checked out
        public List<CartItem> CartItems { get; set; }
    }
}
