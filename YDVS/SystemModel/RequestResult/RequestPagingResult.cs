using System.Collections.Generic;

namespace SystemModel.RequestResult
{
    public class RequestPagingResult<T>
    {

        private int _totalCount;
        private T _resultData;
        private bool _flag;
        private string _msg;
        /// <summary>
        /// 数据总数
        /// </summary>
        public int TotalCount
        {
            get
            {
                return _totalCount;
            }

            set
            {
                _totalCount = value;
            }
        }
        /// <summary>
        /// 结果集
        /// </summary>
        public T ResultData
        {
            get
            {
                return _resultData;
            }

            set
            {
                _resultData = value;
            }
        }

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
