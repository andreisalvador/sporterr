﻿using Sporterr.Core.Enums;
using Sporterr.Core.Messages;
using System;

namespace Sporterr.Cadastro.Application.Events
{
    public class EmpresaAdicionadaUsuarioEvent : Event
    {
        public Guid UsuarioProprietarioId { get; private set; }
        public Guid EmpresaAdicionadaId { get; private set; }
        public string RazaoSocial { get; private set; }
        public string Cnpj { get; private set; } //verificar dps
        public DiasSemanaFuncionamento DiasFuncionamento { get; private set; }
        public TimeSpan HorarioAbertura { get; private set; }
        public TimeSpan HorarioFechamento { get; private set; }

        public EmpresaAdicionadaUsuarioEvent(Guid empresaAdicionadaId, Guid usuarioProprietarioId, string razaoSocial, string cnpj, DiasSemanaFuncionamento diasFuncionamento, TimeSpan horarioAbertura, TimeSpan horarioFechamento)
        {
            AggregateId = EmpresaAdicionadaId;
            EmpresaAdicionadaId = empresaAdicionadaId;
            UsuarioProprietarioId = usuarioProprietarioId;
            RazaoSocial = razaoSocial;
            Cnpj = cnpj;
            DiasFuncionamento = diasFuncionamento;
            HorarioAbertura = horarioAbertura;
            HorarioFechamento = horarioFechamento;
        }
    }
}
