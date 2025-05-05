using static System.Console;

public class MyException : Exception
{
    DateTime _time_exc;

    public DateTime Time_exc { get; private set; }

    public MyException() : base("My exception was called")
    {
        _time_exc = DateTime.Now;
    }

    public MyException(string message) : base(message) { }
    public MyException(string message, Exception innerException) : base(message, innerException) { }
    protected MyException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}