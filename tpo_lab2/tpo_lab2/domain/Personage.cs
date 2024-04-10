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
            state = State.OffCamera;
        }

        public Personage(Person person) : base(person.name, person.surname, person.patronymic)
        {
            state = State.OffCamera;
        }

        public State state { get; private set; }

        public enum State
        {
            OnCamera,
            OffCamera
        }

        public void changeState()
        {
            if (state == State.OffCamera)
            {
                state = State.OnCamera;
            }
            else
            {
                state = State.OffCamera;
            }
        }

        public void setOffCameraState()
        {
            state = State.OffCamera;
        }
    }
}
