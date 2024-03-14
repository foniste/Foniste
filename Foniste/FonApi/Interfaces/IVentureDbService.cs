using FonApi.Models.Ventures;

namespace FonApi.Interfaces{
    public interface IVentureDbService{
        Task<List<object>> GetAllHeaders();
        //Task<List<VenturesDetail>> GetAllDetails();
        //void InsertNewVentureHeader(VenturesHeader venturesHeader);
        //void InsertNewVentureDetails(VenturesDetail venturesDetail);
    }
}