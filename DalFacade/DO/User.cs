

namespace DO;
public record User
(
    int Password,
    string Name,
    bool IsManager,
    int Id
)
{ public User() : this(0, "", false,0) { } }