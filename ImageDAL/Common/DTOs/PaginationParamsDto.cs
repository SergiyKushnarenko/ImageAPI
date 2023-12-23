namespace ImageDAL.Common.DTOs;

public class PaginationParamsDto
{
    public int PageSize { get; set; } = 30;
    public int PageNumber { get; set; } = 1;
}