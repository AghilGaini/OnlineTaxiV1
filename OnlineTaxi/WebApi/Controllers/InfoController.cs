using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class InfoController : ControllerBase
    {
        private readonly IUnitOfWork _context;

        public InfoController(IUnitOfWork context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<JsonResult> AllUserTypes()
        {

            try
            {
                var res = new ApiResponse();

                var userTypes = await _context._userType.GetAllDTOAsync();

                res.Data = userTypes.ToList();
                res.ResponseCode = 0;
                res.Success = true;

                return new JsonResult(res);
            }
            catch (System.Exception ex)
            {
                return new JsonResult(
                   new ApiResponse()
                   {
                       Data = null,
                       ResponseCode = -1,
                       Success = false,
                       ErrorMessage = ex.Message
                   });
            }


        }
    }
}
