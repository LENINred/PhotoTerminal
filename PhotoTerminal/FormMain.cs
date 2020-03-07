using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PhotoTerminal
{
    public partial class FormMain : Form
    {
        public string letter = "none";
        public FormMain()
        {
            InitializeComponent();
            UsbNotification.RegisterUsbDeviceNotification(this.Handle);
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == UsbNotification.WmDevicechange)
            {
                switch ((int)m.WParam)
                {
                    case UsbNotification.DbtDeviceremovecomplete:
                        Usb_DeviceRemoved(); // this is where you do your magic
                        break;
                    case UsbNotification.DbtDevicearrival:
                        Usb_DeviceAdded(); // this is where you do your magic
                        break;
                }
            }
        }

        private void Usb_DeviceAdded()
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();

            foreach (DriveInfo d in allDrives)
            {
                //d.Name;//это буква диска
                //d.VolumeLabel;//метка диска
                //d.DriveFormat;//файловая система
                if(d.DriveType == DriveType.Removable)
                    letter = d.Name;
            }

            buttonGlanPaper.Visible = true;
            buttonMatPaper.Visible = true;
            labelPaperType.Visible = true;
            
            buttonCancelOrder.Visible = true;
        }

        private void Usb_DeviceRemoved()
        {
            buttonGlanPaper.Visible = false;
            buttonMatPaper.Visible = false;
            labelPaperType.Visible = false;
            buttonDeSelAll.Visible = false;
            buttonSelAll.Visible = false;
            buttonCancelOrder.Visible = false;
            buttonDoOrder.Visible = false;
            buttonBack.Visible = false;
        }

        private void buttonGlanPaper_Click(object sender, EventArgs e)
        {
            buttonGlanPaper.Visible = false;
            buttonMatPaper.Visible = false;
            labelPaperType.Visible = false;
            showPaperSizesList();
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"snapshot.txt"))
            {
                file.WriteLine("paper_type");
                file.WriteLine(((Button)sender).Tag);
            }
        }

        private void buttonMatPaper_Click(object sender, EventArgs e)
        {
            buttonGlanPaper.Visible = false;
            buttonMatPaper.Visible = false;
            labelPaperType.Visible = false;
            showPaperSizesList();
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"snapshot.txt"))
            {
                file.WriteLine("paper_type");
                file.WriteLine(((Button)sender).Tag);
            }
        }

        List<int[]> paperSizes = new List<int[]>();
        private void showPaperSizesList()
        {
            paperSizes.Add(new int[] { 9, 13 });
            paperSizes.Add(new int[] { 10, 15 });
            paperSizes.Add(new int[] { 11, 15 });
            paperSizes.Add(new int[] { 13, 18 });
            paperSizes.Add(new int[] { 15, 15 });
            paperSizes.Add(new int[] { 15, 20 });
            paperSizes.Add(new int[] { 15, 21 });
            paperSizes.Add(new int[] { 18, 24 });
            paperSizes.Add(new int[] { 15, 23 });
            paperSizes.Add(new int[] { 20, 30 });
            paperSizes.Add(new int[] { 21, 30 });
            paperSizes.Add(new int[] { 30, 40 });
            paperSizes.Add(new int[] { 30, 45 });
            paperSizes.Add(new int[] { 30, 60 });
            paperSizes.Add(new int[] { 30, 90 });
            for (int i = 0; i < paperSizes.Count; i++)
            {
                Button b = new Button();
                b.Font = new Font(b.Font.FontFamily, 24);
                b.Text = paperSizes.ElementAt(i)[0] + " x " + paperSizes.ElementAt(i)[1];
                b.Tag = paperSizes.ElementAt(i)[0] + ";" + paperSizes.ElementAt(i)[1];
                b.Size = new System.Drawing.Size(200, 80);
                b.Click += B_Click;
                flowLayoutPanelImageSizes.Controls.Add(b);
            }
        }

        private void B_Click(object sender, EventArgs e)
        {
            flowLayoutPanelImageSizes.Dispose();
            buttonDeSelAll.Visible = true;
            buttonSelAll.Visible = true;
            buttonBack.Visible = true;
            buttonDoOrder.Visible = true;
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"snapshot.txt", true))
            {
                file.WriteLine("paper_size");
                file.WriteLine(((Button)sender).Tag.ToString());
            }
            ImageFolders imageFolders = new ImageFolders(this, letter);
        }
    }
}
