namespace tpo_lab2.domain;

public class Person
{
    public string name { get; }
    public string surname { get; }
    public string patronymic { get; }

    public Person(string name, string surname, string patronymic)
    {
        this.name = name;
        this.surname = surname;
        this.patronymic = patronymic;
    }

    public string toStringName()
    {
        return $"{name} {surname}";
    }

    public static List<string> posibleNames = new()
    {
        "Виктор",
        "Сергей",
        "Анатолий",
        "Андрей",
        "Артем",
        "Александр",
        "Николай",
        "Роман",
        "Глеб",
        "Марат",
    };
        
    public static List<string> posiblePatronymics = new()
    {
        "Викторович",
        "Сергеевич",
        "Анатольевич",
        "Андреевич",
        "Артемьевич",
        "Александрович",
        "Николаевич",
        "Романович",
        "Глебович",
        "Маратович",
    };
        
    public static List<string> posibleSurname = new()
    {
        "Иванов",
        "Сидоров",
        "Петров",
        "Бутусов",
        "Хлебников",
        "Мельников",
        "Кирпичев",
        "Лугачев",
        "Сергеев",
        "Гареев",
    };
        
        
    public static Person getRandomPerson()
    {
        var randomName = posibleNames[Random.Shared.Next(0, posibleNames.Count - 1)];
        var randomSurname = posibleSurname[Random.Shared.Next(0, posibleSurname.Count - 1)];
        var randomPatronymic = posiblePatronymics[Random.Shared.Next(0, posiblePatronymics.Count - 1)];
        return new Person(randomName, randomSurname, randomPatronymic);
    }
}