namespace KMA.ProgrammingInCSharp.Lab1.Navigation
{
    internal interface INavigatable<TObject> where TObject : Enum
    {
        TObject ViewType
        {
            get;
        }
    }
}
