using System.Runtime.InteropServices;

namespace MouseHook.Model
{
    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int x;
        public int y;
    }
}