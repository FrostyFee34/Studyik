using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
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


        public async Task<T> InsertAsync(T obj)
        {
            _studyikDbContext.Set<T>().Add(obj);
            await _studyikDbContext.SaveChangesAsync();
            return obj;
        }

        public async Task<T> UpdateAsync(T obj)
        {
            _studyikDbContext.Set<T>().Update(obj);
            await _studyikDbContext.SaveChangesAsync();
            return obj;
        }

        public Task<int> DeleteAsync(T obj)
        {
            _studyikDbContext.Set<T>().Remove(obj);
            return _studyikDbContext.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_studyikDbContext.Set<T>().AsQueryable(), spec);
        }
    }
}