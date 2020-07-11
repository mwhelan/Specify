using System;
using TestStack.Dossier;
using TestStack.Dossier.DataSources.Dictionaries;
using TestStack.Dossier.DataSources.Picking;

namespace Specs.Library.Builders.ValueSuppliers
{
    public class CodeValueSupplier : IAnonymousValueSupplier
    {
        private RepeatingSequenceSource<string> _dataSource ;

        public CodeValueSupplier()
        {
            var wordList = new Words(FromDictionary.FinanceCurrencyCode).Data;
            _dataSource = new RepeatingSequenceSource<string>(wordList);
        }

        /// <inheritdoc />
        public bool CanSupplyValue(Type type, string propertyName)
        {
            return type == typeof(string) 
                   && (propertyName.ToLower().EndsWith("code")  || propertyName.ToLower().EndsWith("name"));
        }

        /// <inheritdoc />
        public object GenerateAnonymousValue(AnonymousValueFixture any, Type type, string propertyName)
        {
            var code = _dataSource.Next();
            return (object)code;
        }
    }
}
