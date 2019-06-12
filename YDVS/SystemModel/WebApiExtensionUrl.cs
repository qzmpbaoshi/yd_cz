namespace SystemModel
{
    public static class WebApiExtensionUrl
    {
        /// <summary>
        /// 获取司机信息，查询入口参数参考 DriverSearchCondition，返回值参考DriverInfoModel
        /// </summary>
        public static string GetDriverInfos_URL = @"/api/Driver/GetDrivers";
        /// <summary>
        /// 增加司机信息
        /// </summary>
        public static string AddDrivers_URL = @"/api/Driver/AddDrivers";
        /// <summary>
        /// 删除司机信息
        /// </summary>
        public static string DelDriversByIds_URL = @"/api/Driver/DelDriversByIds";
        /// <summary>
        /// 更新司机信息
        /// </summary>
        public static string UpdateDriver_URL = @"/api/Driver/UpdateDriver";

        /// <summary>
        /// 获取机车配属段信息，查询入口参数参考 TrainProprietorshipSearchCondition，返回值参考TrainProprietorshipModel
        /// </summary>
        public static string GetTrainProprietorships_URL = @"/api/TrainProprietorship/GetTrainProprietorships";
        /// <summary>
        /// 增加机车信息
        /// </summary>
        public static string AddTrainProprietorships_URL = @"/api/TrainProprietorship/AddTrainProprietorships";
        /// <summary>
        /// 删除机车信息
        /// </summary>
        public static string DelTrainProprietorshipsByIds_URL = @"/api/TrainProprietorship/DelTrainProprietorshipsByIds";
        /// <summary>
        /// 修改机车信息
        /// </summary>
        public static string UpdateTrainProprietorship_URL = @"/api/TrainProprietorship/UpdateTrainProprietorship";

        #region 分析报告添加
        /// <summary>
        /// 分析报告添加
        /// </summary>
        public static string AddAnalysisReportInfo_URL = @"/api/AnalysisReportInfo/AddAnalysisReportInfo";
        /// <summary>
        /// 删除报告信息
        /// </summary>
        public static string DelAnalysisReportInfo_URL = @"/api/AnalysisReportInfo/DelAnalysisReportInfo";
        /// <summary>
        /// 设置分析报告附件文件夹
        /// </summary>
        public static string AnalysisReportAttachment_SetExFolder_URL = @"/AnalysisReportAttachment/SetExFolder";
        /// <summary>
        /// 上传附件
        /// </summary>
        public static string AnalysisReportAttachment_FileUpload_URL = @"/AnalysisReportAttachment/FileUpload";
        /// <summary>
        /// 添加分析报告附件信息
        /// </summary>
        public static string AddAnalysisReportAttachment_URL = @"/AnalysisReportAttachment/AddAnalysisReportAttachment";
        #endregion
    }
}
