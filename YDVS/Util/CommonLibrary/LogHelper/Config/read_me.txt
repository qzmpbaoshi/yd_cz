<log4net>
  <!--错误日志类-->
  <logger name="logerror"><!--日志类的名字-->
    <level value="ALL" /><!--定义记录的日志级别-->
    <appender-ref ref="ErrorAppender" /><!--记录到哪个介质中去-->
  </logger>
  <!--信息日志类-->
  <logger name="loginfo">
    <level value="ALL" />
    <appender-ref ref="InfoAppender" />
  </logger>
  <!--错误日志附加介质-->
  <appender name="ErrorAppender" type="log4net.Appender.RollingFileAppender"><!-- name属性指定其名称,type则是log4net.Appender命名空间的一个类的名称,意思是,指定使用哪种介质-->
     <filter type="log4net.Filter.LevelMatchFilter">
        <levelToMatch value="FATAL" />
      </filter><!--拦截器设置，设置当前配置为哪个级别，若没有该项则所有log均写到同一文件内-->
      <filter type="log4net.Filter.DenyAllFilter" /><!--拦截器设置，设置当前配置为哪个级别，若没有该项则所有log均写到同一文件内-->
	<param name="File" value="Log\\LogError\\" /><!--日志输出到exe程序这个相对目录下-->
    <param name="AppendToFile" value="true" /><!--输出的日志不会覆盖以前的信息-->
    <param name="MaxSizeRollBackups" value="100" /><!--备份文件的个数-->
    <param name="MaxFileSize" value="10240" /><!--当个日志文件的最大大小-->
    <param name="StaticLogFileName" value="false" /><!--是否使用静态文件名-->
    <param name="DatePattern" value="yyyyMMdd&quot;.htm&quot;" /><!--日志文件名-->
    <param name="RollingStyle" value="Date" /><!--文件创建的方式，这里是以Date方式创建-->
    <!--错误日志布局-->
    <layout type="log4net.Layout.PatternLayout">
      <param name="ConversionPattern" value="&lt;HR COLOR=red&gt;%n异常时间：%d [%t] &lt;BR&gt;%n异常级别：%-5p &lt;BR&gt;%n异 常 类：%c [%x] &lt;BR&gt;%n%m &lt;BR&gt;%n &lt;HR Size=1&gt;"  />
    </layout>
  </appender>
  <!--信息日志附加介质-->
  <appender name="InfoAppender" type="log4net.Appender.RollingFileAppender">
    <param name="File" value="Log\\LogInfo\\" />
    <param name="AppendToFile" value="true" />
    <param name="MaxFileSize" value="10240" />
    <param name="MaxSizeRollBackups" value="100" />
    <param name="StaticLogFileName" value="false" />
    <param name="DatePattern" value="yyyyMMdd&quot;.htm&quot;" />
    <param name="RollingStyle" value="Date" />
    <!--信息日志布局-->
    <layout type="log4net.Layout.PatternLayout">
      <param name="ConversionPattern" value="&lt;HR COLOR=blue&gt;%n日志时间：%d [%t] &lt;BR&gt;%n日志级别：%-5p &lt;BR&gt;%n日 志 类：%c [%x] &lt;BR&gt;%n%m &lt;BR&gt;%n &lt;HR Size=1&gt;"  />
    </layout>
  </appender>
</log4net>



//这种配置，是将日志写入到文本文件当中，若是需要将日志已其他形式保存，可以看 http://www.cnblogs.com/qianlifeng/archive/2011/04/22/2024856.html
        
<param name="File" value="Logger/"/>//日志存放位置（这里的value值是一个Logger，表示在项目文件夹中创建一个名叫Logger的文件。也可以是value="c:\log.txt"）
<param name="AppendToFile" value="true"/>//是否追加到文件
<param name="RollingStyle" value="Date"/>//变换的形式为日期
<param name="DatePattern" value="&quot;Logs_&quot;yyyyMMdd&quot;.txt&quot;"/>//生成格式；每天生成一个日志
<param name="StaticLogFileName" value="false"/>//日志文件名，是否固定不变
<layout type="log4net.Layout.PatternLayout,log4net">
<param name="ConversionPattern" value="%d [%t] %-5p %c - %m%n"/>//这3行表示日志输出的格式，若不喜欢这样的样式，可以查看下面的输出样式，自行修改
<param name="Header" value="&#xA;----------------------header--------------------------&#xA;"/>
<param name="Footer" value="&#xA;----------------------footer--------------------------&#xA;"/>    


输出样式：

%m(message):输出的日志消息，如ILog.Debug(…)输出的一条消息 
%n(new line):换行 
%d(datetime):输出当前语句运行的时刻 
%r(run time):输出程序从运行到执行到当前语句时消耗的毫秒数 
%t(thread id):当前语句所在的线程ID 
%p(priority): 日志的当前优先级别，即DEBUG、INFO、WARN…等 
%c(class):当前日志对象的名称，例如： 
%f(file):输出语句所在的文件名。 
%l(line)：输出语句所在的行号。 
%数字：表示该项的最小长度，如果不够，则用空格填充，如“%-5level”表示level的最小宽度是5个字符，如果实际长度不够5个字符则以空格填充。

通过这些东西，你可以任意组合你喜欢的输出格式内容。




 <!-- 控制台前台显示日志 -->
    <appender name="ColoredConsoleAppender" type="log4net.Appender.ColoredConsoleAppender">
      <mapping>
        <level value="ERROR" />
        <foreColor value="Red, HighIntensity" />
      </mapping>
      <mapping>
        <level value="Info" />
        <foreColor value="Green" />
      </mapping>
      <layout type="log4net.Layout.PatternLayout">
        <conversionPattern value="%n%date{HH:mm:ss,fff} [%-5level] %m" />
      </layout>

      <filter type="log4net.Filter.LevelRangeFilter">
        <param name="LevelMin" value="Info" />
        <param name="LevelMax" value="Fatal" />
      </filter>
    </appender>