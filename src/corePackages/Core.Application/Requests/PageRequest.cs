namespace Core.Application.Requests;

public class PageRequest
{
    public int Page { get; set; } = default;
    public int PageSize { get; set; } = default;
}