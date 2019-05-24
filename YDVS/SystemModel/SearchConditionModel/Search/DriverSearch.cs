namespace SystemModel.SearchConditionModel
{
    /// <summary>
    /// 司机信息查询条件
    /// </summary>
    public class DriverSearch
    {
        private string _card;
        /// <summary>
        /// 司机号
        /// </summary>
        public string Card
        {
            get
            {
                return _card;
            }

            set
            {
                _card = value;
            }
        }
    }
}
