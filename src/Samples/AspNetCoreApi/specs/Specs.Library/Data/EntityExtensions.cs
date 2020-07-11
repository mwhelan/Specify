using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ApiTemplate.Api.Domain.Common;
using Newtonsoft.Json;
using TestStack.Dossier;
using TestStack.Dossier.Lists;

namespace Specs.Library.Data
{
    public static class EntityExtensions
    {
        private static IDb _db;

        public static IDb Db
        {
            get
            {
                if (_db == null)
                {
                    throw new InvalidOperationException("The Db needs to be configured at startup");
                }
                return _db;
            }
            set => _db = value;
        }

        public static TEntity WithEntityId<TEntity>(this TEntity entity, int id)
            where TEntity : Entity
        {
            var property = entity.GetType().GetProperty("Id");
            property.SetValue(entity, id, BindingFlags.NonPublic | BindingFlags.Instance, null, null, null);
            return entity;
        }

        public static TObject Persist<TObject, TBuilder>(this TestDataBuilder<TObject, TBuilder> entity)
            where TObject : Entity
            where TBuilder : TestDataBuilder<TObject, TBuilder>, new()
        {
            var obj = entity.Build()
                .WithEntityId(0);
            Db.Insert<TObject>(obj);
            return obj;
        }

        public static List<TObject> Persist<TObject, TBuilder>(this ListBuilder<TObject, TBuilder> entityList)
            where TObject : Entity
            where TBuilder : TestDataBuilder<TObject, TBuilder>, new()
        {
            var list = entityList
                .All()
                .Set(x => x.Id, 0)
                .BuildList()
                .ToArray();
            Db.Insert<TObject>(list);
            return list.ToList();
        }

        public static T Persist<T>(this Entity entity) where T : Entity
        {
            Db.Insert<T>(entity);
            return entity as T;
        }

        public static long Persist<T>(this IEnumerable<Entity> entities) where T : Entity
        {
            return Db.Insert<T>(entities.ToArray());
        }

        public static string ToJson<TObject, TBuilder>(this TestDataBuilder<TObject, TBuilder> entity)
            where TObject : class
            where TBuilder : TestDataBuilder<TObject, TBuilder>, new()
        {
            var obj = entity.Build();
            return obj.ToJson();
        }

        public static string ToJson(this object entity)
        {
            return JsonConvert.SerializeObject(entity);
        }

        public static void SetValue(this object target, string propertyName, object propertyValue)
        {
            PropertyInfo property = target.GetType()
                .GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
            if (property.CanWrite)
            {
                property.SetValue(target, propertyValue, null);
            }
        }
    }
}
