using FonApi.Database;
using FonApi.Interfaces;

namespace FonApi.Service {
    public class VentureDbService : IVentureDbService {
        private readonly VenturesDbContext _venturesDbContext;

        //Constructor
        public VentureDbService(VenturesDbContext venturesDbContext) {
            _venturesDbContext = venturesDbContext ?? throw new ArgumentNullException(nameof(venturesDbContext));
            //_venturesDbContext boş gelmesi durumunda exception
            
        }
        //

    }
}
