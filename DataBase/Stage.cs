using System;
using System.Collections.Generic;
using System.Text;

namespace DataBase
{
    public class Stage
    {
        public int Id { get; set; }
        public int DealId { get; set; }
        public int Priority { get; set; }
        public string? Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishedDate { get; set; }
        public bool IsCurrent { get; set; }

        public Deal? DealForStage { get; set; }
    }
}
