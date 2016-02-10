using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WindowsInput;
using WindowsInput.Native;
using TpaHCJluT.modules;
using Clipboard = System.Windows.Clipboard;
using TextDataFormat = System.Windows.TextDataFormat;

namespace TpaHCJluT
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private HookManager _manager;
        private ITextTransformer _textTransformer;
        InputSimulator _input = new InputSimulator();

        public MainWindow()
        {
            InitializeComponent();
            NotifyIcon ni = new System.Windows.Forms.NotifyIcon();
            ni.Icon = System.Drawing.Icon.ExtractAssociatedIcon(System.Reflection.Assembly.GetEntryAssembly().ManifestModule.Name);
            ni.Visible = true;
            ni.DoubleClick +=
                delegate(object sender, EventArgs args)
                {
                    this.Show();
                    this.WindowState = WindowState.Normal;
                };
        }

        protected override void OnStateChanged(EventArgs e)
        {
            if (WindowState == System.Windows.WindowState.Minimized)
                this.Hide();
            base.OnStateChanged(e);
        }


        private void Window_Initialized(object sender, EventArgs e)
        {
            _manager = new HookManager(this);
            _textTransformer = new TextTransformer();
            _manager.HotkeyPressed += (o, args) =>
            {
                _input.Keyboard
                    .Sleep(50)
                    .ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_A)
                    .Sleep(50)
                    .ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_X)
                    .Sleep(50);
                string buffer = String.Empty;
                Thread staThread = new Thread(o1 =>
                {
                    try
                    {
                        buffer = Clipboard.GetText(TextDataFormat.UnicodeText);
                    }
                    catch (Exception ex)
                    {
                        buffer = ex.Message;
                    }
                });
                staThread.SetApartmentState(ApartmentState.STA);
                staThread.Start();
                staThread.Join();
                Clipboard.SetText(_textTransformer.TransformText(buffer.ToLower()));
                _input.Keyboard.Sleep(50).ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_V);
            };
        }
    }
}
