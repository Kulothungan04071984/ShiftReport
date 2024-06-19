namespace Fuji_I.Models
{
   
    public class lane
    {
        public string lanename { get; set; }
        public AIMEX_1? AIMEX_llC_1 { get; set; }
        public AIMEX_2? AIMEX_llC_2 { get; set; }
        public AIMEX_3? AIMEX_llC_3 { get; set; }
        public decimal AIMEX_llC_1_M1 { get; set; }
        public decimal AIMEX_llC_1_M2 { get; set; }
        public decimal AIMEX_llC_2_M1 { get; set; }
        public decimal AIMEX_llC_2_M2 { get; set; }
        public decimal AIMEX_llC_3_M1 { get; set; }
        public decimal AIMEX_llC_3_M2 { get; set; }
        public string label { get; set; }
        public decimal linevalue { get; set; }
        
    }

    public class AIMEX_1
    {
        public decimal M1 { get; set; }
        public decimal M2 { get; set; }
    }

    public class AIMEX_2
    {
        public decimal M1 { get; set; }
        public decimal M2 { get; set; }
    }
    public class AIMEX_3
    {
        public decimal M1 { get; set; }
        public decimal M2 { get; set; }
    }

}

