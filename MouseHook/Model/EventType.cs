using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseHook.Model
{
    public enum EventType
    {
        LBUTTONDOWN = 0x0201,
        LBUTTONUP = 0x0202,
        MOUSEMOVE = 0x0200,
        MOUSEWHEEL = 0x020A,
        RBUTTONDOWN = 0x0204,
        RBUTTONUP = 0x0205
    }
}
