using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.DataAccess.Common.Errors
{
    public static partial class Errors
    {
        private static Dictionary<string, string> _errorsTextBase=new Dictionary<string, string>();
         static Errors()
        {
            _errorsTextBase.Add("EntityNoFound", "{0} not found. (id={1})");
        }
        /// <summary>
        /// "{0} not found. (id={1})"
        /// </summary>
        public static string EntityNotFound => _errorsTextBase["EntityNoFound"];
        public static class Authentication
        {
            static  Authentication()
            {
                _errorsTextBase.Add("InvalidCredentials", "Invalid credentials");
                _errorsTextBase.Add("UsernameAlreadyExists", "Username already exists");
                _errorsTextBase.Add("EmailAlreadyExists", "Email already exists");
                _errorsTextBase.Add("EntityNoFound", "{0} not found. (id={1})");

            }
            public  static string InvalidCredentials=> _errorsTextBase["InvalidCredentials"];
                
            public static string UsernameAlreadyExists=>_errorsTextBase["UsernameAlreadyExists"];
            public static string EmailAlreadyExists => _errorsTextBase["UsernameAlreadyExists"];
            


        }
    }
}
