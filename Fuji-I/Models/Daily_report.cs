
namespace Fuji_I.Models
{
    public partial class Daily_report
    {


        public string? FG_Name { get; set; }
        
        //public DateOnly? CurrentDate { get; set; }
        public string? CurrentDate { get; set; }

        public string? WorkOrder { get; set; }

        public int? BoardActual { get; set; }
        public int? PCB_COUNT { get; set;}

    }
    public partial class Dailyreport
    {


        public string? FG_Name { get; set; }

        //public DateOnly? CurrentDate { get; set; }
        public string CurrentDate { get; set; }
        public string? WorkOrder { get; set; }

        public int? BoardActual { get; set; }

        public int? PCB_COUNT { get; set; }

    }




}

