using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace asciifyPhotoshop
{
    class BitmapAscii
    {
        private string stringImage { get; set; } = "";//string representation of the desired image
        public char[] charsByScale { get; set; } = { '@', 'y', 'i', '~', '^', '.' };//each char represent a shade of grayness from range1 (the darkest/black) to range6 (the lightest/white).
        public int[] kernelSize = { 1, 1 };//width ,(then) height ~ the kernelSize of an asciiImage is used when resizing an image ~ a kernel of 1(width), 1(height) is the same of the original image, however a kernelSize of 3(width), 3(height) will result in the image being one third of its original size


        public void Asciitize(Bitmap grayImage)
        //returns image as an image of ascii characters
        {
            int x = 0, y = 0;//will be used as the kernels starting position within the image 
            double normalizer = 0.0;//measures the shade of a pixel from 0.0 (the darkest) to 1.0 (the lightest)
            List<double> theKernel = new List<double>();//will set a new pixel based on the values within it
            StringBuilder asciiPicture = new StringBuilder();//will be returned to the stringImage variable


            while (x < grayImage.Width && y < grayImage.Height)
            //while the entire image hasn't been scanned
            {
                bool finished = false;//is true once all pixels have been iterated over


                //utilize two for loops as the kernel ~ the outer loop represent the y axis, the inner represents the x axis
                for (int j = y; j < y + kernelSize[1]; j++)
                {
                    //once the last of the y axis is complete, all pixels have been evaluated
                    if (j >= grayImage.Height)
                    {
                        finished = true;
                        break;
                    }

                    for (int i = x; i < x + kernelSize[0]; i++)
                    {
                        if (i >= grayImage.Width - 1)//if the kernel passes the width of the image break the loop to move on
                        {
                            break;
                        }

                        //reads pixel value and normalizes it
                        normalizer = AveragePixel(grayImage.GetPixel(i, j));

                        //list of normals in the kernel
                        theKernel.Add(normalizer);

                    }
                }


                //find pixel's ascii replacement and place it into the string, then clear the list to ready it for the next group of pixels
                asciiPicture.Append(GrayToChar(AverageNormal(theKernel)));
                theKernel.Clear();

                x += kernelSize[0];//set kernels new starting x


                //if x reaches or passes the edge of the image, begin the next line of the image
                if (x >= grayImage.Width - 1)
                {
                    asciiPicture.Append('\n');
                    x = 0;
                    y += kernelSize[1];
                }


                //once the entire image has been iterated over break the loop
                if (finished)
                {
                    break;
                }
            }

            //the loop adds a single character to the end of the image, remove it then set the stringImage
            asciiPicture.Remove(asciiPicture.Length - 1, 1);
            stringImage = asciiPicture.ToString();
        }


        double AveragePixel(int inputRed, int inputGreen, int inputBlue)
        //finds average pixel value of the integers passed and returns its normal value: should be used to find gray value of a pixel (ex. AveragePixel(grayImage.GetPixel(x, y).R + grayImage.GetPixel(x, y).G + grayImage.GetPixel(x, y).B))
        {
            return (inputRed + inputGreen + inputBlue) / 3.0 / 255.0;
        }
        double AveragePixel(Color inputColor)
        //finds average pixel value of the integers passed and returns its normal value: should be used to find gray value of a pixel (ex. AveragePixel(grayImage.GetPixel(x,y))
        {
            double r, g, b;
            r = inputColor.R + .01;
            g = inputColor.G + .01;
            b = inputColor.B + .01;

            return (r + b + g) / 3.01 / 255.0;
        }
        double AverageColor(List<Color> ListOfColors)
        //finds average of a list of colors and normalizes it
        {
            double ColorAVG = 0.0;
            int colorCounter = 0;
            foreach (Color x in ListOfColors)
            {
                ColorAVG += (double)AveragePixel(x);
                colorCounter++;
            }
            return (ColorAVG / (colorCounter + .001)) / 255.0;
        }
        public string GrayToString(double GrayNormalizer)
        //takes a normalized gray and convert it to its ascii symbol
        {
            if (GrayNormalizer < .16) { return charsByScale[0].ToString(); }
            else if (GrayNormalizer >= .16 && GrayNormalizer < .28) { return charsByScale[1].ToString(); }
            else if (GrayNormalizer >= .28 && GrayNormalizer < .53) { return charsByScale[2].ToString(); }
            else if (GrayNormalizer >= .53 && GrayNormalizer < .68) { return charsByScale[3].ToString(); }
            else if (GrayNormalizer >= .68 && GrayNormalizer < .88) { return charsByScale[4].ToString(); }
            else { return charsByScale[5].ToString(); }
        }
        public char GrayToChar(double GrayNormalizer)
        //takes a normalized gray and convert it to its ascii symbol
        {
            if (GrayNormalizer < .16) { return charsByScale[0]; }
            else if (GrayNormalizer >= .16 && GrayNormalizer < .28) { return charsByScale[1]; }
            else if (GrayNormalizer >= .28 && GrayNormalizer < .53) { return charsByScale[2]; }
            else if (GrayNormalizer >= .53 && GrayNormalizer < .68) { return charsByScale[3]; }
            else if (GrayNormalizer >= .68 && GrayNormalizer < .88) { return charsByScale[4]; }
            else { return charsByScale[5]; }
        }
        public override string ToString()
        //returns asciiImage ~ the full uploaded image created by charsByScale 
        {
            return stringImage;
        }
        double AverageNormal(List<double> lst_Norms)
        //accepts a list of doubles and returns the average
        {
            double Sum = 0.0;
            int count = 0;
            foreach (double x in lst_Norms)
            {
                Sum = Sum + x;
                count++;
            }
            double newCount = count + 0.001;
            return Sum / newCount;

        }
    }
}