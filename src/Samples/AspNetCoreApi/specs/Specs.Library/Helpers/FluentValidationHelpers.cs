using System;
using System.Linq;
using System.Linq.Expressions;
using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Validators;

namespace Specs.Library.Helpers
{
	public static class FluentValidationHelpers
	{
		/// <summary>
		/// Returns all the <see cref="IPropertyValidator"/> of a given field
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="TProperty"></typeparam>
		/// <param name="validator"></param>
		/// <param name="expression"></param>
		public static IPropertyValidator[] GetValidatorsForMember<T, TProperty>(
			this IValidator<T> validator, Expression<Func<T, TProperty>> expression)
		{
			var descriptor = validator.CreateDescriptor();
			var expressionMemberName = expression.GetMember()?.Name;

			return descriptor.GetValidatorsForMember(expressionMemberName).ToArray();
		}

		public static IPropertyValidator[] GetValidatorsForMember<T>(
			this IValidator<T> validator, string memberName)
		{
			var descriptor = validator.CreateDescriptor();
			return descriptor.GetValidatorsForMember(memberName).ToArray();
		}

		/// <summary>
		/// Returns the <see cref="EntityTypeBuilder"/> of the given entity
		/// </summary>
		/// <typeparam name="TEntity">The entity</typeparam>
		/// <typeparam name="TEntityConfiguration">The entitie's configuration</typeparam>
		/// <returns>EntityTypeBuilder<TEntity></returns>
		//public static EntityTypeBuilder<TEntity> GetEntityTypeBuilder<TEntity, TEntityConfiguration>()
		//	where TEntity : class
		//	where TEntityConfiguration : IEntityTypeConfiguration<TEntity>, new()
		//{
		//	var options = new DbContextOptionsBuilder<ShopDbContext>()
		//			.UseSqlite(new SqliteConnection("DataSource=:memory:"))
		//			.Options;

		//	var sut = new ShopDbContext(options);
		//	var conventionSet = ConventionSet.CreateConventionSet(sut);
		//	var modelBuilder = new ModelBuilder(conventionSet);

		//	var entityBuilder = modelBuilder.Entity<TEntity>();
		//	var entityConfig = new TEntityConfiguration();
		//	entityConfig.Configure(entityBuilder);

		//	return entityBuilder;
		//}
	}
}
