using Themes.API.Application.Queries;
using Themes.API.Model;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Themes.API.Controllers
{
    [Route("api/v1/stostheme")]
    [Authorize(AuthenticationSchemes = "TrustedServiceBearer")]
    public class STOSThemeController : ControllerBase
    {
        private readonly IMediator _mediator;
        public STOSThemeController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        [Route("active")]
        public async Task<UserThemeSettingModel> GetActiveTheme(CancellationToken cancellationToken = default)
        {
            return await _mediator.Send(new GetUserThemeSettingQuery(), cancellationToken);
        }
    }
}