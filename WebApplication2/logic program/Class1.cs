using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using WebApplication2.Models;

namespace WebApplication2.logic_program
{

    public class Check_acccountAttribute : ValidationAttribute
    {

        private test_shoppingEntities db = new test_shoppingEntities();

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            int? check_account_unique = db.Database.SqlQuery<int>("Select Count(Account) from Customer  Where Account=@AccountStr", new SqlParameter("AccountStr", value.ToString())).FirstOrDefault();
            Debug.WriteLine(check_account_unique);
            if ((check_account_unique > 0) && (value != null))
            {
                Debug.WriteLine(check_account_unique + "ggg");

                return new ValidationResult("會員帳號重複", new[] { "Account" });
            }
            else
            {
                return ValidationResult.Success;
            }

        }

    }


}














