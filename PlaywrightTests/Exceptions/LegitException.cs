namespace PlaywrightTests.Exceptions;

public class LegitException : Exception
{
    private const string ErrorMessage =
        "****************************************************************************\n" +
        "An unexpected behaviour happened while executing the tests, please analyze the error down bellow.\n" +
        "****************************************************************************\n";
    public LegitException()
    {
        Console.WriteLine(ErrorMessage);
    }

    public LegitException(string message)
        : base(message)
    {
        Console.WriteLine(ErrorMessage);
    }

    public LegitException(string message, Exception innerException)
        : base(message, innerException)
    {
        Console.WriteLine(ErrorMessage);
    }    
}