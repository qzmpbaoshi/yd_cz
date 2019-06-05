using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoAnalysis.HistoryData.ViewModel;

namespace VideoAnalysis.HistoryData.Handler
{
    public class VideoSourceEventArgs:EventArgs
    {
        public List<VideoSource> VideoSources { get; set; }
        public VideoSourceEventArgs(List<VideoSource> _videoSources) {
            this.VideoSources = _videoSources;
        }
    }
}
