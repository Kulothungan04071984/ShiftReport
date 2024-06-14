namespace Fuji_I.Models
{
    public interface Inter_details
    {
        List<LineUtilization> GetLineUtilization();
        List<ModuleUtilization> GetModuleUtilization();
    }
}
