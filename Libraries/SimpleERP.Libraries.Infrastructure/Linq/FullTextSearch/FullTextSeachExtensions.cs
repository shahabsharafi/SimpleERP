﻿//Source : http://stackoverflow.com/a/26762756/790811

using effts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SimpleERP.Libraries.Infrastructure.Linq
{
    public static class FullTextSearchExtensions
    {

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="source"></param>
        /// <param name="expression"></param>
        /// <param name="searchTerm"></param>
        /// <returns></returns>
        public static IQueryable<TEntity> FullTextFreeText<TEntity>(this IQueryable<TEntity> source, Expression<Func<TEntity, object>> expression, string searchTerm) where TEntity : class
        {
            return effts.FullTextSearchExtensions.FullTextFreeText(source, expression, searchTerm);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="source"></param>
        /// <param name="expression"></param>
        /// <param name="searchTerm"></param>
        /// <returns></returns>
        public static IQueryable<TEntity> FullTextContains<TEntity>(this IQueryable<TEntity> source, Expression<Func<TEntity, object>> expression, string searchTerm) where TEntity : class
        {
            return effts.FullTextSearchExtensions.FullTextContains(source, expression, searchTerm);
        }


    }
}
