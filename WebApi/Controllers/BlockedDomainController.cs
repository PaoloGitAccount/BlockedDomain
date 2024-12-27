using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Interfaces;
using Core.Entities;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlockedDomainController : ControllerBase
    {
        private readonly IBlockedDomainService _blockedDomainService;

        public BlockedDomainController(IBlockedDomainService blockedDomainService)
        {
            _blockedDomainService = blockedDomainService;
        }

        [HttpPost]
        public async Task<IActionResult> AddBlockedDomain(BlockedDomain blockedDomain)
        {
            await _blockedDomainService.AddBlockedDomainAsync(blockedDomain);
            return Ok();
        }

        [HttpGet("isBlocked")]
        public async Task<IActionResult> IsDomainBlocked(string domain)
        {
            var isBlocked = await _blockedDomainService.IsDomainBlockedAsync(domain);
            return Ok(isBlocked);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BlockedDomain>>> GetAllBlockedDomains()
        {
            var blockedDomains = await _blockedDomainService.GetAllBlockedDomainsAsync();
            return Ok(blockedDomains);
        }
    }
}
