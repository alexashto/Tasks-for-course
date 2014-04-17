using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Companies.DataEntity;


namespace Companies.Accessors
{
    public class MemoryAccessor<T> : IEntityAccessor<T> where T : BaseEntity
    {
        private List<T> items = new List<T>();
        private int lastIndex;

        public MemoryAccessor()
        {

        }


        public List<T> GetAll()
        {
            return items;
        }


        public void DeleteById(int id)
        {
            items.Remove(GetById(id));
            if (id == lastIndex)
            {
                lastIndex = items.OrderByDescending(x => x.Id).First().Id; //bad: needs to sort to get max
            }
        }


        public void Insert(T item)
        {
            lastIndex++;
            item.Id = lastIndex;
            items.Add(item);
        }


        public T GetById(int id)
        {
            return items.Find(x => x.Id.Equals(id));
        }

    }
}
