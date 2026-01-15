using Academy.Infrastructure.Data;
using Academy.Infrastructure.Entities;
using Academy.Interfaces.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Application.Repositories
{
	public class GenericRepository<TEntity, TKey>(ApplicationDbContext _dbContext) : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
	{
		public async Task AddAsync(TEntity entity)
		=> await _dbContext.AddAsync(entity);

		public void Delete(TEntity entity)
		=> _dbContext.Remove(entity);

		public async Task<IEnumerable<TEntity>> GetAllAsync()
		=> await _dbContext.Set<TEntity>().ToListAsync();

		public async Task<TEntity?> GetByIdAsync(TKey id)
		=> await _dbContext.Set<TEntity>().FindAsync(id);

		public void Update(TEntity entity)
		=> _dbContext.Update(entity);

		public IQueryable<TEntity> Query() => _dbContext.Set<TEntity>();
	}
}
