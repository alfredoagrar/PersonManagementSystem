namespace Managment.core.Database.Interfaces
{
    internal abstract class DatabaseInitializer : IDatabaseInitializer
    {
        public abstract void InitializeDatabase();
    }

    public interface IDatabaseInitializer
    {
        void InitializeDatabase();
    }
}
