using Dal;

namespace DalXml
{
    internal static class Config
    {
        static string s_data_config_xml = "data-config";
        internal const int taskId = 1;
        internal static int NextTaskId { get => XMLTools.GetAndIncreaseNextId(s_data_config_xml, "NextTaskId"); }
        internal static int NextDependentTaskId { get => XMLTools.GetAndIncreaseNextId(s_data_config_xml, "NextDependentTaskId"); }
       
    }
}
