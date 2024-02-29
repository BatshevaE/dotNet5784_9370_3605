

namespace DO;
public record User
(
    int Id,
    string Name,
    bool IsManager
)
{ public User() : this(0, "", false) { } }