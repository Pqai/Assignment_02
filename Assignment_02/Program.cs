using System;
using System.Text;

namespace Assignment02
{
    public abstract class Employee
    {
        public string Name { get; set; }
        public string Ssn { get; set; }
        public double BaseSalary { get; set; }

        // two-argument constructor
        public Employee(string name, string ssn)
        {
            Name = name;
            Ssn = ssn;
            BaseSalary = 0;
        }

        //Virtual Method Default Behavior: update the salary
        public virtual void UpdateSalary(double percent)
        {
            BaseSalary = BaseSalary + BaseSalary * percent;

        }

        //Overriding Object Class ToString() method...
        public override string ToString()
        {
            return Name + "\nSocial security number: " + Ssn;

        }

        // abstract method will be overridden by inherited subclasses        
        public abstract double earnings();
        // no implementation is allowed here, derived calss must override this method!
    }   
  

    public class SalariedEmployee : Employee
    {
        // three-argument constructor
        public SalariedEmployee(string name, string ssn, double salary) : base(name, ssn)
        {
            BaseSalary = salary; // Set weekly salary
        }

        //Must Override base class method                                                           
        public override double earnings()
        {
            return BaseSalary;
        }

        //Overriding BaseClass ToString Method                                                    
        public override string ToString()
        {
            string s = "Salaried employee: " + base.ToString() + "\nWeekly salary " + string.Format("${0:0.00}", BaseSalary) + string.Format("\nEarnings ${0:0.00}", earnings()) + "\n";
            return s;
        }
                                            
    }




    //define and Create 'HourlyEmployee' class 
    //{
    //To do
    // define property for 'wage', 'hours', and overtimeRate
    // define constructor method by matching the total parameters from the main method object instance creation statement  
    // Override the base class earnings method and implement earnings for HourlyEmployee class   
    // Override ToString() method.                                                                                                   
    //}
    public class HourlyEmployee : Employee
    {
        //ToDo
        public float Wage { get; set; }
        public float Hours { get; set; }
        public float OvertimeRate { get; set; }

        public HourlyEmployee(string name, string ssn, float wage, float overtimeRate, float hours): base(name, ssn)
        {
            OvertimeRate = overtimeRate;
            Wage = wage;
            Hours = hours;
        }

        public override double earnings()
        {
            return Wage * Hours;
        }

        public override string ToString()
        {
            string s = "Hourly employee: " + base.ToString() + "\nHourly salary " + string.Format("${0:0.00}, ", Wage) + "Hours worked: " + Hours + "\n" + string.Format("\nEarnings ${0:0.00}", earnings()) + "\n";
            return s;
        }
    }

    //define and Create 'CommissionEmployee' class 
    //{
    //To do
    // define property for 'GrossSales' and 'CommitionRate'
    // define constructor method by matching the total parameters from the main method object instance creation statement  
    // Override the base class earnings method and implement earnings for CommissionEmployee class
    // Override ToString() method.
    //}
    public class CommissionEmployee : Employee
    {
        //ToDo
        public float GrossSales { get; set; }
        public float CommissionRate { get; set; }

        public CommissionEmployee(string name, string ssn, float grossSales, float commitionRate) : base(name, ssn)
        {
            GrossSales = grossSales;

            CommissionRate = commitionRate;
        }

        public override double earnings()
        {
            return GrossSales * CommissionRate;
        }

        public override string ToString()
        {
            string s = "Commission employee: "+ base.ToString() + $"Gross Sales ${GrossSales}," + $"Commission Rate 0.{CommissionRate}%\n"+ $"Earnings ${earnings()}";
            return s;
        }
    }

    //define and Create 'BasePlusCommissionEmployee' class 
    //{
    //To do
    // define constructor method by matching the total parameters from the main method object instance creation statement  
    // Override the base class earnings method and implement earnings for BasePlusCommissionEmployee class 
    // Override the UpdateSalary method if required.
    // Override ToString() method.                                          
    //}
    public class BasePlusCommissionEmployee : CommissionEmployee
    {
        //ToDo

        public BasePlusCommissionEmployee(string name, string ssn, float grossSales, float commitionRate, double salary) : base(name, ssn, grossSales, commitionRate)
        {
            BaseSalary = salary; // Set weekly salary

        }

        public override double earnings()
        {
            return base.earnings() + BaseSalary;
        }

