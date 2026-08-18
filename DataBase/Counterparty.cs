using System;
using System.Collections.Generic;
using System.Text;

namespace DataBase
{
    public class Counterparty
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public ICollection<Deal> Deals { get; set; } = new List<Deal>();
    }
}
