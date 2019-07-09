using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace picfit.application.Infrastructure.Image
{
    public interface IImagePreProcessingService
    {
        IEnumerable<IImage> GetScaledImages(byte[] stream, string extension);
    }
}