        public override void UpdateSalary(double percent)
        {
            BaseSalary += BaseSalary * (percent / 100);
        }

        public override string ToString()
        {
            return "Base Plus Commission Employee: " + base.Name +
               "\nSocial security number" + Ssn +
               "\nGross Sales $" + GrossSales +
               ", Commission Rate " + string.Format("{0:P}", CommissionRate) +
               ", Base Salary $" + BaseSalary +
               "\nEarnings $" + earnings();
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            /*
             Please do not change the content of the main method.
             */

            //Create subclass objects
            SalariedEmployee salariedEmployee = new SalariedEmployee("Mahbub Murshed", "111-22-3333", 800.00); //Name, SSN, BaseSalary
            HourlyEmployee hourlyEmployee = new HourlyEmployee("Stuart Russell", "111-22-4444", 16.75, 1.5, 50); ////Name, SSN, Wage, OvertimeRate, HoursWorked
            CommissionEmployee commissionEmployee = new CommissionEmployee("Susan Harper", "444-44-4444", 10000, .06); //Name, SSN, TotalSales, CommissionRate
            BasePlusCommissionEmployee basePlusCommissionEmployee = new BasePlusCommissionEmployee("David Whatmore", "777-77-7777", 5000, .04, 300); //Name, SSN, TotalSales, CommissionRate, BaseSalary

            //Create an Employee array of base type
            var employees = new Employee[4];
            //Initialize array with different Employees
            employees[0] = salariedEmployee;
            employees[1] = hourlyEmployee;
            employees[2] = commissionEmployee;
            employees[3] = basePlusCommissionEmployee;

            Console.WriteLine("Weekly salary of all employees in the collection:\n");

            //Process every element in the array
            foreach (var currentEmployee in employees)
            {
                Console.WriteLine(currentEmployee); // invokes tostring    
            }

            double percentageIncrease = 30;
            double newCommissionRate = 0.05;
            //Update Salary for BasePlusCommissionEmployee
            //Find BasePlusCommissionEmployee types
            foreach (var currentEmployee in employees)
            { 
                // Check if employee is a BasePlusCommissionEmployee
                if (currentEmployee is BasePlusCommissionEmployee)
                {
                    currentEmployee.UpdateSalary(percentageIncrease); //30% base salary update
                    Console.WriteLine("Base Plus Commission Employee:\nThe new base salary with a {0}% increase is: ${1}", percentageIncrease, currentEmployee.BaseSalary);
                    
                    //Downcast Employee reference to BasePlusCommissionEmployee reference
                    //Downcast is required to access the "CommissionRate" property.
                    BasePlusCommissionEmployee employee = (BasePlusCommissionEmployee) currentEmployee;
                    employee.CommissionRate = newCommissionRate; //New Commission Rate;
                    Console.WriteLine("The new commission rate is: " + employee.CommissionRate+"%");

                } // end if  
            } // end for
            Console.WriteLine();
            //Process every element in the array
            foreach (var currentEmployee in employees)
            {
                Console.WriteLine("Employee {0} is {1}", currentEmployee.Name, currentEmployee.GetType().Name);
                Console.WriteLine("Total Earnings: "+currentEmployee.earnings());    
            }
        }
    }
}



/*
  
If Executed Correctly, Your ptogram will show the following output:

Weekly salary of all employees in the collection:

Salaried employee: Mahbub Murshed
Social security number: 111-22-3333
Weekly salary $800.00
Earnings $800.00

Hourly employee: Stuart Russell
Social security number: 111-22-4444
Hourly Salary $16.75, Hours Worked 50
Earnings $921.25

Commission Employee: Susan Harper
Social security number: 444-44-4444
Gross Sales $10000, Commission Rate 0.06%
Earnings $600

Base Plus Commission Employee: David Whatmore
Social security number777-77-7777
Gross Sales $5000, Commission Rate 0.04%, Base Salary $300
Earnings $500

Base Plus Commission Employee:
The new base salary with a 30% increase is: $390
The new commission rate is: 0.05%

Employee Mahbub Murshed is SalariedEmployee
Total Earnings: 800
Employee Stuart Russell is HourlyEmployee
Total Earnings: 921.25
Employee Susan Harper is CommissionEmployee
Total Earnings: 600
Employee David Whatmore is BasePlusCommissionEmployee
Total Earnings: 640


 */
