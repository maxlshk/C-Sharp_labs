namespace KMA.ProgrammingInCSharp.Lab4.Repository;

internal class StorageManager
{
    internal static void Instance(LocalStorage storage)
    {
        _storage = storage;
    }
    
    private static LocalStorage _storage;

    internal static LocalStorage Storage
    {
        get { return _storage; }
    }
}