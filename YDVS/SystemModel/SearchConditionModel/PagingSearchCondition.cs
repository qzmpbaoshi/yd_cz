namespace SystemModel.SearchConditionModel
{
    public class PagingSearchCondition<T>
    {
        private int _startIndex;
        private int _pageSize;
        private T _SearchCondition;

        public int StartIndex
        {
            get
            {
                return _startIndex;
            }

            set
            {
                _startIndex = value;
            }
        }

        public int PageSize
        {
            get
            {
                return _pageSize;
            }

            set
            {
                _pageSize = value;
            }
        }

        public T SearchCondition
        {
            get
            {
                return _SearchCondition;
            }

            set
            {
                _SearchCondition = value;
            }
        }
    }
}
