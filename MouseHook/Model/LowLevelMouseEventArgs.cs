using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseHook.Model
{
    public class LowLevelMouseEventArgs : EventArgs
    {
        public POINT Point { get; set; }
        public EventType EventType { get; set; }
        public uint Timestamp { get; set; }
        public EventFlags EventFlags { get; set; }
        public uint ExtraInfo { get; set; }

        public LowLevelMouseEventArgs()
        {
            Point = new POINT() { x = 0, y = 0 };
            Timestamp = 0;
            EventFlags = 0;
            ExtraInfo = 0;
        }

        internal LowLevelMouseEventArgs(EventType eventType, MSLLHOOKSTRUCT data)
        {
            Point = data.pt;
            EventType = eventType;
            Timestamp = data.time;
            EventFlags = (EventFlags)data.flags;
            ExtraInfo = (uint)data.dwExtraInfo;
        }
    }
}
