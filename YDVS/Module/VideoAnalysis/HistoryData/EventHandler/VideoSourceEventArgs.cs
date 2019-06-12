using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoAnalysis.HistoryData.ViewModel;

namespace VideoAnalysis.HistoryData.EventHandler
{
    public class VideoSourceEventArgs : EventArgs
    {
        public string ParentDirName { get; set; }
        public List<VideoSource> VideoSources { get; set; }
        public VideoSourceEventArgs(string _parentDirName, List<VideoSource> _videoSources)
        {
            this.ParentDirName = _parentDirName;
            this.VideoSources = _videoSources;
        }
    }
}
