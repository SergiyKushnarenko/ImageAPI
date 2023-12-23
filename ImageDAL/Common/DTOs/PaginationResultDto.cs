namespace ImageDAL.Common.DTOs;

public class PaginationResultDto
{
    public bool IsLastPage => TotalPageCount == PageNumber;
    public int PageNumber { get; set; }
    public long TotalCount { get; set; }
    public int TotalPageCount { get; set; }

    public static int ToPageCount(long total, int pageSize)
    {
        return (int)Math.Ceiling((double)total / (pageSize > 0 ? pageSize : 1));
    }
}

public class PaginationResultDto<T> : PaginationResultDto
{
    public List<T> PageData { get; set; }
}