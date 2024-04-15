using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tpo_lab2.domain
{
    public interface IRepository
    {
        void writeScenario(Scenario scenario, Screenwriter screenwriter, String genre, String theme);
        Dictionary<Personage, Actor> cast(Scenario scenario, Director director);
        Crew crewUp(Director director, Screenwriter screenwriter, Dictionary<Personage, Actor> roles,
            List<CrewMember> crewMembers);
        Movie makeMovie(Crew crew, Scenario scenario);
        void release(List<Cinema> cinemas, Movie movie);
    }
}
