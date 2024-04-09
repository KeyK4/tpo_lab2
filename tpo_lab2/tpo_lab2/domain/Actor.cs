using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tpo_lab2.domain
{
    public class Actor : CrewMember
    {
        public Actor(string name, string surname, string patronymic) : base(name, surname, patronymic)
        {
        }

        public Actor(Person person) : base(person.name, person.surname, person.patronymic){}
    }
}
