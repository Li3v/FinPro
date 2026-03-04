using Microsoft.EntityFrameworkCore;

namespace FinPro;

public class AuthService
{
    private readonly AppDbContext _dbcontext;

    public AuthService(AppDbContext dbContext)
    {
        _dbcontext = dbContext;
    }

    #region CreateUser
    public async Task<User> CreateUser(RegisterUserRequest request)
    {
        await VerifyData(request);
        var user = new User
        {
            Name = request.Name,
            Email = request.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            CreatedAt = DateTime.Now
        };
        _dbcontext.Users.Add(user);
        await _dbcontext.SaveChangesAsync();
        return user;
    }
    #endregion

    #region VerifyData
    public async Task VerifyData(RegisterUserRequest req)
    {
        var verifyData = await _dbcontext.Users.AnyAsync(u => u.Email == req.Email);

        if (verifyData)
            throw new InvalidOperationException("this email is already registered");
    }
    #endregion
}
