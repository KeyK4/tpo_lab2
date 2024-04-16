using tpo_lab2.data;
using tpo_lab2.domain;

string filmName = "1+1";
Scenario filmScenario = new Scenario(filmName);
string filmTheme = "Дружба между парнем с улицы и богатым инвалидом";
string filmGenre = "Комедия, драма";
Screenwriter OliverNakash = new Screenwriter("Оливье", "Накаш", "");

IRepository producer = new RepositoryImpl();
producer.writeScenario(filmScenario, OliverNakash, filmGenre, filmTheme);

Director EricTolledano = new Director("Эрик", "Толедано", "");
Dictionary<Personage, Actor> rolePlayers = producer.cast(filmScenario, EricTolledano);

List<CrewMember> crewMembers = new List<CrewMember>
{
    new ("Матьё", "Вадпье", ""),
    new ("Людовико", "Эйнауди", ""),
    new ("Франсуа", "Эммануэлли", ""),
};

Crew crew = producer.crewUp(EricTolledano, OliverNakash, rolePlayers, crewMembers);

Movie OnePlusOne = producer.makeMovie(crew, filmScenario);

Cinema kinomaks = new Cinema("Киномакс", 20);
Cinema petrovski = new Cinema("Синема Парк", 15);
Cinema imperiaGres = new Cinema("Империя Грез", 18);
List<Cinema> cinemas = new List<Cinema>
{
    kinomaks,
    petrovski,
    imperiaGres
};

producer.release(cinemas, OnePlusOne);

string mibName = "Люди в черном";
Scenario mibScenario = new Scenario(mibName);
string mibTheme = "Обычный полицейский становится членом секретной космической организации";
string mibGenre = "Комедия, фентези";
Screenwriter mibScreenwriter = new Screenwriter("Лоуэлл", "Каннингем", "");

producer.writeScenario(mibScenario, mibScreenwriter, mibGenre, mibTheme);

Director mibDirector = new Director("Барри", "Зонненфельд", "");
Dictionary<Personage, Actor> mibRolePlayers = producer.cast(mibScenario, mibDirector);

List<CrewMember> mibCrewMembers = new List<CrewMember>
{
    new ("Дональд", "Питермен", ""),
    new ("Дэнни", "Элфман", ""),
    new ("Бо", "Уэлш", ""),
};

Crew mibCrew = producer.crewUp(mibDirector, mibScreenwriter, mibRolePlayers, mibCrewMembers);

Movie MenInBlack = producer.makeMovie(mibCrew, mibScenario);

producer.release(cinemas, MenInBlack);

foreach (var movie in imperiaGres.movies)
{
    Console.WriteLine(movie.scenario.name);
}

Console.WriteLine("///////////////////////////////////////////////////////");
Console.WriteLine(OnePlusOne.crew.getСredits());

Console.WriteLine("///////////////////////////////////////////////////////");
Console.WriteLine(MenInBlack.crew.getСredits());