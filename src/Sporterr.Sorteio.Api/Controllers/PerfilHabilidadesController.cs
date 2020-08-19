using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sporterr.Core.Communication.Mediator;
using Sporterr.Core.Messages.CommonMessages.Notifications;
using Sporterr.Core.Messages.CommonMessages.Notifications.Handler;
using Sporterr.Sorteio.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Sporterr.Sorteio.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerfilHabilidadesController : ControllerBase
    {
        private readonly IMediatrHandler _mediatr;
        private readonly ILogger _logger;
        private readonly DomainNotificationHandler _notifications;

        public PerfilHabilidadesController(IMediatrHandler mediatr, 
            INotificationHandler<DomainNotification> notifications,
            ILogger<PerfilHabilidadesController> logger)
        {
            _mediatr = mediatr;
            _logger = logger;
            _notifications = (DomainNotificationHandler)notifications;
        }

        // GET: api/<PerfilHabilidadesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<PerfilHabilidadesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<PerfilHabilidadesController>
        [HttpPost("Novo/{usuarioId:guid}")]
        public async Task<IActionResult> Novo(Guid usuarioId)
        {
            try
            {
                _logger.LogInformation($"Incluindo novo perfil para o usuario de guid {usuarioId}.");

                
                _ = await _mediatr.Send(new AdicionarPerfilHabilidadesCommand(usuarioId));

                if (_notifications.HasNotifications())
                    return BadRequest(_notifications.Notifications.Select(s => s.Value));
            }
            catch (Exception except)
            {
                return StatusCode(500, except.Message);
            }

            return Ok("Novo perfil de habilidades de usuário criado com sucesso");
        }

        [HttpPost("VincularEsporte/{perfilId:guid}/{esporteId:guid}")]
        public async Task<IActionResult> VincularEsporte(Guid perfilId, Guid esporteId)
        {
            try
            {
                _logger.LogInformation($"Vinculando esporte de guid {esporteId} no perfil de guid {perfilId}.");

                _ = await _mediatr.Send(new VincularEsportePerfilHabilidadesCommand(perfilId, esporteId));

                if (_notifications.HasNotifications())
                    return BadRequest(_notifications.Notifications.Select(s => s.Value));
            }
            catch (Exception except)
            {
                return StatusCode(500, except.Message);
            }

            return Ok("Novo esporte vinculado com sucesso");
        }

        [HttpPost("AvaliarHabilidadeUsuario/{perfilId:guid}")]
        public async Task<IActionResult> AvaliarHabilidadeUsuario(Guid perfilId, [FromBody] Dictionary<Guid, double> habilidades)
        {
            try
            {
                _logger.LogInformation($"Avaliando {habilidades?.Count} habilidades no perfil de guid {perfilId}.");

                _ = await _mediatr.Send(new AvaliarHabilidadesUsuarioCommand(perfilId, habilidades));

                if (_notifications.HasNotifications())
                    return BadRequest(_notifications.Notifications.Select(s => s.Value));
            }
            catch (Exception except)
            {
                return StatusCode(500, except.Message);
            }

            return Ok("Perfil avaliado com sucesso");
        }



        // PUT api/<PerfilHabilidadesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PerfilHabilidadesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
