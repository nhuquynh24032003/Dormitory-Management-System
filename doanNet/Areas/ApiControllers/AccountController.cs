using doanNet.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using doanNet.Controllers.DTO;
using BCrypt.Net;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;

namespace doanNet.ApiControllers
{
    public class AccountController : ApiController
    {
        KTXTDTUEntities2 db = new KTXTDTUEntities2();
        public Account GetByID(int? id)
        {
            return db.Accounts.Where(row => row.IDAccount == id).FirstOrDefault();
        }
        public List<Account> GetAll()
        {
            return db.Accounts.ToList();
        }
        public IHttpActionResult CheckingLogin([FromBody] LoginDTO loginTemp) {
            var Account = db.Accounts.Where(row=>row.MSSV == loginTemp.account).FirstOrDefault();
            if (Account==null || !BCrypt.Net.BCrypt.Verify(loginTemp.password, Account.Password))
            {
                return Content(HttpStatusCode.Unauthorized, "Sai Tài Khoản hoặc mật khẩu");
            }
            return Ok(Account);
        }

        public Account GetByAccountId(int id)
        {
            return db.Accounts.Where(row => row.IDAccount == id).FirstOrDefault();
        }
        [HttpPost]
        public IHttpActionResult AddingAccountStudent([FromBody] AccountDTO Account)
        {
            var tempAccount = new Account();
            tempAccount.IDSinhVien=Account.IDSinhVien;
            tempAccount.Password = BCrypt.Net.BCrypt.HashPassword(Account.Password);
            tempAccount.Available = 0;
            tempAccount.AccountTypeID = Account.AccountTypeID;
            tempAccount.DateBegin = DateTime.Now;
            tempAccount.Hide = 0;
            tempAccount.MSSV=Account.MSSV;
            try
            {
                db.Accounts.Add(tempAccount);
                db.SaveChangesAsync();
                return Json(new { Message = "Data received successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { Message = "Adding Failed!Error: " + ex, });
            }
        }

        public IHttpActionResult AddingAccountGiaoVien([FromBody] AccountDTO Account)
        {
            var tempAccount = new Account();
            tempAccount.IDTeacher = Account.IDTeacher;
            tempAccount.Password = BCrypt.Net.BCrypt.HashPassword(Account.Password);
            tempAccount.Available = 1;
            tempAccount.AccountTypeID = 4;
            tempAccount.DateBegin = DateTime.Now;
            tempAccount.Hide = 0;
            tempAccount.MSSV = Account.MSSV;
            try
            {
                db.Accounts.Add(tempAccount);
                db.SaveChangesAsync();
                return Json(new { Message = "Data received successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { Message = "Adding Failed!Error: " + ex, });
            }
        }
        [HttpPut]
        private bool EntityExists(int id)
        {
            return db.Accounts.Any(e => e.IDAccount == id);
        }
        
        [HttpPut]
        public IHttpActionResult HiddingAccount(int id)
        {
            Account hideAccount = db.Accounts.Where(row => row.IDAccount == id).FirstOrDefault();
            hideAccount.Hide = hideAccount.Hide == 0 ? 1 : 0;
            db.SaveChangesAsync();
            return Json(new { Message = "Hiding Succesfully!" });
        }
        [HttpPut]
        public IHttpActionResult ChangingPassword(int id,string password)
        {
            Account AccountNeedToChange = db.Accounts.Where(row => row.IDAccount == id).FirstOrDefault();
            AccountNeedToChange.Password = BCrypt.Net.BCrypt.HashPassword(password);
            if (AccountNeedToChange.Available==0) {
                AccountNeedToChange.Available = 1;
            }
            db.SaveChangesAsync();
            return Json(new { Message = "Changing Succesfully!" });
        }
        [HttpPut]
        public IHttpActionResult PromotingAccountType(int id)
        {
            Account AccountNeedToChange = db.Accounts.Where(row => row.IDAccount == id).FirstOrDefault();
            AccountNeedToChange.AccountTypeID = 5;
            db.SaveChangesAsync();
            return Json(new { Message = "Promoting Succesfully!" });
        }
        
        [HttpPut]
        public IHttpActionResult ChangingStatus(int id)
        {
            Account AccountNeedToChange = db.Accounts.Where(row => row.IDAccount == id).FirstOrDefault();
            AccountNeedToChange.Hide = AccountNeedToChange.Hide==0?1:0;
            db.SaveChangesAsync();
            return Json(new { Message = "Changing Status Succesfully!" });
        }
    }
}
