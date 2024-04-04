namespace KMA.ProgrammingInCSharp.Lab4.Repository;

internal class StorageManager
{
    public static event EventHandler StorageUpdated;
    
    private static LocalStorage _storage;

    internal static LocalStorage Storage
    {
        get { return _storage; }
    }
    public static void NotifyStorageUpdated()
    {
        StorageUpdated?.Invoke(null, EventArgs.Empty);
    }
    internal static void Instance(LocalStorage storage)
    {
        _storage = storage;
    }
}