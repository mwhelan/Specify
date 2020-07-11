using System;

namespace ApiTemplate.Api.Domain.Common
{
    /// <summary>
    /// From Vladimir Khorikov Pluralsight course -  DDD and EF Core: Preserving Encapsulation
    /// https://app.pluralsight.com/library/courses/ddd-ef-core-preserving-encapsulation/table-of-contents
    /// </summary>
    public abstract class Entity
    {
        public int Id { get; set; }

        protected Entity()
        {
        }

        protected Entity(int id)
            : this()
        {
            Id = id;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Entity other))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (GetRealType() != other.GetRealType())
                return false;

            if (Id == 0 || other.Id == 0)
                return false;

            return Id == other.Id;
        }

        public static bool operator ==(Entity a, Entity b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity a, Entity b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetRealType().ToString() + Id).GetHashCode();
        }

        private Type GetRealType()
        {
            Type type = GetType();

            if (type.ToString().Contains("Castle.Proxies."))
                return type.BaseType;

            return type;
        }
    }
}