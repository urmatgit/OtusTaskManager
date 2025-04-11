using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.DataAccess.Common.Errors
{
    public class Errors
    {
        private static readonly Dictionary<string, string> _errorsTextBase = new Dictionary<string, string>();
        static Errors()
        {

            _errorsTextBase.Add("InvalidCredentials", "Invalid credentials");
            _errorsTextBase.Add("UsernameAlreadyExists", "Username already exists");
            _errorsTextBase.Add("EmailAlreadyExists", "Email already exists");
            _errorsTextBase.Add("EntityNoFound", "{0} not found. (id={1})");

            _errorsTextBase.Add("UsernameIsRequired", "Username us required.");
        }
        /// <summary>
        /// "{0} not found. (id={1})"
        /// </summary>
        public static string EntityNotFound { get { return _errorsTextBase["EntityNoFound"]; }  }

        public static string InvalidCredentials => _errorsTextBase["InvalidCredentials"];

        public static string UsernameAlreadyExists => _errorsTextBase["UsernameAlreadyExists"];
        public static string EmailAlreadyExists
        {
            get
            {
                return _errorsTextBase["EmailAlreadyExists"];
            }
        }  
        public static string UsernameIsRequired
        {
            get
            {
                return _errorsTextBase["UsernameIsRequired"];
            }
        }





    }
}
