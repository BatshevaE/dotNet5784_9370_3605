
namespace BO;

public class User
{
    public int Password { get; init; } 
    public string Name { get; init; }   
    public bool IsManager { get; init; }
    public int Id {  get; init; }   
    public override string ToString() => this.ToStringProperty();
    public User()
    {
        Password = 0;
        Name = "";
        IsManager = false;
        Id = 0;
    }
}
