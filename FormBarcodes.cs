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
    public partial class FormBarcodes : Form
    {
        public FormBarcodes()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            myDataGridView1.Rows.Clear();
            string sqlExpression = "SELECT * from tab_barcodes ORDER by ID DESC";
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

                            myDataGridView1.Rows.Add(f1, f2, f3, f4);

                        }
                    }
                }
            }
        }

        private void FormBarcodes_Load(object sender, EventArgs e)
        {
            myDataGridView1.Columns.Add("ID", "ID");
            myDataGridView1.Columns.Add("User", "User");
            myDataGridView1.Columns.Add("Артикул", "Артикул");
            myDataGridView1.Columns.Add("Дата", "Дата");

            myDataGridView1.Columns["Дата"].Width= 150;

            button1_Click(null, null);
        }
    }
}
