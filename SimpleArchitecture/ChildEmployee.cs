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
            MyExñeption ex = new MyExñeption("Error #1"); // user exception
            throw ex;
        }
    }

    // User exception
    [global::System.Serializable]
    public class MyExñeption : Exception
    {
        public MyExñeption() { }
        public MyExñeption(string message) : base(message) { }
        public MyExñeption(string message, Exception iner) : base(message, iner) { }
        protected MyExñeption(System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }

    }
}
