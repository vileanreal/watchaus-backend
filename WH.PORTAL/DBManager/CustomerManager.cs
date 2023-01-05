using DBHelper;
using WH.PORTAL.Models;
using WH.PORTAL.Models.Entities;

namespace WH.PORTAL.DBManager
{
    public class CustomerManager : BaseManager
    {
        #region SELECT
        public Customers SelectCustomer(string col, string val)
        {
            string sql = @$"SELECT * FROM CUSTOMERS WHERE {col} = @Val";
            AddParameter("@Val", val);
            return SelectSingle<Customers>(sql);
        }

        public CustomersVerificationLink SelectCustomerVerificationLink(long customerId, string linkCode)
        {
            string sql = @"SELECT * FROM CUSTOMERS_VERIFICATION_LINKS
                           WHERE customer_id  = @customer_id AND link_code = @link_code AND status = @status";
            AddParameter("@customer_id", customerId);
            AddParameter("@link_code", linkCode);
            AddParameter("@status", Status.PENDING);
            return SelectSingle<CustomersVerificationLink>(sql);
        }

        #endregion

        #region INSERT
        public long InsertCustomer(Customers customer)
        {
            string sql = @"INSERT INTO CUSTOMERS (email, password, phone_no, first_name, middle_name, last_name, status) 
                                          VALUES (@email, @password, @phone_no, @first_name, @middle_name, @last_name, @status)";
            AddParameter("@email", customer.Email);
            AddParameter("@password", customer.Password);
            AddParameter("@phone_no", customer.PhoneNo);
            AddParameter("@first_name", customer.FirstName);
            AddParameter("@middle_name", customer.MiddleName);
            AddParameter("@last_name", customer.LastName);
            AddParameter("@status", Status.PENDING);
            ExecuteQuery(sql);
            return LastInsertedId;
        }

        public long InsertVerificationLink(CustomersVerificationLink link)
        {
            string sql = @"INSERT INTO CUSTOMERS_VERIFICATION_LINKS (customer_id, link_code, date_created, purpose, status) 
                                                             VALUES (@customerId, @linkCode, NOW(), @purpose, @status)";
            AddParameter("@customerId", link.CustomerId);
            AddParameter("@linkCode", link.LinkCode);
            AddParameter("@purpose", link.Purpose);
            AddParameter("@status", link.Status);
            ExecuteQuery(sql);
            return LastInsertedId;
        }
        #endregion

        #region UPDATE
        public void UpdateCustomerStatus(string email, string status)
        {
            string sql = @"UPDATE CUSTOMERS SET status = @status WHERE email = @email";
            AddParameter("@email", email);
            AddParameter("@status", status);
            ExecuteQuery(sql);
        }
        public void UpdateLinkStatus(long linkId, string status)
        {
            string sql = @"UPDATE CUSTOMERS_VERIFICATION_LINKS SET status = @status WHERE link_id = @link_id";
            AddParameter("@link_id", linkId);
            AddParameter("@status", status);
            ExecuteQuery(sql);
        }

        #endregion

        #region DELETE
        #endregion

    }
}
