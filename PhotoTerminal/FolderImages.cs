using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PhotoTerminal
{
    partial class FolderImages : FormMain
    {
        FlowLayoutPanel layoutPanel;
        Form formMain;
        public FolderImages(Form _formMain, FlowLayoutPanel _layoutPanel)
        {
            formMain = _formMain;

            layoutPanel = new FlowLayoutPanel();
            layoutPanel.Name = "layoutPanelImages";
            layoutPanel.BackColor = Color.White;
            layoutPanel.AutoScroll = true;
            formMain.Controls.Add(layoutPanel);
            layoutPanel.Location = new Point(0, 0);
            layoutPanel.Size = new Size(formMain.Width, formMain.Height - 160);
            formMain.SizeChanged += FormMain_SizeChanged;
            layoutPanel.BorderStyle = BorderStyle.FixedSingle;
            layoutPanel.BringToFront();

            Button buttonSelAll = (Button)formMain.Controls.Find("buttonSelAll", true)[0];
            buttonSelAll.Click += ButtonSelAll_Click;
            Button buttonDeSelAll = (Button)formMain.Controls.Find("buttonDeSelAll", true)[0];
            buttonDeSelAll.Click += ButtonDeSelAll_Click;
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"snapshot.txt", true))
            {
                file.WriteLine("selected_images");
            }
            Button buttonBack = (Button)formMain.Controls.Find("buttonBack", true)[0];
            buttonBack.Click += ButtonBack_Click;
        }

        private void FormMain_SizeChanged(object sender, EventArgs e)
        {
            layoutPanel.Size = new Size(formMain.Width, formMain.Height - 160);
        }

        private void ButtonBack_Click(object sender, EventArgs e)
        {
            formMain.Controls.RemoveByKey("layoutPanelImages");
        }

        private void ButtonSelAll_Click(object sender, System.EventArgs e)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"selectedImages.txt", true))
            {
                foreach (string line in imagesInFolder.Keys)
                {
                    file.WriteLine(line);
                }
            }
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"snapshot.txt", true))
            {
                file.WriteLine("selected_folder");
                file.WriteLine(selFolder);
            }
        }

        private void ButtonDeSelAll_Click(object sender, System.EventArgs e)
        {
            string filePath = @"selectedImages.txt";
            List<string> fileLines = File.ReadAllLines(filePath).ToList();
            foreach (string key in imagesInFolder.Keys)
                {
                for (int i = 0; i < fileLines.Count(); i++)
                {
                    if (fileLines[i] == key)
                        fileLines.RemoveAt(i);
                }
            }
            File.WriteAllLines(filePath, fileLines);

            filePath = @"snapshot.txt";
            fileLines = File.ReadAllLines(filePath).ToList();
            for (int i = 0; i < fileLines.Count(); i++)
            {
                if (fileLines[i] == selFolder)
                    fileLines.RemoveAt(i);
            }
            foreach (string key in imagesInFolder.Keys)
            {
                for (int i = 0; i < fileLines.Count(); i++)
                {
                    if (fileLines[i] == key)
                        fileLines.RemoveAt(i);
                }
            }
            File.WriteAllLines(filePath, fileLines);
        }

        Dictionary<string, Image> imagesInFolder = new Dictionary<string, Image>();
        List<string> selectedImages = new List<string>();
        string selFolder = "";
        public void showImages(string path)
        {
            selFolder = path;
            var files = Directory.EnumerateFiles(path, "*.*");
            foreach (string fileName in files)
            {
                if ((fileName.ToLower().Contains(".jpg")) || (fileName.ToLower().Contains(".tiff")) || (fileName.ToLower().Contains(".raw")) || (fileName.ToLower().Contains(".bmp")))
                {
                    PictureBox picture = new PictureBox();
                    picture.BackgroundImageLayout = ImageLayout.Zoom;

                    Image image = Image.FromFile(fileName);
                    imagesInFolder.Add(fileName, image);

                    picture.Height = image.Height;
                    picture.Width = image.Width;
                    picture.Tag = Path.GetFileName(fileName);

                    picture.MouseDown += Picture_MouseDown;
                    picture.MouseUp += Picture_MouseUp;

                    picture.Image = image;
                    picture.SizeMode = PictureBoxSizeMode.Zoom;
                    picture.Size = new Size(250, 300);
                    //picture.Click += Picture_Click;
                    layoutPanel.Controls.Add(picture);
                }
            }
        }

        private void Picture_MouseUp(object sender, MouseEventArgs e)
        {
            buttonUp = true;
        }

        DateTime sw;
        bool buttonUp = false;
        const int holdButtonDuration = 1000;
        private void Picture_MouseDown(object sender, MouseEventArgs e)
        {
            buttonUp = false;
            sw = DateTime.Now;
            while (e.Button == MouseButtons.Left && e.Clicks == 1 && (buttonUp == false && (DateTime.Now - sw).TotalMilliseconds < holdButtonDuration))
                Application.DoEvents();
            if ((DateTime.Now - sw).TotalMilliseconds < holdButtonDuration)
                ShortClick(sender);
            else
                LongClick(sender);
        }

        private void ShortClick(object sender)
        {
            addToSelectedList(layoutPanel.Controls.GetChildIndex(((PictureBox)sender)));
        }

        private void LongClick(object sender)
        {
            FormEditImage editImage = new FormEditImage(((PictureBox)sender).Image, ((PictureBox)sender).Tag.ToString());
            editImage.ShowDialog();
        }

        private void Picture_Click(object sender, EventArgs e)
        {
            addToSelectedList(layoutPanel.Controls.GetChildIndex(((PictureBox)sender)));
        }

        private void addToSelectedList(int index)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"selectedImages.txt", true))
            {
                file.WriteLine(imagesInFolder.Keys.ElementAt(index));
            }
        }
    }
}
