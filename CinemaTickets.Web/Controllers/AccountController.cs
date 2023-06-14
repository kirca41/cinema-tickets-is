using CinemaTickets.Domain.DomainModels;
using CinemaTickets.Domain.DTOs;
using CinemaTickets.Domain.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace CinemaTickets.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<CinemaTicketsApplicationUser> userManager;
        private readonly SignInManager<CinemaTicketsApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public AccountController(UserManager<CinemaTicketsApplicationUser> userManager,
            SignInManager<CinemaTicketsApplicationUser> signInManager,
        RoleManager<IdentityRole> roleManager)
        {

            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }

        [HttpGet, AllowAnonymous]
        public IActionResult Register()
        {
            UserRegistrationDto model = new UserRegistrationDto();
            return View(model);
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Register(UserRegistrationDto request)
        {
            if (ModelState.IsValid)
            {
                var userCheck = await userManager.FindByEmailAsync(request.Email);
                if (userCheck == null)
                {
                    var user = new CinemaTicketsApplicationUser
                    {
                        UserName = request.Email,
                        NormalizedUserName = request.Email,
                        Email = request.Email,
                        PhoneNumber = request.PhoneNumber,
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                        ShoppingCart = new ShoppingCart()
                    };
                    var result = await userManager.CreateAsync(user, request.Password);
                    if (result.Succeeded)
                    {
                        await roleManager.CreateAsync(new IdentityRole("Admin"));
                        await userManager.AddToRoleAsync(user, "Admin");
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        if (result.Errors.Count() > 0)
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError("message", error.Description);
                            }
                        }
                        return View(request);
                    }
                }
                else
                {
                    ModelState.AddModelError("message", "Email already exists.");
                    return View(request);
                }
            }
            return View(request);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            UserLoginDto model = new UserLoginDto();
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginDto model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user != null && !user.EmailConfirmed)
                {
                    ModelState.AddModelError("message", "Email not confirmed yet");
                    return View(model);

                }
                if (await userManager.CheckPasswordAsync(user, model.Password) == false)
                {
                    ModelState.AddModelError("message", "Invalid credentials");
                    return View(model);

                }

                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, true);

                if (result.Succeeded)
                {
                    await userManager.AddClaimAsync(user, new Claim("UserRole", "Standard"));
                    return RedirectToAction("Index", "Movies");
                }
                else if (result.IsLockedOut)
                {
                    return View("AccountLocked");
                }
                else
                {
                    ModelState.AddModelError("message", "Invalid login attempt");
                    return View(model);
                }
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AddUserToRole()
        {
            var users = userManager.Users.ToList();
            var roles = roleManager.Roles.ToList();

            var model = new AddUserToRoleDto
            {
                Users = new SelectList(users, "Id", "UserName"),
                Roles = new SelectList(roles, "Name", "Name")
            };

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> AddUserToRole(AddUserToRoleDto model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByIdAsync(model.SelectedUserId);

                if (user != null)
                {
                    var existingRoles = await userManager.GetRolesAsync(user);
                    await userManager.RemoveFromRolesAsync(user, existingRoles);

                    await userManager.AddToRoleAsync(user, model.SelectedRoleName);

                    return RedirectToAction("Index", "Movies");
                }
            }

            var users = userManager.Users.ToList();
            var roles = roleManager.Roles.ToList();

            model.Users = new SelectList(users, "Id", "UserName");
            model.Roles = new SelectList(roles, "Name", "Name");

            return View(model);
        }


    }

}
