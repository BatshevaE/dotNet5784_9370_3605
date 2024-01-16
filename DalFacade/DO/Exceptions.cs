namespace DO;
[Serializable]
public class DalDoesNotExistException:Exception
{
    public DalDoesNotExistException(string? massage) : base(massage) { }
}
[Serializable]
public class DalAlreadyExistException : Exception
{
    public DalAlreadyExistException(string? massage) : base(massage) { }
}

[Serializable]
public class NullReferenceException : Exception
{
    public NullReferenceException(string? massage) : base(massage) { }
}
