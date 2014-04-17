using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Xml.Serialization;
using Companies.DataEntity;


namespace Companies.Accessors
{
    public class XMLFileAccessor<T> : IEntityAccessor<T> where T : BaseEntity
    {

        private List<T> items = new List<T>();
        private string fileName;
        private int lastIndex;

        public XMLFileAccessor(string fileName)
        {
            try
            {
                this.fileName = fileName;
            }
            catch (Exception e)
            {
                // don't forget to write code here
            }
        }


        private void SaveToXML()
        {
            using (FileStream fileStream = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<T>));
                xmlSerializer.Serialize(fileStream, items);
            }
        }


        private List<T> LoadFromXML()
        {
            if (!File.Exists(fileName))
            {
                SaveToXML();
            }

            using (FileStream fileStream = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<T>));
                items = (List<T>)xmlSerializer.Deserialize(fileStream);
                return items;
            }
        }


        public List<T> GetAll()
        {
            return LoadFromXML();
        }


        public void DeleteById(int id)
        {
            LoadFromXML();

            items.Remove(GetById(id));
            if (id == lastIndex)
            {
                lastIndex = items.OrderByDescending(x => x.Id).First().Id; //bad: needs to sort to get max
            }

            SaveToXML();
        }


        public void Insert(T item)
        {
            LoadFromXML();

            lastIndex++;
            item.Id = lastIndex;
            items.Add(item);

            SaveToXML();
        }


        public T GetById(int id)
        {
            return LoadFromXML().Find(x => x.Id.Equals(id));
        }
     
    }
}
