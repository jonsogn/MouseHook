using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormsDemo
{
    public partial class FormMain : Form
    {
        private MouseHook.MouseHook MouseHook;

        private void InitializeAdditionalComponent()
        {
            MouseHook = new MouseHook.MouseHook(false);
            MouseHook.MouseEvent += MouseHook_MouseEvent;
        }

        private void MouseHook_MouseEvent(object sender, MouseHook.Model.LowLevelMouseEventArgs e)
        {
            tbOutput.Text += $"Time: {e.Timestamp}, Point(x;y): {e.Point.x};{e.Point.y}, Event: {e.EventType}, Flags: {e.EventFlags}" + Environment.NewLine;
        }

        public FormMain()
        {
            InitializeComponent();
            InitializeAdditionalComponent();
        }

        private void BtnHook_Click(object sender, EventArgs e)
        {
            MouseHook.InstallHook();
        }

        private void BtnUnhook_Click(object sender, EventArgs e)
        {
            MouseHook.UninstallHook();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            if (MouseHook.IsHookInstalled)
            {
                MouseHook.UninstallHook();
            }

            Environment.Exit(0);
        }
    }
}
