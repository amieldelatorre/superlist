using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using vlist.Models;
using vlist.Services;

namespace vlist.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VListController : ControllerBase
    {
        private readonly VListService _vlistService;
        public VListController(VListService vlistService) =>
            _vlistService = vlistService;

        [HttpGet("{id:length(24)}")]
        public async Task Get(string id) =>
            await _vlistService.GetAsync(id);

        [HttpPost]
        public async Task<IActionResult> Post(VList newVList)
        {
            newVList.Expiry = DateTime.Now.AddSeconds(30);
            await _vlistService.CreateAsync(newVList);
            return CreatedAtAction(nameof(Get), new { id = newVList.Id }, newVList);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, VList updatedVlist)
        {
            var vList = await _vlistService.GetAsync(id);
            if (vList == null)
            {
                return NotFound();
            }

            updatedVlist.Id = id;
            await _vlistService.UpdateAsync(id, updatedVlist);
            return NoContent();
        }
    }
}
