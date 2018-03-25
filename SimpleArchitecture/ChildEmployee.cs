using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms; // msgBox

namespace BusinessLogic
{
    public class OrdinaryEmployee : Employee
    {
        public OrdinaryEmployee(int id, string name, decimal solary)
            : base(id, name, solary)
        {
        }
        public override void doWork()
        {
            MessageBox.Show("Solary = " + this.Salary.ToString());
        }

    }


    // http://martinfowler.com/eaaDev/uiArchs.html
    // http://rsdn.org/article/patterns/modelviewpresenter.xml (in Russin)
    // Model-View-Presenter и сопутствующие паттерны
     public class MvpModel : Employee
   {
         public MvpModel(int id, string name, decimal solary)
             : base(id, name, solary)
         {
         } 
         public override void doWork()
         {
             MessageBox.Show("Solary = "+this.Salary.ToString());
         }

    }

    public interface IView
    {
        /// <summary>/// Вывод /// </summary>
        void SetSalary(decimal value);

        /// <summary>/// Ввод /// </summary>
        decimal InputSalary { get; }

        /// <summary>/// Событие ввода значения /// </summary>
        event EventHandler<EventArgs> SetSalaryViewEvent;

    }

    public  class Presenter
   {
      //private Model _model = new Model(1, "noname",0);
        private MvpModel _model;
        private IView _view;

           /// <summary>
           /// В конструтор передается конкретный экземпляр представления
           /// и происходит подписка на все нужные события.
           /// <summary>

        public Presenter(IView view, MvpModel model)
      {
          _model = model;
          _view = view;
          _view.SetSalaryViewEvent += new EventHandler<EventArgs>(OnSetSalary); // назначем обработчик
         //MessageBox.Show(_model.Name);
         RefreshView();
      }

      /// <summary>
      /// Обработка события, установка нового значения для Model
      /// </summary>
      private void OnSetSalary(object sender, EventArgs e)
      {
         _model.Salary = _view.InputSalary;
         MessageBox.Show("OnSetSalary:" + _model.Salary.ToString());

         RefreshView();
      }

    /// <summary>
    /// Обновление Представления новыми значениями модели.
    /// По сути Binding (привязка) значений модели к Представлению. 
    /// </summary>
        private void RefreshView()
      {
         _view.SetSalary(_model.Salary);
      }
   }


    // --------------------------------------------------------
    public class EmployeeManager : Employee
    {
        private ulong numberOfOptions;
        public EmployeeManager(int id, string name, decimal solary, ulong options)
            : base(id, name, solary)
        {
            this.numberOfOptions = options;
        }
        public ulong options
        {
            get { return numberOfOptions; }
            set { numberOfOptions = value; }
        }
        public override decimal GetMonthlyPayment()
        {
            return salary / 12 + 1;
        }
        public override void doWork()
        {
            //throw new Exception("The method or operation is not implemented.");
            MyExсeption ex = new MyExсeption("Error #1"); // вызвать пользовательское исключение
            throw ex;
        }
    }

    // пользовательские исключения
    [global::System.Serializable]
    public class MyExсeption : Exception
    {
        public MyExсeption() { }
        public MyExсeption(string message) : base(message) { }
        public MyExсeption(string message, Exception iner) : base(message, iner) { }
        protected MyExсeption(System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }

    }
}
