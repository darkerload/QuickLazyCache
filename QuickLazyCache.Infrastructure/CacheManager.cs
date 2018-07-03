using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace QuickLazyCache.Infrastructure
{
    public class CacheManager
    {
        public static TResult Initialize<TResult>(Type callerType,
                                                   Expression<Func<TResult>> action,
                                                   TimeSpan? cachingInterval = null) where TResult : class
        {
            MethodCallExpression body = (MethodCallExpression)action.Body;

            ICollection<object> parameters = new List<object>();

            foreach (MemberExpression expression in body.Arguments)
            {
                parameters.Add(((FieldInfo)expression.Member).GetValue(((ConstantExpression)expression.Expression).Value));
            }

            StringBuilder builder = new StringBuilder(100);
            builder.Append(callerType.FullName);
            builder.Append(".");

            parameters.ToList().ForEach(x =>
            {
                builder.Append("_");
                builder.Append(x);
            });

            string cacheKey = builder.ToString();

            TResult result = MemoryBaseCache.Instance.Get<TResult>(cacheKey);

            if (result == null)
            {
                result = action.Compile().Invoke();
                if (cachingInterval.HasValue)
                    MemoryBaseCache.Instance.Add(cacheKey, result, cachingInterval.Value);
                else
                    MemoryBaseCache.Instance.Add(cacheKey, result);
            }

            return result;
        }
    }
}
