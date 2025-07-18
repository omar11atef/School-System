using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_test.Moduls
{
    public  class Student
    {
        public int id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public int Age { get; set; }
        public string Stage { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string phone { get; set; }
        public Student()
        {
            id = 0;
            First_Name = Last_Name = Stage = Address = City = phone = "Not Details";
        }
        public Student(int id, string first_Name, string last_Name,int age, string stage, string address, string city, string phone)
        {
            this.id = id;
            First_Name = first_Name;
            Last_Name = last_Name;
            Age=age;
            Stage = stage;
            Address = address;
            City = city;
            this.phone = phone;
        }
    }
}
