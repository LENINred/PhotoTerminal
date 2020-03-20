using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PhotoTerminal
{
    public partial class FormMain : Form
    {
        public string letter = "none";
        int orderNum;
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
            loadPaperSizes();
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
            loadPaperSizes();
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"snapshot.txt"))
            {
                file.WriteLine("paper_type");
                file.WriteLine(((Button)sender).Tag);
            }
        }

        List<int[]> paperSizes = new List<int[]>();
        private void loadPaperSizes()
        {
            DataTable tblOrders = new DataTable();
            using (var mySqlConnection = new DBUtils().getDBConnection())
            {
                mySqlConnection.Open();
                using (var cmd = new MySqlCommand("get_paper_sizes", mySqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                paperSizes.Add(new int[] { Int32.Parse(reader.GetString(1).Split(';')[0]), Int32.Parse(reader.GetString(1).Split(';')[1]) });
                            }
                        }
                    }
                }
            }
            showPaperSizesList();
        }

        private void showPaperSizesList()
        {
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

        string FIO = "", phone = "";
        private void buttonDoOrder_Click(object sender, EventArgs e)
        {
            using (FormOrderData dataForm = new FormOrderData())
            {
                DialogResult dialogResult = dataForm.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    FIO = dataForm.textBoxFIO.Text;
                    phone = dataForm.textBoxPhone.Text;
                    if ((FIO.Length > 5) && (phone.Length == 10))
                    {
                        addNewOrderToCRM(FIO, phone);
                        makeFileForEZ();
                        dataForm.Close();
                        copyFilesToServer();
                    }
                    else
                        MessageBox.Show("Заполните поля правильно");
                }
                if (dialogResult == DialogResult.Cancel)
                {
                    dataForm.Close();
                }
            }
        }

        private void copyFilesToServer()
        {
            string[] selectedFiles = File.ReadAllLines(@"selectedImages.txt");
            foreach(string file in selectedFiles)
            {
                File.Copy(file, "\\192.168.1.1\\folder\\order_" + orderNum + "\\"+ Path.GetFileName(file));
                progressBarUpload.Value += 100 / selectedFiles.Count();
            }
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"check.txt", true))
            {
                file.WriteLine("Фамилия: " + FIO);
                file.WriteLine("Номер телефона: " + phone);
                file.WriteLine("Сумма: " + price + " р.");
            }
            PrintDocument printD = new PrintDocument();
            printD.DocumentName = "check.txt";
            printD.Print();
            MessageBox.Show("Заказ сформирован и отправлен в работу, заберите квитанцию, пройдите для оплаты");
        }

        private void makeFileForEZ()
        {
            string[] selectedFiles = File.ReadAllLines(@"selectedImages.txt");
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"images("+ selectedFiles .Count()+ ").mrk", true))
            {
                file.WriteLine("[HDR]");
                file.WriteLine("GEN REV=01.00");
                file.WriteLine("GEN CRT=\"Reciever\"");
                file.WriteLine("GEN DTM=" + DateTime.Now.ToString("yyyy’:‘MM’:‘dd’T’HH’:’mm’:’ss"));
                file.WriteLine();
                foreach (string image in selectedFiles)
                {
                    file.WriteLine("PRT TYP=STD");
                    file.WriteLine("PRT QTY=1");
                    file.WriteLine("<IMG SCR=\"" + image + "\">");
                    file.WriteLine();
                }
            }
        }

        private int getPrice(string paperSize)
        {
            switch (paperSize)
            {
                case "10;15":
                    return 12;
                case "11;15":
                    return 13;
                case "13;18":
                    return 16;
                case "15;15":
                    return 18;
                case "15;20":
                    return 23;
                case "15;21":
                    return 24;
                case "15;23":
                    return 26;
                case "20;30":
                    return 55;
                case "20;40":
                    return 70;
                case "20;60":
                    return 90;
                case "21;30":
                    return 55;
                case "9;13":
                    return 13;
                default:
                    return 25;
            }
        }

        int price;
        private void addNewOrderToCRM(string custName, string phone)
        {
            orderNum = getLastOrderID();
            List<string> fileLines = File.ReadAllLines("snapshot.txt").ToList();
            string paperSize = fileLines.ElementAt(fileLines.IndexOf("paper_size") + 1);
            price = getPrice(paperSize);
            
            using (var mySqlConnection = new DBUtils().getDBConnection())
            {
                using (var cmd = new MySqlCommand())
                {
                    cmd.Connection = mySqlConnection;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "add_new_order";
                    cmd.Parameters.Clear();
                    MySqlParameter p1 = cmd.Parameters.Add("@customer", MySqlDbType.VarChar);
                    p1.Direction = ParameterDirection.Input;
                    MySqlParameter p2 = cmd.Parameters.Add("@order_info", MySqlDbType.VarChar);
                    p2.Direction = ParameterDirection.Input;
                    MySqlParameter p3 = cmd.Parameters.Add("@order_status", MySqlDbType.VarChar);
                    p3.Direction = ParameterDirection.Input;
                    MySqlParameter p4 = cmd.Parameters.Add("@order_type", MySqlDbType.VarChar);
                    p4.Direction = ParameterDirection.Input;
                    MySqlParameter p5 = cmd.Parameters.Add("@executor", MySqlDbType.VarChar);
                    p5.Direction = ParameterDirection.Input;
                    MySqlParameter p6 = cmd.Parameters.Add("@acceptor", MySqlDbType.VarChar);
                    p6.Direction = ParameterDirection.Input;
                    MySqlParameter p7 = cmd.Parameters.Add("@cost", MySqlDbType.VarChar);
                    p7.Direction = ParameterDirection.Input;
                    MySqlParameter p8 = cmd.Parameters.Add("@fact_cost", MySqlDbType.VarChar);
                    p8.Direction = ParameterDirection.Input;
                    MySqlParameter p9 = cmd.Parameters.Add("@communication", MySqlDbType.VarChar);
                    p9.Direction = ParameterDirection.Input;
                    MySqlParameter p10 = cmd.Parameters.Add("@subCommunication", MySqlDbType.VarChar);
                    p10.Direction = ParameterDirection.Input;
                    MySqlParameter p11 = cmd.Parameters.Add("@orderId", MySqlDbType.Int32);
                    p11.Direction = ParameterDirection.Input;
                    MySqlParameter p12 = cmd.Parameters.Add("@deadline", MySqlDbType.VarChar);
                    p12.Direction = ParameterDirection.Input;
                    MySqlParameter p13 = cmd.Parameters.Add("@point", MySqlDbType.VarChar);
                    p13.Direction = ParameterDirection.Input;
                    MySqlParameter p14 = cmd.Parameters.Add("@cust_notif", MySqlDbType.Int16);
                    p14.Direction = ParameterDirection.Input;

                    p1.Value = custName.TrimStart();
                    p2.Value = "Распечатать фото\n" + File.ReadAllLines(@"selectedImages.txt").Count() + "штук\nФормата: " + paperSize;
                    p3.Value = "Принят";
                    p4.Value = "Печать фотографий";
                    p5.Value = "ФотоТерминал";
                    p6.Value = "ФотоТерминал";
                    p7.Value = price.ToString();
                    p8.Value = "0";
                    p9.Value = phone;
                    p10.Value = "-";
                    p11.Value = orderNum;
                    p12.Value = DateTime.Now.ToString("dd,MM,yyyy HH:mm");
                    p13.Value = "Центральный рынок";
                    p14.Value = true;
                    mySqlConnection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private int getLastOrderID()
        {
            using (var mySqlConnection = new DBUtils().getDBConnection())
            {
                mySqlConnection.Open();
                using (var cmd = new MySqlCommand("get_last_order_id", mySqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                try
                                {
                                    return reader.GetInt32(0);
                                }
                                catch (System.Data.SqlTypes.SqlNullValueException)
                                {
                                    return 0;
                                }
                            }
                        }
                    }
                }
            }
            return -1;
        }
    }
}
