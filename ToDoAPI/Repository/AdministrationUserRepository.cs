using AutoMapper;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ToDoAPI.Repository.IRepository;
using Utils.Shared.DTO;
using Utils.Shared.Helper;

namespace ToDoAPI.Repository
{
    public class AdministrationUserRepository : IAministrationUserRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _usermanager;
        private readonly RoleManager<IdentityRole> _rolemanager;
        private readonly IMapper _mapper;
        private string SecretKey;

        public AdministrationUserRepository(ApplicationDbContext db, IConfiguration configuration,
           UserManager<ApplicationUser> usermanager, IMapper mapper, RoleManager<IdentityRole> rolemanager)
        {
            _db = db;
            _usermanager = usermanager;
            _mapper = mapper;
            _rolemanager = rolemanager;
            SecretKey = configuration.GetValue<string>("APISettings:Secret");
        }

        public bool IsUniqueUser(string username)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(x => x.UserName == username);
            if (user == null)
            {
                return true;
            }
            return false;
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginrequestdto)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(u => u.UserName.ToLower() == loginrequestdto.Username.ToLower());
            bool isvalid = await _usermanager.CheckPasswordAsync(user, loginrequestdto.Password);

            if (user == null || isvalid == false)
            {
                return new LoginResponseDTO()
                {
                    Token = "",
                    User = null

                };
            }
            var roles = await _usermanager.GetRolesAsync(user);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(SecretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName.ToString()),
                    new Claim(ClaimTypes.Role, roles.FirstOrDefault()),
                    new Claim(ClaimTypes.GroupSid, user.Id)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var Token = tokenHandler.CreateToken(tokenDescriptor);
            LoginResponseDTO loginresponsedto = new LoginResponseDTO()
            {
                Token = tokenHandler.WriteToken(Token),
                User = _mapper.Map<AdministrationUserDTO>(user),
                Role = roles.FirstOrDefault(),

            };
            return loginresponsedto;


        }

        public async Task<AdministrationUserDTO> Register(RegistrationRequestDTO registrationrequestdto)
        {
            ApplicationUser user = new()
            {
                UserName = registrationrequestdto.UserName,
                Email = registrationrequestdto.UserName,
                NormalizedEmail = registrationrequestdto.UserName.ToUpper(),
                FirstName = registrationrequestdto.Name,
                LastName = registrationrequestdto.LastNameName,

            };
            try
            {
                var result = await _usermanager.CreateAsync(user, registrationrequestdto.Password);
                if (result.Succeeded)
                {

                    if (!_rolemanager.RoleExistsAsync(SD.Role_BRUS).GetAwaiter().GetResult())
                    {
                        _rolemanager.CreateAsync(new IdentityRole(SD.Role_BRUS)).GetAwaiter().GetResult();
                    }
                    if (!_rolemanager.RoleExistsAsync(SD.Role_BRBO).GetAwaiter().GetResult())
                    {
                        _rolemanager.CreateAsync(new IdentityRole(SD.Role_BRBO)).GetAwaiter().GetResult();
                    } 
                    await _usermanager.AddToRoleAsync(user, SD.Role_BRBO);
                    var userToReturn = _db.ApplicationUsers.FirstOrDefault(u => u.UserName == registrationrequestdto.UserName);
                    return _mapper.Map<AdministrationUserDTO>(userToReturn);
                }
                string errorMessage = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception(errorMessage);
            }
            catch (Exception ex)
            {
                throw new Exception("Registration failed: " + ex.Message);

            }

            return new AdministrationUserDTO();
        }
    }
}
