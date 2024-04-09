using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tpo_lab2.domain
{
    public class Personage: Person
    {
        public Personage(string name, string surname, string patronymic) : base(name, surname, patronymic)
        {
        }
        
        public Personage(Person person): base(person.name, person.surname, person.patronymic){}
    }
}
