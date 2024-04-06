using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tpo_lab2.domain
{
    internal interface Repository
    {
        void writeScenario(Scenario scenario, Screenwriter screenwriter, String genre, String theme);
        List<Actor> cast(Scenario scenario, Director director);
        void crewUp(Crew crew, List<CrewMember> crewMembers, Director director, List<Actor> actors);
        void makeMovie(Movie movie, Crew crew);
        void release(List<Cinema> cinemas, Movie movie);

    }
}
