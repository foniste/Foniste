using FonApi.Models.Ventures;

namespace FonApi.Interfaces{
    public interface IVentureDbService
    {
        Task<List<object>> GetAllHeaders();
        void InsertWhenException(List<dynamic> excList);
        bool IsThereException();
        int ControlDatabaseException();
    }
}