using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoBot54
{
    public class MyDataGridView : DataGridView
    {
        public MyDataGridView()
        {
            this.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom);
            this.AllowUserToAddRows = false;
            // и устанавливаем значение true при создании экземпляра класса
            this.DoubleBuffered = true;
            // или с помощью метода SetStyle
            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();
        }
        /*
        public static IEnumerable<DataGridViewRow> GetSelectedRows(this DataGridView source)
        {
            for (int i = source.SelectedRows.Count - 1; i >= 0; i--)
                yield return source.SelectedRows[i];
        }*/

        public void FormatCols()
        {
            try
            {
                if (this.Columns["_RowString"]!=null)
                    this.Columns["_RowString"].Visible = false;
                if (this.Columns["positionID"] != null)
                    this.Columns["positionID"].Visible = false;
                if (this.Columns["Код статуса"] != null)
                    this.Columns["Код статуса"].Visible = false;
            }
            catch { }

            try
            {
                if (this.Columns["ID"] != null)
                    this.Columns["ID"].Width = 70;

                if (this.Columns["Номер"] != null)
                    this.Columns["Номер"].Width = 70;
 
                if (this.Columns["Бренд"] != null)
                    this.Columns["Бренд"].Width = 70;

                if (this.Columns["Артикул"] != null)
                    this.Columns["Артикул"].Width = 90;

                if (this.Columns["Описание"] != null)
                    this.Columns["Описание"].Width = 300;
                 
                if (this.Columns["Код статуса"] != null)
                    this.Columns["Код статуса"].Width = 50;

                if (this.Columns["Колво"] != null)
                    this.Columns["Колво"].Width = 35;
                               
                    
                if (this.Columns["Цена вход"] != null)
                    this.Columns["Цена вход"].Width = 50;
                if (this.Columns["Цена выход"] != null)
                    this.Columns["Цена выход"].Width = 50;

                if (this.Columns["Цена"] != null)
                    this.Columns["Цена"].Width = 50;

                if (this.Columns["Дата"] != null)
                    this.Columns["Дата"].Width = 115;


                if (this.Columns["Сумма"] != null)
                    this.Columns["Сумма"].Width = 70;


                if (this.Columns["Статус"] != null)
                    this.Columns["Статус"].Width = 110;

                if (this.Columns["deadline"] != null)
                    this.Columns["deadline"].Width = 30;
                if (this.Columns["deadlineMax"] != null)
                    this.Columns["deadlineMax"].Width = 30;

                if (this.Columns["Вес"] != null)
                    this.Columns["Вес"].Width = 40;


                //myDataGridView1.Columns["positionID"].Width = 70;
            }
            catch { }
        }
    }
}
