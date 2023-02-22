using Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers
{
    public class AdminController : Controller
    {
        private UserManager<AppUser> _userManager;
        private IPasswordHasher<AppUser> _passwordHasher;
        private IPasswordValidator<AppUser> _passwordValidator;
        private IUserValidator<AppUser> _userValidator;
        public AdminController(UserManager<AppUser> userManager,
                               IPasswordHasher<AppUser> passwordHash,
                               IPasswordValidator<AppUser> passwordValidator,
                               IUserValidator<AppUser> userValidator
                               )
        {
            _userManager = userManager;
            _passwordHasher = passwordHash;
            _passwordValidator = passwordValidator;
            _userValidator = userValidator;
        }
        public IActionResult Index()
        {
            return View(_userManager.Users);
        }
        public ViewResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = new AppUser
                {
                    UserName = user.Name,
                    Email = user.Email,
                };
                var identityResult = await _userManager.CreateAsync(appUser, user.Password);
                if (identityResult.Succeeded)
                    return RedirectToAction("Index", "Admin");
                else
                    foreach (IdentityError error in identityResult.Errors)
                        ModelState.AddModelError("", error.Description);
            }
            return View(user);
        }

        public async Task<IActionResult> Update(string Id)
        {
            var appUser = await _userManager.FindByIdAsync(Id);
            if (appUser != null)
            {
                var updateUserDTO = new UpdateUserDTO();
                updateUserDTO.Id = appUser.Id;
                updateUserDTO.Name = appUser.UserName ?? "";
                updateUserDTO.Email = appUser.Email == null ? "" : appUser.Email;
                return View(updateUserDTO);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateUserDTO updateUserDTO)
        {
            var appUser = await _userManager.FindByIdAsync(updateUserDTO.Id);
            if (appUser != null)
            {
                if (updateUserDTO.Email != null)
                    appUser.Email = updateUserDTO.Email;
                else
                    ModelState.AddModelError("", "邮箱不能为空");
                if (updateUserDTO.Name != null)
                    appUser.UserName = updateUserDTO.Name;
                else
                    ModelState.AddModelError("", "用户名不能为空");
                if (updateUserDTO.Password != null)
                    appUser.PasswordHash = _passwordHasher.HashPassword(appUser, updateUserDTO.Password);
                else
                    ModelState.AddModelError("", "密码不能为空");
                if (!string.IsNullOrEmpty(updateUserDTO.Email) &&
                    !string.IsNullOrEmpty(updateUserDTO.Password))
                {
                    var result = await _userManager.UpdateAsync(appUser);
                    if (result.Succeeded)
                        return RedirectToAction("Index");
                    else
                    {
                        foreach (IdentityError error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
            }
            else
                ModelState.AddModelError("", "没有发现该用户");
            return View(updateUserDTO);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(string Id)
        {
            var appUser = await _userManager.FindByIdAsync(Id);
            if (appUser != null)
            {
                var result = await _userManager.DeleteAsync(appUser);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                    Errors(result);
            }
            else
                ModelState.AddModelError("", "没有发现该用户");
            return View("Index", _userManager.Users);
        }
        void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }
    }
}
