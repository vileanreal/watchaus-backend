using Utilities;
using WH.PORTAL.DBManager;
using WH.PORTAL.Helper;
using WH.PORTAL.Models;
using WH.PORTAL.Models.Entities;

namespace WH.PORTAL.Services
{
    public class CustomerService
    {
        public bool IsEmailExist(string email)
        {
            using CustomerManager manager = new CustomerManager();
            Customers customer = manager.SelectCustomer("email", email);
            return customer != null;
        }

        public bool IsValidVerificationLink(long customerId, string linkCode) {
            using CustomerManager manager = new CustomerManager();
            var cvl = manager.SelectCustomerVerificationLink(customerId, linkCode);
            return cvl != null;
        }


        public OperationResult Register(Customers customer)
        {
            if (IsEmailExist(customer.Email))
            {
                return OperationResult.Failed("Email is already registered.");
            }

            customer.Password = CryptoHelper.Encrypt(customer.Password);

            using CustomerManager manager = new CustomerManager();
            manager.BeginTransaction();
            var customerId = manager.InsertCustomer(customer);

            var verificationLink = new CustomersVerificationLink() {
                CustomerId = customerId,
                LinkCode = CommonHelper.GenerateEmailVerificationCode(),
                Purpose = "Registration",
                Status = Status.PENDING
            };

            manager.InsertVerificationLink(verificationLink);


            CommonService commonService = new CommonService();
            var setting = commonService.GetSetting("CUSTOMER_EMAIL_CONFIRMATION_LINK");
            var emailTemplate = commonService.GetEmailTemplate("CUSTOMER REGISTRATION");
            var confirmationLink = setting.SettingVal.Replace("<customer_id>", customerId.ToString())
                                                     .Replace("<link_code>", verificationLink.LinkCode.ToString());
            var subject = emailTemplate.Subject;
            var body = emailTemplate.Body.Replace("<confirmation_email_link>", confirmationLink);
            
            EmailHelper emailHelper = new EmailHelper();
            emailHelper.SendEmail(customer.Email, subject, body);



            manager.Commit();
            // TODO: add logger service 
            return OperationResult.Success("Successfully registered!. Check your email to verify your account.");
        }


        public OperationResult ConfirmEmail(long customerId, string linkCode) {
            
            if (!IsValidVerificationLink(customerId, linkCode))
            {
                return OperationResult.Failed("Invalid Link Code.");
            }

            //using CustomerManager manager = new CustomerManager();
            //manager.BeginTransaction();
            //manager.UpdateCustomerStatus(email, Status.ACTIVE);
            //manager.UpdateLinkStatus(cvl.LinkId, Status.SUCCESS);
            //manager.Commit();

            return OperationResult.Success("Email has been successfully verified");
        }
    }
}
