using System;
using System.Collections.Generic;
using System.Text;

namespace picfit.application.Infrastructure.Storage
{
    public interface IStorageResult
    {
        int Code { get; }
        string FileName { get; }
    }
}
