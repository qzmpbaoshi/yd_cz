namespace SystemModel
{
    public static class WebApiExtensionUrl
    {
        /// <summary>
        /// 获取司机信息，查询入口参数参考 DriverSearchCondition，返回值参考DriverInfoModel
        /// </summary>
        public static string GetDriverInfos_URL = @"/Driver/GetDrivers";
        /// <summary>
        /// 增加司机信息
        /// </summary>
        public static string AddDrivers_URL = @"/Driver/AddDrivers";
        /// <summary>
        /// 删除司机信息
        /// </summary>
        public static string DelDriversByIds_URL = @"/Driver/DelDriversByIds";
        /// <summary>
        /// 更新司机信息
        /// </summary>
        public static string UpdateDriver_URL = @"/Driver/UpdateDriver";

        /// <summary>
        /// 获取机车配属段信息，查询入口参数参考 TrainProprietorshipSearchCondition，返回值参考TrainProprietorshipModel
        /// </summary>
        public static string GetTrainProprietorships_URL = @"/TrainProprietorship/GetTrainProprietorships";
        /// <summary>
        /// 增加机车信息
        /// </summary>
        public static string AddTrainProprietorships_URL = @"/TrainProprietorship/AddTrainProprietorships";
        /// <summary>
        /// 删除机车信息
        /// </summary>
        public static string DelTrainProprietorshipsByIds_URL = @"/TrainProprietorship/DelTrainProprietorshipsByIds";
        /// <summary>
        /// 修改机车信息
        /// </summary>
        public static string UpdateTrainProprietorship_URL = @"/TrainProprietorship/UpdateTrainProprietorship";
    }
}
