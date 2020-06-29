using FluentValidation;
using System;

namespace Sporterr.Core.DomainObjects
{
    public abstract class Entity<T> where T : class
    {
        public Guid Id { get; private set; }        
        public DateTime DataCriacao { get; private set; }

        public Entity()
        {
            Id = Guid.NewGuid();            
        }

        public abstract void Validate();

        protected void Validate(T entity, AbstractValidator<T> validator) =>
            validator.ValidateAndThrow<T>(entity);

        public override bool Equals(object obj)
        {
            var compareTo = obj as Entity<T>;

            if (ReferenceEquals(this, compareTo)) return true;
            if (ReferenceEquals(null, compareTo)) return false;

            return Id.Equals(compareTo.Id);
        }

        public static bool operator ==(Entity<T> a, Entity<T> b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity<T> a, Entity<T> b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetType().GetHashCode() ^ 93) + Id.GetHashCode();
        }

        public override string ToString()
        {
            return $"{GetType().Name} [Id={Id}]";
        }
    }
}
