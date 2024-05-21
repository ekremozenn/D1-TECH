using System.Linq.Expressions;
using System.Transactions;
using Microsoft.EntityFrameworkCore;

namespace D1_Tech.Service.Repositories.GenericRepositories;

public static class QueryExecuterExtensions
{
    public static async Task<List<TSource>> ToListUncommitedAsync<TSource>(        
        this IQueryable<TSource> source,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        var transactionOptions = new TransactionOptions
        {
            IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
        };

        using (var transactionScope = new TransactionScope(
                   TransactionScopeOption.Required, 
                   transactionOptions,
                   TransactionScopeAsyncFlowOption.Enabled))
        {
            var result =  await source.ToListAsync(cancellationToken);
            transactionScope.Complete();
            return result;
        }
    }

    public static async Task<TSource?> FirstOrDefaultUncommitedAsync<TSource>(    
        this IQueryable<TSource> source, 
        CancellationToken cancellationToken = default(CancellationToken))
    {
        return await PrivateFirstOrDefaultUncommitedAsync(source, null, cancellationToken);
    }

    public static async Task<TSource?> FirstOrDefaultUncommitedAsync<TSource>(
        this IQueryable<TSource> source, 
        Expression<Func<TSource,bool>> predicate, 
        CancellationToken cancellationToken = default(CancellationToken))
    {
        return await PrivateFirstOrDefaultUncommitedAsync(source, predicate, cancellationToken);
    }

    private static async Task<TSource?> PrivateFirstOrDefaultUncommitedAsync<TSource>(
        IQueryable<TSource> source, 
        Expression<Func<TSource,bool>>? predicate = null, 
        CancellationToken cancellationToken = default(CancellationToken))
    {
        var transactionOptions = new TransactionOptions
        {
            IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
        };

        using (var transactionScope = new TransactionScope(
                   TransactionScopeOption.Required, 
                   transactionOptions,
                   TransactionScopeAsyncFlowOption.Enabled))
        {
            TSource? result;
            if (predicate is null)
            {
                result = await source.FirstOrDefaultAsync(cancellationToken);
            }
            else
            {
                result = await source.FirstOrDefaultAsync(predicate, cancellationToken);
            }

            transactionScope.Complete();
            return result;
        }
    }

    public static async Task<TSource> FirstUncommitedAsync<TSource>(    
        this IQueryable<TSource> source, 
        CancellationToken cancellationToken = default(CancellationToken))
    {
        return await PrivateFirstUncommitedAsync(source, null, cancellationToken);
    }

    public static async Task<TSource> FirstUncommitedAsync<TSource>(
        this IQueryable<TSource> source, 
        Expression<Func<TSource,bool>> predicate, 
        CancellationToken cancellationToken = default(CancellationToken))
    {
        return await PrivateFirstUncommitedAsync(source, predicate, cancellationToken);
    }

    private static async Task<TSource> PrivateFirstUncommitedAsync<TSource>(
        IQueryable<TSource> source, 
        Expression<Func<TSource,bool>>? predicate = null, 
        CancellationToken cancellationToken = default(CancellationToken))
    {
        var transactionOptions = new TransactionOptions
        {
            IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
        };

        using (var transactionScope = new TransactionScope(
                   TransactionScopeOption.Required, 
                   transactionOptions,
                   TransactionScopeAsyncFlowOption.Enabled))
        {
            TSource result;
            if (predicate is null)
            {
                result = await source.FirstAsync(cancellationToken);
            }
            else
            {
                result = await source.FirstAsync(predicate, cancellationToken);
            }

            transactionScope.Complete();
            return result;
        }
    }

    public static async Task<int> CountUncommitedAsync<TSource>(
        this IQueryable<TSource> source, 
        CancellationToken cancellationToken = default(CancellationToken))
    {
        return await PrivateCountUncommitedAsync(source, null, cancellationToken);
    }

    public static async Task<int> CountUncommitedAsync<TSource>(
        this IQueryable<TSource> source, 
        Expression<Func<TSource,bool>> predicate, 
        CancellationToken cancellationToken = default(CancellationToken))
    {
        return await PrivateCountUncommitedAsync(source, predicate, cancellationToken);
    }

