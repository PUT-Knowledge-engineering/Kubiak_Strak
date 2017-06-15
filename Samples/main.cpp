int main()
{
	
	Mat Image;
	Image = imread("1.jpg"); // Œcie¿ka do pliku

	namedWindow("InputImage", CV_WINDOW_NORMAL);
	resizeWindow("InputImage", 669, 500);
	imshow("InputImage",Image);

	Mat GrayImage;
	Mat Temp;
	Image.copyTo(Temp);

	cvtColor(Image, GrayImage, CV_BGR2GRAY);

	Scalar meanValueOfImage = mean(GrayImage);
	cout << "Œrednia jasnoœæ: " << meanValueOfImage.val[0];
	namedWindow("GrayImage", CV_WINDOW_NORMAL);
	resizeWindow("GrayImage", 669, 500);
	imshow("GrayImage", GrayImage);

		Mat SobelX, SobelY, Sobels;
		Sobel(GrayImage, SobelX, -1, 1, 0, 3);
		Sobel(GrayImage, SobelY, -1, 0, 1, 3);

		Sobels = SobelX + SobelY;

		Mat SobelBit;
		bitwise_or(SobelX, SobelY, SobelBit);
		
		namedWindow("SobelBit", CV_WINDOW_NORMAL);
		resizeWindow("SobelBit", 669, 500);
		imshow("SobelBit", SobelBit);


		namedWindow("SobelX", CV_WINDOW_NORMAL);
		resizeWindow("SobelX", 669, 500);
		imshow("SobelX", SobelX);

		namedWindow("SobelY", CV_WINDOW_NORMAL);
		resizeWindow("SobelY", 669, 500);
		imshow("SobelY", SobelY);
		namedWindow("Sobels", CV_WINDOW_NORMAL);
		resizeWindow("Sobels", 669, 500);
		imshow("Sobels", Sobels);



		Mat SobelEqual, SobelEqual1;
		Sobels.copyTo(SobelEqual);
		equalizeHist(SobelBit, SobelEqual);
		

		namedWindow("SobelEqual", CV_WINDOW_NORMAL);
		resizeWindow("SobelEqual", 669, 500);
		imshow("SobelEqual", SobelEqual);


		equalizeHist(Sobels, SobelEqual1);


		namedWindow("SobelEqual1", CV_WINDOW_NORMAL);
		resizeWindow("SobelEqual1", 669, 500);
		imshow("SobelEqual1", SobelEqual1);



		Mat SobelThresh;
		int ThresholdForSobel = 210;
		erode(SobelEqual, SobelThresh, Mat());

		threshold(SobelEqual, SobelThresh, ThresholdForSobel, 255, CV_THRESH_BINARY); //////////////////////
		dilate(SobelThresh, SobelThresh, Mat());
		erode(SobelThresh, SobelThresh, Mat());
		medianBlur(SobelThresh, SobelThresh, 5);


		namedWindow("SobelThresh", CV_WINDOW_NORMAL);
		resizeWindow("SobelThresh", 669, 500);
		imshow("SobelThresh", SobelThresh);


		Mat TempInput = Mat::zeros(Image.size(), CV_8UC1);
		cvtColor(Image, TempInput, CV_BGR2GRAY);

		threshold(TempInput, TempInput, 5, 255, CV_THRESH_BINARY); ////////////////////

		erode(TempInput, TempInput, Mat(),Point(-1,-1),8);
		namedWindow("TempInput", CV_WINDOW_NORMAL);
		resizeWindow("TempInput", 669, 500);
		imshow("TempInput", TempInput);



		Mat VeinsWithoutCircle = Mat::zeros(SobelThresh.size(), CV_8UC1);
		SobelThresh.copyTo(VeinsWithoutCircle, TempInput);
		namedWindow("VeinsWithoutCircle", CV_WINDOW_NORMAL);
		resizeWindow("VeinsWithoutCircle", 669, 500);
		imshow("VeinsWithoutCircle", VeinsWithoutCircle);


		vector<vector<Point>> contours;
		vector<Vec4i> hierarchy;

		findContours(VeinsWithoutCircle, contours, hierarchy, CV_RETR_LIST, CV_CHAIN_APPROX_SIMPLE, Point(0, 0));
		Mat ContoursImage = Mat::zeros(Image.size(), CV_8UC3);

		Scalar color;
		for (int i = 0; i < contours.size(); i++)
		{
			color = Scalar(rand() % 255, rand() % 255, rand() % 255);
			if (contours[i].size() > 15)
				drawContours(ContoursImage, contours, i, color, 1, 8, hierarchy);
		}

		namedWindow("ContoursImage", CV_WINDOW_NORMAL);
		resizeWindow("ContoursImage", 669, 500);
		imshow("ContoursImage", ContoursImage);


		erode(SobelThresh, SobelThresh, Mat(), Point(-1, -1), 3);
		dilate(SobelThresh, SobelThresh, Mat());

	waitKey(0);
	return 0;
}
