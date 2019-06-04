**PhotoshopAscii**

The AsciiPhotoshop application takes an image file and converts the pixels into ascii characters. 

Using the application:
	1st, select an image: 
		To select an image, click the file menu button in the upper-left corner then select import.
			The selected file MUST be an image file type in order to use the program
			Accepted file types:
				.jpg 
				.jpeg
				.png 
				.bmp 
				.tiff 
				.gif (prototypical)
		*The application will take a few seconds to render your image since its reading each pixel individually to ready it for conversion.
	
	2nd, set image options:
		When setting image options, you can make changes to how the image appears. 
			Setting the ranges:
				The application takes into consideration 6 ranges of shade:
					The 6 ranges of shade represent how dark a pixel is with 1 being the darkest and 6 being lightest. (characters with more white space within it are ideal for lighter ranges)
			Setting the kernel:
				You can use the kernel to resize images. A kernel width and height of 1 will display the image at normal size. 
					The kernel width sets the width of the image and the kernel height sets the height.
	3rd, ASCIIFY:
		After setting the options of the image, click the ASCIIFY button to display its string representation in the textbox on the right.
		Give the program a few seconds to replace each pixel with a character (the larger the image, the larger the wait)
		The textbox allows you to zoom-in or out, if needed (pinch method on the mousepad/touchpad or ctrl + mouse wheel)
	
	4th, changing options:
		The loaded image can now be rendered with different options.
			To do this, click the ASCIIFY button after making desired changes.

	5th, changing image:
		To change the target image, select a new image using the same method as step 1. 

	6th, exiting:
		Since AsciiPhotoshop was built as a Windows Form, it follows Windows's generic design pattern. To exit click the X in the upper right hand corner.




AsciiPhotoshop : Coded by TCJ

Special Thanks to my mentor Andrew Stamps, for inspring this project and always pushing me to be better than myself.