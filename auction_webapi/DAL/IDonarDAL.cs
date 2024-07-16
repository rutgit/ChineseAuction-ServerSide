using auction_webapi.Models;

namespace auction_webapi.DAL
{
    public interface IDonorDAL
    {
        public Task<List<Donor>> GetAsync();
        public  Task<Donor> GetByIdAsync(int id);
        public Task<List<Donor>> GetByFilterAsync(string? name, string? lname, int? presentid, string? email);
        public  Task<Donor> PostAsync(Donor d);
        public  Task<string> PutAsync(Donor d);
        public Task<string> DeleteByIdAsync(int id);
    }
}
