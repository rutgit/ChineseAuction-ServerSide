using auction_webapi.Models;

namespace auction_webapi.BL
{
    public interface IDonorService
    {
        public Task<List<Donor>> GetAsync();
        public Task<Donor> GetByIdAsync(int id);
        public Task<List<Donor>> GetByFilterAsync(string? name, string? lname, int? presentid, string? email);
        public Task<Donor> PostAsync(Donor d);
        public Task<string> PutAsync(Donor Donor);

        public Task<string> DeleteAsync(int id);

    }
}
