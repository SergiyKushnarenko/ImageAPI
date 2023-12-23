namespace ImageDAL.Common.DTOs;

public class FilteringBaseDto : PaginationParamsDto
{
    public bool AscSort { get; set; } = true;
}