namespace InfoNet.API.Dtos;

public class ApiResponse
{
    public bool Success { get; set; }=true;
    public string Message { get; set; }
}

public class ApiResponse<T> : ApiResponse
{
    public T Data { get; set; }
}