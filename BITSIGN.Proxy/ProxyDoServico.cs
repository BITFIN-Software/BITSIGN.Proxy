﻿// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

using BITSIGN.Proxy.Comunicacao;
using BITSIGN.Proxy.Comunicacao.APIs;
using BITSIGN.Proxy.Logging;
using System;
using System.Net;
using System.Net.Http;

namespace BITSIGN.Proxy
{
    /// <summary>
    /// Abstrai toda a comunicação com os serviços (APIs) de assinaturas da BITSIGN.
    /// </summary>
    public class ProxyDoServico : IDisposable
    {
        private readonly HttpClient proxy;

        /// <summary>
        /// Inicializa o proxy.
        /// </summary>
        /// <param name="conexao">Dados da conexão com o ambiente.</param>
        /// <param name="logger">Instância de <see cref="ILogger"/> para gestão e armazenamento de logs gerados pelo proxy.</param>
        /// <param name="rastreioDeRequisicao">Gerador de códigos de rastreio de requisições.</param>
        /// <exception cref="ArgumentNullException">Se a <paramref name="conexao"/> não for informada.</exception>
        public ProxyDoServico(Conexao conexao, ILogger logger = null, IRastreio rastreioDeRequisicao = null)
        {
            this.Conexao = conexao ?? throw new ArgumentNullException(nameof(conexao));

            this.proxy = new(new RastreioDaRequisicao(logger, rastreioDeRequisicao))
            {
                BaseAddress = conexao.Url,
                DefaultRequestVersion = HttpVersion.Version20,
                DefaultVersionPolicy = HttpVersionPolicy.RequestVersionOrLower,
                Timeout = conexao.Timeout
            };

            this.proxy.DefaultRequestHeaders.Add(Protocolo.CodigoDoContratante, conexao.CodigoDoContratante.ToString());
            this.proxy.DefaultRequestHeaders.Add(Protocolo.ChaveDeIntegracao, conexao.ChaveDeIntegracao);
            this.proxy.DefaultRequestHeaders.Add("Accept", $"application/{conexao.FormatoDeSerializacao.ToString().ToLower()}");

            this.Lotes = new(proxy);
            this.Documentos = new(proxy);
            this.Financeiro = new(proxy);
            this.Contratantes = new(proxy, conexao.FormatoDeSerializacao);
            this.Notificacoes = new(proxy);
            this.Anexos = new(proxy);
            this.Buscador = new(proxy, conexao.FormatoDeSerializacao);
            this.Status = new(conexao.Status);
        }

        /// <summary>
        /// Dados de conexão com um dos <see cref="Ambiente"/>s disponíveis.
        /// </summary>
        public Conexao Conexao { get; }

        /// <summary>
        /// API de Lotes.
        /// </summary>
        public Lotes Lotes { get; }

        /// <summary>
        /// API de Documentos.
        /// </summary>
        public Documentos Documentos { get; }

        /// <summary>
        /// API da área Financeira.
        /// </summary>
        public Financeiro Financeiro { get; }

        /// <summary>
        /// API de Contratantes.
        /// </summary>
        public Contratantes Contratantes { get; }

        /// <summary>
        /// API de Notificações.
        /// </summary>
        public Notificacoes Notificacoes { get; }

        /// <summary>
        /// API de Anexos.
        /// </summary>
        public Anexos Anexos { get; set; }

        /// <summary>
        /// API de Busca de Recursos.
        /// </summary>
        public Buscador Buscador { get; set; }

        /// <summary>
        /// API de status dos Serviços.
        /// </summary>
        public Status Status { get; }

        /// <summary>
        /// Encerra e remove os recursos de comunicação utilizados por esta classe.
        /// </summary>
        public void Dispose() => this.proxy?.Dispose();
    }
}