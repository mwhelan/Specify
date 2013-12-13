using System;

namespace Specify.Core
{
    public interface ISpecification
    {
        Type Story { get; }
        string Title { get; set; }
    }
}