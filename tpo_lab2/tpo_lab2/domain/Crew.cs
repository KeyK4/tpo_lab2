using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tpo_lab2.domain
{
    public class Crew
    {
        public Director director { get; }
        public Screenwriter screenwriter { get; }
        public Dictionary<Personage, Actor> roles { get; }
        public List<CrewMember> crewMembers { get;}

        public Crew(Director director, Screenwriter screenwriter, Dictionary<Personage, Actor> roles,
            List<CrewMember> crewMembers)
        {
            this.director = director;
            this.screenwriter = screenwriter;
            this.roles = roles;
            this.crewMembers = crewMembers;
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
    }
}
