using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class MainPageController : Controller
    {
        private test_shoppingEntities db = new test_shoppingEntities();
        public String Customer_index_str;
        public ActionResult MainPageSearch()
        {
            return View();
        }
        public ActionResult ProductInformation()
        {
            return View();
        }
        [HttpPost]
        public void ReceiveSelectValue(string SelectValue)
        {
            Debug.WriteLine("ReceiveSelectValue" + SelectValue);
            TempData["CheckSelect"] = SelectValue;
           
        }


        public ActionResult Vendor_login()
        {
            Console.WriteLine("enter");
            var SelectList1 = new List<SelectListItem>()
            {
                new SelectListItem { Text = "please select", Value = "",Selected = true},
                new SelectListItem { Text = "Male", Value = "1" },
                new SelectListItem { Text = "Female", Value = "2" }
            };

            ViewData["GenderChoise"] = SelectList1;
            return View();
        }

        [HttpPost]
        public ActionResult Vendor_login(Customer customer)
        {
            var SelectList1 = new List<SelectListItem>()
            {
                new SelectListItem { Text = "please select", Value = ""},
                new SelectListItem { Text = "Male", Value = "1" },
                new SelectListItem { Text = "Female", Value = "2" }
            };
     
            ViewData["GenderChoise"] = SelectList1;

            //ModelState.IsValid 資料驗證成功
            if ((ModelState.IsValid))
            {
                Debug.WriteLine("ModelState.IsValid1");
                customer.CustomerId = "default";
                db.Customer.Add(customer);

                try
                {
                    //Debug.WriteLine("22222");
                    db.SaveChanges();
                    ModelState.Clear();
                    return JavaScript("alert('註冊會員成功!!!!');");

                }
                catch (Exception ex)
                {
                    throw;
                }

            }

            return View(customer);

        }
        [AcceptVerbs("Get", "Post")]
        public ActionResult VerifyAccount(Customer customer)
        {
            //Debug.WriteLine(customer.Account);
            int check_account_unique = db.Database.SqlQuery<int>("Select Count(Account) from Customer  Where Account=@AccountStr", new SqlParameter("AccountStr", customer.Account.ToString())).FirstOrDefault();
            //Debug.WriteLine(check_account_unique);
            if (TempData["CheckSelect"] != null)
            {
                if ((check_account_unique > 0) && (customer.Account != null) && (TempData["CheckSelect"].ToString() == "非會員"))
                {
                    //Debug.WriteLine(check_account_unique + "ggg");

                    return Json("會員帳號重複", JsonRequestBehavior.AllowGet);
                }
            }
         

            return Json(true, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Member_login(Customer customer)
        {
            var SelectList1 = new List<SelectListItem>()
            {
                new SelectListItem { Text = "", Value = "0", Selected = true },
                new SelectListItem { Text = "Male", Value = "1" },
                new SelectListItem { Text = "Female", Value = "2" }
            };

            ViewData["GenderChoise"] = SelectList1;

            Debug.WriteLine("Member_login");

            String account_str = customer.Account.ToString().Trim();
            String password_str = customer.Password.ToString().Trim();

            var check_info_account = "";
            var check_info_password = "";
            var check_Customer_id = "";
            try
            {
                check_info_account = db.Database.SqlQuery<string>("Select Account From Customer Where Account = @Account", new SqlParameter("Account", account_str)).FirstOrDefault().Trim();
                check_info_password = db.Database.SqlQuery<string>("Select Password From Customer Where Account = @Account", new SqlParameter("Account", account_str)).FirstOrDefault().Trim();
                check_Customer_id = db.Database.SqlQuery<string>("Select Customerid From Customer Where Account = @Account", new SqlParameter("Account", account_str)).FirstOrDefault().Trim();
            }
            catch (Exception ex)
            {

            }

            if ((check_info_account == account_str) && (check_info_password == password_str))
            {
                Debug.WriteLine("帳號密碼正確");
                
                TempData["vendor_info"] = db.Customer.Find(check_Customer_id);
                TempData["customer_index"] = check_Customer_id;
                Customer_index_str = check_Customer_id;

                return JavaScript("window.location = '" + Url.Action("Vendor_Information", "MainPage") + "'");
               
            }
            return JavaScript("alert('無相關資訊')");

        }
        
        public ActionResult Vendor_Information()
        {
            var SelectList1 = new List<SelectListItem>()
            {
                new SelectListItem { Text = "", Value = "0" },
                new SelectListItem { Text = "Male", Value = "1" },
                new SelectListItem { Text = "Female", Value = "2",Selected = true }
            };

            Debug.WriteLine("Vendor_Information");
            Customer Receive_temp = TempData["vendor_info"] as WebApplication2.Models.Customer;

            //查詢 selectlist呈現
            foreach (var item in SelectList1)
            {
                Debug.WriteLine("item.Text:" + item.Value);
                Debug.WriteLine("Gender:" + Receive_temp.Gender.ToString().Trim());
                if (item.Value == Receive_temp.Gender.ToString().Trim())
                {
                    //Debug.WriteLine("78");
                    if (item.Value == "1")
                    {
                        Debug.WriteLine("1");

                        ViewBag.check_who_select = item.Value;


                        Debug.WriteLine(ViewData["display_GenderChoise"]);
                    }
                    else if (item.Value == "2")
                    {
                        Debug.WriteLine("2");

                        ViewBag.check_who_select = item.Value;

                    }

                    break;
                }
            }
            Debug.WriteLine(Receive_temp.Gender);
            ViewData["display_GenderChoise"] = SelectList1;
            TempData["Id_String"] = Receive_temp.CustomerId;
            return View(Receive_temp);
            
        }

        [HttpPost]
        public ActionResult Vendor_Information_Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                //TempData.Keep 生命週期延長
                TempData.Keep("customer_index");

                customer.CustomerId = TempData["customer_index"] as String;
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return JavaScript("alert('success')");
            }
            
            return JavaScript("alert('fail')");
        }
        [HttpPost]
        public ActionResult Vendor_Information_Delete()
        {
            var id_temp = TempData["Id_String"];
            Customer delete_data = db.Customer.Find(id_temp);
            db.Customer.Remove(delete_data);
            db.SaveChanges();
            return JavaScript("alert('delete success')");
        }


        // GET: MainPage
        public ActionResult Index()
        {

            return View(db.GoodsInformation.ToList());
        }

        // GET: MainPage/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GoodsInformation goodsInformation = db.GoodsInformation.Find(id);
            if (goodsInformation == null)
            {
                return HttpNotFound();
            }
            return View(goodsInformation);
        }

        // GET: MainPage/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MainPage/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GoodsId,GoodsName,Price")] GoodsInformation goodsInformation)
        {
            if (ModelState.IsValid)
            {
                db.GoodsInformation.Add(goodsInformation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(goodsInformation);
        }

        // GET: MainPage/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GoodsInformation goodsInformation = db.GoodsInformation.Find(id);
            if (goodsInformation == null)
            {
                return HttpNotFound();
            }
            return View(goodsInformation);
        }

        // POST: MainPage/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GoodsId,GoodsName,Price")] GoodsInformation goodsInformation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(goodsInformation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(goodsInformation);
        }

        // GET: MainPage/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GoodsInformation goodsInformation = db.GoodsInformation.Find(id);
            if (goodsInformation == null)
            {
                return HttpNotFound();
            }
            return View(goodsInformation);
        }

        // POST: MainPage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            GoodsInformation goodsInformation = db.GoodsInformation.Find(id);
            db.GoodsInformation.Remove(goodsInformation);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
