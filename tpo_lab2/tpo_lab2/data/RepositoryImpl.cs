using tpo_lab2.domain;

namespace tpo_lab2.data;

public class RepositoryImpl : IRepository
{
    public void writeScenario(Scenario scenario, Screenwriter screenwriter, string genre, string theme)
    {
        scenario.screenwriter = screenwriter;
        scenario.genre = genre;
        scenario.theme = theme;
        scenario.write();
    }

    public Dictionary<Personage, Actor> cast(Scenario scenario, Director director)
    {
        if (scenario.state != Scenario.ScenarioState.Finished)
        {
            throw new Exception("Сценарий еще не готов");
        }
        var cast = new Dictionary<Personage, Actor>();
        foreach (var personage in scenario.personages)
        {
            var actor = director.cast(personage);
            cast[personage] = actor;
        }

        return cast;
    }

    public Crew crewUp(Director director, Screenwriter screenwriter, Dictionary<Personage, Actor> roles, List<CrewMember> crewMembers)
    {
        Crew crew = new Crew();
        crew.crewUp(director, screenwriter, roles, crewMembers);
        return crew;
    }

    public Movie makeMovie(Crew crew, Scenario scenario)
    {
        Movie movie = new Movie(scenario);
        movie.crew = crew;
        movie.film();
        return movie;
    }

    public void release(List<Cinema> cinemas, Movie movie)
    {
        movie.setReleased();
        foreach (var cinema in cinemas)
        {
            cinema.addMovie(movie);
        }
    }
}