using BlogSolution.Framework.Types;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;

namespace Blog.Persistance.Extensions
{
    public static class EFExtensions
    {
        public static void ApplySoftDeleteFilter(this ModelBuilder modelBuilder)
        {
            foreach (var type in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(ISoftDeletable).IsAssignableFrom(type.ClrType))
                    modelBuilder.ApplySoftDeleteFilter(type.ClrType);
            }
        }

        private static void ApplySoftDeleteFilter(this ModelBuilder modelBuilder, Type entityType) =>
            SetSoftDeleteFilterMethod.MakeGenericMethod(entityType).Invoke(null, new object[] { modelBuilder });

        static readonly MethodInfo SetSoftDeleteFilterMethod = typeof(EFExtensions).GetMethods(BindingFlags.Public | BindingFlags.Static)
            .Single(t => t.IsGenericMethod && t.Name == "ApplySoftDeleteFilter");

        private static void ApplySoftDeleteFilter<TEntity>(this ModelBuilder modelBuilder) where TEntity : class, ISoftDeletable =>
            modelBuilder.Entity<TEntity>().HasQueryFilter(x => !x.IsDeleted);
    }

}
