using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Companies.DataEntity;

namespace Companies.Accessors
{
    public interface IEntityAccessor<T>
    {
        List<T> GetAll();
        T GetById(int id);
        void DeleteById(int id);
        void Insert(T item);
    }
}
