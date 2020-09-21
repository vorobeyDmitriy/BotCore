using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using BotCore.Core.CurrencyBot.Entities;

namespace BotCore.Core.CurrencyBot.Interfaces
{
    public interface ISpecification<T> where T : BaseEntity
    {
        Expression<Func<T, bool>> Criteria { get; }
        List<Expression<Func<T, object>>> Includes { get; }
        List<string> IncludeStrings { get; }
        Expression<Func<T, object>> OrderBy { get; }
        Expression<Func<T, object>> OrderByDescending { get; }
        Expression<Func<T, object>> GroupBy { get; }

        int PageSize { get; }
        int PageNumber { get; }
        bool IsPagingEnabled { get; }
    }
}