using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace asciifyPhotoshop
{
    public partial class asciify_Form : Form
    {
        public asciify_Form()
        {
            InitializeComponent();
        }
        Bitmap grayImage;//will store selected img file as a black & white img
        BitmapAscii asciiImage = new BitmapAscii();//BitmapAscii class converts a black and white image into a string of characters resembling the same image
        private void menuImport_Click(object sender, EventArgs e)
            //user imports image file | filter: image files (.jpg, .png, .bmp, .tiff, .gif)
        {
            if (diaImportFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string filePath = diaImportFile.FileName;//name of selected img file
                Bitmap importedImage = new Bitmap(filePath);//load image as a bitmap
                picNormal.Image = importedImage;//show normal image to user in the form | control name: picNormal (picture box control)
                grayImage = (Bitmap)importedImage.Clone();//clone the pics data into grayImage else so it can show a gray image and a color image.. without cloning the gray pic will point to the same data as the original pic in memory | without cloning the picture, the color image will also be show a gray image
                int x = 0;//starting x-position to traverse the bitmap
                int y = 0;//starting y-position to traverse the bitmap
                int intNumberOfTotalPixels = (grayImage.Size.Height * grayImage.Size.Width);//multiplying the width by the height will return the total number of pixels in the bitmap
                int currentPixel = 0;//placeholder for the current pixel being edited within the totalNumberOfPixels
                while (currentPixel < intNumberOfTotalPixels) {//while the current pixel is less than the total

                    //read pixel value
                    //find gray value
                    int grayFinder = (grayImage.GetPixel(x, y).R + grayImage.GetPixel(x, y).G + grayImage.GetPixel(x,y).B) / 3;//the gray value is the average value of the red green and blue values

                    //set gray value
                    grayImage.SetPixel(x, y, Color.FromArgb(grayFinder, grayFinder, grayFinder));//set the current pixel to its gray value

                    //next pixel
                    x++;

                    //when x reaches the end of the row (when x is the same as the width), set it to the beginning of the next row 
                    if(x == grayImage.Width)
                    {
                        x = 0;
                        y++;
                    }
                    currentPixel++;
                }
                picGray.Image = grayImage;//show the gray image to the user in the form | control name: picGray (picture box control)
            }
        }

        private void btnAsciify_Click(object sender, EventArgs e)//user clicks the 'asciify' button | control name: btnAsciify (button control)
        //shows imported picture as selected ascii chars
        {
            //prepare the image for its transformation by passing the characters that will be used to create the image and the kernel size for resizing
            //the (dot)text property of a text box will return a sting by default, concatenate the strings from the textbox controls in the form | control names: txtRange1 , txtRange2 , txtRange3 , txtRange4 , txtRange5 , txtRange6 (textbox controls)
            string sendAsChars =
                txtRange1_Char.Text +
                txtRange2_Char.Text +
                txtRange3_Char.Text +
                txtRange4_Char.Text +
                txtRange5_Char.Text +
                txtRange6_Char.Text;

            //strings are char[] arrays with a null character at the end to inform the program of the end of the string
            //hopefully by setting the scale as chars instead of strings the computer will know its dealing with a single character and in return speed up the process a little
            asciiImage.charsByScale = sendAsChars.ToCharArray();

            //the numeric up down controls return a decimal by default, kernel sizes are integers so the values from the NumericUpDowns must be converted to (casted as) integers | control names: numWidth_dec , numHeight_dec (numeric up down controls)
            asciiImage.kernelSize[0] = (int) numWidth_dec.Value;
            asciiImage.kernelSize[1] = (int) numHeight_dec.Value;

            //display the newly transformed image to the user | control name: rtxShowAscii (rich textbox)
            asciiImage.Asciitize(grayImage);
            rtxShowAscii.Text = asciiImage.ToString();
        }

    }
}
