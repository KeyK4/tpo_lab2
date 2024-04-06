using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tpo_lab2.domain
{
    public class Person
    {
        public string name { get; set; }
        public string surname { get; set; }
        public string patronymic { get; set; }

        public string getFullName()
        {
            return $"{name} {surname} {patronymic}";
        }
    }
}
