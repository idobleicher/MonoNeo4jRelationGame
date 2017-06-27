using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GameForOurProject.GameHandler
{
    public class Message
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern uint MessageBox(IntPtr hWnd, string text, string caption, uint type);

        public Message()
        {

        }



        public static void Show(string Msg)
        {
            MessageBox(new IntPtr(0), Msg, "You Did Something Wrong Nigga...", 0);
        }
    }
}
