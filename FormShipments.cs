using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoBot54
{
    public partial class FormShipments : Form
    {
        public FormShipments()
        {
            InitializeComponent();
            myDataGridView1.Columns.Add("ID", "ID");
            myDataGridView1.Columns.Add("Номер", "Номер");
            myDataGridView1.Columns.Add("Клиент", "Клиент");
            myDataGridView1.Columns.Add("Номер заказа", "Номер заказа");
            myDataGridView1.Columns.Add("Бренд", "Бренд");
            myDataGridView1.Columns.Add("Поставщик", "Поставщик");
            myDataGridView1.Columns.Add("Артикул", "Артикул");
            myDataGridView1.Columns.Add("Цена", "Цена");
            myDataGridView1.Columns.Add("Колво", "Колво");

            myDataGridView2.Columns.Add("№ отгрузки", "№ отгрузки");
            myDataGridView2.Columns.Add("Клиент", "Клиент");
            myDataGridView2.Columns.Add("Колво", "Колво");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void FormShipments_Load(object sender, EventArgs e)
        {
            button2_Click(null, null);
            myDataGridView2.Rows[0].Selected = true;
            myDataGridView2_DoubleClick(null,null);
            myDataGridView2.Columns["Колво"].Width = 30;
            myDataGridView1.Columns["Колво"].Width = 30;
            myDataGridView1.Columns["Цена"].Width = 30;

            this.Text = "Отгрузки за 20.01.2024";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            myDataGridView2.Rows.Clear();
            string sqlExpression = "SELECT DISTINCT ZAKAZ, CLIENT, COUNT(*) from tab_orders group by ZAKAZ ORDER BY ZAKAZ DESC";
            using (var connection = new SQLiteConnection("Data Source=db_autobot.db"))
            {
                connection.Open();

                SQLiteCommand command = new SQLiteCommand(sqlExpression, connection);
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read())   // построчно считываем данные
                        {
                            var f1 = reader.GetValue(0);
                            var f2 = reader.GetValue(1);
                            var f3 = reader.GetValue(2);

                            myDataGridView2.Rows.Add(f1,f2,f3);
 
                        }
                    }
                }
            }
        }

        private void myDataGridView2_DoubleClick(object sender, EventArgs e)
        {
            myDataGridView1.Rows.Clear();
            string sqlExpression = "SELECT * FROM tab_orders WHERE ZAKAZ='" + myDataGridView2.CurrentRow.Cells[0].Value.ToString() + "'";
            using (var connection = new SQLiteConnection("Data Source=db_autobot.db"))
            {
                connection.Open();

                SQLiteCommand command = new SQLiteCommand(sqlExpression, connection);
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read())   // построчно считываем данные
                        {
                            var f1 = reader.GetValue(0);
                            var f2 = reader.GetValue(1);
                            var f3 = reader.GetValue(2);
                            var f4 = reader.GetValue(3);
                            var f5 = reader.GetValue(4);
                            var f6 = reader.GetValue(5);
                            var f7 = reader.GetValue(6);
                            var f8 = reader.GetValue(7);
                            var f9 = reader.GetValue(8);

                            //MessageBox.Show($"{id} \t {name} \t {age}");
                            myDataGridView1.Rows.Add(f1, f2, f3, f4, f5, f6, f7, f8, f9);
                        }
                    }
                }
            }
            Console.Read();
        }
    }
}
