using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelectandSelectMany
{
    class Employee
    {
        public string Name { get; set; }
        public List<string> Skills { get; set; }
    }


    class Department
    {
        public int DeptID { get; set; }
        public string DeptName { get; set; }
        public int Floor { get; set; }
    }

    class Employee1
    {
        public int EmpID { get; set; }
        public int DeptID { get; set; }
        public string EmpName { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {

            var dept = new List<Department>();
            dept.Add(new Department { DeptID = 1, DeptName = "Marketing", Floor = 1 });
            dept.Add(new Department { DeptID = 2, DeptName = "Sales", Floor = 2 });
            dept.Add(new Department { DeptID = 3, DeptName = "Administration", Floor = 3 });
            dept.Add(new Department { DeptID = 4, DeptName = "Accounts", Floor = 3 });
            dept.Add(new Department { DeptID = 5, DeptName = "HR", Floor = 3 });

            var emp = new List<Employee1>();
            emp.Add(new Employee1 { EmpID = 1, DeptID = 1, EmpName = "Jack Nolas" });
            emp.Add(new Employee1 { EmpID = 2, DeptID = 4, EmpName = "Mark Pine" });
            emp.Add(new Employee1 { EmpID = 3, DeptID = 3, EmpName = "Sandra Simte" });
            emp.Add(new Employee1 { EmpID = 4, DeptID = 4, EmpName = "Larry Lo" });
            emp.Add(new Employee1 { EmpID = 5, DeptID = 3, EmpName = "Sudhir Panj" });
            emp.Add(new Employee1 { EmpID = 6, DeptID = 2, EmpName = "Kathy K" });
            emp.Add(new Employee1 { EmpID = 7, DeptID = 1, EmpName = "Kaff Joe" });
            emp.Add(new Employee1 { EmpID = 8, DeptID = 1, EmpName = "Su Lie" });


            List<Employee> employees = new List<Employee>();
            Employee emp1 = new Employee { Name = "Deepak", Skills = new List<string> { "C", "C++", "Java" } };
            Employee emp2 = new Employee { Name = "Karan", Skills = new List<string> { "SQL Server", "C#", "ASP.NET" } };

            Employee emp3 = new Employee { Name = "Lalit", Skills = new List<string> { "C#", "ASP.NET MVC", "Windows Azure", "SQL Server" } };

            employees.Add(emp1);
            employees.Add(emp2);
            employees.Add(emp3);

            // Query using Select()
            IEnumerable<List<String>> resultSelect = employees.Select(e => e.Skills);

            Console.WriteLine("**************** Select ******************");

            // Two foreach loops are required to iterate through the results
            // because the query returns a collection of arrays.
            foreach (List<String> skillList in resultSelect)
            {
                foreach (string skill in skillList)
                {
                    Console.WriteLine(skill);
                }
                Console.WriteLine();
            }

            // Query using SelectMany()
            IEnumerable<string> resultSelectMany = employees.SelectMany(m => m.Skills);

            Console.WriteLine("**************** SelectMany ******************");

            // Only one foreach loop is required to iterate through the results
            // since query returns a one-dimensional collection.
            foreach (string skill in resultSelectMany)
            {
                Console.WriteLine(skill);
            }

            //This sample uses the ‘Any' operator to list down the Departments that do not have Employees

            var noEmp =
            from d in dept
            where !emp.Any(e => e.DeptID == d.DeptID)
            select new { dId = d.DeptID, dNm = d.DeptName };

            Console.WriteLine("Departments having no Employees");
            foreach (var empl in noEmp)
            {
                Console.WriteLine(@"Dept ID - " + empl.dId + ", Dept Name - " + empl.dNm);

            }


            //Using ‘Contains’ Quantifier in LINQ
            //The following example uses the ‘Contains’ quantifier to find the List of Departments having Employee Names starting with ‘S’
            //C#
            // Functionality Similar to IN operator
            var hasEmp = dept
             .Where(e => emp.Where(contact =>
             contact.EmpName.StartsWith("S"))
             .Select(d => d.DeptID)
             .Contains(e.DeptID));

            Console.WriteLine("/nList of Departments having Employee Names starting with S");
            foreach (var dpt in hasEmp)
            {
                Console.WriteLine("Dept ID - " + dpt.DeptID + ", Dept Name - " + dpt.DeptName);
            }


            //If you want to add another condition to the above query where only Departments in Floor 2 and 3 are to be considered, then here’s how to do so:
            //    C#
            var floorNo = new List<int> { 2, 3 };
            var floo = emp.Where(contact =>
                contact.EmpName.StartsWith("S"))
               .Where(du => dept.Where(dp => floorNo.Contains(dp.Floor))
                   .Select(dp => dp.DeptID)
                   .Contains(du.DeptID));

            Console.WriteLine("List of Employess with Names starting with S\nand are on Floor 2 or 3");
            foreach (var dpt in floo)
            {
                Console.WriteLine("Dept ID - " + dpt.DeptID + ", Employee Name - " + dpt.EmpName);
            }

            //Using ‘All' Quantifier in LINQ
            //Using the ‘All' operator, we can determine whether all employees have their names starting with ‘A’
            //C#
            Console.WriteLine("Find if all Employees have their names starting with 'A'");
            var chkName = emp.All(e =>
                               e.EmpName.StartsWith("A"));
            Console.WriteLine("Result : " + chkName);


            try
            {
                F();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in Main: " + e.Message);
            }


            finally
            {
                Console.WriteLine("This is the line in Finally");

            }

            Console.WriteLine("This is the last line in Main");
            Console.ReadKey();
        }

        static void F()
        {
            try
            {
                G();
            }
            catch (Exception e)
            {
                //Line 1 - e = new Exception("F"); - if line 1 & 2 will be uncommented then next
                //Line 2 - throw; -- line “console.WriteLine…” will not execute.
                // It display “Exception in Main: G” and "This is the last line in Main"
                //e = new Exception(nameof(F));
                //throw; // re-throw

                Console.WriteLine("Exception in F: " + e.Message);
                e = new Exception(nameof(F));
                throw; // re-throw
            }
        }
        static void G() { throw new Exception(nameof(G)); }

    }

}
