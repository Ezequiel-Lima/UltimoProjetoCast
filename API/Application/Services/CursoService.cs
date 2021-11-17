using API.Infrastructure.Data;
using API.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Application.Services
{
    public interface ICursoService
    {
        Task<Curso> Create(Curso dados);
        Task<Curso> Delete(Guid id);
    }
    public class CursoService : ICursoService
    {
        private readonly IWriteRepository<Curso> _writeRepository;
        private readonly IReadRepository<Curso> _readRepository;
        private readonly IUnitOfWork _ouw;

        public CursoService(IWriteRepository<Curso> writeRepository,IReadRepository<Curso> readRepository, IUnitOfWork ouw)
        {
            _writeRepository = writeRepository ?? throw new ArgumentNullException(nameof(writeRepository));
            _readRepository = readRepository ?? throw new ArgumentNullException(nameof(readRepository));
            _ouw = ouw ?? throw new ArgumentNullException(nameof(ouw));
        }

        public async Task<Curso> Create(Curso dados)
        {
            var result = new Curso();

            result.Id = Guid.NewGuid();
            result.Descricao = dados.Descricao;
            result.DataInicio = dados.DataInicio;
            Validacao();
            result.DataTermino = dados.DataTermino;
            result.QuantidadeAlunos = dados.QuantidadeAlunos;
            result.CategoriaId = dados.CategoriaId;

            await _writeRepository.AddAsync(result);
            await _ouw.CommitAsync();

            return result;
        }

        public async Task<Curso> Delete(Guid id)
        {
            var result = _readRepository.FindByCondition(x => x.Id == id);

            if (result == null)
                throw new Exception();

            _writeRepository.Delete(result.FirstOrDefault());
            await _ouw.CommitAsync();
            return null;
        }
        public void Validacao()
        {
            var result = _readRepository.FindAll();
            foreach (var teste in result)
            {
                result.Where(x => x.DataInicio >= x.DataTermino || x.DataTermino <= x.DataInicio);
            }
        }
    }
}
