﻿using NLayer.Core.UnitOfWorks;

namespace NLayer.Repository.UnitOfWorks;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    private readonly AppDbContext _context = context;

    public void Commit() => _context.SaveChanges();

    public async Task CommitAsync() => await _context.SaveChangesAsync();
}
