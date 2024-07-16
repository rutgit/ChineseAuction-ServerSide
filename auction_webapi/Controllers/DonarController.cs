using auction_webapi.BL;
using auction_webapi.DAL;
using auction_webapi.DTO;
using auction_webapi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace auction_webapi.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class DonorController : ControllerBase
    {

        private IDonorService _DonorService;
        private IMapper _imapper;
        public DonorController(IDonorService DonorService, IMapper imapper)
        {
            this._DonorService = DonorService ?? throw new ArgumentNullException(nameof(DonorService));
            this._imapper = imapper ?? throw new ArgumentNullException(nameof(imapper));
        }

        [HttpGet("GetDonors")]
        public async Task<ActionResult<List<DonorDTO>>> GetDonors()
        {
            List<Donor> Donors = await _DonorService.GetAsync();
            List<DonorDTO> DonorDTOs = _imapper.Map<List<Donor>, List<DonorDTO>>(Donors);

            foreach (Donor Donor in Donors)
            {
                IEnumerable<PresentDTO> presentDTOs = _imapper.Map<ICollection<Present>, IEnumerable<PresentDTO>>(Donor.Presents);
                DonorDTOs.First(d => d.Id == Donor.Id).Presents = presentDTOs.ToList();
            }

            return DonorDTOs;
        }

        [HttpGet("Donor/{id}")]
        public async Task<ActionResult<DonorDTO>> GetDonorById(int id)
        {
            Donor Donors = await _DonorService.GetByIdAsync(id);
            DonorDTO DonorDTOs = _imapper.Map<Donor,DonorDTO>(Donors);
            IEnumerable<PresentDTO> presentDTOs = _imapper.Map<ICollection<Present>, IEnumerable<PresentDTO>>(Donors.Presents);
            DonorDTOs.Presents = presentDTOs.ToList();
            return DonorDTOs;
        }

        [HttpGet("Donor/filter")]
        public async Task<List<DonorDTO>> GetByFilterAsync(string? name, string? lname, int? presentid, string? email)
        {
            List<Donor> Donors = await _DonorService.GetByFilterAsync(name,lname,presentid,email);
            List<DonorDTO> DonorDTOs = _imapper.Map<List<Donor>, List<DonorDTO>>(Donors);

            foreach (Donor Donor in Donors)
            {
                IEnumerable<PresentDTO> presentDTOs = _imapper.Map<ICollection<Present>, IEnumerable<PresentDTO>>(Donor.Presents);
                DonorDTOs.First(d => d.Id == Donor.Id).Presents = presentDTOs.ToList();
            }

            return DonorDTOs;
        }



        [HttpDelete("deleteDonor/{id}")]
        public async Task<ActionResult<string>> DeleteDonor(int id)
        {
            return await _DonorService.DeleteAsync(id);
        }
        [HttpPost("addDonor")]
        public async Task<ActionResult<Donor>> PostDonor([FromBody] DonorDTO Donor)
        {
            try
            {
                Donor d = _imapper.Map<DonorDTO, Donor>(Donor);
                return await _DonorService.PostAsync(d);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPut("updateDonor")]
        public async Task<ActionResult<string>> UpdateDonor([FromBody] DonorDTO Donor)
        {   
            try
            {
                Donor d = _imapper.Map<DonorDTO, Donor>(Donor);
                return await _DonorService.PutAsync(d);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}
