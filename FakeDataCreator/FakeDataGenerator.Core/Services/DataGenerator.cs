using Bogus;
using PersonModel = FakeDataGenerator.Core.Models.Person;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FakeDataGenerator.Core.Services
{
    public class DataGenerator
    {

        public async Task<IEnumerable<PersonModel>> GeneratePersons(int counts)
        {

            return await Task.Run(() =>
            {
                return new Faker<PersonModel>()
                    .RuleFor(p => p.Id, f => f.IndexGlobal)
                    .RuleFor(p => p.Name, f => f.Name.FullName())
                    .RuleFor(p => p.Email, f => f.Internet.Email())
                    .RuleFor(p => p.BirthDate, f => f.Date.Past(80))
                    .Generate(counts);
            });
        }


    }
}
