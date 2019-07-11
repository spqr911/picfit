using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace picfit.application.Infrastructure.Image
{
    public interface IImageProcessingService
    {
        IEnumerable<IImage> GetScaledImages(byte[] stream, string extension);

        byte[] Mutate(byte[] data, int width, int height, string extension);
    }
}
