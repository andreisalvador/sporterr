using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sporterr.Cadastro.Api.Models;
using Sporterr.Cadastro.Application.Commands;
using Sporterr.Core.Communication.Mediator;
using Sporterr.Core.Messages.CommonMessages.Notifications;
using Sporterr.Core.Messages.CommonMessages.Notifications.Handler;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sporterr.Cadastro.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly ILogger<UsuarioController> _logger;
        private readonly IMediatrHandler _mediatr;
        private readonly DomainNotificationHandler _notifications;

        public UsuarioController(ILogger<UsuarioController> logger, IMediatrHandler mediatr, INotificationHandler<DomainNotification> notifications)
        {
            _logger = logger;
            _mediatr = mediatr;
            _notifications = (DomainNotificationHandler)notifications;
        }

        [Route("Novo")]
        [HttpPost]
        public async Task<IActionResult> Novo([FromBody] UsuarioModel usuario)
        {
            try
            {
                _logger.LogInformation($"Incluindo novo usuario.");

                _ = await _mediatr.Send(new AdicionarUsuarioCommand(usuario.Nome, usuario.Email, usuario.Senha));

                if (_notifications.HasNotifications())
                    return BadRequest(_notifications.Notifications.Select(s => s.Value));
            }
            catch (Exception except)
            {
                return StatusCode(500, except.Message);
            }

            return Ok("Novo usuário criado com sucesso");
        }
    }
}
