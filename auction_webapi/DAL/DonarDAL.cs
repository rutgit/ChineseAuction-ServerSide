using auction_webapi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace auction_webapi.DAL
{
    public class DonorDAL : IDonorDAL
    {
        private AuctionContext _auctionContext;
        public DonorDAL(AuctionContext auctionContext)
        {
            this._auctionContext = auctionContext ?? throw new ArgumentNullException(nameof(auctionContext));
        }
        public async Task<List<Donor>> GetAsync()
        {
            try { 
            return await _auctionContext.Donors
                    .Include(d => d.Presents)
                     .ToListAsync();
            }
            catch(Exception ex)
            {
                throw new Exception("can't get");
            }
        }
        public async Task<Donor> GetByIdAsync(int id)
        {
            return await _auctionContext.Donors
                .Include(d => d.Presents) 
                .FirstOrDefaultAsync(d => d.Id == id);
        }
        public async Task<List<Donor>> GetByFilterAsync(string? name, string? lname, int? presentid, string? email)
        {
            var query = _auctionContext.Donors.Where(Donor =>
             (name == null && lname==null ? (true) : (Donor.FirstName.Contains(name)&& Donor.LastName.Contains(lname)))
             && ((email == null) ? (true) : (Donor.Email == email))
             && ((presentid == null) ? (true) : (Donor.Presents.Any(p => p.PresentId == presentid) ))
             );
            List<Donor> Donors = await query.ToListAsync();
            return Donors;
        }
        public async Task<Donor> PostAsync(Donor d)
        {
            try
            {
                await _auctionContext.Donors.AddAsync(d);
                await _auctionContext.SaveChangesAsync();
                return d;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while saving the Donor entity.", ex);
            }
        }
        public async Task<string> PutAsync(Donor d)
        {
            _auctionContext.Donors.Update(d);
            await _auctionContext.SaveChangesAsync();
            return $"Donor {d.FirstName} updated";
        }
        public async Task<string> DeleteByIdAsync(int id)
        {
            Donor d = await _auctionContext.Donors.FindAsync(id);
             _auctionContext.Donors.Remove(d);
            await _auctionContext.SaveChangesAsync();
            return $" Donor {d.FirstName} was Deleted";

        }






    }
}
