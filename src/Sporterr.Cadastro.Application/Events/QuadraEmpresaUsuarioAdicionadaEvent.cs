﻿using Sporterr.Core.Enums;
using Sporterr.Core.Messages;
using System;

namespace Sporterr.Cadastro.Application.Events
{
    public class QuadraEmpresaUsuarioAdicionadaEvent : Event
    {
        public Guid UsuarioId { get; private set; }
        public Guid EmpresaId { get; private set; }
        public Guid GrupoId { get; private set; }
        public TimeSpan TempoLocacao { get; private set; }
        public decimal ValorTempoLocado { get; private set; }
        public Esporte TipoEsporteQuadra { get; private set; }

        public QuadraEmpresaUsuarioAdicionadaEvent(Guid usuarioId, Guid empresaId, Guid id, TimeSpan tempoLocacao, decimal valorTempoLocado, Esporte tipoEsporteQuadra)
        {
            AggregateId = empresaId;
            UsuarioId = usuarioId;
            EmpresaId = empresaId;
            GrupoId = id;
            TempoLocacao = tempoLocacao;
            ValorTempoLocado = valorTempoLocado;
            TipoEsporteQuadra = tipoEsporteQuadra;
        }
    }
}