using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Companies;
using Companies.DataEntity;
using Companies.Accessors;

namespace ConsoleClient
{
    class ConsoleView
    {
        CompaniesService companiesManager;

        public ConsoleView()
        {

        }

        public void Start()
        {
            MakeAccessor(SelectAccessor());
            bool work = true;
            while (work)
            {
                work = ExecuteCommand(SelectCommand());

            }       
        }

        public int SelectAccessor()
        {
            int accessorNumber;
            Console.WriteLine(@"Выберите хранилище данных:
    1 - ОП
    2 - Файл
    3 - БД ADO.NET
    4 - БД MyORM");
            while (!Int32.TryParse(Console.ReadLine(), out accessorNumber) || !((accessorNumber > 0) && (accessorNumber < 5)))
            {
                Console.WriteLine("Ошибка ввода");
            }
            return accessorNumber;
        }





        public int SelectCommand()
        {
            int commandNumber;
            Console.WriteLine(@"
Выберите команду:
    1 - Показать список всех компаний | 5 - Показать список всех сотрудников
    2 - Показать компанию             | 6 - Показать сотрудника
    3 - Добавить новую компанию       | 7 - Добавить сотрудника
    4 - Удалить компанию              | 8 - Удалить сотрудника

    0 - Выход
");
            while (!Int32.TryParse(Console.ReadLine(), out commandNumber) || !((commandNumber >= 0) && (commandNumber < 9)))
            {
                Console.WriteLine("Ошибка ввода");
            }
            return commandNumber;
        }

        public bool ExecuteCommand(int command)
        {
            bool result = true;
            int compId;
            int empId; 
            switch (command)
            {
                case 1:
                    foreach (var c in companiesManager.GetCompanies())
                        WriteCompany(c);
                    break;
                case 2:
                    Console.Write("Введите ID компании: ");
                    
                    while (!Int32.TryParse(Console.ReadLine(), out compId))
                    {
                        Console.WriteLine("Ошибка ввода");
                    }
                    WriteCompany(companiesManager.GetCompany(compId));
                    break;
                case 3:
                    Console.Write("Введите название компании: ");
                    string name = Console.ReadLine();

                    Console.Write("Введите адрес: ");
                    string addr = Console.ReadLine();

                    Console.Write("Введите номер телефона: ");
                    string phone = Console.ReadLine();

                    companiesManager.AddCompany(new Company(name, addr, phone));

                    break;
                case 4:
                    Console.Write("Введите ID компании: ");
                    
                    while (!Int32.TryParse(Console.ReadLine(), out compId))
                    {
                        Console.WriteLine("Ошибка ввода");
                    }
                    companiesManager.DeleteCompany(compId);

                    break;
                case 5:
                    foreach (var e in companiesManager.GetEmployees())
                        WriteEmployee(e);
                    break;
                case 6:
                    Console.Write("Введите ID сотрудника: ");
                    
                    while (!Int32.TryParse(Console.ReadLine(), out empId))
                    {
                        Console.WriteLine("Ошибка ввода");
                    }
                    WriteEmployee(companiesManager.GetEmployee(empId));
                    break;
                case 7:
                    Console.Write("Введите имя: ");
                    string firstName = Console.ReadLine();

                    Console.Write("Введите фамилилю: ");
                    string lastName = Console.ReadLine();

                    DateTime birthDate;
                    Console.Write("Введите дату рождения: ");
                    while (!DateTime.TryParse(Console.ReadLine(), out birthDate))
                    {
                        Console.WriteLine("Ошибка ввода");
                    }

                    Console.Write("Введите ID компании, в которой он работает: ");
                    while (!Int32.TryParse(Console.ReadLine(), out compId))
                    {
                        Console.WriteLine("Ошибка ввода");
                    }

                    Console.Write("Введите должность: ");
                    string position = Console.ReadLine();

                    DateTime empDate;
                    Console.Write("Введите дату устройства: ");
                    while (!DateTime.TryParse(Console.ReadLine(), out empDate))
                    {
                        Console.WriteLine("Ошибка ввода");
                    }

                    companiesManager.AddEmployee(new Employee(firstName, lastName, birthDate, compId, position, empDate));

                    break;
                case 8:
                    Console.WriteLine("Введите ID сотрудника: ");
                    
                    while (!Int32.TryParse(Console.ReadLine(), out empId))
                    {
                        Console.WriteLine("Ошибка ввода");
                    }
                   companiesManager.DeleteEmployee(empId);
                    break;
                case 0:
                    return false;                    
            }



            return result;
        }

        public void MakeAccessor(int accessorNumber)
        {
            switch (accessorNumber)
            {
                case 1: 
                    companiesManager = new CompaniesService(new MemoryAccessor<Employee>(), new MemoryAccessor<Company>()); 
                    break;
                case 2:
                    companiesManager = new CompaniesService(new XMLFileAccessor<Employee>("employees.xml"), new XMLFileAccessor<Company>("companies.xml")); 
                    break;
                case 3: 
                    companiesManager = new CompaniesService(new DBEmployeeAccessor(), new DBCompanyAccessor()); 
                    break;
                case 4: 
                    companiesManager = new CompaniesService(new MyOrmAccessor<Employee>(), new MyOrmAccessor<Company>()); 
                    break;
            }
        }

        private void WriteEmployee(Employee e)
        {
            string company;
            if (companiesManager.GetCompany(e.CompanyId) == null)
            {
                company = "Отсутствует";
            }
            else
            {
                company = companiesManager.GetCompany(e.CompanyId).Name;
            }
              
            Console.WriteLine("ID: {0}, Имя: {1}, Фамилия: {2}, Дата рождения: {3}\nНазвание компании: {4}, Должность: {5}, Дата устройства: {6}",
                               e.Id, e.FirstName, e.LastName, e.BirthDate.ToShortDateString(), company, e.Position, e.EmploymentDate.ToShortDateString());
        }

        private void WriteCompany(Company c)
        {
            Console.WriteLine("ID: {0}, Название: {1}, Адрес: {2}, Телефон: {3}", c.Id, c.Name, c.Address, c.Phone);
        }

    }
}
