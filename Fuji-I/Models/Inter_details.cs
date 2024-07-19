namespace Fuji_I.Models
{
    public interface Inter_details
    {
        List<Line1Utilization> GetLine1Utilization();
        List<Line2Utilization> GetLine2Utilization();
        List<Line3Utilization> GetLine3Utilization();
        //List<ModuleUtilization> GetModuleUtilization();
        List<List<string>> ReadCsv1(string filePath1);
        List<List<string>> ReadCsv2(string filePath2);
        List<List<string>> ReadCsv3(string filePath3);

        string ConvertCsvToHtmlTable(string filePath);
    }
}
