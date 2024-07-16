using auction_webapi.DAL;
using auction_webapi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using static System.Net.Mail.MailAddress;

namespace auction_webapi.BL
{
    public class DonorService:IDonorService
    {
        private IDonorDAL _DonorDAL;
        public DonorService(IDonorDAL DonorDAL)
        {
                this._DonorDAL= DonorDAL ?? throw new ArgumentNullException(nameof(DonorDAL));       
        }
        public Task<List<Donor>> GetAsync()
        {
            return _DonorDAL.GetAsync();
        }
        public Task<Donor> GetByIdAsync(int id)
        {
            return _DonorDAL.GetByIdAsync(id);
        }
        public Task<List<Donor>> GetByFilterAsync(string? name, string? lname, int? presentid, string? email)
        {
            return _DonorDAL.GetByFilterAsync(name, lname,presentid,email);
        }

        public Task<string> DeleteAsync(int id)
        {
            return _DonorDAL.DeleteByIdAsync(id);
        }
        public async Task<Donor> PostAsync(Donor d)
        {

            if (!ValidateEmail(d.Email))
            {
                throw new Exception("invalid email");
            }
            Donor newd = await _DonorDAL.PostAsync(d);


            return newd;

        }
    

        public async Task<string> PutAsync(Donor d)
        {

            if (!ValidateEmail(d.Email))
            {
                throw new Exception("invalid email");
            }
            return await _DonorDAL.PutAsync(d);

        }
        public bool ValidateEmail(string email)
        {
            try
            {
                MailAddress mailAddress = new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
