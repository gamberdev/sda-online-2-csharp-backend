using System.Security.Cryptography.X509Certificates;

public class queryParameters
{
   public int PageNumber { get; set; } = 1;
    public int Limit { get; set; } = 5;
    public string SearchTerm { get; set; } = string.Empty;
    public int SortBy { get; set; } = 0;
    public string SortOrder { get; set; } = "asc";

}