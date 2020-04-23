using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Barnamenevisan_MVC.Classes;
using CaptchaMvc.HtmlHelpers;
using DataLayer;
using Utilities;
using ViewModels;

namespace MyEshop.Controllers
{
    public class AccountController : Controller
    {
        MyEshop_DBEntities db = new MyEshop_DBEntities();
        // GET: Account
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel register)
        {
            if (ModelState.IsValid)
            {
                 if (this.IsCaptchaValid("Captcha is not valid"))
            {
                if (!IsUserNameExist(register.UserName))
                {
                    if (!IsEmailExist(register.Email))
                    {
                            User user = new User() {
                                Email = register.Email.Trim().ToLower(),
                                IsActive = false,
                                ActiveCode = Guid.NewGuid().ToString(),
                                Password = FormsAuthentication.HashPasswordForStoringInConfigFile(register.Password, "MD5"),
                                RoleID = 2,
                                RegisterDate = DateTime.Now,
                                UserName = register.UserName
                            };
                            db.Users.Add(user);
                        db.SaveChanges();

                        string Body = PartialToStringClass.RenderPartialView("EmailSender", "ActiveAccount", user);

                        SendEmailGmail.Send(user.Email,"فعال سازی",Body);


                        ViewBag.IsSuccess = true;
                    }
                    else
                    {
                        ModelState.AddModelError("Email", "ایمیل معتبر نمی باشد");
                    }
                }
                else
                {
                    ModelState.AddModelError("UserName", "نام کاربری معتبر نمی باشد");
                }
            }
                 else
                 {
                     ModelState.AddModelError("CaptchaInputText", "Captcha is not valid");
                   
                 }
            }
            return View(register);
        }



        public ActionResult ActiveUser(string id)
        {
            var user = db.Users.SingleOrDefault(u => u.ActiveCode == id && !u.IsActive);
            if (user != null)
            {
                user.ActiveCode = Guid.NewGuid().ToString();
                user.IsActive = true;
                db.SaveChanges();
                ViewBag.IsOk = true;
            }

            return View();
        }



        bool IsEmailExist(string email)
        {
            return db.Users.Any(u => u.Email == email.Trim().ToLower());
        }

        bool IsUserNameExist(string username)
        {
            return db.Users.Any(u => u.UserName == username.Trim().ToLower());
        }

        public ActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel login, string ReturnUrl="/")
        {
            if (ModelState.IsValid)
            {
                string hashPassword = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(login.Password, "MD5");
                var user = db.Users.SingleOrDefault(u => u.UserName == login.UserName && u.Password == hashPassword);
                if (user!=null)
                {
                    if (user.IsActive)
                    {

                        System.Web.Security.FormsAuthentication.SetAuthCookie(login.UserName, login.RememberMe);
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError("UserName", "حساب کاربری فوق فعال نشده است");
                    }
                }
                else
                {
                    ModelState.AddModelError("UserName","کاربری یافت نشد");
                }
            }
            return View(login);
        }

        public ActionResult LogOff()
        {
            System.Web.Security.FormsAuthentication.SignOut();
            return Redirect("/");
        }

        public ActionResult RecoveryPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RecoveryPassword(RecoveryPasswordViewModel recovery)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.SingleOrDefault(u=>u.Email==recovery.Email);
                if (user !=null)
                {
                    if (user.IsActive)
                    {
                        string Body = PartialToStringClass.RenderPartialView("EmailSender", "RecoveryPassword", user);

                        SendEmailGmail.Send(user.Email, "بازیابی کلمه عبور ", Body);


                        ViewBag.IsSuccess = true;
                    }
                }
                else
                {
                    ModelState.AddModelError("Email","کاربری یافت نشد");
                }
            }
            return View(recovery);
        }
        public ActionResult ResetPassword(string id)
        {
            var user = db.Users.SingleOrDefault(u => u.ActiveCode == id);
            if (user !=null)
            {
                return View(new ResetPasswordViewModel()
                {
                    ActiveCode=id
                });
            }
            else
            {
                ViewBag.NotFound = true;
            }
            return View();
        }


        [HttpPost]
        public ActionResult ResetPassword(ResetPasswordViewModel reset)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.SingleOrDefault(u=>u.ActiveCode== reset.ActiveCode);
                if (user !=null)
                {
                    string hashPassword= System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(reset.Password, "MD5");
                    user.Password = hashPassword;
                    user.ActiveCode = Guid.NewGuid().ToString();
                    db.SaveChanges();
                    return Redirect("/Account/Login");
                }
            }
            else
            {

            }
            return View(reset);
        }
    }
}