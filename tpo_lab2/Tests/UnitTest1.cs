using System.Reflection.Metadata.Ecma335;
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
    public void PersonGetRandomPersonMethodTest()
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
    public void PersonToStringNameMethodTest()
    {
        Person person = Person.getRandomPerson();

        string expectedString = $"{person.name} {person.surname}";

        string actualString = person.toStringName();
        
        Assert.AreEqual(expectedString, actualString);
    }

    [Test]
    public void CrewMemberSetBusyWhenFreeMethodTest()
    {
        CrewMember crewMember = new CrewMember("name", "surname", "patronymic");
        
        Assert.AreEqual(CrewMember.State.Free, crewMember.state);
        
        crewMember.setBusy();
        
        Assert.AreEqual(CrewMember.State.Busy, crewMember.state);
    }
    
    [Test]
    public void CrewMemberSetBusyWhenAlreadyBusyMethodTest()
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
    public void CrewMemberSetFreeWhenBusyMethodTest()
    {
        CrewMember crewMember = new CrewMember("name", "surname", "patronymic");
        crewMember.setBusy();
        
        Assert.AreEqual(CrewMember.State.Busy, crewMember.state);
        
        crewMember.setFree();
        
        Assert.AreEqual(CrewMember.State.Free, crewMember.state);
    }
    
    [Test]
    public void CrewMemberSetFreeWhenAlreadyFreeMethodTest()
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

    [Test]
    public void DirectorCastMethodTest()
    {
        Director director = new Director("n", "s", "p");
        Personage personage = new Personage("n", "s", "p");

        Actor actor = director.cast(personage);
        
        Assert.True(Person.posibleNames.Contains(actor.name));
        Assert.True(Person.posibleSurname.Contains(actor.surname));
        Assert.True(Person.posiblePatronymics.Contains(actor.patronymic));
    }

    [Test]
    public void CinemaAddMovieMethodTest()
    {
        Scenario scenario = new Scenario("name");
        Movie movie = new Movie(scenario);
        string cinemaName = "cinemaName";
        uint maxNumberOfFilms = 20;
        Cinema cinema = new Cinema(cinemaName, maxNumberOfFilms);
        
        Assert.False(cinema.movies.Contains(movie));
        cinema.addMovie(movie);
        Assert.True(cinema.movies.Contains(movie));
    }

    [Test]
    public void CinemaAddMovieWithOverloadMethodTest()
    {
        Scenario scenario = new Scenario("name");
        Movie movie = new Movie(scenario);
        string cinemaName = "cinemaName";
        uint maxNumberOfFilms = 5;
        Cinema cinema = new Cinema(cinemaName, maxNumberOfFilms);

        var methodCall = () => { cinema.addMovie(movie);};
        
        for (int i = 0; i < maxNumberOfFilms; i++)
        {
            methodCall();
        }

        TestDelegate testDelegate = new TestDelegate(methodCall);

        Assert.Catch(testDelegate);
        try
        {
            methodCall();
        }
        catch (Exception e)
        {
            Assert.AreEqual("Превышено максимальное количество фильмов в кинотеатре", e.Message);
        }
    }

    [Test]
    public void CinemaSetClosedWhenOpenMethodTest()
    {
        string cinemaName = "cinemaName";
        uint maxNumberOfFilms = 5;
        Cinema cinema = new Cinema(cinemaName, maxNumberOfFilms);
        
        Assert.AreEqual(Cinema.CinemaState.Open, cinema.state);
        
        cinema.setClosed();
        
        Assert.AreEqual(Cinema.CinemaState.Closed, cinema.state);
    }
    
    [Test]
    public void CinemaSetClosedWhenAlreadyClosedMethodTest()
    {
        string cinemaName = "cinemaName";
        uint maxNumberOfFilms = 5;
        Cinema cinema = new Cinema(cinemaName, maxNumberOfFilms);
        
        cinema.setClosed();
        
        Assert.AreEqual(Cinema.CinemaState.Closed, cinema.state);

        Assert.Catch(cinema.setClosed);
        try
        {
            cinema.setClosed();
        }
        catch (Cinema.WrongStateException e)
        {
            string expectedMessage = "Кинотеатр уже закрыт";
            Assert.AreEqual(expectedMessage, e.Message);
        }
    }
    
    [Test]
    public void CinemaSetOpenWhenClosedMethodTest()
    {
        string cinemaName = "cinemaName";
        uint maxNumberOfFilms = 5;
        Cinema cinema = new Cinema(cinemaName, maxNumberOfFilms);
        cinema.setClosed();
        
        Assert.AreEqual(Cinema.CinemaState.Closed, cinema.state);
        
        cinema.setOpen();
        
        Assert.AreEqual(Cinema.CinemaState.Open, cinema.state);
    }
    
    [Test]
    public void CinemaSetOpenWhenAlreadyOpenMethodTest()
    {
        string cinemaName = "cinemaName";
        uint maxNumberOfFilms = 5;
        Cinema cinema = new Cinema(cinemaName, maxNumberOfFilms);
        
        Assert.AreEqual(Cinema.CinemaState.Open, cinema.state);

        Assert.Catch(cinema.setOpen);
        try
        {
            cinema.setOpen();
        }
        catch (Cinema.WrongStateException e)
        {
            string expectedMessage = "Кинотеатр уже открыт";
            Assert.AreEqual(expectedMessage, e.Message);
        }
    }

    [Test]
    public void CrewGetCreditsMethodTest()
    {
        String name = "name";
        String surname = "surname";
        String patronymic = "patronymic";

        //TODO: Переделать на константы
        
        Director director = new Director("Режиссер", "Режиссеров", "Режиссерович");
        Screenwriter screenwriter = new Screenwriter("Сценарист", "Сценаристов", "Сценарстович");
        Dictionary<Personage, Actor> roles = new Dictionary<Personage, Actor>()
        {
            {new Personage("Персонаж", "Первый", "Pers"),
                new Actor("Актер", "Первый", "Act")},
            {new Personage("Персонаж", "Второй", "Pers"),
                new Actor("Актер", "Второй", "Act")}
        };
        List<CrewMember> crewMembers = new List<CrewMember>()
        {
            new ("Оператор", "Операторов", "Операторович"),
            new ("Звукорежиссер", "Звукорежиссеров", "Звукорежиссерович"),
        };

        Crew crew = new Crew(director, screenwriter, roles, crewMembers);

        string expectedCredits = "Режиссер: Режиссер Режиссеров\n" + 
                                "Автор сценария: Сценарист Сценаристов\n" +
                                "В ролях: \n" +
                                "Персонаж Первый: Актер Первый\n" +
                                "Персонаж Второй: Актер Второй\n" +
                                "Съемочная группа: \n" +
                                "Оператор Операторов\n" +
                                "Звукорежиссер Звукорежиссеров\n";

        string actualCredits = crew.getСredits();
        
        Assert.AreEqual(expectedCredits, actualCredits);
    }

    [Test]
    public void ScenarioWriteMethodTest()
    {
        string name = "Some Movie 2";
        Scenario scenario = new Scenario(name);
        scenario.theme = "theme";
        scenario.genre = "genre";
        scenario.screenwriter = new Screenwriter("n", "s", "p");
        scenario.write();
        Assert.AreEqual(Scenario.ScenarioState.Finished, scenario.state);
        Assert.True(scenario.personages.Count != 0);
    }

    [Test]
    public void ScenarioWriteWithoutThemeMethodTest()
    {
        string name = "Some Movie 2";
        Scenario scenario = new Scenario(name);
        scenario.genre = "genre";
        scenario.screenwriter = new Screenwriter("n", "s", "p");
        Assert.Catch(scenario.write);
        try
        {
            scenario.write();
        }
        catch (MissingFieldException e)
        {
            string expectedMessage = "Theme is null!";
            
            Assert.AreEqual(expectedMessage, e.Message);
        }
    }
    
    [Test]
    public void ScenarioWriteWithoutGenreMethodTest()
    {
        string name = "Some Movie 2";
        Scenario scenario = new Scenario(name);
        scenario.theme = "theme";
        scenario.screenwriter = new Screenwriter("n", "s", "p");
        Assert.Catch(scenario.write);
        try
        {
            scenario.write();
        }
        catch (MissingFieldException e)
        {
            string expectedMessage = "Genre is null!";
            
            Assert.AreEqual(expectedMessage, e.Message);
        }
    }
    
    [Test]
    public void ScenarioWriteWithoutScreenwriterMethodTest()
    {
        string name = "Some Movie 2";
        Scenario scenario = new Scenario(name);
        scenario.theme = "theme";
        scenario.genre = "genre";
        Assert.Catch(scenario.write);
        try
        {
            scenario.write();
        }
        catch (MissingFieldException e)
        {
            string expectedMessage = "Screenwriter is null!";
            
            Assert.AreEqual(expectedMessage, e.Message);
        }
    }

    [Test]
    public void ScenarioGeneratePersonages()
    {
        string name = "Some Movie 2";
        Scenario scenario = new Scenario(name);
        int maxCountPers = 100;
        int minCountPers = 30;
        
        List<Personage> actualPersonages = scenario.generatePersonages();
        int personagesCount = actualPersonages.Count;
        
        Assert.Less(minCountPers, personagesCount);
        Assert.Greater(maxCountPers, personagesCount);

        Personage firstPersonage = actualPersonages[0];
        int firstPersonageCount = actualPersonages.Count(personage => { return personage == firstPersonage; });
        Assert.Less(firstPersonageCount, personagesCount);
    }

    [Test]
    public void MovieFilmMethodTest()
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
        
        movie.film();

        int maxExpectedDuration = 180;
        int minExpectedDuration = 20;
        Movie.MovieState expectedState = Movie.MovieState.Finished;
        
        Assert.GreaterOrEqual(maxExpectedDuration, movie.duration);
        Assert.LessOrEqual(minExpectedDuration, movie.duration);
        Assert.AreEqual(expectedState, movie.state);
    }

    [Test]
    public void MovieFilmWithoutCrewMethodTest()
    {
        Scenario scenario = new Scenario("name");

        Movie movie = new Movie(scenario);

        Assert.Catch(movie.film);

        try
        {
            movie.film();
        }
        catch (MissingFieldException e)
        {
            string expectedMessage = "Crew is null!";
            string actualMessage = e.Message;
            
            Assert.AreEqual(expectedMessage, actualMessage);
        }
    }
}