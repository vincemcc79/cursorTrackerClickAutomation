using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;
using TestStack.White;

namespace ScreenCaptureAndAutomation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void KeepReportMousePos()
        {
            //var ts = new CancellationTokenSource();
            //CancellationToken ct = ts.Token;

            //Endless Report Mouse position
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    //if (ct.IsCancellationRequested)
                    //{
                    //    break;
                    //}

                    this.Dispatcher.Invoke(
                        DispatcherPriority.SystemIdle,
                        new Action(() =>
                        {
                            GetCursorPos();
                        }));
                }
              //  ts.Cancel();
            });
        }
        public void GetCursorPos()
        {
            //get the mouse position and show on the TextBlock
            System.Drawing.Point p = System.Windows.Forms.Cursor.Position;
            TBK1.Text = $"100% : {p.X },{p.Y}";
            TBK125.Text = $"125% : {p.X / 1.25},{p.Y / 1.25}";
            TBK15.Text = $"150% : {p.X / 1.5},{p.Y / 1.5}";
            TBK175.Text = $"175% : {p.X / 1.75},{p.Y / 1.75}";
            TBK2.Text = $"200% : {p.X /2},{p.Y /2}";
        }

        protected override void OnMouseMove(MouseEventArgs mouseEv)
        {
            KeepReportMousePos();
        }


    }
}
