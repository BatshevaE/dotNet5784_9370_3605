using System.Data.Common;

namespace DalApi;
/// <summary>
/// /
/// </summary>
public interface IDal
{
    /// <summary>
    /// 
    /// </summary>
    ITask Task { get; }
    IEngineer Engineer { get; }
    IDependency Dependency { get; }
    DateTime? StartProject { get; set; }
   }
