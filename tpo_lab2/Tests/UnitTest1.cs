using NuGet.Frameworks;

namespace Tests;
using tpo_lab2.domain;
    
public class Tests
{
    [Test]
    public void PersonFieldsTest()
    {
        //setUP
        String name = "name";
        String surname = "surname";
        String patronymic = "patronymic";
        Person person = new Person(name, surname, patronymic);
        
        Assert.AreEqual(person.name, name);
        Assert.AreEqual(person.surname, surname);
        Assert.AreEqual(person.patronymic, patronymic);
    }

    [Test]
    public void CrewMemberFieldsTest()
    {
        String name = "name";
        String surname = "surname";
        String patronymic = "patronymic";
        CrewMember crewMember = new CrewMember(name, surname, patronymic);
        
        Assert.AreEqual(crewMember.state, CrewMember.State.Free);
    }

    [Test]
    public void PersonageFieldsTest()
    {
        String name = "name";
        String surname = "surname";
        String patronymic = "patronymic";

        Personage personageFromBaseConstructor = new Personage(name, surname, patronymic);
        Person person = new Person(name, surname, patronymic);
        Personage personageFromPerson = new Personage(person);
        
        Assert.AreEqual(personageFromBaseConstructor.name, name);
        Assert.AreEqual(personageFromBaseConstructor.surname, surname);
        Assert.AreEqual(personageFromBaseConstructor.patronymic, patronymic);
        
        Assert.AreEqual(personageFromPerson.name, name);
        Assert.AreEqual(personageFromPerson.surname, surname);
        Assert.AreEqual(personageFromPerson.patronymic, patronymic);
    }

    [Test]
    public void ActorFieldsTest()
    {
        String name = "name";
        String surname = "surname";
        String patronymic = "patronymic";

        Actor actorFromBaseConstructor = new Actor(name, surname, patronymic);
        Person person = new Person(name, surname, patronymic);
        Actor actorFromPerson = new Actor(person);
        
        Assert.AreEqual(actorFromBaseConstructor.name, name);
        Assert.AreEqual(actorFromBaseConstructor.surname, surname);
        Assert.AreEqual(actorFromBaseConstructor.patronymic, patronymic);
        
        Assert.AreEqual(actorFromPerson.name, name);
        Assert.AreEqual(actorFromPerson.surname, surname);
        Assert.AreEqual(actorFromPerson.patronymic, patronymic);
    }

    [Test]
    public void ScenarioFieldsTest()
    {
        String name = "name";
        String surname = "surname";
        String patronymic = "patronymic";

        Scenario scenario = new Scenario(name);
        
        Assert.AreEqual(scenario.name, name);
        Assert.AreEqual(scenario.theme, null);
        Assert.AreEqual(scenario.screenwriter, null);
        Assert.AreEqual(scenario.genre, null);
        Assert.AreEqual(scenario.personages, null);
        Assert.AreEqual(scenario.state, Scenario.ScenarioState.NotStarted);

        string genre = "genre";
        string theme = "theme";
        Screenwriter screenwriter = new Screenwriter(name+"SW", surname+"SW", patronymic+"SW");
        scenario.theme = theme;
        scenario.genre = genre;
        scenario.screenwriter = screenwriter;
        
        Assert.AreEqual(scenario.theme, theme);
        Assert.AreEqual(scenario.screenwriter, screenwriter);
        Assert.AreEqual(scenario.genre, genre);
    }

    [Test]
    public void CrewFieldsTest()
    {
        String name = "name";
        String surname = "surname";
        String patronymic = "patronymic";

        Director director = new Director(name+"Dir", surname+"Dir", patronymic+"Dir");
        Screenwriter screenwriter = new Screenwriter(name+"SW", surname+"SW", patronymic+"SW");
        Dictionary<Personage, Actor> roles = new Dictionary<Personage, Actor>()
        {
            {new Personage(name+"Pers", surname+"Pers", patronymic+"Pers"),
                new Actor(name+"Act", surname+"Act", patronymic+"Act")}
        };
        List<CrewMember> crewMembers = new List<CrewMember>()
        {
            new (name+"CM", surname+"CM", patronymic+"CM")
        };

        Crew crew = new Crew(director, screenwriter, roles, crewMembers);
        
        Assert.AreEqual(crew.director , director);
        Assert.AreEqual(crew.screenwriter , screenwriter);
        Assert.AreEqual(crew.roles , roles);
        Assert.AreEqual(crew.crewMembers , crewMembers);
    }

