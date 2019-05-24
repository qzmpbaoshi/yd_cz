namespace SystemModel
{
    public static class WebApiExtensionUrl
    {
        /// <summary>
        /// 获取司机信息，查询入口参数参考 DriverSearchCondition，返回值参考DriverInfoModel
        /// </summary>
        public static string GetDriverInfos_URL = @"/Driver/GetDrivers";

        /// <summary>
        /// 获取机车配属段信息，查询入口参数参考 TrainProprietorshipSearchCondition，返回值参考TrainProprietorshipModel
        /// </summary>
        public static string GetTrainProprietorships_URL = @"/TrainProprietorship/GetTrainProprietorships";
    }
}
