using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Companies.DataEntity;
using Companies.Accessors;
using NLog;

namespace Companies
{
    public class CompaniesService
    {
        private IEntityAccessor<Employee> EmployeeAccessor;
        private IEntityAccessor<Company> CompanyAccessor;

        private static Logger logger = LogManager.GetCurrentClassLogger();



        public CompaniesService (IEntityAccessor<Employee> employeeAccessor, IEntityAccessor<Company> companyAccessor)
	    {
            EmployeeAccessor = employeeAccessor;
            CompanyAccessor = companyAccessor;
	    }

        public void AddCompany(Company company)
        {
            CompanyAccessor.Insert(company);
        }

        public void AddEmployee(Employee employee)
        {
            EmployeeAccessor.Insert(employee);
        }

        public Company GetCompany(int id)
        {
            return CompanyAccessor.GetById(id);
        }

        public Employee GetEmployee(int id)
        {
            return EmployeeAccessor.GetById(id);
        }

        public Company[] GetCompanies()
        {
            return CompanyAccessor.GetAll().ToArray();
        }

        public Employee[] GetEmployees()
        {
            return EmployeeAccessor.GetAll().ToArray();
        }

        public void DeleteCompany(int id)
        {
            CompanyAccessor.DeleteById(id);
        }

        public void DeleteEmployee(int id)
        {
            EmployeeAccessor.DeleteById(id);
        }

    }
}
