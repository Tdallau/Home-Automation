namespace HomeAutomation.Models.Base
{
  public class Response<T>
  {
    public bool Success { get; set; }
    public T Data { get; set; }
    public string Error { get; set; }
  }
}