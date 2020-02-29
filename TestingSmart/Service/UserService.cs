using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TestingSmart.DataContext;
using TestingSmart.Helper;
using TestingSmart.Models;

namespace TestingSmart.Service
{
    public interface IUserService
    {
        Task<User> Authenticate(string username, string password);
        IEnumerable<User> GetAll();
    }
    public class UserService : IUserService
    {


        private List<User> _users = new List<User>
        {
            new User { Id = "44884fdjk", UserName = "Test", PasswordHash = "User",  Email = "test" }
        };

        private readonly AppSetting _appSettings;
        private readonly UserManager<User> _userManager;
        private readonly AppDbContext _context;
        public UserService(IOptions<AppSetting> appSettings, UserManager<User> userManager,AppDbContext context)
        {
            _appSettings = appSettings.Value;
            _userManager = userManager;
            _context = context;
        }

        public async Task<User> Authenticate(string username, string password)
        {
            var user = new User();

            //var otherTable =_context.Units.Where(x=>x.Id==8).FirstOrDefault();
            //if(otherTable!=null)
            //{

            //}
            try
            {
                user= await _userManager.FindByNameAsync(username); 

                if (user != null)
                {
                    
                    var check = await _userManager.CheckPasswordAsync(user, password);
                    if (check)
                    {
                        var tokenHandler = new JwtSecurityTokenHandler();
                        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                        var tokenDescriptor = new SecurityTokenDescriptor
                        {
                            Subject = new ClaimsIdentity(new Claim[]
                            {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                            }),
                            Expires = DateTime.UtcNow.AddMinutes(150),
                            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                        };
                        var token = tokenHandler.CreateToken(tokenDescriptor);
                        user.SecurityStamp = tokenHandler.WriteToken(token);

                     
                    }else
                    {
                        return null;
                    }
                }

                else
                {
                    return null;
                }

                return user.WithoutPassword();
            }
            catch (Exception ex)
            {

                return null;
            }
            // var user =.Users.Where(x=>x.UserName==username && x.PasswordHash==password).SingleOrDefault(x => x.UserName == username && x.PasswordHash == password);
          
         
                // return null if user not found
                if (user == null)
                    return null;

                // authentication successful so generate jwt token
               
            
              
        }

        public IEnumerable<User> GetAll()
        {
            return _users.WithoutPasswords();
        }
    }
}
