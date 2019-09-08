using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace FamiFail.Common.DataContracts
{
    public interface IServiceRegistrar
    {
        void Register(IServiceCollection serviceCollection);
    }
}