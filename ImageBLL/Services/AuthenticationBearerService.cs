using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using ImageBLL.Helpers;
using ImageBLL.Models.Auth;
using ImageBLL.Models;
using ImageDAL.Models;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using ImageDLL.Enums;
using Serilog;
using ImageBLL.Services.Interfaces;
using ImageDAL.Repositories.Interfaces;

namespace ImageBLL.Services;

public class AuthenticationBearerService : IAuthenticationBearerService
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _config;
    private readonly IMapper _mapper;

    public AuthenticationBearerService(IConfiguration config, IMapper mapper, IUserRepository userRepository)
    {
        _config = config;
        _mapper = mapper;
        _userRepository = userRepository;
    }

    /// <summary>
    /// Authentication Method , Checking password and get JWTBearerToken
    /// </summary>
    /// <returns>JWTBearerToken</returns>
    public async Task<ResponseModelDto> Login(LoginDto loginDto)
    {
        try
        {
            var currentUser = await _userRepository.GetAsyncByEmail(loginDto.Email);

            if (currentUser == null)
            {
                throw new Exception("User is not found");
            }

            var verifyResult = PasswordHelper.VerifyHashedPassword(currentUser.HashedPassword, loginDto.Password);
            if (verifyResult)
            {
                var jwtBearerToken = GetJWTBearerToken(currentUser);
                var mappedResult = _mapper.Map<ResponseModelDto>(currentUser);
                mappedResult.JwtBearerToken = jwtBearerToken;
                return mappedResult;
            }
            throw new Exception("An error during Verify Password");
        }
        catch (Exception ex)
        {
            Log.Error(ex, "An error occurred during login");
            throw;
        }
    }

    /// <summary>
    /// User registration method
    /// </summary>
    /// <returns>JWTBearerToken</returns>
    public ResponseModelDto Register(CreateUserDto userDto)
    {
        var user = _mapper.Map<User>(userDto);
        user.HashedPassword = PasswordHelper.HashPassword(userDto.Password);
        var random = new Random();
        user.Id = random.Next();
        try
        {
            _userRepository.CreateAsync(user);
            var jwtBearerToken = GetJWTBearerToken(user);
            var mappedResult = _mapper.Map<ResponseModelDto>(user);
            mappedResult.JwtBearerToken = jwtBearerToken;
            return mappedResult;

        }
        catch (Exception ex)
        {
            Log.Error(ex, "An error occurred during registration");
            throw;
        }
    }

    private string GetJWTBearerToken(User user)
    {
        var jwtKey = _config["Jwt:Key"];
        var jwtIssuer = _config["Jwt:Issuer"];
        var tokenHelper = new TokenHelper(jwtKey, jwtIssuer);

        var tokenConfig = new TokenConfig
        {
            ExpirationTime = DateTime.Now.AddHours(user.UserRoleId == UserRole.Admin ? _config.GetValue<int>("JwtSettings:AdminExpirationHours") : _config.GetValue<int>("JwtSettings:UserExpirationHours")),
            Claims = new Dictionary<string, string>()
                {
                    { JwtRegisteredClaimNames.NameId, user.Id.ToString() },
                    { ClaimTypes.Role, user.UserRoleId.ToString() },
                    { "DateOfJoining", DateTime.UtcNow.ToString() },
                    { JwtRegisteredClaimNames.Email, user.Email },
                    { "FirstName", user.FirstName },
                    { "LastName", user.FirstName }
                }
        };

        var jwtBearerToken = tokenHelper.GenerateJWTBearerToken(tokenConfig);
        return jwtBearerToken;
    }
}