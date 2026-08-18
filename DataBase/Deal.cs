using System;
using System.Collections.Generic;
using System.Text;

namespace DataBase
{
    public  class Deal
    {
        public int Id {  get; set; }
        public int CounterpartyId { get; set; }
        public string? Title { get; set; }
        public decimal Amount { get; set; }

        public ICollection<Stage> Stages { get; set; } = new List<Stage>();
        public Counterparty? CounterpartyForDeal { get; set; }

    }
}
