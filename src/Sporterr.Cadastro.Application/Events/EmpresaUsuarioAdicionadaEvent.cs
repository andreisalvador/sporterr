using Sporterr.Core.Enums;
using Sporterr.Core.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sporterr.Cadastro.Application.Events
{
    public class EmpresaUsuarioAdicionadaEvent : Event
    {
        public Guid UsuarioProprietarioId { get; private set; }
        public string RazaoSocial { get; private set; }
        public string Cnpj { get; private set; } //verificar dps
        public DiasSemanaFuncionamento DiasFuncionamento { get; private set; }
        public TimeSpan HorarioAbertura { get; private set; }
        public TimeSpan HorarioFechamento { get; private set; }

        public EmpresaUsuarioAdicionadaEvent(Guid usuarioProprietarioId, string razaoSocial, string cnpj, DiasSemanaFuncionamento diasFuncionamento, TimeSpan horarioAbertura, TimeSpan horarioFechamento)
        {
            AggregateId = usuarioProprietarioId;
            UsuarioProprietarioId = usuarioProprietarioId;
            RazaoSocial = razaoSocial;
            Cnpj = cnpj;
            DiasFuncionamento = diasFuncionamento;
            HorarioAbertura = horarioAbertura;
            HorarioFechamento = horarioFechamento;
        }
    }
}
