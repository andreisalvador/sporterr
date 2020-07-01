using FluentValidation;
using System;

namespace Sporterr.Core.DomainObjects
{
    public abstract class Entity<TEntity> where TEntity : class
    {
        public Guid Id { get; private set; }        
        public DateTime DataCriacao { get; private set; }

        public Entity()
        {
            Id = Guid.NewGuid();            
        }

        public abstract void Validate();

        protected void Validate(TEntity entity, AbstractValidator<TEntity> validator) =>
            validator.ValidateAndThrow(entity);

        public override bool Equals(object obj)
        {
            var compareTo = obj as Entity<TEntity>;

            if (ReferenceEquals(this, compareTo)) return true;
            if (ReferenceEquals(null, compareTo)) return false;

            return Id.Equals(compareTo.Id);
        }

        public static bool operator ==(Entity<TEntity> a, Entity<TEntity> b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity<TEntity> a, Entity<TEntity> b)
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
