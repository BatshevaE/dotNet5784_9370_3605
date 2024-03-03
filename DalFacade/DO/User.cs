

namespace DO;
public record User
(
    int Password,
    string Name,
    bool IsManager
)
{ public User() : this(0, "", false) { } }