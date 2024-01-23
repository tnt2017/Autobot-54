using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Newtonsoft.Json;
using System.Configuration;
using System.Xml.Linq;
using iTextSharp.text.pdf;
using iTextSharp.text;
using BarcodeLib;
using System.IO;
using System.Drawing.Imaging;
using System.Globalization;
using System.Drawing.Printing;
using BarcodeStandard;
using System.Runtime.Remoting.Messaging;
using System.Runtime.InteropServices.ComTypes;
using System.Diagnostics;
using static System.Net.WebRequestMethods;
using System.Net.Http;
//using PdfViewer;
//using System.Drawing.Image;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Security.Policy;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Web;
using System.Text.Json;
using Microsoft.Office.Interop.Excel;
using Newtonsoft.Json.Linq;
using System.Web.Security;
using iTextSharp.text.pdf.parser;
using System.Web.UI.WebControls;
using static System.Net.Mime.MediaTypeNames;
using com.itextpdf.text.pdf;
using Microsoft.VisualBasic;
using System.Data.SQLite;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace AutoBot54
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static string GET(string Url)
        {
            string myurl = Url;
            System.Net.WebRequest req = System.Net.WebRequest.Create(myurl);
            System.Net.WebResponse resp = req.GetResponse();
            System.IO.Stream stream = resp.GetResponseStream();
            System.IO.StreamReader sr = new System.IO.StreamReader(stream);
            string Out = sr.ReadToEnd();
            sr.Close();
            return Out;
        }


        dynamic parsed;

        System.Data.DataTable dtOrders = new System.Data.DataTable();
        System.Data.DataTable dtSales = new System.Data.DataTable();
        System.Data.DataTable dtPositions = new System.Data.DataTable();



        string version = "Autobot54 v0.2 (build 23.01.2024)";

        private void Form1_Load(object sender, EventArgs e)
        {
            checkedListBox1.SetItemChecked(1, true);
            checkedListBox1.SetItemChecked(2, true);
            checkedListBox1.SetItemChecked(3, true);
            checkedListBox1.SetItemChecked(4, true);

            this.Text = version;

            string api = ConfigurationManager.AppSettings["api"];
            string login = ConfigurationManager.AppSettings["login"];
            string pass = ConfigurationManager.AppSettings["pass"];
            textBox1.Text = api;
            textBox6.Text = "userlogin=" + login + "&userpsw=" + pass;

            myDataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtSales.Columns.Add("Номер", typeof(string));
            dtSales.Columns.Add("Поставщик", typeof(string));
            dtSales.Columns.Add("Клиент", typeof(string));
            dtSales.Columns.Add("positionId", typeof(string));
            dtSales.Columns.Add("Статус", typeof(string));
            dtSales.Columns.Add("Код статуса", typeof(string));

            dtSales.Columns.Add("Бренд", typeof(string));
            dtSales.Columns.Add("Артикул", typeof(string));
            dtSales.Columns.Add("Описание", typeof(string));
            dtSales.Columns.Add("Колво", typeof(string));
            dtSales.Columns.Add("Цена", typeof(string));
            dtSales.Columns.Add("_RowString", typeof(string));
            dtSales.Columns.Add("Галка", typeof(Boolean));
            //dtSales.Columns.Add("Дата", typeof(string));

            //   dtSales.Columns.Add("Печать1", typeof(System.Windows.Forms.Button));
            //   dtSales.Columns.Add("Печать2", typeof(System.Windows.Forms.Button));


            dtOrders.Columns.Add("Номер", typeof(string));
            dtOrders.Columns.Add("Дата", typeof(string));
            dtOrders.Columns.Add("Имя", typeof(string));
            dtOrders.Columns.Add("Сумма", typeof(string));

            dtPositions.Columns.Add("ID", typeof(string));


            dtPositions.Columns.Add("Поставщик", typeof(string));
            dtPositions.Columns.Add("Артикул", typeof(string));
            dtPositions.Columns.Add("Описание", typeof(string));


            dtPositions.Columns.Add("Цена вход", typeof(string));
            dtPositions.Columns.Add("Цена выход", typeof(string));
            dtPositions.Columns.Add("Колво", typeof(string));
            dtPositions.Columns.Add("Статус", typeof(string));
            dtPositions.Columns.Add("Вес", typeof(string));


            dtPositions.Columns.Add("deadline", typeof(string));
            dtPositions.Columns.Add("deadlineMax", typeof(string));
            dtPositions.Columns.Add("Сроки", typeof(string)); //поставки

            comboBox1.SelectedIndex = 1;
            button1_Click(null, null);
            //WindowState = WindowState.Maximized;

            button7_Click_2(null, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            parsed = null;

            //for (int stat = 252782; stat < 252785; stat++)
            //{

            string s = GET(textBox1.Text + "cp/orders?" + textBox6.Text + textBox2.Text + "%2000:00:00" + textBox5.Text); //"&statusCode=" + stat.ToString());  ; ;  ; 
                var data1 = s;

                parsed = JsonConvert.DeserializeObject<dynamic>(data1);

                for (int i = 0; i < parsed.Count; i++)
                {
                    //listBox1.Items.Add(parsed[i].number + " " + parsed[i].dateUpdated + " " + parsed[i].userName + " " + parsed[i].sum);
                    dtOrders.Rows.Add(new object[] { parsed[i].number, parsed[i].dateUpdated, parsed[i].userName, parsed[i].sum });
                }
           // }
      
            myDataGridView2.DataSource = dtOrders;
            myDataGridView2.FormatCols();

            dtSales.Clear();
            button2_Click(null, null);

        }


        void FormatColsBills()
        {
            (myDataGridView1.DataSource as System.Data.DataTable).AcceptChanges();
            for (int i = 0; i < myDataGridView1.RowCount; i++)
            {
                try
                {
                    string status = myDataGridView1.Rows[i].Cells["Статус"].Value.ToString();

                    if (status == "Пришло на склад")
                    {
                        myDataGridView1.Rows[i].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#20DBFF");
                    }
                    if (status == "Обработка")
                    {
                        myDataGridView1.Rows[i].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#FDFFA8");
                    }
                    if (status == "В работе")
                    {
                        myDataGridView1.Rows[i].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#FFF979");
                    }
                    if (status == "Ожидает поставки")
                    {
                        myDataGridView1.Rows[i].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#9BFFF1");
                    }
                    if (status == "Заказ отправлен")
                    {
                        myDataGridView1.Rows[i].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#CCFF81");
                    }
                    if (status == "Выдано")
                    {
                        myDataGridView1.Rows[i].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#818181");
                    }
                    if (status == "Отказ")
                    {
                        myDataGridView1.Rows[i].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#FF2011");
                    }
                }
                catch (Exception)
                {
                    //
                }
            }


        }

        Boolean buttons_created = false;


        public static string clean(string s) /* убираем дефисы кавычки итп */
        {
            StringBuilder sb = new StringBuilder(s);
            sb.Replace("-", "");
            sb.Replace("'", "");
            sb.Replace(" ", " ");
            return sb.ToString().ToUpper();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < parsed.Count; i++)
            {
                for (int j = 0; j < parsed[i].positions.Count; j++)
                {
                    //if (parsed[i].number == "157540227")
                    //    MessageBox.Show("111");

                    parsed[i].positions[j].number = clean(parsed[i].positions[j].number.ToString());



                    string sRowString = parsed[i].number + "\t" + parsed[i].positions[j].number + "\t" + parsed[i].positions[j].distributorName + parsed[i].userName + "\t" +
                     "\t" + parsed[i].positions[j].id + "\t" + parsed[i].positions[j].status + "\t" + parsed[i].positions[j].statusCode + "\t" + parsed[i].positions[j].brand + "\t" + parsed[i].positions[j].quantityFinal + "\t" + parsed[i].positions[j].description;


                    string need_stat = comboBox1.Items[comboBox1.SelectedIndex].ToString();

                    DataGridViewButtonColumn uninstallButtonColumn = new DataGridViewButtonColumn();
                    uninstallButtonColumn.Name = "uninstall_column";
                    uninstallButtonColumn.Text = "++";

                    if (need_stat == "Все" || need_stat == "Все нужные")
                    {
                        if (need_stat == "Все")
                        {
                                dtSales.Rows.Add(new object[] { parsed[i].number, parsed[i].positions[j].distributorName, parsed[i].userName,
                                parsed[i].positions[j].id, parsed[i].positions[j].status, parsed[i].positions[j].statusCode,  parsed[i].positions[j].brand, parsed[i].positions[j].number,
                                parsed[i].positions[j].description, parsed[i].positions[j].quantityFinal, parsed[i].positions[j].priceOut, sRowString });

                        }
                        if (need_stat == "Все нужные")
                        {
                            if (parsed[i].positions[j].status != "Получено в заказ" &&
                                parsed[i].positions[j].status != "Обработка" &&
                                parsed[i].positions[j].status != "Возврат" &&
                                parsed[i].positions[j].status != "Выдано" &&
                                parsed[i].positions[j].status != "Отказ" &&
                                parsed[i].positions[j].status != "Пришло на склад")
                            {
                                dtSales.Rows.Add(new object[] { parsed[i].number, parsed[i].positions[j].distributorName, parsed[i].userName,
                                parsed[i].positions[j].id, parsed[i].positions[j].status, parsed[i].positions[j].statusCode,  parsed[i].positions[j].brand, parsed[i].positions[j].number,
                                parsed[i].positions[j].description, parsed[i].positions[j].quantityFinal, parsed[i].positions[j].priceOut, sRowString });
                            }
                        }
                    }
                    else
                    {
                        if (parsed[i].positions[j].status == need_stat)
                        {
                            dtSales.Rows.Add(new object[] {  parsed[i].number, parsed[i].positions[j].distributorName, parsed[i].userName,
                                parsed[i].positions[j].id, parsed[i].positions[j].status, parsed[i].positions[j].statusCode,  parsed[i].positions[j].brand, parsed[i].positions[j].number,
                                parsed[i].positions[j].description, parsed[i].positions[j].quantityFinal, parsed[i].positions[j].priceOut, sRowString });

                        }
                    }

                    //listBox2.Items.Add(parsed[i].number + " -> " + parsed[i].positions[j].number + "   " + parsed[i].positions[j].distributorName +
                    // "   " + parsed[i].positions[j].status + "           brand=" + parsed[i].positions[j].brand + " колво=" + parsed[i].positions[j].quantity);


                    // this.myDataGridView1.Rows.Add(parsed[i].number, parsed[i].positions[j].number, parsed[i].positions[j].distributorName, parsed[i].positions[j].brand, parsed[i].positions[j].status, parsed[i].positions[j].quantity);
                }


            }
            myDataGridView1.DataSource = dtSales;
            myDataGridView1.FormatCols();
            FormatColsBills();


            if (!buttons_created)
            {

                DataGridViewButtonColumn button1 = new DataGridViewButtonColumn();
                {
                    button1.Name = "button";
                    button1.HeaderText = "Button1";
                    button1.Text = "Печать 1шт";
                    button1.UseColumnTextForButtonValue = true; //dont forget this line
                    this.myDataGridView1.Columns.Add(button1);
                }
                DataGridViewButtonColumn button2 = new DataGridViewButtonColumn();
                {
                    button2.Name = "button";
                    button2.HeaderText = "Button2";
                    button2.Text = "Печать несколько";
                    button2.UseColumnTextForButtonValue = true; //dont forget this line
                    this.myDataGridView1.Columns.Add(button2);
                }
                buttons_created = true;
            }

            
            this.Text = version + " - " + myDataGridView2.RowCount + " заказов." + myDataGridView1.RowCount + " позиций";
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

            dtSales.DefaultView.RowFilter = string.Format("[Клиент] LIKE '%{0}%' AND [_RowString] LIKE '%{1}%'", textBox_filter_client.Text, textBox_filter1.Text);
            FormatColsBills();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (myDataGridView1.RowCount > 0)
            {
                button2_Click(null, null);
            }
        }


        void MakePdf(string fname, string client, string brand, string articul, string kolvo, string data)
        {

            int bar_width = int.Parse(ConfigurationManager.AppSettings["bar_width"]);
            int bar_height = int.Parse(ConfigurationManager.AppSettings["bar_height"]);
            int font_size = int.Parse(ConfigurationManager.AppSettings["font_size"]);

            int margin_left = int.Parse(ConfigurationManager.AppSettings["margin_left"]);
            int margin_right = int.Parse(ConfigurationManager.AppSettings["margin_right"]);
            int margin_top = int.Parse(ConfigurationManager.AppSettings["margin_top"]);
            int margin_bottom = int.Parse(ConfigurationManager.AppSettings["margin_bottom"]);

            BarcodeLib.TYPE bar_type = BarcodeLib.TYPE.CODE128B;

            var barcode = new BarcodeLib.Barcode();

            Document document = new Document();
            document.SetMargins(margin_left, margin_right, margin_top, margin_bottom);
            var width = 150;
            var height = 90;
            // Половины от PageSize.A4
            iTextSharp.text.Rectangle _neededSize = new iTextSharp.text.Rectangle(width, height);
            var a4Size = new iTextSharp.text.Rectangle(_neededSize);
            document.SetPageSize(a4Size);
            var fileStream = new FileStream(fname, FileMode.Create, FileAccess.Write, FileShare.None);
            PdfWriter.GetInstance(document, fileStream);
            // Для отображения русских букв
            var baseFont = BaseFont.CreateFont(@"Arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED); //C:\Windows\Fonts\
            var font = new iTextSharp.text.Font(baseFont, font_size);
            document.Open();

            try
            {
                var imageBarcode = barcode.Encode(bar_type, textBox3.Text + ":" + kolvo, Color.Black, Color.White, bar_width, bar_height); //item.Barcode
                var image = iTextSharp.text.Image.GetInstance(imageBarcode, ImageFormat.Jpeg);
                document.Add(new Paragraph("Autobot54", font)); //item.Description
                document.Add(image);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            document.Add(new Paragraph("Клиент: " + client, font)); //Клиент
            document.Add(new Paragraph("Бренд: " + brand, font)); //Бренд
            document.Add(new Paragraph("Артикул: " + articul, font)); //Артикул
            document.Add(new Paragraph("Дата: " + data, font)); //Дата
            document.Add(new Paragraph("Кол-во: " + kolvo, font)); //Кол-во
            document.Close();
            //MessageBox.Show(fname);
        }



        private void button4_Click(object sender, EventArgs e)
        {

        }


        private void button6_Click_1(object sender, EventArgs e)
        {
            string client = "";
            string brand = "";
            string articul = "";
            string kolvo = "";
            string data = "";

            if (myDataGridView1.CurrentRow != null)
            {
                client = myDataGridView1.Rows[myDataGridView1.CurrentRow.Index].Cells["Клиент"].Value.ToString();
                brand = myDataGridView1.Rows[myDataGridView1.CurrentRow.Index].Cells["Бренд"].Value.ToString();
                articul = myDataGridView1.Rows[myDataGridView1.CurrentRow.Index].Cells["Артикул"].Value.ToString();
                kolvo = myDataGridView1.Rows[myDataGridView1.CurrentRow.Index].Cells["Колво"].Value.ToString();
                data = DateTime.Now.ToString();
            }

            int ikolvo = Int32.Parse(myDataGridView1.Rows[myDataGridView1.CurrentRow.Index].Cells["Колво"].Value.ToString());
            //MessageBox.Show("кол-во=" + kolvo.ToString());

            string fname = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\Barcodes.pdf";

            if (radioButton1.Checked)
                MakePdf(fname, client, brand, articul, kolvo, data);
            else
                MakePdf(fname, client, brand, articul, "1", data);


            if (radioButton1.Checked)
            {
                Process p = new Process();
                p.StartInfo = new ProcessStartInfo()
                {
                    CreateNoWindow = true,
                    Verb = "print",
                    FileName = fname, //put the correct path here
                    WorkingDirectory = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath)
                };
                p.Start();
            }
            else
            {
                for (int i = 0; i < ikolvo; i++)
                {
                    Process p = new Process();
                    p.StartInfo = new ProcessStartInfo()
                    {
                        CreateNoWindow = true,
                        Verb = "print",
                        FileName = fname //put the correct path here
                    };
                    p.Start();
                    MessageBox.Show("Печатать следующую ?");
                }
            }

            button5_Click_1(null, null);

            if (checkBox1.Checked)
                button10_Click(null, null);
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (checkBox2.Checked)
                textBox4.Text += e.KeyChar + "";
        }

        void printDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            //  System.Drawing.Point position = new System.Drawing.Point(100, 100);

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {


        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //"&statusCode[0]=252782&statusCode[1]=252783&statusCode[2]=252784&statusCode[3]=252785"//

        }

        private void checkedListBox1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("checkedListBox1_Click");
            string s = "";
            int i = 0;

            foreach (object itemChecked in checkedListBox1.CheckedItems)
            {
                string x = itemChecked.ToString().Substring(0, 6);
                s += "&statusCode[" + i + "]=" + x;
                i++;
            }
            textBox5.Text = s;
        }

        private void myDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            string number = textBox8.Text;
            string positionId = textBox9.Text;

            if (myDataGridView1.CurrentRow != null)
            {
                number = myDataGridView1.Rows[myDataGridView1.CurrentRow.Index].Cells["Номер"].Value.ToString();
                positionId = myDataGridView1.Rows[myDataGridView1.CurrentRow.Index].Cells["positionID"].Value.ToString();
            }

            string url = textBox1.Text + "cp/order/statusHistory?" + textBox6.Text + "&number=" + number + "&positionId=" + positionId;
            string s = GET(url);

            var data1 = s;
            parsed = JsonConvert.DeserializeObject<dynamic>(data1);

            string sout = "";
            for (int i = 0; i < parsed.Count; i++)
            {
                sout += " " + parsed[i].datetime.ToString() + " " + parsed[i].statusCode.ToString() + " " + parsed[i].status.ToString() + " " + parsed[i].managerName.ToString() + "\r\n";
            }

            MessageBox.Show(sout);
        }
        HttpClient client = new HttpClient();



        HttpWebRequest GetRequest(String url, NameValueCollection nameValueCollection)
        {
            // Here we convert the nameValueCollection to POST data.
            // This will only work if nameValueCollection contains some items.
            var parameters = new StringBuilder();

            foreach (string key in nameValueCollection.Keys)
            {
                parameters.AppendFormat("{0}={1}&",
                    HttpUtility.UrlEncode(key),
                    HttpUtility.UrlEncode(nameValueCollection[key]));
            }

            parameters.Length -= 1;

            // Here we create the request and write the POST data to it.
            var request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = "POST";

            using (var writer = new StreamWriter(request.GetRequestStream()))
            {
                writer.Write(parameters.ToString());
            }

            WebResponse wresp = request.GetResponse();
            Stream stream2 = wresp.GetResponseStream();
            StreamReader reader2 = new StreamReader(stream2);
            string s = reader2.ReadToEnd();
            MessageBox.Show(s);

            return request;
        }

        async private void button7_Click_1(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {


        }

        async private void button9_Click(object sender, EventArgs e)
        {
            string number = textBox8.Text;
            string positionId = textBox9.Text;

            string url1 = "https://abcp45206.public.api.abcp.ru/cp/order/?userlogin=api@abcp45206&userpsw=99caccb0fcef552ac97b6059cb8818e4&number=" + number + "&positionId=" + positionId;
            string s = GET(url1);
            var data1 = s;
            string txt = "";
            Order parsed1 = JsonConvert.DeserializeObject<Order>(data1);

            txt += "Email=" + parsed1.userEmail + "\r\n";
            txt += "Примечание=" + parsed1.comment + "\r\n";

            FormOrder f = new FormOrder();
            f.Text = "Заказ #" + parsed1.number;
            f.textBox1.Text = txt;
            f.textBox2.Text = parsed1.number;
            f.textBox3.Text = parsed1.date;
            f.textBox4.Text = parsed1.dateUpdated;
            f.textBox5.Text = parsed1.deliveryAddress;
            f.textBox6.Text = parsed1.userName;
            f.textBox7.Text = parsed1.sum.ToString();
            f.textBox8.Text = parsed1.debt;
            f.textBox9.Text = parsed1.userEmail;

            dtPositions.Rows.Clear();

            float s1 = 0;
            float s2 = 0;

            for (int i = 0; i < parsed1.positions.Length; i++)
            {
                string lineref = parsed1.positions[i].lineReference;
                string sroki = lineref;

                //if (lineref.IndexOf("s:9") > 0)
                //  sroki = lineref.Substring(lineref.IndexOf("s:9"), 20);
                // if (lineref.IndexOf("s:10") > 0)
                //   sroki = lineref.Substring(lineref.IndexOf("s:10"), 14);
                //if (lineref.IndexOf("s:12") > 0)
                //  sroki = lineref.Substring(lineref.IndexOf("s:12"), 50);
                //if (lineref.IndexOf("s:14") > 0)
                //  sroki = lineref.Substring(lineref.IndexOf("s:14"), 14);


                dtPositions.Rows.Add(new object[] { parsed1.positions[i].id, parsed1.positions[i].distributorName, parsed1.positions[i].number ,parsed1.positions[i].description ,
                                                    parsed1.positions[i].priceIn, parsed1.positions[i].priceOut,  parsed1.positions[i].quantity,
                                                    parsed1.positions[i].status, parsed1.positions[i].weight, parsed1.positions[i].deadline, parsed1.positions[i].deadlineMax, sroki });


                s1 += float.Parse(parsed1.positions[i].priceIn.ToString()) * int.Parse(parsed1.positions[i].quantity);
                s2 += float.Parse(parsed1.positions[i].priceOut.ToString()) * int.Parse(parsed1.positions[i].quantity);
            }



            f.label4.Text = "СуммаВход=" + s1.ToString();
            f.label5.Text = "СуммаВыход=" + s2.ToString();
            f.label6.Text = "Разница=" + (s2 - s1).ToString();

            f.myDataGridView1.DataSource = dtPositions;
            f.myDataGridView1.FormatCols();
            f.ShowDialog();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string number = textBox8.Text;
            string positionId = textBox9.Text;

            if (myDataGridView1.CurrentRow != null)
            {
                number = myDataGridView1.Rows[myDataGridView1.CurrentRow.Index].Cells["Номер"].Value.ToString();
                positionId = myDataGridView1.Rows[myDataGridView1.CurrentRow.Index].Cells["positionID"].Value.ToString();
            }


            string status = textBox10.Text; /// 252781 - получен в работу, 252785 - получен на склад

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["userlogin"] = "api@abcp45206";
                data["userpsw"] = "99caccb0fcef552ac97b6059cb8818e4";
                data["order[number]"] = number;
                data["order[positions][0][id]"] = positionId;
                data["order[positions][0][statusCode]"] = status;
                //data["order[positions][0][quantity]"] = "10";
                //data["order[notes][0][value]"] = "текст заметки";

                var response = wb.UploadValues("https://abcp45206.public.api.abcp.ru/cp/order", "POST", data);
                string responseInString = Encoding.UTF8.GetString(response);

                myDataGridView1.Rows.Remove(myDataGridView1.CurrentRow);
                // MessageBox.Show(responseInString);
            }

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            dtSales.DefaultView.RowFilter = string.Format("[Клиент] LIKE '%{0}%' AND [_RowString] LIKE '%{1}%'", textBox_filter_client.Text, textBox_filter1.Text);
            FormatColsBills();
        }

        private void myDataGridView1_Click(object sender, EventArgs e)
        {
            textBox8.Text = myDataGridView1.Rows[myDataGridView1.CurrentRow.Index].Cells["Номер"].Value.ToString();
            textBox9.Text = myDataGridView1.Rows[myDataGridView1.CurrentRow.Index].Cells["positionID"].Value.ToString();
            textBox4.Text = myDataGridView1.Rows[myDataGridView1.CurrentRow.Index].Cells["Артикул"].Value.ToString();
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            FormSborka f = new FormSborka();

            /* System.Data.DataTable dt = new System.Data.DataTable();
                 dt.Merge((myDataGridView1.DataSource) as System.Data.DataTable);
                 dt.Rows.Clear();
                 for (int i = 0; i < myDataGridView1.SelectedRows.Count; i++)
                 {
                     DataRowView selRowView = (myDataGridView1.SelectedRows[i].DataBoundItem as DataRowView);
                     dt.LoadDataRow(selRowView.Row.ItemArray, false);
                 }*/

            System.Data.DataTable dt = (myDataGridView1.DataSource as System.Data.DataTable).GetChanges();
            f.myDataGridView1.DataSource = dt;
            f.ShowDialog();

        }

        private void myDataGridView1_DoubleClick(object sender, EventArgs e)
        {
            //myDataGridView1.

            //if (myDataGridView1.CurrentRow.Cells == 1)
            //{
                button9_Click(null, null);
            //}            
        }

        private void myDataGridView2_DoubleClick(object sender, EventArgs e)
        {
            textBox8.Text = myDataGridView2.Rows[myDataGridView2.CurrentRow.Index].Cells["Номер"].Value.ToString();
            //textBox9.Text = myDataGridView2.Rows[myDataGridView2.CurrentRow.Index].Cells["positionID"].Value.ToString();
            button9_Click(null, null);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("//////////////////////////");
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }

        private void чтоЗаХххххххххххToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void вПоликлиникеToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void вДуркеToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void печатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button9_Click(null, null);
        }

        private void сохрантьКакаЭкселToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAbout f = new FormAbout();
            f.ShowDialog();
        }

        private void отгрузкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormShipments f = new FormShipments();
            f.ShowDialog();
        }

        private void найтиПоIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string ID;
            ID = Interaction.InputBox("Введите ID документа", "ID", "157493227");
            textBox8.Text = ID;
            button9_Click(null, null);
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            this.KeyPreview = true;

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            FormBarcodes f = new FormBarcodes();
            f.ShowDialog();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            if (textBox4.Text != "")
            {
                using (var connection = new SQLiteConnection("Data Source=db_autobot.db"))
                {
                    connection.Open();

                    DateTime dt = DateTime.Now;

                    string query = "INSERT into tab_barcodes values (NULL, '" + textBox_user.Text + "', '" + textBox4.Text + "', '" + dt + "')";
                    // MessageBox.Show(query);
                    try
                    {
                        // Создаем команду для выполнения SQL-запроса
                        using (SQLiteCommand command = new SQLiteCommand(query, connection))
                        {
                            // Выполняем SQL-запрос и получаем результат
                            string result = command.ExecuteScalar()?.ToString();
                            MessageBox.Show("Записали в БД штрихкодов " + textBox4.Text + "=" + textBox3.Text);

                            button7_Click_2(null, null);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Пустой штрихкод");
            }
        }

        private void button7_Click_2(object sender, EventArgs e)
        {
            using (var connection = new SQLiteConnection("Data Source=db_autobot.db"))
            {
                connection.Open();

                string query = "SELECT MAX(ID) from tab_barcodes";
                //MessageBox.Show(query);
                try
                {
                    // Создаем команду для выполнения SQL-запроса
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        // Выполняем SQL-запрос и получаем результат
                        string result = command.ExecuteScalar()?.ToString();
                        // MessageBox.Show(result);

                        textBox3.Text = (Int32.Parse(result) + 1).ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }


        }

        private void штрихкодыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormBarcodes f = new FormBarcodes();
            f.ShowDialog();
        }

        private void myDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 13)
            {
                radioButton1.Checked = true;
                radioButton2.Checked = false;
                button6_Click_1(null, null);
                //MessageBox.Show();
            }
            if (e.ColumnIndex == 14)
            {
                radioButton2.Checked = true;
                radioButton1.Checked = false;
                button6_Click_1(null, null);
            }
        }

        private void textBox_filter1_Click(object sender, EventArgs e)
        {
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new System.Globalization.CultureInfo("en-US"));

        }
    }
}

