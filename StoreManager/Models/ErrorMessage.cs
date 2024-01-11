namespace StoreManager.Models;

public class ErrorMessage
{
    public ErrorMessage(string message)
    {
        Message = message;
    }

    public string Message { get; set; }
}