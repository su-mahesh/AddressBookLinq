﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AddressBookLinq
{
    public class Contact
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Address { get; set; } = "";
        public string City { get; set; } = "";
        public string State { get; set; } = "";
        public string Zip { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public string Email { get; set; } = "";

        public string Type { get; set; } = "";

        public Contact(string FirstName, string LastName, string Address, string City, string State, string Zip, string PhoneNumber, string Email, string Type)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Address = Address;
            this.City = City;
            this.State = State;
            this.Zip = Zip;
            this.PhoneNumber = PhoneNumber;
            this.Email = Email;
            this.Type = Type;
        }
    }
}
