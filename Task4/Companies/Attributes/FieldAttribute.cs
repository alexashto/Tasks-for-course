using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Companies.Attributes
{
    class FieldAttribute : Attribute
    {
        public string FieldName { get; set; }
        public string FieldType { get; set; }
        public FieldAttribute(string fieldName, string fieldType)
        {
            FieldName = fieldName;
            FieldType = fieldType;
        }

        public FieldAttribute()
        {

        }
    }
}
