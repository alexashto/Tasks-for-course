using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Companies.Attributes;


namespace Companies.DataEntity
{

    [Table(TableName = "Employee", IdField = "EmployeeId")]
    public class Employee : BaseEntity
    {
        [Field(FieldName = "EmployeeId", FieldType = "int")]
        public override int Id { get; set; }

        [Field(FieldName = "FirstName", FieldType = "string")]
        public string FirstName { get; set; }

        [Field(FieldName = "LastName", FieldType = "string")]
        public string LastName { get; set; }

        [Field(FieldName = "BirthDate", FieldType = "int")]
        public DateTime BirthDate { get; set; }

        [Field(FieldName = "CompanyId", FieldType = "int" )]
        public int CompanyId { get; set; }

        [Field(FieldName = "Position", FieldType = "string")]
        public string Position { get; set; }

        [Field(FieldName = "EmploymentDate", FieldType = "int")]
        public DateTime EmploymentDate { get; set; }



        public Employee() { }

        public Employee(string firstName, string lastName, DateTime birthDate, int companyId, string position, DateTime employmentDate, int id = 0)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            CompanyId = companyId;
            Position = position;
            EmploymentDate = employmentDate;
        }

    }
}
