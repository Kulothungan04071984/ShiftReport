
namespace Fuji_I.Models
{
    public partial class Work_order
    {
        public int LineDetaileID { get; set; }

        public string? WorkOrderNumber { get; set; }

        public string? FG_Number { get; set; }

        public int? Qty { get; set; }

        public int? Cycle_time { get; set; }

        public int? uph { get; set; }

        public string? CurrentDate { get; set; }

    }

}

