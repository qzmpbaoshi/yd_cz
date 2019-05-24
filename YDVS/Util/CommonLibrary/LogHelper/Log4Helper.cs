using log4net;
using System;

namespace CommonLibrary.LogHelper
{
    public static class Log4Helper
    {
        private static int _debug = 1;
        private static int _info = 2;
        private static int _warn = 3;
        private static int _error = 4;
        private static int _fatal = 5;
        private static int _nothing = 6;
        private static int _level = _debug;

        /// <summary>
        /// 致命的错误信息
        /// </summary>
        /// <param name="type">报错类型</param>
        /// <param name="message">自定义信息打印</param>
        /// <param name="exception">异常信息打印，默认为“null”不打印</param>
        public static void Fatal(Type type, object message, Exception exception = null)
        {
            if (_level > _fatal) return;
            ILog log = LogManager.GetLogger(type);
            if (exception == null)
                log.Fatal(message);
            else
                log.Fatal(message, exception);
        }
        /// <summary>
        /// 普通的错误信息
        /// </summary>
        /// <param name="type">报错类型</param>
        /// <param name="message">自定义信息打印</param>
        /// <param name="exception">异常信息打印，默认为“null”不打印</param>
        public static void Error(Type type, object message, Exception exception = null)
        {
            if (_level > _error) return;
            ILog log = LogManager.GetLogger(type);
            if (exception == null)
                log.Error(message);
            else
                log.Error(message, exception);
        }
        /// <summary>
        /// 警告信息
        /// </summary>
        /// <param name="type">报错类型</param>
        /// <param name="message">自定义信息打印</param>
        /// <param name="exception">异常信息打印，默认为“null”不打印</param>
        public static void Warn(Type type, object message, Exception exception = null)
        {
            if (_level > _warn) return;
            ILog log = LogManager.GetLogger(type);
            if (exception == null)
                log.Warn(message);
            else
                log.Warn(message, exception);
        }
        /// <summary>
        ///  普通信息
        /// </summary>
        /// <param name="type">报错类型</param>
        /// <param name="message">自定义信息打印</param>
        /// <param name="exception">异常信息打印，默认为“null”不打印</param>
        public static void Info(Type type, object message, Exception exception = null)
        {
            if (_level > _info) return;
            ILog log = LogManager.GetLogger(type);
            if (exception == null)
                log.Info(message);
            else
                log.Info(message, exception);
        }
        /// <summary>
        ///  测试信息
        /// </summary>
        /// <param name="type">报错类型</param>
        /// <param name="message">自定义信息打印</param>
        /// <param name="exception">异常信息打印，默认为“null”不打印</param>
        public static void Debug(Type type, object message, Exception exception = null)
        {
            if (_level > _debug) return;
            ILog log = LogManager.GetLogger(type);
            if (exception == null)
                log.Debug(message);
            else
                log.Debug(message, exception);
        }
    }
}
