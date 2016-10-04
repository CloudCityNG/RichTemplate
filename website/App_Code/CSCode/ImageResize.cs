using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;



/// <summary>
/// Summary description for thumbnail
/// </summary>
public class Resize
{
    public Resize()
    { }

    #region Functions for Generating Thumb Nails for the Images
    
    public static string FileName()
    {
        string _filename;

        _filename = System.DateTime.Now.Day + "" + System.DateTime.Now.Month + "" + System.DateTime.Now.Year + "" + System.DateTime.Now.Hour + "" + System.DateTime.Now.Minute + "" + System.DateTime.Now.Second + "" + System.DateTime.Now.Millisecond;

        return _filename;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="SourceImgPath">Physical path with file name</param>
    /// <param name="ReqSize">widthxheight e.g. 100x100</param>
    /// <param name="TargetImgPath">Physical path with file name</param>
    /// <param name="brush">Brusgh color e.g. Brushes.Olive</param>
    public static void GenerateThumbnail(string SourceImgPath, string ReqSize, string TargetImgPath, Brush brush)
    {
        try
        {
            string[] sSize = ReqSize.Split('x');
            int height = 0;
            int width = 0;

            if (Int32.TryParse(sSize[0], out width) && Int32.TryParse(sSize[1], out height))
            {
                Resize.GenerateThumbnail(SourceImgPath, width, height, TargetImgPath, brush);
            }
            else
                throw new Exception("Incorrect dimentions");
            
        }
        finally
        {}
    }

    //Function used to create the thumnails 
    /// <summary>
    /// 
    /// </summary>
    /// <param name="SourceImgPath">Physical path with file name</param>
    /// <param name="ReqWidth"></param>
    /// <param name="ReqHeight"></param>
    /// <param name="TargetImgPath">Physical path with file name</param>
    /// <param name="brush">Brusgh color e.g. Brushes.Olive</param>
    public static void GenerateThumbnail(string SourceImgPath, int ReqWidth, int ReqHeight, string TargetImgPath, Brush brush)
    {
        Bitmap loBMP = null;
        Bitmap bmpOut = null;
        try
        {
            loBMP = new Bitmap(SourceImgPath);
            ImageFormat loFormat = loBMP.RawFormat;
            int ImgHeight = 0;
            int ImgWidth = 0;

            GetTargetImageSize(loBMP.Height, loBMP.Width, ReqWidth, ReqHeight, out ImgHeight, out ImgWidth);

            bmpOut = new Bitmap(ImgWidth, ImgHeight);
            Graphics g = Graphics.FromImage(bmpOut);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            if (brush == null)
                brush = Brushes.White;
            

            int iPosX = 0;
            int iPosY = 0;
            
            g.DrawImage(loBMP, iPosX, iPosY, ImgWidth, ImgHeight);


            if (!Directory.Exists(Path.GetDirectoryName(TargetImgPath)))
                Directory.CreateDirectory(Path.GetDirectoryName(TargetImgPath));
            bmpOut.Save(TargetImgPath, ImageFormat.Jpeg);
        }
        finally
        {
            loBMP.Dispose();
            bmpOut.Dispose();
        }
    }

    private static void GetTargetImageSize(int ActualHeight, int ActualWidth,int ReqWidth ,int ReqHeight, out int ImgHeight, out int ImgWidth)
    {
        ImgWidth = ActualWidth;
        ImgHeight = ActualHeight;

        if (ActualHeight > ReqHeight || ActualWidth > ReqWidth)
        {
            if (ActualHeight > ActualWidth)
            {
                ImgWidth = (int)(ReqWidth * ((float)ActualWidth / (float)ActualHeight));
                ImgHeight = ReqHeight;
            }
            else
            {
                ImgHeight = (int)(ReqHeight * ((float)ActualHeight / (float)ActualWidth));
                ImgWidth = ReqWidth;
            }
        }

    }
    #endregion

}
