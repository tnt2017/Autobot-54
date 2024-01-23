using iTextSharp.xmp.impl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Net;
using System.Collections.Specialized;

namespace AutoBot54
{
    public partial class FormSborka : Form
    {
        public FormSborka()
        {
            InitializeComponent();
        }

        private void FormSborka_Load(object sender, EventArgs e)
        {
            myDataGridView1.FormatCols();
            
            if (myDataGridView1.RowCount > 0)
            {
                try
                {
                myDataGridView1.Columns["Статус"].Visible = false;
                myDataGridView1.Columns["Клиент"].Visible = false;
                myDataGridView1.Columns["Описание"].Width = 150;
                }
                catch   
                { 
            
                }

                if(myDataGridView1.Rows[0].Cells["Клиент"]!=null)
                textBox1.Text = myDataGridView1.Rows[0].Cells["Клиент"].Value.ToString(); ;
            }

            float sum = 0;
            for (int i = 0; i < myDataGridView1.Rows.Count; i++)
            {
                sum += float.Parse(myDataGridView1.Rows[i].Cells["Цена"].Value.ToString()) * int.Parse(myDataGridView1.Rows[i].Cells["Колво"].Value.ToString());
            }

            label4.Text="Сумма = " + sum.ToString();
            DateTime now = DateTime.Now;
 
            textBox4.Text=now.ToString("ddMM_HHmmss");
            this.Text = "Отборочный лист #" + textBox4.Text;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook ExcelWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet ExcelWorkSheet;
            //Книга.
            ExcelWorkBook = ExcelApp.Workbooks.Add(System.Reflection.Missing.Value);
            //Таблица.
            ExcelWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ExcelWorkBook.Worksheets.get_Item(1);

            float sum=0;

            ExcelApp.Cells[1, 1] = "Номер листа";
            ExcelApp.Cells[1, 2] = "20001";
            ExcelApp.Cells[2, 1] = "Клиент";
            ExcelApp.Cells[2, 2] = textBox1.Text;


            ExcelApp.Cells[4, 1] = "Бренд";
            ExcelApp.Cells[4, 2] = "Артикул";
            ExcelApp.Cells[4, 3] = "Описание"; 
            ExcelApp.Cells[4, 4] = "Колво";
            ExcelApp.Cells[4, 5] = "Цена";

            for (int i = 0; i < myDataGridView1.Rows.Count; i++)
            {
                sum += float.Parse(myDataGridView1.Rows[i].Cells["Цена"].Value.ToString()) * int.Parse(myDataGridView1.Rows[i].Cells["Колво"].Value.ToString());

                for (int j = 6; j < myDataGridView1.ColumnCount - 2; j++)
                {
                    ExcelApp.Cells[i + 5, j - 5] = myDataGridView1.Rows[i].Cells[j].Value;
                }
            }

            ExcelApp.Cells[myDataGridView1.Rows.Count + 7, 5] = "Сумма=" + sum.ToString();
            ExcelApp.Columns.AutoFit();

            //Вызываем нашу созданную эксельку.
            ExcelApp.Visible = true;
            ExcelApp.UserControl = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }



        private void button2_Click(object sender, EventArgs e)
        {
            using (var connection = new SQLiteConnection("Data Source=db_autobot.db"))
            {
                connection.Open();

                for (int i = 0; i < myDataGridView1.Rows.Count; i++)
                {
                    //sum += float.Parse(myDataGridView1.Rows[i].Cells["Цена"].Value.ToString()) * int.Parse(myDataGridView1.Rows[i].Cells["Колво"].Value.ToString());
                    

                    string query = "INSERT into tab_orders values (NULL, '" + textBox4.Text + "','" + textBox1.Text 
                                    + "','" + myDataGridView1.Rows[i].Cells["Номер"].Value.ToString()
                                    + "','" + myDataGridView1.Rows[i].Cells["Бренд"].Value.ToString()
                                    + "','" + myDataGridView1.Rows[i].Cells["Поставщик"].Value.ToString()
                                    + "','" + myDataGridView1.Rows[i].Cells["Артикул"].Value.ToString()
                                    + "','" + myDataGridView1.Rows[i].Cells["Цена"].Value.ToString()
                                    + "','" + myDataGridView1.Rows[i].Cells["Колво"].Value.ToString()
                                    + "')";

                   // MessageBox.Show(query);

                    try
                    {
                        // Создаем команду для выполнения SQL-запроса
                        using (SQLiteCommand command = new SQLiteCommand(query, connection))
                        {
                            // Выполняем SQL-запрос и получаем результат
                            string result = command.ExecuteScalar()?.ToString();
                            MessageBox.Show("OK"); 
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            FormShipments f=new FormShipments();
            f.ShowDialog();



        }

        private void button4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < myDataGridView1.Rows.Count; i++)
            {
               // myDataGridView1.CurrentRow.Index++;
                string number = "";
                string positionId = "";

                if (myDataGridView1.CurrentRow != null)
                {
                    number = myDataGridView1.Rows[myDataGridView1.CurrentRow.Index].Cells["Номер"].Value.ToString();
                    positionId = myDataGridView1.Rows[myDataGridView1.CurrentRow.Index].Cells["positionID"].Value.ToString();
                }

                string status = "252786";  //252786 - выдано

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
        }
    }
    
}
