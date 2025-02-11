﻿using System.Transactions;
using EntityFrameworkCore.AutoHistory.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CoffeePeek.Data;

/// <summary>
/// Represents the default implementation of the <see cref="IUnitOfWork"/> and <see cref="IUnitOfWork{TContext}"/> interface.
/// </summary>
/// <typeparam name="TContext">The type of the db context.</typeparam>
public sealed class UnitOfWork<TContext> : IRepositoryFactory, IUnitOfWork<TContext> where TContext : DbContext
{
    private bool _disposed = false;
    private Dictionary<Type, object> _repositories;

    public TContext DbContext { get; }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="UnitOfWork{TContext}"/> class.
    /// </summary>
    /// <param name="context">The context.</param>
    public UnitOfWork(TContext context)
    {
        DbContext = context ?? throw new ArgumentNullException(nameof(context));
        
        _repositories = new Dictionary<Type, object>();
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken, bool ensureAutoHistory = false,
        params IUnitOfWork[] unitOfWorks)
    {
        using var ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        var count = 0;
        
        foreach (var unitOfWork in unitOfWorks)
        {
            count += await unitOfWork.SaveChangesAsync(cancellationToken, ensureAutoHistory).ConfigureAwait(false);
        }

        count += await SaveChangesAsync(cancellationToken, ensureAutoHistory);

        ts.Complete();

        return count;
    }

    /// <summary>
    /// Gets the specified repository for the <typeparamref name="TEntity"/>.
    /// </summary>
    /// <param name="hasCustomRepository"><c>True</c> if providing custom repositry</param>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <returns>An instance of type inherited from <see cref="Repository{TEntity}"/> interface.</returns>
    public IRepository<TEntity> GetRepository<TEntity>(bool hasCustomRepository = false) where TEntity : class
    {
        // what's the best way to support custom reposity?
        if (hasCustomRepository)
        {
            var customRepo = DbContext.GetService<IRepository<TEntity>>();
            if (customRepo != null)
            {
                return customRepo;
            }
        }

        var type = typeof(TEntity);
        if (!_repositories.ContainsKey(type))
        {
            _repositories[type] = new Repository<TEntity>(DbContext);
        }

        return (IRepository<TEntity>)_repositories[type];
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken, bool ensureAutoHistory = false)
    {
        if (ensureAutoHistory)
        {
            DbContext.EnsureAutoHistory();
        }

        return await DbContext.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Executes the specified raw SQL command.
    /// </summary>
    /// <param name="sql">The raw SQL.</param>
    /// <param name="parameters">The parameters.</param>
    /// <returns>The number of state entities written to database.</returns>
    public int ExecuteSqlCommand(string sql, params object[] parameters) =>
        DbContext.Database.ExecuteSqlRaw(sql, parameters);

    public Task<IQueryable<TEntity>> FromSqlAsync<TEntity>(string sql, CancellationToken cancellationToken = default, params object[] parameters) where TEntity : class
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Uses raw SQL queries to fetch the specified <typeparamref name="TEntity" /> data.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <param name="sql">The raw SQL.</param>
    /// <param name="parameters">The parameters.</param>
    /// <returns>An <see cref="IQueryable{T}" /> that contains elements that satisfy the condition specified by raw SQL.</returns>
    public IQueryable<TEntity> FromSql<TEntity>(string sql, params object[] parameters) where TEntity : class =>
        DbContext.Set<TEntity>().FromSqlRaw(sql, parameters);

    /// <summary>
    /// Saves all changes made in this context to the database.
    /// </summary>
    /// <returns>The number of state entries written to the database.</returns>
    public int SaveChanges(bool ensureAutoHistory = false)
    {
        return DbContext.SaveChanges();
    }

    /// <summary>
    /// Asynchronously saves all changes made in this unit of work to the database.
    /// </summary>
    /// <returns>A <see cref="Task{TResult}"/> that represents the asynchronous save operation. The task result contains the number of state entities written to database.</returns>
    public async Task<int> SaveChangesAsync(bool ensureAutoHistory = false)
    {
        return await DbContext.SaveChangesAsync();
    }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);

        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    /// <param name="disposing">The disposing.</param>
    private void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _repositories.Clear();

                DbContext.Dispose();
            }
        }

        _disposed = true;
    }

    public void TrackGraph(object rootEntity, Action<EntityEntryGraphNode> callback)
    {
        DbContext.ChangeTracker.TrackGraph(rootEntity, callback);
    }
}