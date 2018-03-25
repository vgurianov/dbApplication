using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    abstract public class Employee : IComparable
    {
        protected int ID;
        protected string name;
        protected decimal salary;
        public Employee(int id, string name, decimal salary)
        {
            this.name = name;
            this.salary = salary;
            this.ID = id;
        }
        public Employee(string name) : this(9999, name, 0) { }
        public int Id
        {
            get
            {
                return ID;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
        public virtual decimal Salary
        {
            get
            {
                return salary;
            }
            set
            {
                if (value >= 0)
                {
                    salary = value;
                }
                else salary = 0;
            }
        }
        public virtual decimal GetMonthlyPayment()
        {
            return salary / 12;
        }
        public override string ToString()
        {
            return ID.ToString() + " , Name: " + name + ", Salary: $" + salary.ToString();
        }
        public abstract void doWork();

        int IComparable.CompareTo(object ob)
        {
            Employee tmp = (Employee)ob;
            if (this.ID > tmp.ID) return 1;
            if (this.ID < tmp.ID) return -1;
            else return 0;

        }


    }

}
