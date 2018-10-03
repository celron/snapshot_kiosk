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
using System.Drawing;
using System.IO;

/*****************
 * Snapshot - is a windows form program that takes a snapshot of the entire screen.
 * It remains minimized and places the snapshot into c:\screenshots
 */

namespace Snapshot
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.WindowState = System.Windows.WindowState.Minimized;
            SnapshotProcess snapshot = new SnapshotProcess();
            snapshot.ExecuteProgram();
        }
    }

    class SnapshotProcess
    {
        // a method to pause the console. 
        // not a part of this project! 
        public static void pause()
        {
            Console.Read();
        }
        public static System.Drawing.Rectangle windowSize()
        {
            return System.Windows.Forms.Screen.PrimaryScreen.WorkingArea;
        }
        public void ExecuteProgram()
        {

            //https://code.msdn.microsoft.com/windowsdesktop/Saving-a-screenshot-using-C-6883abb3
            //this.WindowState = FormWindowState.Minimized;
            string filestamp = DateTime.Now.ToString("yyy-MM-dd-HHmmss");
            // parameters to change the width and height
            // note that this was changed to a windows form so that the height and width are pulled from GDI
            int width = 1080;
            int height = 1920;
            System.Drawing.Rectangle workingRectangle = windowSize();// System.Windows.Forms.Screen.PrimaryScreen.WorkingArea;


            width = workingRectangle.Width;// System.Windows.SystemParameters.PrimaryScreenWidth;

            height = workingRectangle.Height;// System.Windows.SystemParameters.PrimaryScreenHeight;
                                             // Start the process... 
            Console.WriteLine("Initializing the variables..." + width + ":" + height);


            Bitmap memoryImage;
            // this should be 1092 height x1080 width for the kiosk
            // this should be 1080 height x 1920 width for everything else?  how about screen size?
            // Bitmap(width,height)
            memoryImage = new Bitmap(width, height);
            System.Drawing.Size s = new System.Drawing.Size(memoryImage.Width, memoryImage.Height);

            // Create graphics 
            Graphics memoryGraphics = Graphics.FromImage(memoryImage);

            // Copy data from screen 
            memoryGraphics.CopyFromScreen(0, 0, 0, 0, s);

            //That's it! Save the image in the directory and this will work like charm. 
            string str = "";
            try
            {
                //str = string.Format(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +
                //      @"\Screenshot.png");
                // note that the directory must exist
                // filename should be based on date and time yyyymmdd-hh-ss
                str = string.Format(@"c:\screenshots\" + filestamp + ".jpg");// screenshot.jpg");
            }
            catch (Exception er)
            {
                Console.WriteLine("Sorry, there was an error: " + er.Message);
                Console.WriteLine();
            }

            // Save it! 
            //            Console.WriteLine("Saving the image...");
            memoryImage.Save(str);

        }
    }

}
