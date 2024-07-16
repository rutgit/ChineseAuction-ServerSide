using auction_webapi.DAL;
using auction_webapi.Models;
using System.Net.Mail;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using System.Net;

namespace auction_webapi.BL
{
    public class RaffleService:IRaffleService

    {
        private IRaffleDAL _raffleDAL;
        public RaffleService(IRaffleDAL raffleDAL)
        {
            this._raffleDAL = raffleDAL ?? throw new ArgumentNullException(nameof(raffleDAL));
        }

        public async Task<Winner> Raffle(Present p)
        {
            Winner winner = await _raffleDAL.Raffle(p);

            //// send email using SMTP client
            //var email = winner.Email;
            //if (!string.IsNullOrEmpty(email))
            //{
            //    var fromAddress = new MailAddress("your-email@example.com", "Your Name");
            //    var toAddress = new MailAddress(email, winner.User.FirstName);
            //    const string fromPassword = "your-password";
            //    const string subject = "Congratulations! You are the winner!";
            //    const string body = "Dear " + winner.User.FirstName + ", you have won the raffle!";

            //    using (var smtpClient = new SmtpClient
            //    {
            //        Host = "smtp.example.com", // replace with your SMTP server address
            //        Port = 587, // replace with your SMTP server port
            //        EnableSsl = true, // replace with true if your SMTP server requires SSL
            //        DeliveryMethod = SmtpDeliveryMethod.Network,
            //        UseDefaultCredentials = false,
            //        Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            //    })
            //    {
            //        using (var mailMessage = new MailMessage(fromAddress, toAddress)
            //        {
            //            Subject = subject,
            //            Body = body
            //        })
            //        {
            //            await smtpClient.SendMailAsync(mailMessage);
            //        }
            //    }
            //}

            return winner;
        }
        public async Task<string> Results()
        {
            return await _raffleDAL.Results();
        }
        public async Task<string> Income()
        {
            return await _raffleDAL.Income();
        }
    }
}
