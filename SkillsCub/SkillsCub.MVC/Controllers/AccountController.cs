using System;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SkillsCub.DataLibrary.Entities.Implementation;
using SkillsCub.MVC.Extensions;
using SkillsCub.MVC.Models.AccountViewModels;
using SkillsCub.MVC.ViewModels.AccountViewModels;
using SkillsCub.TelegramLogger;
using IEmailSender = SkillsCub.EmailSenderService.IEmailSender;

namespace SkillsCub.MVC.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private readonly ITelegramLogger _telegramLogger;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            ILogger<AccountController> logger, 
            RoleManager<IdentityRole> roleManager,
            ITelegramLogger telegramLogger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
            _roleManager = roleManager;
            _telegramLogger = telegramLogger;
        }

        [TempData]
        public string ErrorMessage { get; set; }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    return RedirectToLocal(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToAction(nameof(LoginWith2fa), new { returnUrl, model.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToAction(nameof(Lockout));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> LoginWith2fa(bool rememberMe, string returnUrl = null)
        {
            // Ensure the user has gone through the username & password screen first
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();

            if (user == null)
            {
                throw new ApplicationException($"Unable to load two-factor authentication user.");
            }

            var model = new LoginWith2faViewModel { RememberMe = rememberMe };
            ViewData["ReturnUrl"] = returnUrl;

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginWith2fa(LoginWith2faViewModel model, bool rememberMe, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var authenticatorCode = model.TwoFactorCode.Replace(" ", string.Empty).Replace("-", string.Empty);

            var result = await _signInManager.TwoFactorAuthenticatorSignInAsync(authenticatorCode, rememberMe, model.RememberMachine);

            if (result.Succeeded)
            {
                _logger.LogInformation("User with ID {UserId} logged in with 2fa.", user.Id);
                return RedirectToLocal(returnUrl);
            }
            else if (result.IsLockedOut)
            {
                _logger.LogWarning("User with ID {UserId} account locked out.", user.Id);
                return RedirectToAction(nameof(Lockout));
            }
            else
            {
                _logger.LogWarning("Invalid authenticator code entered for user with ID {UserId}.", user.Id);
                ModelState.AddModelError(string.Empty, "Invalid authenticator code.");
                return View();
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> LoginWithRecoveryCode(string returnUrl = null)
        {
            // Ensure the user has gone through the username & password screen first
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                throw new ApplicationException($"Unable to load two-factor authentication user.");
            }

            ViewData["ReturnUrl"] = returnUrl;

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginWithRecoveryCode(LoginWithRecoveryCodeViewModel model, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                throw new ApplicationException($"Unable to load two-factor authentication user.");
            }

            var recoveryCode = model.RecoveryCode.Replace(" ", string.Empty);

            var result = await _signInManager.TwoFactorRecoveryCodeSignInAsync(recoveryCode);

            if (result.Succeeded)
            {
                _logger.LogInformation("User with ID {UserId} logged in with a recovery code.", user.Id);
                return RedirectToLocal(returnUrl);
            }
            if (result.IsLockedOut)
            {
                _logger.LogWarning("User with ID {UserId} account locked out.", user.Id);
                return RedirectToAction(nameof(Lockout));
            }
            else
            {
                _logger.LogWarning("Invalid recovery code entered for user with ID {UserId}", user.Id);
                ModelState.AddModelError(string.Empty, "Invalid recovery code entered.");
                return View();
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Lockout()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
                    await _emailSender.SendEmailConfirmationAsync(model.Email, callbackUrl);

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    _logger.LogInformation("User created a new account with password.");
                    return RedirectToLocal(returnUrl);
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{userId}'.");
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return RedirectToAction(nameof(ForgotPasswordConfirmation));
                }

                // For more information on how to enable account confirmation and password reset please
                // visit https://go.microsoft.com/fwlink/?LinkID=532713
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.ResetPasswordCallbackLink(user.Id, code, Request.Scheme);
                await _emailSender.SendEmailAsync(model.Email, "Reset Password",
                   $"Please reset your password by clicking here: <a href='{callbackUrl}'>link</a>");
                return RedirectToAction(nameof(ForgotPasswordConfirmation));
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string code = null)
        {
            if (code == null)
            {
                throw new ApplicationException("A code must be supplied for password reset.");
            }
            var model = new ResetPasswordViewModel { Code = code };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction(nameof(ResetPasswordConfirmation));
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(ResetPasswordConfirmation));
            }
            AddErrors(result);
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmRequest(Guid id)
        {

            try
            {
                var user = await _userManager.FindByIdAsync(id.ToString("D"));
                if (user == null)
                {
                    await _telegramLogger.Error($"User {id:D} not exist in DB");
                    return null;
                }
                await _telegramLogger.Debug($"User {id:D} go to create password View");

                //set vmodel to Id and 2 passwords
                return View(new ConfirmRequsetViewModel(){Id = id});

            }
            catch (Exception ex)
            {
                await _telegramLogger.Error($"Request was confirmed with Error {Environment.NewLine} {ex.Message}");
                return null;
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmRequestForTeacher(Guid id)
        {

            try
            {
                var user = await _userManager.FindByIdAsync(id.ToString("D"));
                if (user == null)
                {
                    await _telegramLogger.Error($"User {id:D} not exist in DB");
                    return null;
                }
                await _telegramLogger.Debug($"User {id:D} go to create password View");

                //set vmodel to Id and 2 passwords
                return View(new ConfirmRequsetViewModel(){Id = id});

            }
            catch (Exception ex)
            {
                await _telegramLogger.Error($"Request was confirmed with Error {Environment.NewLine} {ex.Message}");
                return null;
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmRequest(ConfirmRequsetViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _telegramLogger.Debug($"User {model.Id:D} create password");

                    var user = await _userManager.FindByIdAsync(model.Id.ToString("D"));
                    if (user == null)
                    {
                        await _telegramLogger.Error($"User {model.Id:D} not exist in DB");
                        return null;
                    }
                    //possible move activation after password set
                    user.IsActive = true;
                    user.EmailConfirmed = true;
                    var result = await _userManager.UpdateAsync(user);
                    await _telegramLogger.Debug($"User {model.Id:D} email confirmed & activate");

                    var result2 = await _userManager.AddPasswordAsync(user, model.Password);
                    await _telegramLogger.Debug($"User {model.Id:D} password added");

                    if (!_roleManager.Roles.Any(role => role.Name.Equals("User")))
                    {
                        await _roleManager.CreateAsync(new IdentityRole("User"));
                        await _telegramLogger.Debug($"User role added");

                    }
                    var result3 = await _userManager.AddToRoleAsync(user, "User");
                    await _telegramLogger.Debug($"Role added to User {model.Id:D} ");


                    if (!result.Succeeded || !result2.Succeeded || !result3.Succeeded)
                    {
                        await _telegramLogger.Error($"SMTH with User {model.Id:D} went wrong. " +
                                                    $"{Environment.NewLine} Activation: {Json(result)} " +
                                                    $"{Environment.NewLine} Adding password: {Json(result2)} " +
                                                    $"{Environment.NewLine} Adding role: {Json(result3)}");

                        return null;
                    }

                    _logger.LogInformation("User created a new account with password.");
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    _logger.LogInformation("User created a new account with password.");
                    return RedirectToAction("Index", "Home");
                }
                await _telegramLogger.Error($"Model of creation password of User {model.Id:D} non valid. ");
                return null;
            }
            catch (Exception ex)
            {
                await _telegramLogger.Error($"Password was added with Error {Environment.NewLine} {ex.Message}");

                return null;
            }
        }

        public async Task<IActionResult> ConfirmRequestForTeacher(ConfirmRequsetViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _telegramLogger.Debug($"Teacher {model.Id:D} create password");

                    var user = await _userManager.FindByIdAsync(model.Id.ToString("D"));
                    if (user == null)
                    {
                        await _telegramLogger.Error($"Teacher {model.Id:D} not exist in DB");
                        return null;
                    }
                    //possible move activation after password set
                    user.IsActive = true;
                    user.EmailConfirmed = true;
                    var result = await _userManager.UpdateAsync(user);
                    await _telegramLogger.Debug($"Teacher {model.Id:D} email confirmed & activate");

                    var result2 = await _userManager.AddPasswordAsync(user, model.Password);
                    await _telegramLogger.Debug($"Teacher {model.Id:D} password added");

                    if (!_roleManager.Roles.Any(role => role.Name.Equals("Teacher")))
                    {
                        await _roleManager.CreateAsync(new IdentityRole("Teacher"));
                        await _telegramLogger.Debug($"Teacher role added");

                    }
                    var result3 = await _userManager.AddToRoleAsync(user, "Teacher");
                    await _telegramLogger.Debug($"Role added to Teacher {model.Id:D} ");


                    if (!result.Succeeded || !result2.Succeeded || !result3.Succeeded)
                    {
                        await _telegramLogger.Error($"SMTH with User {model.Id:D} went wrong. " +
                                                    $"{Environment.NewLine} Activation: {Json(result)} " +
                                                    $"{Environment.NewLine} Adding password: {Json(result2)} " +
                                                    $"{Environment.NewLine} Adding role: {Json(result3)}");

                        return null;
                    }

                    _logger.LogInformation("User created a new account with password.");
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    _logger.LogInformation("User created a new account with password.");
                    return RedirectToAction("Index", "Home");
                }
                await _telegramLogger.Error($"Model of creation password of User {model.Id:D} non valid. ");
                return null;
            }
            catch (Exception ex)
            {
                await _telegramLogger.Error($"Password was added with Error {Environment.NewLine} {ex.Message}");

                return null;
            }
        }


        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        #endregion

      
    }
}
