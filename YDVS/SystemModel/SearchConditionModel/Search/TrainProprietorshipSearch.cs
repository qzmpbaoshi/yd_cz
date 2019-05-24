namespace SystemModel.SearchConditionModel
{
    /// <summary>
    /// 机车配属段信息查询条件
    /// </summary>
    public class TrainProprietorshipSearch
    {
        private string _trainShortName;
        /// <summary>
        /// 机车简称 车型+车号
        /// </summary>
        public string TrainShortName
        {
            get
            {
                return _trainShortName;
            }

            set
            {
                _trainShortName = value;
            }
        }
    }
}
