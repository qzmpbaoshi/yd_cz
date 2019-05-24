using System;
using System.ServiceModel;

namespace CommonLibrary.Factory
{
    public static class WebServiceFactory<T> where T : class
    {
        private static ChannelFactory<T> _instance;

        internal static ChannelFactory<T> instance
        {
            get
            {
                _instance.Faulted += (i, j) => { };
                _instance.Closed += (i, j) => { };
                _instance.Opened += (i, j) => { };
                return _instance;
            }
            set { _instance = value; }
        }
        /// <summary>
        /// 获取服务
        /// </summary>
        /// <param name="baseUrl">基础连接</param>
        /// <param name="serviceName">服务名称</param>
        /// <returns></returns>
        public static T WebInstance(string baseUrl, string serviceName)
        {
            try
            {
                string serviceUrl = baseUrl + "/" + serviceName;
                BasicHttpBinding http = new BasicHttpBinding();
                http.OpenTimeout = new TimeSpan(0, 0, 0, 10);
                http.CloseTimeout = new TimeSpan(0, 0, 0, 10);
                http.SendTimeout = new TimeSpan(0, 0, 5, 0);
                http.ReceiveTimeout = new TimeSpan(0, 0, 5, 0);
                http.MaxBufferPoolSize = long.MaxValue - 1;
                http.MaxReceivedMessageSize = int.MaxValue - 1;
                http.MaxBufferSize = int.MaxValue - 1;
                instance = new ChannelFactory<T>(http, new EndpointAddress(serviceUrl));
                return instance.CreateChannel();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
