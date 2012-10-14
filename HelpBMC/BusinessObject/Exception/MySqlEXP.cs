using System;
using System.Collections.Generic;
using System.Linq;
using CommonUtils;
using System.Text;

namespace Exceptions
{
    public class MySqlEXP : Exception
    {
        private string _message;
        private int _errorCode;

        public MySqlEXP() : base() { }

        public MySqlEXP(string message, MySql.Data.MySqlClient.MySqlException mySqlEx)
            : base(message)
        {
            _message = message;
            _errorCode = mySqlEx.Number;
        }


        public override string Message
        {
            get
            {
                return _message;
            }
        }

        public int ErrorCode
        {
            get
            {
                return _errorCode;
            }
        }


    }
}
