using AdDatabase.BLL.Entities;
namespace AdDatabase.BLL.Models;
public record ApiResponse<T>(bool Success,string Message,T? Data) { public static ApiResponse<T> Ok(T data,string message="Success")=>new(true,message,data); public static ApiResponse<T> Fail(string message)=>new(false,message,default); }
public record PagedResult<T>(IReadOnlyList<T> Items,int Page,int PageSize,int Total) { public int TotalPages => (int)Math.Ceiling(Total/(double)PageSize); }
public record AdFilter(int Page=1,int PageSize=20,int? CategoryId=null,int? RegionId=null,decimal? MinPrice=null,decimal? MaxPrice=null,string? Search=null);
public record RegisterRequest(string Email,string Password,string UserName,string? FullName);
public record LoginRequest(string Email,string Password);
public record AuthResult(string Token,DateTime ExpiresAt,int UserId,string UserName,string Role);
public record CreateAdRequest(string Title,string Description,decimal Price,int CategoryId,int RegionId,DateTime? ExpiresAt,List<string>? ImageUrls,Dictionary<string,string>? Attributes);
public record UpdateAdRequest(string Title,string Description,decimal Price,int CategoryId,int RegionId,DateTime? ExpiresAt,List<string>? ImageUrls,Dictionary<string,string>? Attributes);
public record AddCommentRequest(int AdId,string Body);
public record SendMessageRequest(string Body,List<string>? AttachmentUrls);
public record RatingSummary(double Average,int Count);
public sealed class JwtOptions { public string Issuer {get;set;}="AdDatabase"; public string Audience {get;set;}="AdDatabase.Client"; public string Secret {get;set;}=""; public int ExpiryMinutes {get;set;}=60; }
