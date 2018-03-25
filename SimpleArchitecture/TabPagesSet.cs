using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Data;

using Database;
using BusinessLogic;

namespace TabPagesSet
{
   
    // -------------------------------------------------------------
    // Using:
    // http://www.codenet.ru/progr/cpp/dot-net-events.php
    // https://ru.wikipedia.org/wiki/Model-View-Presenter

    
     
// ------------------------------------------------------------
        public class TabPageOfReport : TabPage
        {
            public WebBrowser wb;

            public TabPageOfReport()
            {
                wb = new WebBrowser();
                this.Controls.Add(wb);
                wb.Dock = DockStyle.Fill;
            }

        }

// -----------------------------------------------------------
        public class TabPageOfView : TabPage, IView
    {
            TextBox tb;
            public Button b1, b2;

            public TabPageOfView()
        {
            tb = new TextBox();
            tb.Location = new Point(14, 18);
            //tb.Text = "Table ";
            tb.AutoSize = true;
            this.Controls.Add(tb);

            b1 = new Button(); b1.Text = "Ok";
            b1.Location = new Point(431, 248); b1.Size = new Size(72, 23);
            b1.Click += new System.EventHandler(OkButtonClicked); 
            this.Controls.Add(b1);
            b2 = new Button(); b2.Text = "Cancel";
            b2.Location = new Point(431, 291); b2.Size = new Size(72, 23);
            this.Controls.Add(b2);

        }
      #region Реализация IView
      /// <summary>/// Вывод /// </summary>
      public void SetSalary(decimal value)
      {
         tb.Text = value.ToString();
      }
      /// <summary>/// Ввод нового значения /// </summary>
      public decimal InputSalary
      {
         get { return Convert.ToDecimal(tb.Text); }
      }
      /// <summary>
      /// Событие ввода значения 
      /// </summary>
      // EventHandler<EventArgs> - делегат; он есть определять не надо
      public event EventHandler<EventArgs> SetSalaryViewEvent; // объявляем событие

      #endregion

      /// <summary>
      /// Вызываем событие (событие - фиксируем, на вью обновлены данные)
      /// </summary>
      private void OkButtonClicked(object sender, EventArgs e)
      { // 
          if (SetSalaryViewEvent != null)   //  Перед вызовом мы проверяем, закреплены ли за этими событиями обработчики 
              SetSalaryViewEvent(this, EventArgs.Empty); // вызываем эти события
      }

     }


}
