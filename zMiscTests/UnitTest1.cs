using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace zMiscTests
{
    class Person
    {
        public string Name;
        public int Age;

        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }
    }
    [TestClass]
    public class UnitTest1
    {
        List<Person> people;

        [TestInitialize]
        public void ini()
        {
            people = new List<Person>();
            people.Add(new Person("Miles", 26));
            people.Add(new Person("Adam", 7));
            people.Add(new Person("Mason", 24));
        }

        [TestMethod]
        public void LinqTest()
        {
            string[] names = people.Select(x => x.Name).ToArray();
            int[] ages = people.Select(x => x.Age).ToArray();
            Person miles = people.First(x => x.Name == "Miles");
            Person[] old = people.Where(x => x.Age > 20).ToArray();
        }
    }
}
