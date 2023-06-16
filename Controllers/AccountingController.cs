using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using WebAppTest.Data;
using WebAppTest.Models;
using WebAppTest.Models.Domain;


namespace WebAppTest.Controllers
{
    [Authorize]
    public class AccountingController : Controller
    {
        private readonly MainDbContext mainDbContext;
        public AccountingController(MainDbContext mainDbContext)
        {
            this.mainDbContext = mainDbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Expenses()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Settings()
        {
            return View();
        }

        // 新增資料庫資料(記帳科目)
        [HttpPost]
        public async Task<IActionResult> AddAccountingName(AddAccountName addAccountName)
        {
            var acountname = new AccountName()
            {
                Id = addAccountName.Id,
                Type = addAccountName.Type,
                Category = addAccountName.Category,
                Name = addAccountName.Name,
            };
            //向狀態為“已添加”的DbContext添加新實體並開始對其進行跟踪的非同步方法
            await mainDbContext.AccountNames.AddAsync(acountname);
            await mainDbContext.SaveChangesAsync(); // 保存修改
            return RedirectToAction("Index", "Accounting"); // 資料保存後網頁跳轉至網站首頁
        }
        // 新增資料庫資料(收支紀錄)
        [HttpPost]
        public async Task<IActionResult> AddCashFlow(AddCashFlow addCashFlow)
        {
            var cashflow = new CashFlow()
            {
                Id = Guid.NewGuid(),
                //ProposerId = addCashFlow.ProposerId,
                ReceiptId = addCashFlow.ReceiptId,
            };
            //向狀態為“已添加”的DbContext添加新實體並開始對其進行跟踪的非同步方法
            await mainDbContext.CashFlows.AddAsync(cashflow);
            await mainDbContext.SaveChangesAsync(); // 保存修改
            return RedirectToAction("Index", "Accounting"); // 資料保存後網頁跳轉至網站首頁
        }

        // 讀取資料庫資料-顯示登載介面(收支紀錄)
        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var cashflow = await mainDbContext.CashFlows.FirstOrDefaultAsync(x => x.Id == id);
            if (cashflow != null)
            {
                var viewModel = new UpdateCashFlow()
                {
                    Id = Guid.NewGuid(),
                    //ProposerId = cashflow.ProposerId,
                    ReceiptId = cashflow.ReceiptId,
                };
                return await Task.Run(() => View("View", viewModel));
            }
            return RedirectToAction("Index");
        }
        // 更改資料庫資料
        [HttpPost]
        public async Task<IActionResult> View(UpdateCashFlow model)
        {
            var cashflow = await mainDbContext.CashFlows.FindAsync(model.Id);
            if (cashflow != null)
            {
                cashflow.Id = model.Id;
                //cashflow.ProposerId = model.ProposerId;
                cashflow.ReceiptId = model.ReceiptId;
                await mainDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}