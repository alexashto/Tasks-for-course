using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Companies;

namespace PersonTests
{
    [TestFixture]
    public class CompaniesUnitTests
    {

        [Test]
        public void InsertAndGetByNameTest()
        {

            IPersonAccessor personAccessor = new MemoryPersonAccessor();
            personAccessor.Insert(new Person(11, "Сидоренко В.А.", DateTime.Parse("01.02.03")));
            Assert.IsTrue(personAccessor.GetByName("Сидоренко") != null);

        } 


        [Test]
        public void DeleteByNameTest()
        {
            IPersonAccessor personAccessor = new MemoryPersonAccessor();
            personAccessor.DeleteById("Петров");
            Assert.AreEqual(8, personAccessor.GetAll().Count());
            
        }



    }
}
