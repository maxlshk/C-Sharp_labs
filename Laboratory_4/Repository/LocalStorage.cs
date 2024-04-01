using System.IO;
using System.Text.Json;
using KMA.ProgrammingInCSharp.Lab4.Models;

namespace KMA.ProgrammingInCSharp.Lab4.Repository;
class LocalStorage
{
    #region Fields
    private List<Person>? _storedUsers;
    // Saved in C:\Users\{User}\AppData\Roaming
    private static readonly string RepoFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Lab4UsersRepo.txt");
    
    private static readonly string RepositoryFile = Path.Combine(RepoFolder, "Users.txt");
    #endregion

    #region Constructor
    internal LocalStorage()
    {
        if (!File.Exists(RepositoryFile))
        {
            if (!Directory.Exists(RepoFolder))
            {
                Directory.CreateDirectory(RepoFolder);
            }
            var random = new Random();
            _storedUsers = new List<Person>();
            // 50 users
            SaveInStorage(); 
        }
        else
        {
            _storedUsers = RetrieveStorageUsers();
        }
    }
    #endregion

    private List<Person>? RetrieveStorageUsers()
    {
        string? stringifiedUsers;
        using (StreamReader sr = new StreamReader(RepositoryFile))
            stringifiedUsers = sr.ReadToEnd();
        
        return JsonSerializer.Deserialize<List<Person>>(stringifiedUsers);
    }

    private void SaveInStorage()
    {
        var stringObj = JsonSerializer.Serialize(_storedUsers);
        using StreamWriter sw = File.CreateText(RepositoryFile);
            sw.Write(stringObj);
    }

    public void EditPerson(Person previos, Person next)
    {
        _storedUsers[_storedUsers.IndexOf(previos)] = next;
    }

    public void AddPerson(Person next)
    {
        _storedUsers.Add(next);
    }

    public void RemovePerson(Person person)
    {
        _storedUsers.Remove(person);
    }

    public List<Person>? PersonsList
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
}
