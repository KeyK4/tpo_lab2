namespace Tests;
using tpo_lab2.domain;
    
public class Tests
{
    [Test]
    public void PersonMethodGetFullNameTest()
    {
        //setUP
        Person person = new Person();
        person.name = "name";
        person.surname = "surname";
        person.patronymic = "patronymic";
        
        //getting result 
        string resultFullName = person.getFullName();
        
        //expected result
        string expectedResult = "name surname patronymic";
        
        Assert.AreEqual(resultFullName, expectedResult);
    }
}