namespace Managment.core.Database.Interfaces
{
    internal abstract class DatabaseInitializer : IDatabaseInitializer
    {
        public abstract void InitializeDatabase();
    }

    internal interface IDatabaseInitializer
    {
        void InitializeDatabase();
    }
}
