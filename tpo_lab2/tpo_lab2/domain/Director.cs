using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tpo_lab2.domain
{
    public class Director : CrewMember
    {
        public Director(string name, string surname, string patronymic) : base(name, surname, patronymic)
        {
        }

        public Actor cast(Personage personage)
        {
            return new Actor(Person.getRandomPerson());
        }
    }
}
