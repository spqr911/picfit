using picfit.application.Infrastructure.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace picfit.application.Infrastructure.Storage
{
    public interface IStorageFactory
    {
        IStorageService CreateStorage();
    }
}
