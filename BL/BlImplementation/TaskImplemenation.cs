using BlApi;
using BO;

namespace BlImplementation;

internal class TaskImplemenation : ITask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    public int Create(BO.Task item)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public BO.Task? Read(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<TaskInList> ReadAll()
    {
        throw new NotImplementedException();
    }

    public void Update(BO.Task item)
    {
        throw new NotImplementedException();
    }

    public void updateStartDate(int id, DateTime startDate)
    {
        throw new NotImplementedException();
    }
}
