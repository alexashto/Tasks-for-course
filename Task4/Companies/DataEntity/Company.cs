using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Companies.Attributes;


namespace Companies.DataEntity
{
    [Table(TableName = "Company", IdField = "CompanyId")]
    public class Company : BaseEntity
    {
        [Field(FieldName = "CompanyId", FieldType = "int")]
        public override int Id { get; set; }

        [Field(FieldName = "Name", FieldType = "string")]  
        public string Name { get; set; }

        [Field(FieldName = "Address", FieldType = "string")]
        public string Address { get; set; }

        [Field(FieldName = "Phone", FieldType = "string")]
        public string Phone { get; set; }

        public Company(string name, string address, string phone, int id = 0)
        {
            Name = name;
            Address = address;
            Phone = phone;
            Id = id;
        }

        public Company()
        {

        }
    }
}