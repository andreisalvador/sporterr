using Bogus;

namespace Sporterr.Tests.Common.Fixtures
{
    public class BaseFixture
    {
        protected Faker Faker { get; }
        public string Language { get; }
        public BaseFixture(string language = "pt_BR")
        {
            Language = language;
            Faker = new Faker(language);
        }

        protected Faker<TEntity> NewFakerInstance<TEntity>() where TEntity : class
            => new Faker<TEntity>(Language);
    }
}
