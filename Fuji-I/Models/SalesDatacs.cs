namespace Fuji_I.Models
{
    public class SalesData
    {
        //public string Month { get; set; }
        //public int Sales { get; set; }
        //public int Revenue { get; set; }

        public string Category { get; set; }
        public int Year { get; set; }
        public int Sales { get; set; }


    }


    public class SalesDataGroup
    {
        public int Year { get; set; }
        public List<int> SalesByCategory { get; set; }
    }

    public class StackedBarChartViewModel
    {
        public List<string> Categories { get; set; }
        public List<int> Years { get; set; }
        public List<SalesDataGroup> DataGroupedByYear { get; set; }
    }

}
