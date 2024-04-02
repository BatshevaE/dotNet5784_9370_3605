

namespace DO;
/// <summary>
/// user to enter the system
/// </summary>
/// <param name="Password">the user's password</param>
/// <param name="Name">the user's name</param>
/// <param name="IsManager">if the user is manager</param>
/// <param name="Id">the user's id</param>
public record User
(
    int Password,
    string Name,
    bool IsManager,
    int Id
)
    
{ public User() : this(0, "", false,0) { } }