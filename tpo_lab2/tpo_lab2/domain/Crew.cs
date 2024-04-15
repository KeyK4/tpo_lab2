using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tpo_lab2.domain
{
    public class Crew
    {
        public Director director { get; private set; }
        public Screenwriter screenwriter { get; private set; }
        public Dictionary<Personage, Actor> roles { get; private set; }
        public List<CrewMember> crewMembers { get; private set; }
        
        public CrewState state { get; private set; }

        public Crew()
        {
            state = CrewState.NotCompleted;
        }

        public void crewUp(Director director, Screenwriter screenwriter, Dictionary<Personage, Actor> roles,
            List<CrewMember> crewMembers)
        {
            this.director = director;
            this.screenwriter = screenwriter;
            this.roles = roles;
            this.crewMembers = crewMembers;
            state = CrewState.Completed;
        }
        
        public string getСredits()
        {
            string credits = $"Режиссер: {director.toStringName()}\n"
                             + $"Автор сценария: {screenwriter.toStringName()}\n"
                             + $"В ролях: \n";
            foreach (var role in roles)
            {
                var personage = role.Key;
                var actor = role.Value;
                credits += $"{personage.toStringName()}: {actor.toStringName()}\n";
            }

            credits += "Съемочная группа: \n";

            foreach (var crewMember in crewMembers)
            {
                credits += $"{crewMember.toStringName()}\n";
            }

            return credits;
        }

        public void setBusy()
        {
            if(state != CrewState.Completed)
                throw new Exception("Команда не была собрана");
            
            foreach (var crewMember in crewMembers)
            {
                crewMember.setBusy();
            }
            foreach (var person2actor in roles)
            {
                var actor = person2actor.Value;
                actor.setBusy();
            }
            director.setBusy();
        }

        public void disband()
        {
            if(state == CrewState.Completed)
            {
                state = CrewState.Disbanded;
            }
            else
            {
                throw new Exception("Команда не была собрана");
            }
            
            foreach (var crewMember in crewMembers)
            {
                crewMember.setFree();
            }
            foreach (var person2actor in roles)
            {
                var actor = person2actor.Value;
                actor.setFree();
            }
            director.setFree();
        }
        
        public enum CrewState
        {
            NotCompleted,
            Completed,
            Disbanded 
        }
    }
}
