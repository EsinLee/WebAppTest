using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text;
using System.Security.Cryptography;
using static WebAppTest.Models.MemberModel;
using WebAppTest.Models.Domain;
using WebAppTest.Data;
using Microsoft.EntityFrameworkCore;
using NuGet.Common;
using Microsoft.CodeAnalysis;
using System;
using Microsoft.Extensions.Caching.Distributed;
using Azure;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace WebAppTest.Controllers
{
    public class MemberController : Controller
    {
        private readonly MainDbContext mainDbContext;
        private readonly IDistributedCache _cache;
        public MemberController(MainDbContext mainDbContext, IDistributedCache cache)
        {
            this.mainDbContext = mainDbContext;
            this._cache = cache;
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        public string Dotest(DoLoginIn inModel)
        {
            return inModel.UserID;
            //return inModel.UserPwd;
        }

        public ActionResult DoLogin(DoLoginIn inModel)
        {
            DoLoginOut outModel = new DoLoginOut();

            // 檢查輸入資料
            if (string.IsNullOrEmpty(inModel.UserID) || string.IsNullOrEmpty(inModel.UserPwd))
            {
                outModel.ErrMsg = "請輸入資料";
            }
            else
            {
                SqlConnection sqlConnection = new SqlConnection(@"Data Source=localhost\SQLEXPRESS; Initial Catalog=Users; Integrated Security=True;");
                try
                {
                    // 資料庫連線
                    if (sqlConnection.State == ConnectionState.Closed)
                    {
                        sqlConnection.Open();
                    }

                    /*
                    // 將密碼轉為 SHA256 雜湊運算(不可逆)
                    string salt = inModel.UserID.Substring(0, 1).ToLower(); //使用帳號前一碼當作密碼鹽
                    SHA256 sha256 = SHA256.Create();
                    byte[] bytes = Encoding.UTF8.GetBytes(salt + inModel.UserPwd); //將密碼鹽及原密碼組合
                    byte[] hash = sha256.ComputeHash(bytes);
                    StringBuilder result = new StringBuilder();
                    for (int i = 0; i < hash.Length; i++)
                    {
                        result.Append(hash[i].ToString("X2"));
                    }
                    string CheckPwd = result.ToString(); // 雜湊運算後密碼
                    */

                    // 檢查帳號、密碼是否正確
                    string query = "SELECT COUNT(1) FROM Users WHERE u_account=@account AND u_password=@password";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlConnection);
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.AddWithValue("@account", inModel.UserID);
                    sqlCmd.Parameters.AddWithValue("@password", inModel.UserPwd);
                    // 取得查詢資料總筆數
                    int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
                    // 讀取查詢出的資料
                    SqlDataReader dataReader = sqlCmd.ExecuteReader();
                    if (count > 0)
                    {
                        // 有查詢到資料，表示帳號密碼正確

                        // 將登入帳號記錄在 Session 內
                        //Session["UserID"] = inModel.UserID;

                        outModel.ResultMsg = "登入成功";
                        // 切換至主視窗

                        // 停止鏈接至資料庫
                        //sqlConnection.Close();
                    }
                    else
                    {
                        // 查無資料，帳號或密碼錯誤
                        outModel.ErrMsg = "帳號或密碼錯誤";
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    if (sqlConnection != null)
                    {
                        //關閉資料庫連線
                        sqlConnection.Close();
                        sqlConnection.Dispose();
                    }
                }

            }
            // 輸出json
            return Json(outModel);
        }

        void CreateCookie()
        {
            var token = Guid.NewGuid().ToString();
            var answer = "Login_" + DateTime.Now;
            _cache.SetString(token, answer.ToString(), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            });
            ViewBag.Token = token;
        }
        [HttpGet]
        public ActionResult Index(string ReturnUrl)
        {
            CreateCookie();
            ViewBag.ReturnUrl = ReturnUrl;

            LoginMemberAccount loginMemberAccount = new LoginMemberAccount();
            loginMemberAccount.AlertString = "";
            loginMemberAccount.BoolDoAlert = false;
            return View(loginMemberAccount);
        }
        // 查詢資料實作-用戶登入
        [HttpPost]
        public async Task<IActionResult> Index(LoginMemberAccount loginMemberAccount, string token, string ReturnUrl)
        {
            string inalldata = "Must input all data.";
            string confirmpas = "Account or password wrong.";
            // 舊(一般資料比對) start------------------------------------------------------------------------------------------------------------------------------------
            /*if ((loginMemberAccount.Account != null) && (loginMemberAccount.Account.ToString().Length != 0) &&
                (loginMemberAccount.Password != null) && (loginMemberAccount.Password.ToString().Length != 0))
            {
                var security_acc = await mainDbContext.Securitys.FirstOrDefaultAsync(x => x.Account == loginMemberAccount.Account);
                var security_pas = await mainDbContext.Securitys.FirstOrDefaultAsync(x => x.Password == loginMemberAccount.Password);
                if ((security_acc != null) && (security_pas != null))
                {
                    //Console.WriteLine("_Debug_" + security_acc.ToString() + "_" + security_pas.ToString());
                    return RedirectToAction("Index","Home");
                }
                loginMemberAccount.BoolDoAlert = true;
                loginMemberAccount.AlertString = confirmpas;
                return View(loginMemberAccount);
            }*/
            // 舊(一般資料比對) end------------------------------------------------------------------------------------------------------------------------------------
            // 新(使用cookie登入) start--------------------------------------------------------------------------------------------------------------------------------
            if ((loginMemberAccount.Account != null) && (loginMemberAccount.Account.ToString().Length != 0) &&
                (loginMemberAccount.Password != null) && (loginMemberAccount.Password.ToString().Length != 0))
            {
                // cookie設定 start------------------------------------------------------------------------------------------------------------------------------------
                try
                {
                    var answer = await _cache.GetStringAsync(token);
                    if (answer == null)
                        throw new ApplicationException("Invalid Token");
                    await _cache.RemoveAsync(token);
                    var security_acc = await mainDbContext.Securitys.FirstOrDefaultAsync(x => x.Account == loginMemberAccount.Account);
                    //var security_pas = await mainDbContext.Securitys.FirstOrDefaultAsync(x => x.Password == loginMemberAccount.Password);
                    if ((security_acc != null) /*&& (security_pas != null)*/ && (security_acc.Password == loginMemberAccount.Password)) // 登入成功
                    {
                        var security_name = await mainDbContext.Members.FirstOrDefaultAsync(x => x.Id == security_acc.Id);
                        await SignIn(security_name.Name ?? "User");
                        return Redirect(ReturnUrl ?? "~/");
                        //return RedirectToAction("Index","Home");
                    }
                    else // 登入失敗
                    {
                        loginMemberAccount.BoolDoAlert = true;
                        loginMemberAccount.AlertString = confirmpas;
                    }
                }
                catch (Exception ex)
                {
                    loginMemberAccount.AlertString = ex.Message;
                }
                // cookie設定 end------------------------------------------------------------------
                CreateCookie();
                ViewBag.ReturnUrl = ReturnUrl;
                return View(loginMemberAccount);
            }
            CreateCookie();
            ViewBag.ReturnUrl = ReturnUrl;
            // 新(使用cookie登入) end------------------------------------------------------------------------------------------------------------------------------------
            loginMemberAccount.BoolDoAlert = true;
            loginMemberAccount.AlertString = inalldata;
            return View(loginMemberAccount);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("~/");
        }

        // 資料新增介面
        [HttpGet]
        public IActionResult Register()
        {
            AddMemberAccount addMemberAccount = new AddMemberAccount();
            addMemberAccount.AlertString = "";
            addMemberAccount.BoolDoAlert = false;
            return View(addMemberAccount);
        }
        // 新增資料實作-用戶註冊
        [HttpPost]
        public async Task<IActionResult> Register(AddMemberAccount addMemberAccount)
        {
            string inalldata = "Must input all data.";
            string confirmpas = "Confirn password different.";
            if ((addMemberAccount.Name != null) && (addMemberAccount.Name.ToString().Length != 0) &&
                (addMemberAccount.Email != null) && (addMemberAccount.Email.ToString().Length != 0) &&
                (addMemberAccount.Account != null) && (addMemberAccount.Account.ToString().Length != 0) &&
                (addMemberAccount.Password != null) && (addMemberAccount.Password.ToString().Length != 0)) 
            {
                if(addMemberAccount.Password != addMemberAccount.ConfirmPassword)
                {
                    addMemberAccount.BoolDoAlert = true;
                    addMemberAccount.AlertString = confirmpas;
                    return View(addMemberAccount);
                }
                else
                {
                    var member = new Member()
                    {
                        Id = Guid.NewGuid(),
                        Name = addMemberAccount.Name,
                        Email = addMemberAccount.Email,
                        Gender = "Hide",
                        Address = "Hide",
                    };
                    var security = new Security()
                    {
                        Id = member.Id,
                        Account = addMemberAccount.Account,
                        Password = addMemberAccount.Password,
                        SecurityLevel = "9",
                    };
                    //向狀態為“已添加”的DbContext添加新實體並開始對其進行跟踪的非同步方法
                    await mainDbContext.Members.AddAsync(member);
                    await mainDbContext.SaveChangesAsync(); // 保存修改
                    await mainDbContext.Securitys.AddAsync(security);
                    await mainDbContext.SaveChangesAsync(); // 保存修改

                    return RedirectToAction("Index", "Home"); // 資料保存後網頁跳轉至網站首頁
                }
            }
            else 
            {
                //_ = Response.WriteAsync("<script>alert('Must input all data.')</script>");
                addMemberAccount.BoolDoAlert = true;
                addMemberAccount.AlertString = inalldata;
                Console.WriteLine(ViewBag.Message + "__" + ViewBag.returnsbool);
                //return RedirectToAction("Register");
                return View(addMemberAccount);
            }
        }

        async Task SignIn(string name)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, name),
                new Claim("FullName", name),
                new Claim(ClaimTypes.Role, "Administrator"),
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                //AllowRefresh = <bool>,
                // Refreshing the authentication session should be allowed.

                //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                // The time at which the authentication ticket expires. A 
                // value set here overrides the ExpireTimeSpan option of 
                // CookieAuthenticationOptions set with AddCookie.

                //IsPersistent = true,
                // Whether the authentication session is persisted across 
                // multiple requests. When used with cookies, controls
                // whether the cookie's lifetime is absolute (matching the
                // lifetime of the authentication ticket) or session-based.

                //IssuedUtc = <DateTimeOffset>,
                // The time at which the authentication ticket was issued.

                //RedirectUri = <string>
                // The full path or absolute URI to be used as an http 
                // redirect response value.
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

        }
    }
}
