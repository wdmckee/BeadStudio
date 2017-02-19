using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeadStudio
{
    public partial class Main : Form
    {

        #region enums
        public enum CurrentState { FullSize, Zoomed, Pixelated, ColorMapped, Gridlines  }
        #endregion

        #region fields
        string currentfile;
        Bitmap currentFullSizeBitmap;
        Bitmap current28x28SizeBitmap;
        Bitmap currentPixelatedBitmap;
        Bitmap currentPixelatedColorMappedBitmap;
        Bitmap currentPixelatedColorMappedBitmapWithGridlines;
        Bitmap current10x30ColorBoxImage;

        bool eventInQueue = false;      

        Color[] ColorArray =
               {
            Color.FromArgb(255, 196, 36, 27),  // red
            Color.FromArgb(255, 202, 73, 136),  // pink
            Color.FromArgb(255, 91, 39, 140),  // purple
            Color.FromArgb(255, 12, 25, 104),  // navy blue
            Color.FromArgb(255, 26, 75, 152),  // royal blue
            Color.FromArgb(255, 20, 110, 59),  //dark green
            Color.FromArgb(255, 53, 29, 19),  // brown
            Color.FromArgb(255, 102, 102, 102),  // gray
            Color.FromArgb(255, 0, 0, 0),  // black
            Color.FromArgb(255, 197, 137, 109),  // tan
            Color.FromArgb(255, 148, 130, 190),  // purple
            Color.FromArgb(255, 249, 176, 68),  // orange
            Color.FromArgb(255, 136, 192, 218),  // baby blue
            Color.FromArgb(255, 96, 187, 47),  // light green
            Color.FromArgb(255, 255, 255, 255),  // white
            Color.FromArgb(255, 229, 197, 53),  // yellow
            Color.FromArgb(255, 225, 93, 21),  // ?
            // THESE ARE OUR BASE COLORS
            
        };

        List<int[]> Selection = new List<int[]>();


        #endregion

        #region Properties
        public CurrentState _currentState { get; set; }
        #endregion        

        #region METHODS
        private void GetFullSizeImage(string file)
        {

            if (file != null)
            {
                using (Stream BitmapStream = System.IO.File.Open(file, System.IO.FileMode.Open))
                {
                    Image img = Image.FromStream(BitmapStream);

                    Bitmap mBitmap = new Bitmap(img);
                    //mBitmap = Helpers.ResizeImage(mBitmap, width, height);
                    FileInfo fi = new FileInfo(file);
                    mBitmap.Tag = fi.Name;
                    currentFullSizeBitmap = mBitmap;


                        current28x28SizeBitmap = currentFullSizeBitmap.ResizeImage(28, 28);
                        currentPixelatedBitmap = current28x28SizeBitmap.Pixelate(null, 280, 280);
                        currentPixelatedColorMappedBitmap = current28x28SizeBitmap.Pixelate(ColorArray, 280, 280);
                        currentPixelatedColorMappedBitmapWithGridlines = currentPixelatedColorMappedBitmap.DrawGridlines();
                

                }

            }





        }

        #endregion
        
        #region EVENT CLICKS

        private void pictureBoxImg_Paint(object sender, PaintEventArgs e)
        {


            if (eventInQueue)
            {


                // (1) UNHOOK ALL EVENTS, CLEAR ALL CHECKBOXES
                //checkBox_Resized.CheckedChanged -= new System.EventHandler(this.checkBox_Resized_CheckedChanged);
                checkBox_Pixelated.CheckedChanged -= new System.EventHandler(this.checkBox_Pixelated_CheckedChanged);
                //checkBox_ColorMap.CheckedChanged -= new System.EventHandler(this.checkBox_ColorMap_CheckedChanged);
                checkBox_Gridlines.CheckedChanged -= new System.EventHandler(this.checkBox_Gridlines_CheckedChanged);

                //checkBox_Resized.Checked = false;
                checkBox_Pixelated.Checked = false;
                //checkBox_ColorMap.Checked = false;
                checkBox_Gridlines.Checked = false;



                // (2) DO OUR LOGIC
                if (_currentState == CurrentState.Zoomed)
                {
                    pictureBoxImg.Image = current28x28SizeBitmap;
                    //checkBox_Resized.Checked = true;
                }
                else if (_currentState == CurrentState.Pixelated)
                {
                    pictureBoxImg.Image = currentPixelatedBitmap;
                    checkBox_Pixelated.Checked = true;
                }
                else if (_currentState == CurrentState.ColorMapped)
                {
                    pictureBoxImg.Image = currentPixelatedColorMappedBitmap;
                    //checkBox_ColorMap.Checked = true;
                }
                else if (_currentState == CurrentState.Gridlines)
                {
                    pictureBoxImg.Image = currentPixelatedColorMappedBitmapWithGridlines;
                    checkBox_Gridlines.Checked = true;
                }
                else
                {
                    pictureBoxImg.Image = currentFullSizeBitmap;
                }


                // (3) ENABLE ALL EVENTS AGAIN
                //checkBox_Resized.CheckedChanged += new System.EventHandler(this.checkBox_Resized_CheckedChanged);
                checkBox_Pixelated.CheckedChanged += new System.EventHandler(this.checkBox_Pixelated_CheckedChanged);
                //checkBox_ColorMap.CheckedChanged += new System.EventHandler(this.checkBox_ColorMap_CheckedChanged);
                checkBox_Gridlines.CheckedChanged += new System.EventHandler(this.checkBox_Gridlines_CheckedChanged);

                eventInQueue = false;


            }

        }
        private void Load_Picture_Click(object sender, EventArgs e)
        {

            using (var openFileDialog = new OpenFileDialog())
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    currentfile = openFileDialog.FileName;
                    GetFullSizeImage(currentfile);
                }
            }
            pictureBoxImg.Image = currentFullSizeBitmap;
            pictureBoxImg.SizeMode = PictureBoxSizeMode.StretchImage;
           
        }
        private void FillColorBox(Color[] colorArray)
        {
            var tempColorArray = colorArray;  // holds the colors we have already filled
            // NOTE: X & Y REVERSED

            var mBitmap = new Bitmap(9, 30); // currently we support up to 300 colors
            for (int _y = 0; _y < mBitmap.Height; _y++)
            {
                for (int _x = 0; _x < mBitmap.Width; _x++)
                {
                    if (tempColorArray.Length > 0)
                    { 
                        var pixel_x = (_x == 0 ? _x : _x - 1); // these allow us to get the pixel we just set 
                        pixel_x = (_x == 0 && _y > 0 ? 8 : pixel_x); // did we drop to the next line?
                        var pixel_y = (_x == 0 && _y > 0 ? _y-1 : _y);

                        Color originalColor = mBitmap.GetPixel(pixel_x, pixel_y);
                        var newColor = originalColor.GetClosestColor(tempColorArray);
                        tempColorArray = tempColorArray.Where(c => c != newColor).ToArray();  // removes color from array so we don't use it again
                        mBitmap.SetPixel(_x, _y, newColor);
                       
                    }
                    else
                    {
                        mBitmap.SetPixel(_x, _y, Color.Transparent);   //splitContainer1.BackColor                                            
                    }
                }
             
            }

            current10x30ColorBoxImage = mBitmap;
            pictureBox_Colors.Image = current10x30ColorBoxImage.Pixelate(colorArray, 180, 600);
            //pictureBox_Colors.SizeMode = PictureBoxSizeMode.AutoSize;
        }


        private void checkBox_Resized_CheckedChanged(object sender, EventArgs e)
        {
            // THIS METHOD IS NO LONGER BEING USED ON THE UI. JUST TOO CONFUSING TO A USER.
            /*
            if (checkBox_Resized.Checked)
            {
                _currentState = CurrentState.Zoomed;
            }
            else
            {
                _currentState = CurrentState.FullSize;
            }
            eventInQueue = true;
            this.Refresh();
           */
        }
        private void checkBox_Pixelated_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_Pixelated.Checked)
            {
                _currentState = CurrentState.Pixelated;
            }
            else
            {
                _currentState = CurrentState.FullSize;
            }
            eventInQueue = true;
            this.Refresh();
           
        }
        private void checkBox_ColorMap_CheckedChanged(object sender, EventArgs e)
        {
            // THIS METHOD HAS BEEN MERGED WITH checkBox_Gridlines_CheckedChanged AND IS NOT USED ON THE UI
            /*
            if (checkBox_ColorMap.Checked)
            {
                _currentState = CurrentState.ColorMapped;
            }
            else
            {
                _currentState = CurrentState.FullSize;
            }
            eventInQueue = true;
            this.Refresh();
           */
        }
        private void checkBox_Gridlines_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_Gridlines.Checked)
            {
                _currentState = CurrentState.Gridlines;
            }
            else
            {
                _currentState = CurrentState.FullSize;
            }
            eventInQueue = true;
            this.Refresh();
        }
       


        private void print_btn_Click(object sender, EventArgs e)
        {

            System.Drawing.Printing.PrintDocument myPrintDocument1 = new System.Drawing.Printing.PrintDocument();

            PrintDialog myPrinDialog1 = new PrintDialog();

            myPrintDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(printDocument_PrintPage);

            myPrinDialog1.Document = myPrintDocument1;

            if (myPrinDialog1.ShowDialog() == DialogResult.OK)
            {
                myPrintDocument1.Print();
            }



        }
        private void printDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(pictureBoxImg.Image, 0, 0);

        }





        private void pictureBox_Colors_Click(object sender, EventArgs e)
        {
            //var openFileDialog = new OpenFileDialog();
            //openFileDialog.Filter = "PALLET File (.pallet)|*.pallet";

            //using (openFileDialog)
            //{
            //    if (openFileDialog.ShowDialog() == DialogResult.OK)
            //    {
            //        var file = openFileDialog.FileName;
                   
            //        var lines = File.ReadAllLines(file).Select(a => a.Split(',')).ToArray();

            //        Color[] _ColorArray = new Color[lines.Count()+1]; // plus 1 since we alway sadd a transparent color
        
            //        for (int i = 0; i < lines.Count(); i++)
            //        {

            //            int a = int.Parse(lines[i][0]);
            //            int r = int.Parse(lines[i][1]);
            //            int g = int.Parse(lines[i][2]);
            //            int b = int.Parse(lines[i][3]);

                        
            //            _ColorArray[i] = Color.FromArgb(a, r, g, b);
            //        }
            //        _ColorArray[lines.Count()] = Color.Transparent;

            //        ColorArray = _ColorArray;
            //        FillColorBox(ColorArray);

            //    }
            //}

            //GetFullSizeImage(currentfile);
            //eventInQueue = true;
            //this.Refresh();
            //// reload & set image data

            //// HERE WE WOULD LOAD AND PROCESS A COLOR FILE
        }
        private void pictureBox_Colors_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PALLET File (.pallet)|*.pallet";

            using (openFileDialog)
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var file = openFileDialog.FileName;

                    var lines = File.ReadAllLines(file).Select(a => a.Split(',')).ToArray();

                    Color[] _ColorArray = new Color[lines.Count() + 1]; // plus 1 since we alway sadd a transparent color

                    for (int i = 0; i < lines.Count(); i++)
                    {

                        int a = int.Parse(lines[i][0]);
                        int r = int.Parse(lines[i][1]);
                        int g = int.Parse(lines[i][2]);
                        int b = int.Parse(lines[i][3]);


                        _ColorArray[i] = Color.FromArgb(a, r, g, b);
                    }
                    _ColorArray[lines.Count()] = Color.Transparent;

                    ColorArray = _ColorArray;
                    FillColorBox(ColorArray);

                }
            }

            GetFullSizeImage(currentfile);
            eventInQueue = true;
            this.Refresh();
            // reload & set image data

            // HERE WE WOULD LOAD AND PROCESS A COLOR FILE
        }
        private void pictureBoxImg_Click(object sender, EventArgs e)
        {
            //// TO BE IMPLEMENTED VIA DRAG AND DROP
            //if (_currentState == CurrentState.Gridlines)
            //{
            //    MouseEventArgs me = (MouseEventArgs)e;
            //    Point coordinates = me.Location;

            //    var ratio_x = (double)((double)28 / pictureBoxImg.Size.Width);
            //    var small_x = (ratio_x * coordinates.X);


            //    var ratio_y = (double)((double)28 / pictureBoxImg.Size.Height);
            //    var small_y = (ratio_y * coordinates.Y);

            //    var _x = (int) Math.Truncate(small_x);
            //    var _y = (int) Math.Truncate(small_y);

            //    Color _Color = current28x28SizeBitmap.GetPixel(_x, _y);
            //}
        }
        private void pictureBoxImg_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && Control.ModifierKeys == Keys.Shift)
            {
                MouseEventArgs me = (MouseEventArgs)e;
                Point coordinates = me.Location;


                var ratio_x = (double)((double)28 / pictureBoxImg.Size.Width);
                var small_x = (ratio_x * coordinates.X);


                var ratio_y = (double)((double)28 / pictureBoxImg.Size.Height);
                var small_y = (ratio_y * coordinates.Y);

                var _x = (int)Math.Truncate(small_x);
                var _y = (int)Math.Truncate(small_y);

                Selection.Add(new int[] { _x,_y});


                // MARK PIXEL GRAY FOR NOW
                // don't like having to do this all over again. Need a function for this.
                current28x28SizeBitmap.SetPixel(_x, _y, Color.Gray);
                currentPixelatedBitmap = current28x28SizeBitmap.Pixelate(null, 280, 280);
                currentPixelatedColorMappedBitmap = current28x28SizeBitmap.Pixelate(ColorArray, 280, 280);
                currentPixelatedColorMappedBitmapWithGridlines = currentPixelatedColorMappedBitmap.DrawGridlines();

                eventInQueue = true;
                this.Refresh();




            }
        }

        // DRAG N DROP AREA
        private void pictureBox_Colors_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBoxImg.AllowDrop = true;
            // TO BE IMPLEMENTED VIA DRAG AND DROP
            if (_currentState == CurrentState.Gridlines)
            {
                MouseEventArgs me = (MouseEventArgs)e;
                Point coordinates = me.Location;

                

                var ratio_x = (double)((double)9 / pictureBox_Colors.Size.Width);
                var small_x = (ratio_x * coordinates.X);


                var ratio_y = (double)((double)30 / pictureBox_Colors.Size.Height);
                var small_y = (ratio_y * coordinates.Y);

                var _x = (int)Math.Truncate(small_x);
                var _y = (int)Math.Truncate(small_y);

                 Color _Color = current10x30ColorBoxImage.GetPixel(_x, _y);


                if (e.Button == MouseButtons.Left && Selection.Count() == 0)
                {
                    pictureBox_Colors.DoDragDrop(string.Format("{0},{1},{2},{3}", _Color.A, _Color.R, _Color.G, _Color.B), DragDropEffects.All);
                }
                else
                {
                    foreach (var points in Selection)
                    {
                        // don't like having to do this all over again. Need a function for this.
                        current28x28SizeBitmap.SetPixel(points[0], points[1], _Color);
                     
                    }
                    Selection.Clear();
                    currentPixelatedBitmap = current28x28SizeBitmap.Pixelate(null, 280, 280);
                    currentPixelatedColorMappedBitmap = current28x28SizeBitmap.Pixelate(ColorArray, 280, 280);
                    currentPixelatedColorMappedBitmapWithGridlines = currentPixelatedColorMappedBitmap.DrawGridlines();
                    eventInQueue = true;
                    this.Refresh();

                }
                



            }



           








        }
        private void pictureBoxImg_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;

        }
        private void pictureBoxImg_DragDrop(object sender, DragEventArgs e)
        {

            string points = e.Data.GetData(DataFormats.Text).ToString();
            string[] items = points.Split(',');

            int a = int.Parse(items[0]);
            int r = int.Parse(items[1]);
            int g = int.Parse(items[2]);
            int b = int.Parse(items[3]);


           var _newColor = Color.FromArgb(a, r, g, b);
            // AT THIS POINT WE HAVE TRANSFERRED OUR COLOR OVER
            // NOW TIME TO GET THE DESTINATION PIXEL


            //MouseEventArgs me = (MouseEventArgs)e;
            Point clientPoint = pictureBoxImg.PointToClient(new Point(e.X, e.Y));
            Point coordinates = clientPoint;

            var ratio_x = (double)((double)28 / pictureBoxImg.Size.Width);
            var small_x = (ratio_x * coordinates.X);


            var ratio_y = (double)((double)28 / pictureBoxImg.Size.Height);
            var small_y = (ratio_y * coordinates.Y);

            var _x = (int)Math.Truncate(small_x);
            var _y = (int)Math.Truncate(small_y);

            Color _Color = current28x28SizeBitmap.GetPixel(_x, _y);



            // don't like having to do this all over again. Need a function for this.
            current28x28SizeBitmap.SetPixel(_x, _y, _newColor);
            currentPixelatedBitmap = current28x28SizeBitmap.Pixelate(null, 280, 280);
            currentPixelatedColorMappedBitmap = current28x28SizeBitmap.Pixelate(ColorArray, 280, 280);
            currentPixelatedColorMappedBitmapWithGridlines = currentPixelatedColorMappedBitmap.DrawGridlines();
            
            eventInQueue = true;
            this.Refresh();









        }
        // END DRAG N DROP


        private void btn_Save_Click(object sender, EventArgs e)
        {


            //using (var fbd = new FolderBrowserDialog())
            //{
            //    DialogResult result = fbd.ShowDialog();

            //    if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
            //    {
                    current28x28SizeBitmap.Save(currentfile);
                    
            //    }
            //}




        }

        #endregion



        public Main()
        {
            InitializeComponent();
            this.Width = 700;
            this.Height = 625;

            System.Reflection.Assembly _Assembly = System.Reflection.Assembly.GetExecutingAssembly();
            var a = _Assembly.GetManifestResourceNames();
            Stream str = _Assembly.GetManifestResourceStream("BeadStudio.logo_app3.png");
            pictureBoxImg.Image = new Bitmap(str);
            pictureBoxImg.SizeMode = PictureBoxSizeMode.StretchImage;

            FillColorBox(ColorArray);
        }

       
    }




    static class Helpers
    {

        public static Bitmap ResizeImage(this Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        public static Bitmap Pixelate(this Image image, Color[] colorArray, int width, int height)
        {
            //currentPixelatedBitmap
            Bitmap mBitmap = (Bitmap)image;


            Bitmap PixelatedBitmap = new Bitmap(width, height);  //280, 280




            for (int x = 0; x < mBitmap.Width; x++)
            {
                for (int y = 0; y < mBitmap.Height; y++)
                {

                    //get the pixel from the original image
                    Color originalColor = mBitmap.GetPixel(x, y);

                    if (colorArray != null) // if we pass in a color array, use it!
                    {
                        originalColor = originalColor.GetClosestColor(colorArray);
                    }

                    for (int xx = (x * 10); xx < (x * 10) + 10; xx++)
                    {
                        for (int yy = (y * 10); yy < (y * 10) + 10; yy++)
                        {
                            PixelatedBitmap.SetPixel(xx, yy, originalColor);
                        }

                    }
                    //expandY

                }
            }



            return PixelatedBitmap;


        }

        public static Bitmap DrawGridlines(this Image image)
        {

            Bitmap mBitmap = (Bitmap)image;
            Bitmap mBitmapReturn = new Bitmap(mBitmap);


            Color originalColor = Color.LightGray;

            for (int x = 0; x < mBitmap.Width; x++)
            {
                for (int y = 0; y < mBitmap.Height; y++)
                {
                    if (x % 10 == 0 || y % 10 == 0) 
                        mBitmapReturn.SetPixel(x, y, originalColor);
                }
            }

            
            return mBitmapReturn;
        }

        public static Color GetClosestColor(this Color baseColor ,Color[] colorArray)
        {
            var colors = colorArray.Select(x => new { Value = x, Diff = GetDiff(x, baseColor) }).ToList();
            var min = colors.Min(x => x.Diff);
            return colors.Find(x => x.Diff == min).Value;
        }

        public static int GetDiff(Color color, Color baseColor)
        {
            int a = color.A - baseColor.A,
                r = color.R - baseColor.R,
                g = color.G - baseColor.G,
                b = color.B - baseColor.B;
            return a * a + r * r + g * g + b * b;
        }

       



    }




    }
