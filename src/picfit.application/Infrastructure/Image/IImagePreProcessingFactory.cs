using System;
using System.Collections.Generic;
using System.Text;

namespace picfit.application.Infrastructure.Image
{
    public interface IImagePreProcessingFactory
    {
        IImagePreProcessingService CreateImagePreProcessing();
    }
}
