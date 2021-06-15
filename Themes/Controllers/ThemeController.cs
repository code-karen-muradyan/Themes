using Themes.API.Application.Commands;
using Themes.API.Application.Queries;
using Themes.API.Model;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;
using UserPrivilegePolicy;
using UserPrivilegePolicy.Services;

namespace Themes.API.Controllers
{
    [Authorize(Policies.AdminPartnerSalePortal)]
    [Route("api/v1/theme")]
    public class ThemeController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ThemeController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [Route("all")]
        [HttpGet]
        [RequireFeature(BoneeFeatures.Theme)]
        public async Task<ActionResult<UserTheme>> GetAll(CancellationToken cancellationToken = default)
        {
            var query = new GetUserThemesQuery();
            var userTheme = await _mediator.Send(query, cancellationToken);
            return Ok(userTheme);
        }
        [Route("createtheme")]
        [HttpPost]
        [RequireFeature(BoneeFeatures.Theme)]
        public async Task<ActionResult<bool>> CreateTheme([FromBody]CreateActiveThemeCommand command, CancellationToken cancellationToken = default)
        {
            return await _mediator.Send(command, cancellationToken);
        }
        [Route("savetheme")]
        [HttpPost]
        [RequireFeature(BoneeFeatures.Theme)]
        public async Task<ActionResult<bool>> SaveTheme([FromBody]SaveThemeCommand command, CancellationToken cancellationToken = default)
        {
            return await _mediator.Send(command, cancellationToken);
        }
        [Route("saveactivetheme")]
        [HttpPost]
        [RequireFeature(BoneeFeatures.Theme)]
        public async Task<ActionResult<bool>> SaveActiveTheme([FromBody]SaveActiveThemeCommand command, CancellationToken cancellationToken = default)
        {
            return await _mediator.Send(command, cancellationToken);
        }
        [Route("deletetheme")]
        [HttpDelete]
        [RequireFeature(BoneeFeatures.Theme)]
        public async Task<ActionResult<bool>> DeleteTheme([FromQuery]DeleteThemeCommand command, CancellationToken cancellationToken = default)
        {
            return await _mediator.Send(command, cancellationToken);
        }
    }
}