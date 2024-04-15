using System.Reflection.Metadata.Ecma335;
using NuGet.Frameworks;
using tpo_lab2.data;

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

        Personage.PersState expectedState = Personage.PersState.OffCamera;
        
        Assert.AreEqual(personageFromBaseConstructor.name, name);
        Assert.AreEqual(personageFromBaseConstructor.surname, surname);
        Assert.AreEqual(personageFromBaseConstructor.patronymic, patronymic);
        Assert.AreEqual(expectedState, personageFromBaseConstructor.state);
        
        Assert.AreEqual(personageFromPerson.name, name);
        Assert.AreEqual(personageFromPerson.surname, surname);
        Assert.AreEqual(personageFromPerson.patronymic, patronymic);
        Assert.AreEqual(expectedState, personageFromPerson.state);
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
        Assert.AreEqual(scenario.state, Scenario.ScenarioState.NotReady);

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

        Crew crew = new Crew();
        
        Assert.AreEqual(Crew.CrewState.NotCompleted, crew.state);
        
        crew.crewUp(director, screenwriter, roles, crewMembers);
        
        Assert.AreEqual(crew.director , director);
        Assert.AreEqual(crew.screenwriter , screenwriter);
        Assert.AreEqual(crew.roles , roles);
        Assert.AreEqual(crew.crewMembers , crewMembers);
        Assert.AreEqual(Crew.CrewState.Completed, crew.state);
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
        Crew crew = new Crew();
        crew.crewUp(director, screenwriter, roles, crewMembers);

        Movie movie = new Movie(scenario);
        movie.crew = crew;
        
        Assert.AreEqual(crew, movie.crew);
        Assert.AreEqual(scenario, movie.scenario);
        Assert.AreEqual(Movie.MovieState.NotReady, movie.state);
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
    public void PersonageChangeStateMethodTest()
    {
        String name = "name";
        String surname = "surname";
        String patronymic = "patronymic";

        Personage personage = new Personage(name, surname, patronymic);
        Assert.AreEqual(Personage.PersState.OffCamera, personage.state);
        
        personage.changeState();
        
        Assert.AreEqual(Personage.PersState.OnCamera, personage.state);
        
        personage.changeState();
        
        Assert.AreEqual(Personage.PersState.OffCamera, personage.state);
    }

    [Test]
    public void PersonageSetOffCameraStateMethodTest()
    {
        String name = "name";
        String surname = "surname";
        String patronymic = "patronymic";

        Personage personage = new Personage(name, surname, patronymic);
        Assert.AreEqual(Personage.PersState.OffCamera, personage.state);
        
        personage.changeState();
        
        Assert.AreEqual(Personage.PersState.OnCamera, personage.state);
        
        personage.setOffCameraState();
        
        Assert.AreEqual(Personage.PersState.OffCamera, personage.state);
        
        personage.setOffCameraState();
        
        Assert.AreEqual(Personage.PersState.OffCamera, personage.state);
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
    public void CrewCrewUpMethodTest()
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

        Crew crew = new Crew();
        crew.crewUp(director, screenwriter, roles, crewMembers);
        
        Assert.AreEqual(director, crew.director);
        Assert.AreEqual(screenwriter, crew.screenwriter);
        Assert.AreEqual(roles, crew.roles);
        Assert.AreEqual(crewMembers, crew.crewMembers);
        Assert.AreEqual(Crew.CrewState.Completed, crew.state);
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

        Crew crew = new Crew();
        crew.crewUp(director, screenwriter, roles, crewMembers);

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
    public void CrewSetBusyMethodTest()
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
            new("Оператор", "Операторов", "Операторович"),
            new("Звукорежиссер", "Звукорежиссеров", "Звукорежиссерович"),
        };
        Crew crew = new Crew();
        crew.crewUp(director, screenwriter, roles, crewMembers);
        
        crew.setBusy();
        
        foreach (var crewMember in crew.crewMembers)
        {
            Assert.AreEqual(CrewMember.State.Busy, crewMember.state);
        }
        foreach (var person2actor in roles)
        {
            var actor = person2actor.Value;
            Assert.AreEqual(CrewMember.State.Busy, actor.state);
        }
        Assert.AreEqual(CrewMember.State.Busy, director.state);
    }

    [Test]
    public void CrewSetBusyWithNotCompletedCrewMethodTest()
    {
        Crew crew = new Crew();
        Assert.Catch(crew.setBusy);
        
        try
        {
            crew.setBusy();
        }
        catch (Exception e)
        {
            string expectedExceptionMessage = "Команда не была собрана";
            string actualExceptionMessage = e.Message;
            
            Assert.AreEqual(expectedExceptionMessage, actualExceptionMessage);
        }
    }

    [Test]
    public void CrewDisbandMethodTest()
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

        Crew crew = new Crew();
        crew.crewUp(director, screenwriter, roles, crewMembers);
        
        crew.setBusy();

        Assert.AreEqual(Crew.CrewState.Completed, crew.state);
        crew.disband();
        Assert.AreEqual(Crew.CrewState.Disbanded, crew.state);
    }

    [Test]
    public void CrewDisbandErrorMethodTest()
    {
        Crew crew = new Crew();
        Assert.Catch(crew.disband);
        
        try
        {
            crew.disband();
        }
        catch (Exception e)
        {
            string expectedExceptionMessage = "Команда не была собрана";
            string actualExceptionMessage = e.Message;
            
            Assert.AreEqual(expectedExceptionMessage, actualExceptionMessage);
        }
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
        string scenName = "Some Movie 2";
        Scenario scenario = new Scenario(scenName);
        string theme = "theme";
        string genre = "genre";
        Screenwriter screenwriter = new Screenwriter("n", "s", "p");
        IRepository repository = new RepositoryImpl();
        repository.writeScenario(scenario, screenwriter, genre, theme);
        String name = "name";
        String surname = "surname";
        String patronymic = "patronymic";
        Director director = new Director(name+"Dir", surname+"Dir", patronymic+"Dir");
        Dictionary<Personage, Actor> roles = repository.cast(scenario, director);
        List<CrewMember> crewMembers = new List<CrewMember>();
        Crew crew = new Crew();
        crew.crewUp(director, screenwriter, roles, crewMembers);

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

    [Test]
    public void MovieFilmWithDifferentPersonagesMethodTest()
    {
        string scenName = "Some Movie 2";
        Scenario scenario = new Scenario(scenName);
        scenario.theme = "theme";
        scenario.genre = "genre";
        scenario.screenwriter = new Screenwriter("n", "s", "p");
        scenario.write();
        String name = "name";
        String surname = "surname";
        String patronymic = "patronymic";
        Director director = new Director(name+"Dir", surname+"Dir", patronymic+"Dir");
        Screenwriter screenwriter = new Screenwriter(name+"SW", surname+"SW", patronymic+"SW");
        Dictionary<Personage, Actor> roles = new Dictionary<Personage, Actor>();
        List<CrewMember> crewMembers = new List<CrewMember>();
        Crew crew = new Crew();
        crew.crewUp(director, screenwriter, roles, crewMembers);

        Scenario secondScenario = new Scenario("Some Movie 2");
        secondScenario.theme = "theme";
        secondScenario.genre = "genre";
        secondScenario.screenwriter = new Screenwriter("n", "s", "p");
        secondScenario.write();

        while (scenario.personages == secondScenario.personages)
        {
            secondScenario.write();
        }
        
        Movie movie = new Movie(secondScenario);
        movie.crew = crew;

        Assert.Catch(movie.film);
        
        try
        {
            movie.film();
        }
        catch (Exception e)
        {
            string expectedMessage = "Персонажи команды и сценария не совпадают";
            string actualMessage = e.Message;
            
            Assert.AreEqual(expectedMessage, actualMessage);
        }
        
    }

    [Test]
    public void MovieSetReleasedMethodTest()
    {
        string scenName = "Some Movie 2";
        Scenario scenario = new Scenario(scenName);
        scenario.theme = "theme";
        scenario.genre = "genre";
        scenario.screenwriter = new Screenwriter("n", "s", "p");
        scenario.write();
        String name = "name";
        String surname = "surname";
        String patronymic = "patronymic";
        Director director = new Director(name+"Dir", surname+"Dir", patronymic+"Dir");
        Screenwriter screenwriter = new Screenwriter(name+"SW", surname+"SW", patronymic+"SW");
        Dictionary<Personage, Actor> roles = new Dictionary<Personage, Actor>();
        List<CrewMember> crewMembers = new List<CrewMember>();
        Crew crew = new Crew();
        crew.crewUp(director, screenwriter, roles, crewMembers);

        Movie movie = new Movie(scenario);
        movie.crew = crew;
        
        movie.film();
        
        movie.setReleased();

        Movie.MovieState expectedState = Movie.MovieState.Released;
        
        Movie.MovieState actualState = movie.state;
        
        Assert.AreEqual(expectedState, actualState);
    }

    [Test]
    public void RepositoryWriteScenarioMethodTest()
    {
        IRepository repository = new RepositoryImpl();
        
        string name = "Some Movie 2";
        Scenario scenario = new Scenario(name);
        
        string theme = "theme";
        string genre = "genre";
        Screenwriter screenwriter = new Screenwriter("n", "s", "p");
        
        repository.writeScenario(scenario, screenwriter, genre, theme);
        
        Assert.AreEqual(theme, scenario.theme);
        Assert.AreEqual(genre, scenario.genre);
        Assert.AreEqual(screenwriter, scenario.screenwriter);
        Assert.AreEqual(Scenario.ScenarioState.Finished, scenario.state);
        Assert.True(scenario.personages.Count != 0);
    }

    [Test]
    public void RepositoryCastMethodTest()
    {
        IRepository repository = new RepositoryImpl();
        
        string name = "Some Movie 2";
        Scenario scenario = new Scenario(name);
        
        string theme = "theme";
        string genre = "genre";
        Screenwriter screenwriter = new Screenwriter("n", "s", "p");
        
        repository.writeScenario(scenario, screenwriter, genre, theme);
        Director director = new Director("n", "s", "p");

        Dictionary<Personage, Actor> roles = repository.cast(scenario, director);
        
        Assert.Greater(roles.Count, 0);
        foreach (var actor in roles.Values)
        {
            Assert.True(Person.posibleNames.Contains(actor.name));
            Assert.True(Person.posibleSurname.Contains(actor.surname));
            Assert.True(Person.posiblePatronymics.Contains(actor.patronymic));
        }
    }

    [Test]
    public void RepositoryCrewUpMethodTest()
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
        
        IRepository repository = new RepositoryImpl();

        Crew crew = repository.crewUp(director, screenwriter, roles, crewMembers);
        
        Assert.AreEqual(director, crew.director);
        Assert.AreEqual(screenwriter, crew.screenwriter);
        Assert.AreEqual(roles, crew.roles);
        Assert.AreEqual(crewMembers, crew.crewMembers);
        Assert.AreEqual(Crew.CrewState.Completed, crew.state);
    }

    [Test]
    public void RepositoryMakeMovieMethodTest()
    {
        string scenName = "Some Movie 2";
        Scenario scenario = new Scenario(scenName);
        scenario.theme = "theme";
        scenario.genre = "genre";
        scenario.screenwriter = new Screenwriter("n", "s", "p");
        scenario.write();
        String name = "name";
        String surname = "surname";
        String patronymic = "patronymic";
        Director director = new Director(name+"Dir", surname+"Dir", patronymic+"Dir");
        Screenwriter screenwriter = new Screenwriter(name+"SW", surname+"SW", patronymic+"SW");
        Dictionary<Personage, Actor> roles = new Dictionary<Personage, Actor>();
        List<CrewMember> crewMembers = new List<CrewMember>();
        Crew crew = new Crew();
        crew.crewUp(director, screenwriter, roles, crewMembers);

        IRepository repository = new RepositoryImpl();

        Movie movie = repository.makeMovie(crew, scenario);

        int maxExpectedDuration = 180;
        int minExpectedDuration = 20;
        Movie.MovieState expectedState = Movie.MovieState.Finished;
        
        Assert.GreaterOrEqual(maxExpectedDuration, movie.duration);
        Assert.LessOrEqual(minExpectedDuration, movie.duration);
        Assert.AreEqual(expectedState, movie.state);
    }

    [Test]
    public void RepositoryReleaseMethodTest()
    {
        Scenario scenario = new Scenario("name");
        Movie movie = new Movie(scenario);
        string cinemaName = "cinemaName";
        uint maxNumberOfFilms = 20;
        Cinema cinema = new Cinema(cinemaName, maxNumberOfFilms);
        Cinema cinema2 = new Cinema(cinemaName, maxNumberOfFilms);
        Cinema cinema3 = new Cinema(cinemaName, maxNumberOfFilms);

        List<Cinema> cinemas = new List<Cinema>() { cinema, cinema2, cinema3 };
        
        foreach (var cinemaV in cinemas)
        {
            Assert.False(cinemaV.movies.Contains(movie));
        }
        
        IRepository repository = new RepositoryImpl();
        repository.release(cinemas, movie);

        foreach (var cinemaV in cinemas)
        {
            Assert.True(cinemaV.movies.Contains(movie));
        }
        Assert.AreEqual(Movie.MovieState.Released, movie.state);
    }

    [Test]
    public void FirstSystemStatesTest()
    {
        //1 -> 11 ->  13
        
    }
    
    [Test]
    public void SecondSystemStatesTest()
    {
        //1 -> 2 -> 3 -> 11 …
        
    }
    
    [Test]
    public void ThirdSystemStatesTest()
    {
        //1 -> 2 -> 3 -> 4 -> 5 -> 11…
        
    }
    
    [Test]
    public void FourthSystemStatesTest()
    {
        //1 -> 2 -> 3 -> 4 -> 5 -> 6 -> 11 …
        
    }
    
    [Test]
    public void FifthSystemStatesTest()
    {
        //1 -> 2 -> 3 -> 4 -> 5 -> 6 -> 7 -> 8 -> 11…
        
    }
    
    [Test]
    public void SixthSystemStatesTest()
    {
        //1 -> 2 -> 3 -> 4 -> 5 -> 6 -> 7 -> 8 -> 7 …
        
    }
    
    [Test]
    public void SeventhSystemStatesTest()
    {
        //1 -> 2 -> 3 -> 4 -> 5 -> 6 -> 7 -> 8 -> 9 -> 10 -> 11…
        
    }
    
    [Test]
    public void EightSystemStatesTest()
    {
        //1 -> 2 -> 3 -> 4 -> 5 -> 6 -> 7 -> 8 -> 9 -> 10 -> 12 -> 13
        
    }
}