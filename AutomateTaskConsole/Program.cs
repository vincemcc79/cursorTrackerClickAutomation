using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestStack.White.InputDevices;

namespace AutomateTaskConsole
{
    class Program
    {
        private const float textResizeFactor = 0.6666F;
        // normal pixels
        private const int areaOfInterestXPos = 0;
        private const int areaOfInterestYPos = 86;
        private const int areaOfInterestWidth = 595;
        private const int areaOfInterestHeight = 595;

        //following need to be scaled to account of text sizes
        private static int resetExpressionsXPos = Convert.ToInt32(764 * textResizeFactor);
        private static int resetExpressionsYPos = Convert.ToInt32(1267 * textResizeFactor);

        private static int angryX = Convert.ToInt32(1201 * textResizeFactor);
        private static int angryY = Convert.ToInt32(195 * textResizeFactor);
        private static int disgustX = Convert.ToInt32(985 * textResizeFactor);
        private static int disgustY = Convert.ToInt32(266 * textResizeFactor);

        private static int happy1X = Convert.ToInt32(1070 * textResizeFactor);
        private static int happy1Y = Convert.ToInt32(481 * textResizeFactor);

        private static int happy2X = Convert.ToInt32(967 * textResizeFactor);
        private static int happy2Y = Convert.ToInt32(549 * textResizeFactor);

        private static int morphingX = Convert.ToInt32(1219 * textResizeFactor);
        private static int morphingY = Convert.ToInt32(120 * textResizeFactor);

        private static int generateTabX = Convert.ToInt32(688 * textResizeFactor);
        private static int generateTabY = Convert.ToInt32(118 * textResizeFactor);

        private static int generateButtonX = Convert.ToInt32(730 * textResizeFactor);
        private static int generateButtonY = Convert.ToInt32(285 * textResizeFactor);


        private static int GenAge15X = Convert.ToInt32(880 * textResizeFactor);
        private static int GenAge30X = Convert.ToInt32(880 * textResizeFactor);
        private static int GenAge45X = Convert.ToInt32(880 * textResizeFactor);
        private static int GenAge60X = Convert.ToInt32(880 * textResizeFactor);

        private static int GenAge15Y = Convert.ToInt32(526 * textResizeFactor);
        private static int GenAge30Y = Convert.ToInt32(609 * textResizeFactor);
        private static int GenAge45Y = Convert.ToInt32(697 * textResizeFactor);
        private static int GenAge60Y = Convert.ToInt32(818 * textResizeFactor);

        private static string TickName = "";
        private static string AgeFolder = "15";

        public static void Main(string[] args)
        {
            #region reference rubbish
            // var processToRun = new ProcessStartInfo(@"C:\Program Files (x86)\Singular Inversions\FaceGen Modeller 3.5 Free\FaceGenDemo.exe");
            // var app = Application.Launch(processToRun);
            //app.Close();
            #endregion


            Thread.Sleep(6000);//open the window to copy from during this time
          //  StartProcess("Female");
            StartProcess("Female/Mixed");
        }

        public static void StartProcess(string folderName)
        {
            int count = 10;
            string name = "";
            for (int i = 0; i < count; i++)
            {
                TickName = DateTime.Now.Ticks.ToString();
                //generate new face
                MakeClick(generateTabX, generateTabY);
                MakeClick(generateButtonX, generateButtonY);

                //set to 20
                CreateFaceForAge(GenAge15X, GenAge15Y, "15", ref name);
                CreateEmotions(name, folderName);

                //generate 40
                CreateFaceForAge(GenAge30X, GenAge30Y, "30", ref name);
                // create emotions
                CreateEmotions(name, folderName);

                CreateFaceForAge(GenAge45X, GenAge45Y, "45", ref name);
                // create emotions
                CreateEmotions(name, folderName);

                //generate 60
                CreateFaceForAge(GenAge60X, GenAge60Y, "60", ref name);
                // create emotions
                CreateEmotions(name, folderName);

            }
        }

