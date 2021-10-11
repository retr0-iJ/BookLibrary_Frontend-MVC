using Binus.SampleWebAPI.Model.AppModel;
using Binus.SampleWebAPI.Web.Class;
using Binus.WebAPI.Model.Common;
using Binus.WebAPI.REST;
using System;
using System.Web.Mvc;

namespace Binus.SampleWebAPI.Web.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Auth(AuthUser model)
        {
            JsonResult ReturnData = new JsonResult();
            try
            {
                var x = (new REST(Global.WebAPIBaseURL, "/api/Training/BookDB/V1/App/Login/GetLoginData", REST.Method.POST, Global.OAuthBookDBEndPoint, model));
                var Result = x.Result;
                if (Result.Success)
                {
                    AuthUser User = Result.Deserialize<AuthUser>();
                    UserModel UserSession = Result.Deserialize<UserModel>();
                    if (User != null)
                    {
                        Session["User"] = UserSession;
                        Session.Timeout = 60;
                        ReturnData = Json(new
                        {
                            Status = "Success",
                            URL = Global.BaseURL + "/Book"
                        });
                    }
                    else
                    {
                        ReturnData = Json(new
                        {
                            Status = "Failed",
                            Message = "Username or Password is wrong"
                        });
                    }
                }
                else
                {
                    ReturnData = Json(new
                    {
                        Status = "Failed",
                        Message = "Failed When logging in.."
                    });
                }
            }
            catch (Exception ex)
            {
                ReturnData = Json(new
                {
                    Status = "Failed",
                    Message = ex.Message
                });
            }
            return ReturnData;
        }

        public ActionResult Logout()
        {
            Session.Remove("User");

            return RedirectToAction("index", "login");
        }
    }
}