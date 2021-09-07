using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StudyikDbContext _studyikDbContext;

        public GenericRepository(StudyikDbContext studyikDbContext)
        {
            _studyikDbContext = studyikDbContext;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _studyikDbContext.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _studyikDbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetEntityWithSpecification(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        public async Task<bool> Insert(T obj)
        {
            _studyikDbContext.Set<T>().Add(obj);
            var task =  _studyikDbContext.SaveChangesAsync();
            await task;
            return task.IsCompleted;

        }

        public async Task<bool> Update(T obj)
        {
            _studyikDbContext.Set<T>().Update(obj);
            var task = _studyikDbContext.SaveChangesAsync();
            await task;
            return task.IsCompleted;

        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_studyikDbContext.Set<T>().AsQueryable(), spec);
        }
    }
}