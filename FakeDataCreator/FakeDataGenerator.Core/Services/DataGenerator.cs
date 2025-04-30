using Bogus;
using PersonModel = FakeDataGenerator.Core.Models.Person;

namespace FakeDataGenerator.Core.Services
{
    public class DataGenerator
    {

        public IEnumerable<PersonModel> GeneratePersons(int counts)
        {
            return new Faker<PersonModel>()
                .RuleFor(p => p.Id, f => f.IndexGlobal)
                .RuleFor(p => p.Name, f => f.Name.FullName())
                .RuleFor(p => p.Email,f => f.Internet.Email())
                .RuleFor(p => p.BirthDate, f => f.Date.Past(80))
                .Generate(counts);
        }


    }
}
