using System.Data.Common;

namespace DalApi;
/// <summary>
/// //intarface that contains all definitions of the functions of DAL

/// </summary>
public interface IDal
{
   
    ITask Task { get; }
    IEngineer Engineer { get; }
    IDependency Dependency { get; }
    DateTime? StartProject { get; set; }
    DateTime? EndProject { get; set; }
    IUser User { get; }
   }