        private static void CreateFaceForAge(int x, int y, string age, ref string name)
        {
            name = $"{TickName}_{age}";
            AgeFolder = age;
            ClickGenerateTab();

            //as the slider goes in increments of 10 make 5 to get to 40 (halfway and 10 to get the other direction)
            int numClicks = 5;
            if (age == "15")
                numClicks = 7;

            for (int i = 0; i < numClicks; i++)
            {
                MakeClick(x, y);// manipulate age slider
            }
        }

        private static void CreateEmotions(string name, string folderName)
        {
           // Thread.Sleep(2000);
            // click morphing
            ClickMorphingTab();
            CreateRegular(name, folderName);           
            CreateAngry(name, folderName);
            CreateHappy(name, folderName);
        }

        private static void ClickMorphingTab()
        {
            MakeClick(morphingX, morphingY);
        }

        private static void ClickGenerateTab()
        {
            MakeClick(generateTabX, generateTabY);
        }

        // set up angry face
        private static void CreateAngry(string name, string folderName)
        {
            MakeClick(angryX, angryY);
            MakeClick(disgustX, disgustY);
            Thread.Sleep(100);
            SaveScreenShot(folderName, $"{name}_Angry");
            ResetExpressions();
        }

        private static void CreateHappy(string name, string folderName)
        {
            MakeClick(happy1X, happy1Y);
            MakeClick(happy2X, happy2Y);
            Thread.Sleep(100);
            SaveScreenShot(folderName, $"{name}_Happy");
            ResetExpressions();
        }

        private static void CreateRegular(string name, string folderName)
        {
            SaveScreenShot(folderName, $"{name}_Reg");
            ResetExpressions();
        }

        private static void ResetExpressions()
        {
            MakeClick(resetExpressionsXPos, resetExpressionsYPos);
          //  Thread.Sleep(1000);
        }
        

        private static void MakeClick(int x, int y)
        {
            Mouse.Instance.Location = new System.Windows.Point(x, y);
            Mouse.Instance.Click();
        }

        private static void MakeClick(System.Windows.Point toClick)
        {
            Mouse.Instance.Location = toClick;
            Mouse.Instance.Click();
        }

        private static void SaveScreenShot(string folderName, string name)
        {
            Console.WriteLine("SCREENSHOT HERE:" + name);
           // Thread.Sleep(2000);
            Rectangle rect = new Rectangle(areaOfInterestXPos, areaOfInterestYPos, areaOfInterestWidth, areaOfInterestHeight);
            Bitmap bmpSShot = new Bitmap(rect.Width, rect.Height, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(bmpSShot);
            g.CopyFromScreen(rect.Left, rect.Top, 0, 0, bmpSShot.Size, CopyPixelOperation.SourceCopy);
            string saveLocation = $@"C:\Users\vmccarthy\Documents\Visual Studio 2015\Projects\ScreenCaptureAndAutomation\Pictures\{folderName}\{TickName}\{AgeFolder}\{name}.jpg";
            Console.WriteLine(saveLocation);
            CreateFolderIfItDoesntExist(saveLocation);
            bmpSShot.Save(saveLocation, ImageFormat.Jpeg);
        }

        //private Bitmap CropBitmap(Bitmap bitmap, int cropX, int cropY, int cropWidth, int cropHeight)
        //{
        //    Rectangle rect = new Rectangle(cropX, cropY, cropWidth, cropHeight);
        //    Bitmap cropped = bitmap.Clone(rect, bitmap.PixelFormat);
        //    return cropped;
        //}

        private static void CreateFolderIfItDoesntExist(string path)
        {
            string directoryName = Path.GetDirectoryName(path);
            if ((directoryName.Length > 0) && (!Directory.Exists(directoryName)))
            {
                Directory.CreateDirectory(directoryName);
            }
        }
    }
}
