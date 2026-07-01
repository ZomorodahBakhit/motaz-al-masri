using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using University.Core.Helpers;
using University.Core.DTOs;
using University.Core.Exceptions;
using University.Core.Forms;
using UniversitySystemSummer.Core.DTOs;

namespace University.Core.Services
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(RegisterForm form);
        Task<string> LoginAsync(LoginForm form);
    }

    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JwtTokenHelper _jwtTokenHelper;

        public AuthService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, JwtTokenHelper jwtTokenHelper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtTokenHelper = jwtTokenHelper;
        }

        public async Task<string> RegisterAsync(RegisterForm form)
        {
            if (!await _roleManager.RoleExistsAsync(form.Role))
                throw new BusinessException($"Role '{form.Role}' does not exist.");

            var user = new IdentityUser { UserName = form.Email, Email = form.Email };
            var result = await _userManager.CreateAsync(user, form.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                throw new BusinessException(string.Join(", ", errors));
            }

            
            await _userManager.AddToRoleAsync(user, form.Role);
            return "User registered successfully.";
        }

        public async Task<string> LoginAsync(LoginForm form)
        {
            var user = await _userManager.FindByEmailAsync(form.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, form.Password))
                throw new BusinessException("Invalid email or password.");

            var roles = await _userManager.GetRolesAsync(user);
            var userRole = roles.FirstOrDefault() ?? "Student";

            var userDto = new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                Role = userRole
            };

            return _jwtTokenHelper.GenerateToken(userDto);
        }
    }
}