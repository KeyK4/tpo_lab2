using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tpo_lab2.domain
{
    internal interface IRepository
    {
        void writeScenario(Scenario scenario, Screenwriter screenwriter, String genre, String theme);
        Dictionary<Personage, Actor> cast(Scenario scenario, Director director);
        Movie makeMovie(Crew crew, Scenario scenario);
        void release(List<Cinema> cinemas, Movie movie);
    }
}
