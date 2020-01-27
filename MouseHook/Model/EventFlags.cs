using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseHook.Model
{
    public enum EventFlags : uint
    {
        INJECTED = 0x01,
        LOWER_IL_INJECTED = 0x02,
    }
}
