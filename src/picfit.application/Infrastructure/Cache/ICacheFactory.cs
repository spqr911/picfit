using System;
using System.Collections.Generic;
using System.Text;

namespace picfit.application.Infrastructure.Cache
{
    public interface ICacheFactory
    {
        ICacheService CreateCache();
    }
}