    private static async Task<int> PrivateCountUncommitedAsync<TSource>(
        IQueryable<TSource> source, 
        Expression<Func<TSource,bool>>? predicate = null, 
        CancellationToken cancellationToken = default(CancellationToken))
    {
        var transactionOptions = new TransactionOptions
        {
            IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
        };

        using (var transactionScope = new TransactionScope(
                   TransactionScopeOption.Required, 
                   transactionOptions,
                   TransactionScopeAsyncFlowOption.Enabled))
        {
            int result;
            if (predicate is null)
            {
                result =  await source.CountAsync(cancellationToken);
            }
            else
            {
                result =  await source.CountAsync(predicate, cancellationToken);
            }
            
            transactionScope.Complete();
            return result;
        }
    }

    public static async Task<bool> AnyUncommitedAsync<TSource>(
        this IQueryable<TSource> source, 
        CancellationToken cancellationToken = default(CancellationToken))
    {
        return await PrivateAnyUncommitedAsync(source, null, cancellationToken);
    }

    public static async Task<bool> AnyUncommitedAsync<TSource>(
        this IQueryable<TSource> source, 
        Expression<Func<TSource,bool>> predicate, 
        CancellationToken cancellationToken = default(CancellationToken))
    {
        return await PrivateAnyUncommitedAsync(source, predicate, cancellationToken);
    }

    private static async Task<bool> PrivateAnyUncommitedAsync<TSource>(
        IQueryable<TSource> source, 
        Expression<Func<TSource,bool>>? predicate = null, 
        CancellationToken cancellationToken = default(CancellationToken))
    {
        var transactionOptions = new TransactionOptions
        {
            IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
        };

        using (var transactionScope = new TransactionScope(
                   TransactionScopeOption.Required, 
                   transactionOptions,
                   TransactionScopeAsyncFlowOption.Enabled))
        {
            bool result;
            if (predicate is null)
            {
                result =  await source.AnyAsync(cancellationToken);
            }
            else
            {
                result =  await source.AnyAsync(predicate, cancellationToken);
            }
            
            transactionScope.Complete();
            return result;
        }
    }

    public static TSource? MaxByUncommited<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector)
    {
        var transactionOptions = new TransactionOptions
        {
            IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
        };

        using (var transactionScope = new TransactionScope(
                   TransactionScopeOption.Required, 
                   transactionOptions,
                   TransactionScopeAsyncFlowOption.Enabled))
        {
            var result = source.MaxBy(keySelector);
            transactionScope.Complete();
            return result;
        }
    }

    public static TSource? MinByUncommited<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector)
    {
        var transactionOptions = new TransactionOptions
        {
            IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
        };

        using (var transactionScope = new TransactionScope(
                   TransactionScopeOption.Required, 
                   transactionOptions,
                   TransactionScopeAsyncFlowOption.Enabled))
        {
            var result = source.MinBy(keySelector);
            transactionScope.Complete();
            return result;
        }
    }

    public static Task<bool> ContainsUnCommitedAsync<TSource>(
        this IQueryable<TSource> source,
        TSource item,
        CancellationToken cancellationToken = default)
    {
        var transactionOptions = new TransactionOptions
        {
            IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
        };

        using (var transactionScope = new TransactionScope(
                   TransactionScopeOption.Required, 
                   transactionOptions,
                   TransactionScopeAsyncFlowOption.Enabled))
        {
            var result = source.ContainsAsync(item, cancellationToken);
            transactionScope.Complete();
            return result;
        }
    }

    public static Task<decimal> SumUnCommitedAsync<TSource>(
        this IQueryable<TSource> source,
        Expression<Func<TSource, decimal>> selector,
        CancellationToken cancellationToken = default)
    {
        var transactionOptions = new TransactionOptions
        {
            IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
        };

        using (var transactionScope = new TransactionScope(
                   TransactionScopeOption.Required, 
                   transactionOptions,
                   TransactionScopeAsyncFlowOption.Enabled))
        {
            var result = source.SumAsync(selector, cancellationToken);
            transactionScope.Complete(); 
            return result;
        }
    }
}