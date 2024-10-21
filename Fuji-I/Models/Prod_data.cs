namespace Fuji_I.Models
{
    public class Prod_data
    {
        public int Id { get; set; }

        public string CurrentDate { get; set; }
        public string Hour { get; set; }
        public string FG_Name { get; set; }
        public int Target {  get; set; }
        public string Actual { get; set; }

       // public List<Prod_data> lstProd { get; set; }
        //public string Workorder_no { get; set; }
    
   
        //public string WO_Quantity  { get; set; }
        //public string uph { get; set; }

        //public DateTime created_at { get; set; }





    }

    public class HourlyData
    {
        public DateTime Date { get; set; }
        public int Hour { get; set; }
        public int Actual { get; set; }
        public int Target { get; set; }
    }

    public class Item
    {
        public string Name { get; set; }
        public int Value { get; set; }
    }
}
