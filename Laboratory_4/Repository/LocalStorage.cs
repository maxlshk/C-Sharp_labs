using System.IO;
using System.Text.Json;
using KMA.ProgrammingInCSharp.Lab4.Models;

namespace KMA.ProgrammingInCSharp.Lab4.Repository;
class LocalStorage
{
    #region Fields
    private List<Person> _storedUsers;
    private static readonly string RepoFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Lab4Users");
    private static readonly string RepositoryFile = Path.Combine(RepoFolder, "Users.txt");
    #endregion

    #region Properties
    public List<Person> PersonsList
    {
        get
        {
            return _storedUsers.ToList();

        }
        set
        {
            _storedUsers = value;

        }
    }
    #endregion

    #region Constructor
    internal LocalStorage()
    {
        try
        {
            _storedUsers = RetrieveStorageUsers();
        }
        catch
        {
            EnsureDirectoryExists(RepoFolder);
            _storedUsers = FillUsers();
            SaveInStorage();
        }
    }
    #endregion

    #region Methods
    private List<Person> RetrieveStorageUsers()
    {
        using StreamReader sr = new StreamReader(RepositoryFile);
        var stringifiedUsers = sr.ReadToEnd();

        var users = JsonSerializer.Deserialize<List<Person>>(stringifiedUsers);
        if (users == null) throw new InvalidOperationException("Deserialized users are null.");

        return users;
    }
    
    private List<Person> FillUsers(){
        var random = new Random();
        List<Person> users = new List<Person>();

        for (int i = 1; i < 51; i++)
        {
            var name = "Name_" + i;
            var surname = "Surname_" + i;
            var email = "user.email." + i + "@gmail.com";
            var birthDay = new DateTime(random.Next(1940, 2021), random.Next(1, 13), random.Next(1, 27));
            var person = new Person(name, surname, email, birthDay);
            users.Add(person);
        }

        return users;
    }

    private void EnsureDirectoryExists(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }

    private void SaveInStorage()
    {
        var stringObj = JsonSerializer.Serialize(_storedUsers);
        using StreamWriter sw = File.CreateText(RepositoryFile);
        sw.Write(stringObj);
    }
    #endregion
    
    public void EditPerson(Person previos, Person next)
    {
        _storedUsers[_storedUsers.IndexOf(previos)] = next;
        SaveInStorage();
    }

    public void AddPerson(Person next)
    {
        _storedUsers.Add(next);
        SaveInStorage();
    }

    public void RemovePerson(Person person)
    {
        _storedUsers.Remove(person);
        SaveInStorage();
    }
}
