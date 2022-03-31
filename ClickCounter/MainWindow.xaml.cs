using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using WindowsHook;

namespace ClickCounter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IKeyboardMouseEvents mGlobalHook;

        public MainWindow()
        {
            InitializeComponent();
        }

        public void Subscribe()
        {
            mGlobalHook = Hook.GlobalEvents();
            mGlobalHook.MouseDownExt += GlobalHookMouseDownExt;
            //mGlobalHook.KeyPress += GlobalHookKeyPress;

        }

        private void GlobalHookKeyPress(object sender, KeyPressEventArgs e)
        {
            Console.WriteLine("KeyPress: \t{0}", e.KeyChar);
        }

        private void GlobalHookMouseDownExt(object sender, MouseEventExtArgs e)
        {
            Console.WriteLine("MouseDown: \t{0}; \t System Timestamp: \t{1}", e.Button, e.Timestamp);

            // uncommenting the following line will suppress the middle mouse button click
            // if (e.Buttons == MouseButtons.Middle) { e.Handled = true; }
            int count;
            switch (e.Button)
            {
                case MouseButtons.Left:
                    count = int.Parse((string)LeftClickLabel.Content);
                    count++;
                    LeftClickLabel.Content = count.ToString();
                    break;
                case MouseButtons.None:
                    break;
                case MouseButtons.Right:
                    count = int.Parse((string)RightClickLabel.Content);
                    count++;
                    RightClickLabel.Content = count.ToString();
                    break;
                case MouseButtons.Middle:
                    count = int.Parse((string)MiddleClickLabel.Content);
                    count++;
                    MiddleClickLabel.Content = count.ToString();
                    break;
                case MouseButtons.XButton1:
                    break;
                case MouseButtons.XButton2:
                    break;
                default:
                    break;
            }
        }

        public void Unsubscribe()
        {
            mGlobalHook.MouseDownExt -= GlobalHookMouseDownExt;
            mGlobalHook.KeyPress -= GlobalHookKeyPress;

            //It is recommened to dispose it
            mGlobalHook.Dispose();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Subscribe();
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            Unsubscribe();
        }
    }
}
