using ApiTemplate.Api.Domain.Common;
using TestStack.Dossier;

namespace Specs.Library.Builders.Entities.MasterFiles
{
    public abstract class MasterFileBuilder<TObject, TBuilder> : TestDataBuilder<TObject, TBuilder>
        where TObject : MasterFile
        where TBuilder : TestDataBuilder<TObject, TBuilder>, new()
    {
        protected MasterFileBuilder()
        {
            Set(x => x.ActiveFlag, true);
        }
    }
}