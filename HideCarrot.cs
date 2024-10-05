/*
* Added component to hide caret in TextBoxes. Code from Stack Overflow:
* https://stackoverflow.com/questions/44131/how-do-i-hide-the-input-caret-in-a-system-windows-forms-textbox
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_System
{
    public partial class HideCarrot : TextBox
    {
        /*
        [DllImport("user32.dll")]
        static extern bool HideCaret(IntPtr hWnd);

        public HideCarrot()
        {
            this.ReadOnly = false;
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            HideCaret(this.Handle);
        }
        */
    }
}
