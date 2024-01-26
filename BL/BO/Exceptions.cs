namespace BO;
/// <summary>
/// id does not exist
/// </summary>
[Serializable]
public class BlDoesNotExistException : Exception
{
    public BlDoesNotExistException(string? massage) : base(massage) { }
}
/// <summary>
/// /// id is already  exist
/// </summary>
[Serializable]
public class BlAlreadyExistException : Exception
{
    public BlAlreadyExistException(string? massage) : base(massage) { }
}

/// <summary>
/// the list is null
/// </summary>

[Serializable]
public class NullReferenceException : Exception
{
    public NullReferenceException(string? massage) : base(massage) { }
}
/// <summary>
/// 
/// </summary>
[Serializable]
public class BlXMLFileLoadCreateException : Exception
{
    public BlXMLFileLoadCreateException(string? massage) : base(massage) { }
}