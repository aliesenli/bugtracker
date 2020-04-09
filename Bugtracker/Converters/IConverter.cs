using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bugtracker.Converters
{
    public interface IConverter<in TFrom, out TTo>
    {
        TTo Convert(TFrom source);
    }
}
