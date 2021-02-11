﻿// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

using System;
using System.Diagnostics;

namespace BITSIGN.Proxy.DTOs
{
    /// <summary>
    /// Classe utilizada para recepcionar os callbacks gerados pela plataforma.
    /// </summary>
    [DebuggerDisplay("{Evento} - Status: {Status}")]
    public class Callback
    {
        /// <summary>
        /// Nome do evento indicando de qual entidade se refere o callback.
        /// </summary>
        public string Evento { get; set; }

        /// <summary>
        /// Identificador da entidade que gerou o callback.
        /// </summary>
        /// <remarks>Na maioria das vezes será um <see cref="Guid"/>; porém quando se tratar de um assinante, o <see cref="Id"/> será o número de seu documento (CNPJ/CPF).</remarks>
        public string Id { get; set; }

        /// <summary>
        /// Status que foi atribuído à entidade.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Data em que o callback ocorreu.
        /// </summary>
        public DateTime Data { get; set; }

        /// <summary>
        /// Alguma informação complementar relevante para este evento.
        /// </summary>
        public string Complemento { get; set; }

        /// <summary>
        /// Se a entidade que gerou o callback possuir tags associadas, elas serão informadas nesta propriedade.
        /// </summary>
        public string Tags { get; set; }
    }
}