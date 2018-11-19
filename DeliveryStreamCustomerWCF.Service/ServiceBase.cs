// 2014.01.06 FSWW, Ramesh M Added VersionNo as input parameter in all methods For Versioning handling
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using DeliveryStreamCustomerWCF.Utils;

namespace DeliveryStreamCustomerWCF.Service
{
    /// <summary>
    /// ServiceBase class
    /// </summary>
    public class ServiceBase
    {
        private static String _customerID = String.Empty;
        private static String _customerPassword = string.Empty;
       
        /// <summary>
        /// Customer ID
        /// </summary>
        private static String CustomerID
        {
            get
            {
                if (String.IsNullOrEmpty(_customerID))
                {
                    try
                    {
                        _customerID = ConfigurationManager.AppSettings["CustomerID"];
                    }
                    catch (Exception ex)
                    {
                        Logging.LogError(ex);
                    }
                }
                return _customerID;
            }
        }

        /// <summary>
        /// Customer Password
        /// </summary>
        private static String CustomerPassword
        {
            get
            {
                if (String.IsNullOrEmpty(_customerPassword))
                {
                    try
                    {
                        _customerPassword = ConfigurationManager.AppSettings["CustomerPassword"];
                    }
                    catch (Exception ex)
                    {
                        Logging.LogError(ex);
                    }
                }
                return _customerPassword;
            }
        }

        /// <summary>
        /// Default constructor for ServiceBase
        /// </summary>
        static ServiceBase()
        {

        }

        /// <summary>
        /// GetConnectionString
        /// Function to get connection string for connection
        /// </summary>
        /// <returns>Connection string</returns>
        private static string GetConnectionString()
        {
            ConnectionStringSettingsCollection connectionStrings = ConfigurationManager.ConnectionStrings;

            foreach (ConnectionStringSettings connection in connectionStrings)
            {
                if (connection.Name != ApplicationConstants.Connection.ConnectionString)
                    continue;
                return connection.ConnectionString;
            }

            throw new ApplicationException(String.Format(ApplicationConstants.Errors.ConnectionString, ApplicationConstants.Connection.ConnectionString));
        }

        /// <summary>
        /// GetSession
        /// Function to get new session
        /// </summary>
        /// <returns>session - ISession</returns>
        protected static ISession GetSession()
        {
            ISession session = new Session(GetConnectionString());

            session.Open();
            return session;
        }

        /// <summary>
        /// Function to close current session
        /// </summary>
        /// <param name="session">Session object</param>
        protected static void CloseSession(ISession session)
        {
            try
            {
                session.Close();
                session = null;
            }
            catch (Exception ex)
            {
                session = null;
                Logging.LogError(ex);
            }

        }

        /// <summary>
        /// GetNewSession
        /// Function to get new session
        /// </summary>
        /// <returns>session - ISession</returns>
        public static ISession GetNewSession()
        {
            ISession session = new Session(GetConnectionString());
            session.Open();
            return session;
        }

        /// <summary>
        /// ValidateCustomerLogin
        /// Function to validate customer credientials
        /// </summary>
        /// <param name="companyID">Company ID</param>
        /// <param name="password">Password</param>
        /// <returns>True = Valid customer, False = Failed</returns>
        protected Boolean ValidateCustomerLogin(String companyID, String password, String VersionNo = "")
        {
            Boolean isValid = false;
           
            try
            {              
                if (companyID.Equals(CustomerID, StringComparison.CurrentCulture) && password.Equals(CustomerPassword, StringComparison.CurrentCulture))
                {
                    isValid = true;
                }
                else
                {
                    throw new ApplicationException(ApplicationConstants.Errors.InvalidCustomerCreadentials);
                }
               
            }
            catch (ApplicationException ex)
            {               
                isValid = false;
                throw ex;
            }
            catch (Exception ex)
            {               
                isValid = false;
                Logging.LogError(ex);
                throw ex;
            }

            return isValid;
        }
    }
}
