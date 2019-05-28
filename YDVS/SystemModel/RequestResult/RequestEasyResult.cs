using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemModel.RequestResult
{
    public class RequestEasyResult
    {
        private bool _flag;
        private string _msg;
        public bool Flag
        {
            get
            {
                return _flag;
            }

            set
            {
                _flag = value;
            }
        }

        public string Msg
        {
            get
            {
                return _msg;
            }

            set
            {
                _msg = value;
            }
        }
    }
}
