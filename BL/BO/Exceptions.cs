namespace BO;
/// <summary>
/// id does not exist
/// </summary>
[Serializable]
public class BlDoesNotExistException : Exception
{
    public BlDoesNotExistException(string? message) : base(message) { }
    public BlDoesNotExistException(string message, Exception innerException)
                : base(message, innerException) { }

}
/// <summary>
/// /// id is already  exist
/// </summary>
[Serializable]
public class BlAlreadyExistException : Exception
{
    public BlAlreadyExistException(string? message) : base(message) { }
    public BlAlreadyExistException(string message, Exception innerException)
                : base(message, innerException) { }
}

/// <summary>
/// the list is null
/// </summary>

[Serializable]
public class NullReferenceException : Exception
{
    public NullReferenceException(string? message) : base(message) { }
    public NullReferenceException(string message, Exception innerException)
                : base(message, innerException) { }
}
/// <summary>
/// 
/// </summary>
[Serializable]
public class BlXMLFileLoadCreateException : Exception
{
    public BlXMLFileLoadCreateException(string? message) : base(message) { }
    public BlXMLFileLoadCreateException(string message, Exception innerException)
                : base(message, innerException) { }
}
[Serializable]
public class BlCanNotDelete : Exception
{
    public BlCanNotDelete(string? message) : base(message) { }
    public BlCanNotDelete(string message, Exception innerException)
                : base(message, innerException) { }
}
[Serializable]
public class BlWrongInput : Exception
{
    public BlWrongInput(string? message) : base(message) { }
    public BlWrongInput(string message, Exception innerException)
                : base(message, innerException) { }
}
