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
            state = PersState.OffCamera;
        }

        public Personage(Person person) : base(person.name, person.surname, person.patronymic)
        {
            state = PersState.OffCamera;
        }

        public PersState state { get; private set; }

        public enum PersState
        {
            OnCamera,
            OffCamera
        }

        public void changeState()
        {
            if (state == PersState.OffCamera)
            {
                state = PersState.OnCamera;
            }
            else
            {
                state = PersState.OffCamera;
            }
        }

        public void setOffCameraState()
        {
            state = PersState.OffCamera;
        }
    }
}
