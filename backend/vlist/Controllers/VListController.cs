using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Globalization;
using System.Net;
using vlist.Data;
using vlist.Models.VList;
using vlist.Validation;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace vlist.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VListController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IRepo _vListRepo;
        public VListController(ILogger<VListController> logger, IRepo vListRepo)
        {
            _logger = logger;
            _vListRepo = vListRepo;
        }

        [HttpGet("{id:length(24)}")]
        public async Task Get(string id) =>
            await _vListRepo.GetAsync(id);

        [HttpPost]
        public async Task<IActionResult> Post(VListCreate vListCreate)
        {
            try
            {
                _logger.LogInformation("Creating list.");
                Dictionary<string, List<string>> validationErrors = vListCreate.ValidateListCreate();
                if (validationErrors.Count != 0)
                    return BadRequest(new
                    {
                        errors = validationErrors,
                        staus = HttpStatusCode.BadRequest,
                        title = "One or more validation errors occured."
                    });


                VList newVList = new(
                    vListCreate.Title.Trim(),
                    vListCreate.Description.Trim(),
                    vListCreate.CreatedBy.Trim(),
                    DateValidation.ParseDate(vListCreate.Expiry).ToUniversalTime(),
                    vListCreate.PassPhrase
                    );

                await _vListRepo.CreateAsync(newVList);
                _logger.LogDebug("Successfully created list.");

                VListPresent vListPresent = new(newVList);
                return CreatedAtAction(nameof(Get), new { id = vListPresent.Id }, vListPresent);
            }
            catch (Exception)
            {
                _logger.LogError("Failed creating list.");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, VList updatedVlist)
        {
            var vList = await _vListRepo.GetAsync(id);
            if (vList == null)
            {
                return NotFound();
            }

            updatedVlist.Id = id;
            await _vListRepo.UpdateAsync(id, updatedVlist);
            return NoContent();
        }
    }
}
