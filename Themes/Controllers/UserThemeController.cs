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
    [AllowAnonymous]
    [Route("api/v1/usertheme")]
    public class UserThemeController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserThemeController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [Route("active")]
        [HttpGet]
        public async Task<ActionResult<ActiveUserThemeReadModel>> GetActive(CancellationToken cancellationToken = default)
        {
            var userTheme = await _mediator.Send(new GetActiveUserThemeQuery(), cancellationToken);
            return Ok(userTheme);
        }
    }
}