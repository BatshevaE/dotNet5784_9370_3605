
namespace BO;

public class User
{
    public int Id { get; init; } 
    public string Name { get; init; }   
    public bool IsManager { get; init; }
    public override string ToString() => this.ToStringProperty();
    public User()
    {
        Id = 0;
        Name = "";
        IsManager = false;
    }
}
