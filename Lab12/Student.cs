using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab12
{
    public class Student : IStudy
    {
        private int _money;
        private readonly string _name;
        private short _retakes;
        private static bool yeap = true;

        public int prop { get { return 0; } set { } }
        public int pro;

        public static int countOfStudents = 0;

        static public int CountOfStudents()
        {
            return countOfStudents++;
        }

        public string GetName() => _name;
        public int GetMoney() => _money;
        public short GetRetakes() => _retakes;
        public bool GetYeap() => yeap;
        public Student()
        {
            _name = "Sanya";
            _money = 50;
            _retakes = 0;
        }

        static Student()
        {
            //Console.WriteLine("Static constructor");
            CountOfStudents();
        }


        ~Student()
        {
            Console.WriteLine("I am distructor");
        }

        public Student(Student std)
        {
            _name = std._name;
            _money = std._money;
            _retakes = std._retakes;
            CountOfStudents();
        }


        public Student(string name, int money, short retakes)
        {
            _name = name;
            _money = money;
            _retakes = retakes;
            CountOfStudents();
        }

        public bool Equals(Student std)
        {
            if (_money == std.GetMoney() && _name == std.GetName() && _retakes == std.GetRetakes())
                return true;
            else return false;
        }

        public override int GetHashCode()
        {
            int hash = _name.GetHashCode() + _money.GetHashCode() + _retakes.GetHashCode();
            return hash;
        }

        public override string ToString()
        {
            string str = "Name: " + _name + ", money: " + _money + ", retakes: " + _retakes + '.';
            return str;
        }

        public void Study(int x)
        {
            Console.WriteLine("I am student "+x);
        }
    }
}
