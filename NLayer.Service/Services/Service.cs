using Microsoft.EntityFrameworkCore;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Service.Exceptions;
using System.Linq.Expressions;

namespace NLayer.Service.Services;

public class Service<T>(IGenericRepository<T> repository, IUnitOfWork unitOfWork) : IService<T> where T : class
{
    private readonly IGenericRepository<T> _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<T> AddAsync(T entity)
    {
        await _repository.AddAsync(entity);
        await _unitOfWork.CommitAsync();
        return entity;
    }

    public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
    {
        await _repository.AddRangeAsync(entities);
        await _unitOfWork.CommitAsync();
        return entities;
    }

    public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression) => await _repository.AnyAsync(expression);

    public async Task<IEnumerable<T>> GetAllAsync() => await _repository.GetAll().ToListAsync();

    public async Task<T> GetByIdAsync(int id)
    {
        var hasProduct = await _repository.GetByIdAsync(id);

        return hasProduct ?? throw new NotFoundExcepiton($"{typeof(T).Name}({id}) not found");
    }

    public async Task RemoveAsync(T entity)
    {
        _repository.Remove(entity);
        await _unitOfWork.CommitAsync();
    }

    public async Task RemoveRangeAsync(IEnumerable<T> entities)
    {
        _repository.RemoveRange(entities);
        await _unitOfWork.CommitAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        _repository.Update(entity);
        await _unitOfWork.CommitAsync();
    }

    public IQueryable<T> Where(Expression<Func<T, bool>> expression) => _repository.Where(expression);
}
