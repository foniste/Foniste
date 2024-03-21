namespace FonApi.Interfaces{
    public interface IVentureDbService
    {
        public Task<List<object>> GetAllHeaders();
        public Task<List<object>> GetAllDetails();
        public void InsertWhenException(List<dynamic> excList);
        public object InsertNewVentureHeader(string header, int targetFund);
        public object InsertNewVentureDetail(int id, string description, int fundAmount, int minInvestValue, int numberOfInvestor);
        public object InsertNewVentureImg(byte[] imgFile, string imgName, int orgId);

    }
}