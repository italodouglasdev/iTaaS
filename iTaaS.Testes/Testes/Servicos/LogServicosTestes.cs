using iTaaS.Api.Aplicacao.DTOs;
using iTaaS.Api.Aplicacao.DTOs.Auxiliares;
using iTaaS.Api.Aplicacao.Interfaces.Entidades;
using iTaaS.Api.Aplicacao.Interfaces.Mapeadores;
using iTaaS.Api.Aplicacao.Interfaces.Repositorios;
using iTaaS.Api.Aplicacao.Interfaces.Servicos;
using iTaaS.Api.Aplicacao.Servicos;
using iTaaS.Api.Dominio.Entidades;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace iTaaS.Testes.Testes.Servicos
{
    public class LogServicoTestes
    {
        private readonly Mock<ILogRepositorio> _mockLogRepository;
        private readonly Mock<ILogMapeador> _mockLogMapper;
        private readonly Mock<IHttpContextoServico> _mockHttpContextoServico;
        private readonly LogServico _logServico;

        public LogServicoTestes()
        {
            _mockLogRepository = new Mock<ILogRepositorio>();
            _mockLogMapper = new Mock<ILogMapeador>();
            _mockHttpContextoServico = new Mock<IHttpContextoServico>();

            _logServico = new LogServico(_mockLogRepository.Object, _mockLogMapper.Object, _mockHttpContextoServico.Object);
        }

        [Fact]
        public async Task ObterPorId_DeveRetornarLogDto_QuandoLogExiste()
        {
            var logId = 1;
            var logEntity = new LogEntidade { Id = logId };
            var logDto = new LogDto { Id = logId };

            _mockLogRepository.Setup(repo => repo.ObterPorId(logId))
                .ReturnsAsync(new Resultado<LogEntidade> { Dados = logEntity });

            _mockLogMapper.Setup(mapper => mapper.MapearDeEntidadeParaDto(logEntity))
                .Returns(logDto);


            var resultado = await _logServico.ObterPorId(logId);

            Assert.True(resultado.Sucesso);

            Assert.Equal(logDto, resultado.Dados);
        }

        [Fact]
        public async Task Criar_DeveRetornarLogDto_QuandoCriacaoForBemSucedida()
        {
            // Arrange
            var logDto = new LogDto { Id = 1 };
            var logEntity = new LogEntidade { Id = 1 };
            var hash = Guid.NewGuid().ToString();

            // Configura o mock do mapeador para converter DTO para entidade
            _mockLogMapper.Setup(mapper => mapper.MapearDeDtoParaEntidade(logDto))
                .Returns(logEntity);

            // Configura o mock do repositório para simular uma criação bem-sucedida
            _mockLogRepository.Setup(repo => repo.Criar(It.Is<LogEntidade>(l => l.Hash == hash)))
                .ReturnsAsync(new Resultado<LogEntidade> { Dados = logEntity });

            // Configura o mock do mapeador para converter entidade para DTO
            _mockLogMapper.Setup(mapper => mapper.MapearDeEntidadeParaDto(logEntity))
                .Returns(logDto);

            // Act
            var resultado = await _logServico.Criar(logDto);

            // Assert
            Assert.True(resultado.Sucesso); // Verifica se o resultado foi bem-sucedido
            Assert.Equal(logDto, resultado.Dados); // Verifica se o DTO retornado é o esperado

            // Verifica se o método Criar do repositório foi chamado exatamente uma vez
            _mockLogRepository.Verify(repo => repo.Criar(It.Is<LogEntidade>(l => l.Hash == hash)), Times.Once);
        }

        //[Fact]
        //public async Task Criar_DeveRetornarInconsistencias_QuandoCriacaoFalhar()
        //{
        //    // Arrange
        //    var logDto = new LogDto { Id = 1, Mensagem = "Teste de log" };
        //    var logEntity = new LogEntidade { Id = 1, Mensagem = "Teste de log" };
        //    var hash = Guid.NewGuid().ToString();
        //    var inconsistencias = new List<string> { "Erro ao criar log" };

        //    // Configura o mock do mapeador para converter DTO para entidade
        //    _mockLogMapper.Setup(mapper => mapper.MapearDeDtoParaEntidade(logDto))
        //        .Returns(logEntity);

        //    // Configura o mock do repositório para simular uma falha na criação
        //    _mockLogRepository.Setup(repo => repo.Criar(It.Is<LogEntidade>(l => l.Hash == hash)))
        //        .ReturnsAsync(new Resultado<LogEntidade> { Inconsistencias = inconsistencias });

        //    // Act
        //    var resultado = await _logServico.Criar(logDto);

        //    // Assert
        //    Assert.False(resultado.Sucesso); // Verifica se o resultado falhou

        //    Assert.Equal(inconsistencias, resultado.Inconsistencias); // Verifica se as inconsistências são as esperadas

        //    // Verifica se o método Criar do repositório foi chamado exatamente uma vez
        //    _mockLogRepository.Verify(repo => repo.Criar(It.Is<LogEntidade>(l => l.Hash == hash)), Times.Once);
        //}


        //[Fact]
        //public async Task ObterPorId_DeveRetornarInconsistencias_QuandoLogNaoExiste()
        //{
        //    // Arrange
        //    var logId = 1;
        //    var inconsistencias = new List<string> { "Log não encontrado" };

        //    _mockLogRepository.Setup(repo => repo.ObterPorId(logId))
        //        .ReturnsAsync(new Resultado<LogEntidade> { Sucesso = false, Inconsistencias = inconsistencias });

        //    // Act
        //    var resultado = await _logServico.ObterPorId(logId);

        //    // Assert
        //    Assert.False(resultado.Sucesso);
        //    Assert.Equal(inconsistencias, resultado.Inconsistencias);
        //}

        //[Fact]
        //public async Task Criar_DeveRetornarLogDto_QuandoCriacaoForBemSucedida()
        //{
        //    // Arrange
        //    var logDto = new LogDto { Id = 1 };
        //    var logEntity = new LogEntidade { Id = 1 };

        //    _mockLogMapper.Setup(mapper => mapper.MapearDeDtoParaEntidade(logDto))
        //        .Returns(logEntity);

        //    _mockLogRepository.Setup(repo => repo.Criar(logEntity))
        //        .ReturnsAsync(new Resultado<LogEntidade> { Sucesso = true, Dados = logEntity });

        //    _mockLogMapper.Setup(mapper => mapper.MapearDeEntidadeParaDto(logEntity))
        //        .Returns(logDto);

        //    // Act
        //    var resultado = await _logServico.Criar(logDto);

        //    // Assert
        //    Assert.True(resultado.Sucesso);
        //    Assert.Equal(logDto, resultado.Dados);
        //}

        //[Fact]
        //public async Task Criar_DeveRetornarInconsistencias_QuandoCriacaoFalhar()
        //{
        //    // Arrange
        //    var logDto = new LogDto { Id = 1 };
        //    var logEntity = new LogEntidade { Id = 1 };
        //    var inconsistencias = new List<string> { "Erro ao criar log" };

        //    _mockLogMapper.Setup(mapper => mapper.MapearDeDtoParaEntidade(logDto))
        //        .Returns(logEntity);

        //    _mockLogRepository.Setup(repo => repo.Criar(logEntity))
        //        .ReturnsAsync(new Resultado<LogEntidade> { Sucesso = false, Inconsistencias = inconsistencias });

        //    // Act
        //    var resultado = await _logServico.Criar(logDto);

        //    // Assert
        //    Assert.False(resultado.Sucesso);

        //    Assert.Equal(inconsistencias, resultado.Inconsistencias);
        //}

        // Adicione mais testes para os outros métodos da classe LogServico
    }
}
