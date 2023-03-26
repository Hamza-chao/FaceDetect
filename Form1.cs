using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.UI;
using Emgu.CV.CvEnum;
using Emgu.CV.ImgHash;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Emgu.CV.Features2D;
using System.Security.Policy;
using System.IO;
using Emgu.CV.Dnn;
using System.CodeDom;


namespace FaceDetect
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


            VideoCapture capture;
            string facePath = Path.GetFullPath(@"C:\Users\chaoh\Downloads\Haar-Training\Haar Training\training\positive\eye.xml");
            CascadeClassifier classifierFace;


            private void Form1_Load(object sender, EventArgs e)
            {
                Mat frame = new Mat();
                capture = new VideoCapture(0);
                capture.Retrieve(frame);
                classifierFace = new CascadeClassifier(facePath);


                Application.Idle += Application_Idle;


            }






            private void Application_Idle(object sender, EventArgs e)
            {
                Mat frame = capture.QueryFrame();

                Image<Bgr, byte> imgInput = capture.QueryFrame().Clone().ToImage<Bgr, byte>();
                var imgGray = imgInput.Convert<Gray, byte>().Clone();
                Rectangle[] faces = classifierFace.DetectMultiScale(imgGray, 1.1, 4);
                foreach (var face in faces)
                {
                    imgInput.Draw(face, new Bgr(0, 0, 255), 2);

                    imgGray.ROI = face;


                }
                pictureBox1.Image = imgInput.ToBitmap();
            }


        }


    }


