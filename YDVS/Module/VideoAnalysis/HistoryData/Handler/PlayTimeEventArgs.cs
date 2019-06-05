using System;

namespace VideoAnalysis.HistoryData.Handler
{
    public class PlayTimeEventArgs : EventArgs
    {
        public DateTime? PlayCurrentTime { get; set; }
        public PlayTimeEventArgs(DateTime? _PlayCurrentTime)
        {
            this.PlayCurrentTime = _PlayCurrentTime;

        }
    }
}
