namespace Dal;
/// <summary>
/// has the id of the next task and dependency by it's order
/// </summary>
    internal static class Config
    {
        static string s_data_config_xml = "data-config";
        internal const int taskId = 1;
        internal static int NextTaskId { get => XMLTools.GetAndIncreaseNextId(s_data_config_xml, "NextTaskId"); }
        internal static int NextDependentTaskId { get => XMLTools.GetAndIncreaseNextId(s_data_config_xml, "NextDependentTaskId"); }
       
    }