    [Test]
    public void MovieFieldsTest()
    {
        Scenario scenario = new Scenario("name");
        String name = "name";
        String surname = "surname";
        String patronymic = "patronymic";
        Director director = new Director(name+"Dir", surname+"Dir", patronymic+"Dir");
        Screenwriter screenwriter = new Screenwriter(name+"SW", surname+"SW", patronymic+"SW");
        Dictionary<Personage, Actor> roles = new Dictionary<Personage, Actor>();
        List<CrewMember> crewMembers = new List<CrewMember>();
        Crew crew = new Crew(director, screenwriter, roles, crewMembers);

        Movie movie = new Movie(scenario);
        movie.crew = crew;
        
        Assert.AreEqual(crew, movie.crew);
        Assert.AreEqual(scenario, movie.scenario);
        Assert.AreEqual(Movie.MovieState.NotStarted, movie.state);
    }

    [Test]
    public void CinemaFieldsTest()
    {
        string name = "name";
        uint maxMoviesNumber = 20;
        Cinema cinema = new Cinema(name, maxMoviesNumber);

        List<Movie> movies = new List<Movie>();
        
        Assert.AreEqual(name, cinema.name);
        Assert.AreEqual(maxMoviesNumber, cinema.maxMoviesNumber);
        Assert.AreEqual(movies, cinema.movies);
        Assert.AreEqual(Cinema.CinemaState.Open, cinema.state);
    }

    [Test]
    public void PersonMethodGetRandomPersonTest()
    {
        Person randomPerson = Person.getRandomPerson();

        Assert.True(Person.posibleNames.Contains(randomPerson.name));
        Assert.True(Person.posibleSurname.Contains(randomPerson.surname));
        Assert.True(Person.posiblePatronymics.Contains(randomPerson.patronymic));

        Person otherRandomPerson = Person.getRandomPerson();

        Assert.AreNotSame(randomPerson, otherRandomPerson);

        //TODO:тут случайность может пройти, а может и нет
        Assert.AreNotEqual(randomPerson, otherRandomPerson);
    }

    [Test]
    public void PersonMethodToStringNameTest()
    {
        Person person = Person.getRandomPerson();

        string expectedString = $"{person.name} {person.surname}";

        string actualString = person.toStringName();
        
        Assert.AreEqual(expectedString, actualString);
    }

    [Test]
    public void CrewMemberSetBusyWhenFreeTest()
    {
        CrewMember crewMember = new CrewMember("name", "surname", "patronymic");
        
        Assert.AreEqual(CrewMember.State.Free, crewMember.state);
        
        crewMember.setBusy();
        
        Assert.AreEqual(CrewMember.State.Busy, crewMember.state);
    }
    
    [Test]
    public void CrewMemberSetBusyWhenAlreadyBusyTest()
    {
        CrewMember crewMember = new CrewMember("name", "surname", "patronymic");
        
        crewMember.setBusy();
        
        Assert.AreEqual(CrewMember.State.Busy, crewMember.state);

        Assert.Catch(crewMember.setBusy);
        try
        {
            crewMember.setBusy();
        }
        catch (CrewMember.WrongStateException e)
        {
            string expectedMessage = "Член команды уже занят";
            Assert.AreEqual(expectedMessage, e.Message);
        }
    }
    
    [Test]
    public void CrewMemberSetFreeWhenBusyTest()
    {
        CrewMember crewMember = new CrewMember("name", "surname", "patronymic");
        crewMember.setBusy();
        
        Assert.AreEqual(CrewMember.State.Busy, crewMember.state);
        
        crewMember.setFree();
        
        Assert.AreEqual(CrewMember.State.Free, crewMember.state);
    }
    
    [Test]
    public void CrewMemberSetFreeWhenAlreadyFreeTest()
    {
        CrewMember crewMember = new CrewMember("name", "surname", "patronymic");
        
        Assert.AreEqual(CrewMember.State.Free, crewMember.state);

        Assert.Catch(crewMember.setFree);
        try
        {
            crewMember.setFree();
        }
        catch (CrewMember.WrongStateException e)
        {
            string expectedMessage = "Член команды уже свободен";
            Assert.AreEqual(expectedMessage, e.Message);
        }
    }
}