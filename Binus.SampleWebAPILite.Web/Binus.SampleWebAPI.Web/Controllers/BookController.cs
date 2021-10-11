using Binus.SampleWebAPI.Model.AppModel;
using Binus.SampleWebAPI.Web.Class;
using Binus.SampleWebAPI.Web.ViewModels;
using Binus.WebAPI.REST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Binus.SampleWebAPI.Web.Controllers
{
    public class BookController : Controller
    {

        public ActionResult Index()
        {
            JsonResult RetData = new JsonResult();

            try
            {
                if (Session["User"] != null)
                {
                    HomeViewModel hvm = new HomeViewModel();
                    RESTResult Result = (new REST(Global.WebAPIBaseURL, "/api/Training/BookDB/V1/App/Book/GetAllBook", REST.Method.GET, REST.NeedOAuth.False)).Result;
                    if (Result.Success)
                    {
                        hvm.Books = Result.Deserialize<List<BookModel>>();
                    }
                    else
                    {
                        hvm.Books = new List<BookModel>();
                    }

                    hvm.UserID = Convert.ToInt32(((UserModel)Session["User"]).UserID);

                    return View("index", hvm);
                }
                else
                {
                    return RedirectToAction("index", "login");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult Borrow()
        {
            JsonResult RetData = new JsonResult();

            try
            {
                if (Session["User"] != null)
                {
                    string UserID = ((UserModel)Session["User"]).UserID.ToString();

                    BorrowBookViewModel bvm = new BorrowBookViewModel();
                    RESTResult Result = (new REST(Global.WebAPIBaseURL, "/api/Training/BookDB/V1/App/BorrowBook/GetBorrowedBooks?UserID=" + UserID, REST.Method.GET, REST.NeedOAuth.False)).Result;
                    if (Result.Success)
                    {
                        bvm.BorrowedBooks = Result.Deserialize<List<BorrowBookModel>>();
                    }
                    else
                    {
                        bvm.BorrowedBooks = new List<BorrowBookModel>();
                    }

                    bvm.username = (((UserModel)Session["User"]).UserName).ToString();

                    return View("borrow", bvm);
                }
                else
                {
                    return RedirectToAction("index", "login");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult InsertBook(BookModel model)
        {
            JsonResult RetData = new JsonResult();
            try
            {
                if (model.BookID != 0)
                {
                    RESTResult Result = (new REST(Global.WebAPIBaseURL, "/api/Training/BookDB/V1/App/Book/UpdateBook", REST.Method.POST, REST.NeedOAuth.False, model)).Result;

                    if (Result.Success)
                    {
                        RetData = Json(new
                        {
                            Status = "Success",
                            URL = Global.BaseURL + "/Book"
                        });
                    }
                    else
                    {
                        RetData = Json(new
                        {
                            Status = "Failed",
                            Message = "Failed when updating data.."
                        });
                    }
                }
                else
                {
                    RESTResult Result = (new REST(
                        BaseURL: Global.WebAPIBaseURL,
                        URL: "api/Training/BookDB/V1/App/Book/InsertBookWithModel",
                        Method: REST.Method.POST,
                        NeedOAuth: REST.NeedOAuth.False,
                        Data: model)).Result;
                    if (Result.Success)
                    {
                        RetData = Json(new
                        {
                            Status = "Success",
                            URL = Global.BaseURL + "/Book"
                        });
                    }
                    else
                    {
                        RetData = Json(new
                        {
                            Status = "Failed",
                            Message = "Failed When Inserting Data.."
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                RetData = Json(new
                {
                    Status = "Failed",
                    Message = ex.Message
                });
            }

            return RetData;
        }

        public ActionResult Delete(BookModel Model)
        {
            JsonResult retData = new JsonResult();
            try
            {
                RESTResult Result = (new REST(Global.WebAPIBaseURL, "/api/Training/BookDB/V1/App/Book/DeleteBook", REST.Method.POST, REST.NeedOAuth.False, Model)).Result;

                if (Result.Success)
                {
                    retData = Json(new
                    {
                        Status = "Success",
                        URL = Global.BaseURL + "/Book"
                    });
                }
                else
                {
                    retData = Json(new
                    {
                        Status = "Failed",
                        Message = "Failed When Deleting Data.."
                    });
                }
            }
            catch (Exception ex)
            {
                retData = Json(new
                {
                    Status = "Failed",
                    Message = ex.Message
                });
            }
            return retData;
        }

        public ActionResult Edit(BookModel model)
        {
            JsonResult retData = new JsonResult();
            try
            {
                RESTResult Result = (new REST(Global.WebAPIBaseURL,
                    "/api/Training/BookDB/V1/App/Book/GetOneBook?BookID=" + model.BookID,
                    REST.Method.GET,
                    REST.NeedOAuth.False)).Result;

                if (Result.Success)
                {
                    retData = Json(new
                    {
                        Status = "Success",
                        Data = Result.Deserialize<BookModel>()
                    });
                }
                else
                {
                    retData = Json(new
                    {
                        Status = "Failed",
                        Message = "Failed when getting data.."
                    });
                }
            }
            catch (Exception ex)
            {
                retData = Json(new
                {
                    Status = "Failed",
                    Message = ex.Message
                });
            }
            return retData;
        }

        public ActionResult BorrowBook(BorrowBookModel model)
        {
            JsonResult retData = new JsonResult();
            try
            {
                RESTResult Result = (new REST(Global.WebAPIBaseURL, "/api/Training/BookDB/V1/App/BorrowBook/BorrowBook", REST.Method.POST, REST.NeedOAuth.False, model)).Result;

                if (Result.Success)
                {
                    retData = Json(new
                    {
                        Status = "Success",
                        URL = Global.BaseURL + "/Book",
                        Data = Result.Deserialize<BorrowBookModel>()
                    });
                }
                else
                {
                    retData = Json(new
                    {
                        Status = "Failed",
                        Message = "Failed when borrowing book.."
                    });
                }
            }
            catch (Exception e)
            {
                retData = Json(new
                {
                    Status = "Failed",
                    Message = e.Message
                });
            }
            return retData;
        }
    }
}