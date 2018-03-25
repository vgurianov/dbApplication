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
    // Model-View-Presenter � ������������� ��������
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
        /// <summary>/// ����� /// </summary>
        void SetSalary(decimal value);

        /// <summary>/// ���� /// </summary>
        decimal InputSalary { get; }

        /// <summary>/// ������� ����� �������� /// </summary>
        event EventHandler<EventArgs> SetSalaryViewEvent;

    }

    public  class Presenter
   {
      //private Model _model = new Model(1, "noname",0);
        private MvpModel _model;
        private IView _view;

           /// <summary>
           /// � ���������� ���������� ���������� ��������� �������������
           /// � ���������� �������� �� ��� ������ �������.
           /// <summary>

        public Presenter(IView view, MvpModel model)
      {
          _model = model;
          _view = view;
          _view.SetSalaryViewEvent += new EventHandler<EventArgs>(OnSetSalary); // �������� ����������
         //MessageBox.Show(_model.Name);
         RefreshView();
      }

      /// <summary>
      /// ��������� �������, ��������� ������ �������� ��� Model
      /// </summary>
      private void OnSetSalary(object sender, EventArgs e)
      {
         _model.Salary = _view.InputSalary;
         MessageBox.Show("OnSetSalary:" + _model.Salary.ToString());

         RefreshView();
      }

    /// <summary>
    /// ���������� ������������� ������ ���������� ������.
    /// �� ���� Binding (��������) �������� ������ � �������������. 
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
            MyEx�eption ex = new MyEx�eption("Error #1"); // ������� ���������������� ����������
            throw ex;
        }
    }

    // ���������������� ����������
    [global::System.Serializable]
    public class MyEx�eption : Exception
    {
        public MyEx�eption() { }
        public MyEx�eption(string message) : base(message) { }
        public MyEx�eption(string message, Exception iner) : base(message, iner) { }
        protected MyEx�eption(System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }

    }
}
