namespace BlImplementation;
using BlApi;

internal class EngineerImplementation : IEngineer
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    public int Create(BO.Engineer item)
    {
            if ((item.Id <= 0) || (item.Name == "") || (item.Name == null) || (item.Cost <= 0))
                throw new FormatException("wrong input");
        
        DO.Engineer doEngineer = new DO.Engineer
          (item.Id,item.Name,item.Email,(DO.EngineerLevel)item.Level,item.Cost);
        try
        {
            int idEngineer = _dal.Engineer.Create(doEngineer);
            return idEngineer;
        }
        catch (DO.DalAlreadyExistException ex)
        {
            throw new BO.BlAlreadyExistsException($"Engineer with ID={item.Id} already exists", ex);
        }

    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public BO.Engineer? Read(int id)
    {
        DO.Engineer? doStudent = _dal.Engineer.Read(id);
        if (doStudent == null)
            throw new BO.BlDoesNotExistException($"Student with ID={id} does Not exist");

        return new BO.Engineer()
        {
            Id = id,
            Name = doStudent.Name,
            Email= doStudent.EmailAsress,
            Level= (BO.EngineerLevel)doStudent.Complexity,
            Cost= doStudent.CostForHour,
            Task= new Tuple<int,string>()

        };

    }

    public IEnumerable<EngineerInTask> ReadAll()
    {
        throw new NotImplementedException();
    }

    public void Update(Engineer item)
    {
        throw new NotImplementedException();
    }
}
