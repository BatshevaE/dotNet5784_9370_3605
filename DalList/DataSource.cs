namespace Dal;
/// <summary>
/// this is The database of the data layer. for each collection of entities of a 
/// certain type will be saved as lists in the internal memory of the List type. 
/// </summary>

internal static class DataSource
{
    /// <summary>
    /// this is an inner class which will generate automatic running numbers for us, 
    /// for the fields that are defined as "running ID number"
    /// </summary>
    internal static class Config
    {
        //this is for the task id
        internal const int taskId = 1;
        private static int nextTaskId = taskId;
        internal static int NextTaskId { get => nextTaskId++; }

        //this is for the dependent task id
        internal const int dependentTaskId = 1;
        private static int nextDependentTaskId = dependentTaskId;
        internal static int NextDependentTaskId { get => nextDependentTaskId++; }
        public static DateTime? startWorkProject = null;
        public static DateTime? endWorkProject = null;
         
    }
   
    internal static List<DO.Task> Tasks { get; } = new();//a list for al the tasks
    internal static List<DO.Dependency> Dependencys { get; } = new();//a list for al the Dependency tasks
    internal static List<DO.Engineer> Engineers { get; } = new();//a list for al the Engineers
    internal static List<DO.User> Users { get; } = new();//a list for al the Users


}
