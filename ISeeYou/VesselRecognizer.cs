using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using Emgu;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;


namespace ISeeYou
{
    class VesselRecognizer
    {
        private Mat tmpMatImage;
        string outputFile;

        public VesselRecognizer(String newImage)
        {

            try
            {
                this.tmpMatImage = CvInvoke.Imread(newImage);
                outputFile = newImage.Split('.')[0] + ".output";
            }
            catch (Exception)
            {
                
                throw;
            }
          
        }

        public int TresholdForSobel { get; set; } = 210;

        public void recognizeVessel()
        {
            this.transformateToScaleGrey();

            // Wyświetlanie poszczególnych etapów -> Okienek można zakomentować
            //CvInvoke.NamedWindow("Scale of gray",NamedWindowType.FreeRatio);
            //CvInvoke.Imshow("Scale of gray", this.tmpMatImage);

            //Średnia jasność obrazu - bardzo nieprzydatna, bo daje nam za mało informacji
            MCvScalar meanValueOfImage = CvInvoke.Mean(this.tmpMatImage);

            Mat SobelX = new Mat();
            Mat SobelY = new Mat();
            Mat Sobels = new Mat();
            CvInvoke.Sobel(this.tmpMatImage, SobelX, tmpMatImage.Depth, 1, 0, 3);
            CvInvoke.Sobel(this.tmpMatImage, SobelY, tmpMatImage.Depth, 0, 1, 3);

            CvInvoke.BitwiseOr(SobelX,SobelY,Sobels);

            //CvInvoke.NamedWindow("Sobels", NamedWindowType.FreeRatio);
            //CvInvoke.Imshow("Sobels", Sobels);


            Mat SobelEqual = new Mat();
            Sobels.CopyTo(SobelEqual);
            CvInvoke.EqualizeHist(Sobels,SobelEqual);

            Mat SobelTresh = new Mat();        


            CvInvoke.Erode(SobelEqual,SobelTresh,new Mat(3,3,DepthType.Cv8U, 1), new Point(-1,-1),1,BorderType.Constant,default(MCvScalar));


            //CvInvoke.NamedWindow("Erode", NamedWindowType.FreeRatio);
            //CvInvoke.Imshow("Erode", SobelTresh);
            CvInvoke.Threshold(SobelEqual, SobelTresh, TresholdForSobel, 255, ThresholdType.Binary);
            CvInvoke.Dilate(SobelTresh,SobelTresh,new Mat(3,3,DepthType.Cv8U,1),new Point(-1,-1),1,BorderType.Default, default(MCvScalar));
            CvInvoke.Erode(SobelTresh,SobelTresh,new Mat(3,3,DepthType.Cv8U,1),new Point(-1,-1),1,BorderType.Default, default(MCvScalar));
            CvInvoke.MedianBlur(SobelTresh,SobelTresh,5);


            //CvInvoke.NamedWindow("SobelsThreshold", NamedWindowType.FreeRatio);
            //CvInvoke.Imshow("SobelsThreshold", SobelTresh);

            Mat TempInput = new Mat();
            CvInvoke.CvtColor(this.tmpMatImage,TempInput,ColorConversion.BayerBg2Gray);
            CvInvoke.Threshold(TempInput, TempInput, 5, 255, ThresholdType.Binary);
            CvInvoke.Erode(TempInput,TempInput,new Mat(3,3,DepthType.Cv8U, 1),new Point(-1,-1),1,BorderType.Constant,default(MCvScalar));

            // CvInvoke.NamedWindow("TempInput", NamedWindowType.FreeRatio);
            // CvInvoke.Imshow("TempInput", TempInput);

            Mat VeinsWithouthCircle = new Mat(SobelTresh.Size,DepthType.Cv8U,1);
            SobelTresh.CopyTo(VeinsWithouthCircle,TempInput);

            CvInvoke.NamedWindow("VeinsWithouthCircle", NamedWindowType.FreeRatio);
            CvInvoke.Imshow("VeinsWithouthCircle", VeinsWithouthCircle);

           // VectorOfColorPoint countours = new VectorOfColorPoint();
            VectorOfVectorOfPoint countours = new VectorOfVectorOfPoint();
            Mat hierarchy =  new Mat();
           // VectorOfByte hierarchy = new VectorOfByte();

            CvInvoke.FindContours(VeinsWithouthCircle, countours, hierarchy, RetrType.List, ChainApproxMethod.ChainApproxSimple, new Point(0, 0));

            Mat DrawImage = new Mat(SobelTresh.Size, DepthType.Cv8U, 3);

            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            Random rnd = new Random(Environment.TickCount);
            for (int i = 0; i < countours.Size; i++)
            {
                
                var r = rnd.Next(255);
                var g = rnd.Next(255);
                var b = rnd.Next(255);
                Console.WriteLine("" + r + g + b);
                if (countours[i].Size > 15){
                    sb.Append("[");
                    var pointsArray = countours[i].ToArray();
                    for (int j = 0; j < countours[i].Size; j++)
                    {
                        sb.Append("(");
                        sb.Append(pointsArray[j].X);
                        sb.Append(",");
                        sb.Append(pointsArray[j].Y);
                        if (j == countours[i].Size - 1)
                            sb.Append(")");
                        else
                            sb.Append("),");
                    }
                    if(i == countours.Size - 1)
                        sb.Append("]");
                    else
                        sb.Append("],");
                    MCvScalar color = new MCvScalar(r, g, b);
                    CvInvoke.DrawContours(DrawImage, countours, i, color, 1, LineType.EightConnected, hierarchy);
                }
               // CvInvoke.DrawContours(DrawImage, countours, i, color, 1, LineType.EightConnected, hierarchy);
            }
            sb.Append("]");
            File.WriteAllText(outputFile, sb.ToString());


            CvInvoke.NamedWindow("DrawImage", NamedWindowType.FreeRatio);
            CvInvoke.Imshow("DrawImage", DrawImage);




        }

        private void transformateToScaleGrey()
        {
            try
            {
                CvInvoke.CvtColor(this.tmpMatImage, this.tmpMatImage, ColorConversion.Bgr2Gray);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        
    }
}
