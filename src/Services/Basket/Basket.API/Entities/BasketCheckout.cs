﻿namespace Basket.API.Entities
{
    public class BasketCheckout
    {
        public string UserName { get; set; } = null!;
        public decimal TotalPrice { get; set; }

        //Billing Address
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string EmailAddress { get; set; } = null!;
        public string AddressLine { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string State { get; set; } = null!;
        public string ZipCode { get; set; } = null!;

        //Payment Details 
        public string? CardName { get; set; }
        public string? CardNumber { get; set; }
        public string? Expiration { get; set; }
        public string? CVV { get; set; }
        public int PaymentMethod { get; set; }
    }
}
