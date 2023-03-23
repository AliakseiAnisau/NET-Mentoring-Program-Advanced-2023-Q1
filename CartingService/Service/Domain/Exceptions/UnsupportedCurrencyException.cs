using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartingService.Domain.Exceptions;
public class UnsupportedCurrencyException : Exception
{
    public UnsupportedCurrencyException(string code)
        : base($"Currency \"{code}\" is unsupported.")
    {
    }
}
