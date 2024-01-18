namespace DO;
/// <summary>
/// id does not exist
/// </summary>
[Serializable]
public class DalDoesNotExistException:Exception
{
    public DalDoesNotExistException(string? massage) : base(massage) { }
}
/// <summary>
/// /// id is already  exist
/// </summary>
[Serializable]
public class DalAlreadyExistException : Exception
{
    public DalAlreadyExistException(string? massage) : base(massage) { }
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
public class DalXMLFileLoadCreateException : Exception
{
    public DalXMLFileLoadCreateException(string? massage) : base(massage) { }
}