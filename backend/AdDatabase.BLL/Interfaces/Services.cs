using AdDatabase.BLL.Entities; using AdDatabase.BLL.Models;
namespace AdDatabase.BLL.Interfaces;
public interface IAuthService { Task<AuthResult> RegisterAsync(RegisterRequest request,CancellationToken ct); Task<AuthResult> LoginAsync(LoginRequest request,CancellationToken ct); Task<User?> GetProfileAsync(int userId,CancellationToken ct); }
public interface IAdService { Task<PagedResult<Ad>> SearchAsync(AdFilter filter,CancellationToken ct); Task<Ad> GetAsync(int id,CancellationToken ct); Task<Ad> CreateAsync(int userId,CreateAdRequest request,CancellationToken ct); Task<Ad> UpdateAsync(int userId,int id,UpdateAdRequest request,CancellationToken ct); Task DeleteAsync(int userId,int id,CancellationToken ct); }
public interface IConversationService { Task<List<Conversation>> MineAsync(int userId,CancellationToken ct); Task<Conversation> MessagesAsync(int userId,int id,CancellationToken ct); Task<Message> SendAsync(int userId,int id,SendMessageRequest request,CancellationToken ct); }
