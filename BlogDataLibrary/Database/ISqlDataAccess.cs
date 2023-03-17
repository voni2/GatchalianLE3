namespace BlogDataLibrary.Database
{
    public interface ISqlDataAccess
    {
        List<T> LoadData<T, U>(string sqlStatement, U parameters, string connectStringName, bool isStoredProcedure);
        void SaveData<T>(string sqlStatement, T parameters, string connectStringName, bool isStoredProcedure);
    }
}