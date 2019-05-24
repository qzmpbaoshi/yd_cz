using System;
using System.ComponentModel.Composition;
using System.Windows;
using SystemModel;

namespace VideoAnalysis
{
    [Export(typeof(ModuleInterface.ModuleInterface))]
    public class VideoAnalysisMef : ModuleInterface.ModuleInterface
    {
        public event EventHandler CloseEvent;

        public UIElement GetControl(User User)
        {
            MainControl mainControl = new MainControl();
            mainControl.CloseEvent += this.CloseEvent;
            return mainControl;
        }

        public string GetControlCode()
        {
            return "VideoAnalysis";
        }

        public string GetControlName()
        {
            return "YDVS-6Z视频监测分析系统 V1.0";
        }
    }
}
