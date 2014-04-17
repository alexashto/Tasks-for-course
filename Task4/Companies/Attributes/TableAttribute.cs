using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Companies.Attributes
{
    class TableAttribute : Attribute
    {
        public string  TableName { get; set; }
        public string IdField { get; set; }
        public TableAttribute(string name, string idField)
        {
            TableName = name;
            IdField = idField;
        }

        public TableAttribute()
        {

        }
    }
}
