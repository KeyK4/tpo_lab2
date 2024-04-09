using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tpo_lab2.domain
{
    public class CrewMember : Person
    {
        public CrewMember(string name, string surname, string patronymic) : base(name, surname, patronymic)
        {
            state = State.Free;
        }

        public State state { get; private set; }
        
        public enum State
        {
            Free,
            Busy
        }
        
        public void setBusy()
        {
            if (state == State.Free)
            {
                state = State.Busy;
            }
            else throw new WrongStateException("Член команды уже занят");
        }

        public void setFree()
        {
            if (state == State.Busy)
            {
                state = State.Free;
            }
            else throw new WrongStateException("Член команды уже свободен");
        }

        public class WrongStateException : Exception
        {
            public WrongStateException(string message) : base(message)
            {
            }
        }
    }
}
