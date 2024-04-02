
namespace BO;

public class User
{
    /// <summary>
    /// password
    /// </summary>
    public int Password { get; init; } 
    /// <summary>
    /// name
    /// </summary>
    public string Name { get; init; }   
    /// <summary>
    /// the user is manager or not
    /// </summary>
    public bool IsManager { get; init; }
    /// <summary>
    /// id
    /// </summary>
    public int Id {  get; init; }   
    public override string ToString() => this.ToStringProperty();
    /// <summary>
    /// empty ctor
    /// </summary>
    public User()
    {
        Password = 0;
        Name = "";
        IsManager = false;
        Id = 0;
    }
}
