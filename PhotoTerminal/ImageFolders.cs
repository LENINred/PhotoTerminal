using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace PhotoTerminal
{
    class ImageFolders
    {
        List<string> neededFolders = new List<string>();
        //List<List<string>> imagesInFolder = new List<List<string>>();
        FlowLayoutPanel layoutPanel;
        Form formMain;
        public ImageFolders(Form _formMain, string letter)
        {
            formMain = _formMain;
            layoutPanel = new FlowLayoutPanel();
            _formMain.Controls.Add(layoutPanel);
            layoutPanel.Location = new Point(0, 0);
            layoutPanel.Size = new Size(_formMain.Width, _formMain.Height - 160);
            _formMain.SizeChanged += FormMain_SizeChanged;
            layoutPanel.BorderStyle = BorderStyle.FixedSingle;

            Thread thrA = new Thread(() => TreeScan(letter));
            thrA.Start();
        }

        private void FormMain_SizeChanged(object sender, EventArgs e)
        {
            layoutPanel.Size = new Size(formMain.Width, formMain.Height - 160);
        }

        private void TreeScan(string sDir)
        {
            //List<string> listImg = new List<string>();
            List<Image> cacheImageList = new List<Image>();
            bool emptyFolder = true;

            foreach (string d in Directory.GetDirectories(sDir))
            {
                TreeScan(d);
            }
            var files = Directory.EnumerateFiles(sDir, "*.*");
            
            int j = 0;
            foreach (string fileName in files)
            {
                if ((fileName.ToLower().Contains(".jpg")) || (fileName.ToLower().Contains(".tiff")) || (fileName.ToLower().Contains(".raw")) || (fileName.ToLower().Contains(".bmp")))
                {
                    emptyFolder = false;

                    Directory.CreateDirectory("snapshot");
                    Directory.CreateDirectory("snapshot\\" + sDir.Split(Path.DirectorySeparatorChar).Last());
                    string photoInSnapshot = "snapshot\\" + sDir.Split(Path.DirectorySeparatorChar).Last() + "\\" + Path.GetFileName(fileName);
                    File.Copy(fileName, photoInSnapshot, true);

                    //listImg.Add(photoInSnapshot);

                    if (j < 3)
                    {
                        try
                        {
                            Image thmb = Image.FromFile(fileName).GetThumbnailImage(128, 128, null, new IntPtr(0));
                            cacheImageList.Add(thmb);
                        }
                        catch (OutOfMemoryException)
                        {
                            Debug.Print(fileName);
                        }
                        j++;
                    }
                }
            }
            if (!emptyFolder)
            {
                neededFolders.Add("snapshot\\" + sDir.Split(Path.DirectorySeparatorChar).Last());
                //imagesInFolder.Add(listImg);

                if (Application.OpenForms[0].InvokeRequired)
                {
                    Application.OpenForms[0].Invoke(new Action(() => drawForlderWithPreview(cacheImageList)));
                    return;
                }
                
            }
        }

        private void drawForlderWithPreview(List<Image> cacheImageList)
        {
            PictureBox picture = new PictureBox();
            picture.BackgroundImageLayout = ImageLayout.Stretch;
            Bitmap source1 = new Bitmap(Properties.Resources.Folder);
            List<Bitmap> sources = new List<Bitmap>();

            if (cacheImageList.Count == 3)
            {
                sources.Add((Bitmap)cacheImageList[0]);
                sources.Add((Bitmap)cacheImageList[1]);
                sources.Add((Bitmap)cacheImageList[2]);
            }
            else
            {
                for (int i = 0; i < cacheImageList.Count; i++)
                {
                    sources.Add((Bitmap)cacheImageList[i]);
                    i++;
                }
            }

            picture.Height = source1.Height;
            picture.Width = source1.Width;
            picture.Tag = neededFolders[neededFolders.Count - 1];

            using (Graphics grfx = Graphics.FromImage(source1))
            {
                grfx.DrawString(neededFolders[neededFolders.Count - 1].Split('\\')[neededFolders[neededFolders.Count - 1].Split('\\').Count() - 1], new Font("Arial", 12), new SolidBrush(Color.Black), new Point(128, 32));
                grfx.CompositingMode = CompositingMode.SourceOver;
                int x = 34, y = 54;
                foreach (Bitmap bmp in sources)
                {
                    grfx.DrawImage(bmp, x, y);
                    x += 64;
                    y += 10;
                }
            }

            picture.Image = source1;
            picture.MouseUp += Picture_MouseUp;
            layoutPanel.Controls.Add(picture);
        }

        private void Picture_MouseUp(object sender, MouseEventArgs e)
        {
            layoutPanel.Controls.Clear();
            FolderImages folderImages = new FolderImages(formMain, layoutPanel);
            folderImages.showImages(((PictureBox)sender).Tag.ToString());
        }
    }
}
