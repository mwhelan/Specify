using System;
using System.Collections.Generic;
using System.Linq;
using TestStack.Dossier.DataSources.Picking;

namespace Specs.Library.Builders.ObjectMothers
{
    public class SequentialMother
    {
        public RepeatingSequenceSource<int> Keys(int size, bool isNew = false)
        {
            var list = isNew
                ? Enumerable.Range(0, size).Select(x => x * -1).ToList()
                : Enumerable.Range(1, size).ToList(); 
            return Pick.RepeatingSequenceFrom(list);
        }

        public RepeatingSequenceSource<DateTimeOffset> Days(int size, DateTimeOffset startDate, int increment)
        {
            var dataSource = new List<DateTimeOffset>();
            for (var counter = 1; counter <= size; counter++)
            {
                dataSource.Add(startDate.AddDays(counter * increment));
            }
            return Pick.RepeatingSequenceFrom(dataSource);
        }

        public RepeatingSequenceSource<DateTimeOffset> Months(int size, DateTimeOffset startDate, int increment)
        {
            var dataSource = new List<DateTimeOffset> { startDate };

            for (var counter = 1; counter < size; counter++)
            {
                dataSource.Add(startDate.AddMonths(counter * increment));
            }
            return Pick.RepeatingSequenceFrom(dataSource);
        }

        public RepeatingSequenceSource<TEntity> Entities<TEntity>(List<TEntity> source)
        {
            return Pick.RepeatingSequenceFrom(source);
        }
    }
}