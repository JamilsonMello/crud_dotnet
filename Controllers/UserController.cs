using CRUD_DotNet.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_DotNet.Controllers 
{
    public class UserController : Controller 
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            var userHandler = new UserModel();

            var userSession = userHandler.GetUser(user);

            if (user != null)
            {
                ViewBag.Message = "Você está logado";
                HttpContext.Session.SetInt32("UserID", userSession.Id);
                HttpContext.Session.SetString("UserName", userSession.Name);

                return Redirect("Register");
            }

            ViewBag.Message = "Falha ao fazer o login";

            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            if (HttpContext.Session.GetInt32("UserID") != null)
            {
                return Redirect("Index");
            }

            var userHandler = new UserModel();

            userHandler.CreateUser(user);

            ViewBag.Message = "Usuário criado com sucesso!";

            return View();
        }

        public ActionResult Update(int Id)
        {
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("Login");
            }

            var userHandler = new UserModel();

            var user = userHandler.FindUserByID(Id);

            return View(user);
        }

        [HttpPost]
        public ActionResult Update(User user)
        {
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("Login");
            }

            var userHandler = new UserModel();

            userHandler.UpdateUser(user);

            ViewBag.Message = "Usuário atualizado com sucesso!";

            return RedirectToAction("List");
        }

        public ActionResult Delete(int Id)
        {
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("Login");
            }

            var userHandler = new UserModel();

            userHandler.DeleteUser(Id);

            return RedirectToAction("List");
        }
        public IActionResult List()
        {
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("Login");
            }

            var userHandler = new UserModel();

            var users = userHandler.GetUsers();

            return View(users);
        }

        public ActionResult LogOut()
        {
            HttpContext.Session.Clear();

            return View("Login");
        }
    }
}