using System;

namespace Specify
{
    public interface ISpecification
    {
        Type Story { get; }
        string Title { get; set; }
    }
}