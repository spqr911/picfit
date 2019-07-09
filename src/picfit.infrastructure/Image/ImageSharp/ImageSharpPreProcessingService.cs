using picfit.application.Infrastructure.Image;
using picfit.application.Extensions;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.IO;
using IImage = picfit.application.Infrastructure.Image.IImage;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

namespace picfit.infrastructure.Image.ImageSharp
{
    public class ImageSharpPreProcessingService : IImagePreProcessingService
    {
        //private readonly ImagePreProcessingServiceConfig _config;
        private readonly ushort[] _scales = new ushort[] { 75, 100, 150, 200 };
        private readonly ILogger _logger;
        
        public ImageSharpPreProcessingService(ushort[] scales, ILogger<ImageSharpPreProcessingService> logger)
        {
            _scales = scales;
            _logger = logger;
        }
        public IEnumerable<IImage> GetScaledImages(byte[] data, string extension)
        {
            using (Image<Rgba32> image = SixLabors.ImageSharp.Image.Load(data))
            {
                foreach (ushort scale in _scales)
                {
                    if (scale == 100)
                    {
                        yield return new Image(scale, data);
                    }
                    else
                    {
                        image.Mutate(
                        x => x.Resize(
                            Convert.ToInt32(
                                Math.Round(
                                    image.Width * scale / 100.0, MidpointRounding.AwayFromZero)),
                            Convert.ToInt32(
                                Math.Round(
                                    image.Height * scale / 100.0, MidpointRounding.AwayFromZero))
                                ));
                        var scaledData = GetScaledData(image, extension);
                        yield return new Image(scale, scaledData);
                    }
                }
            }
        }

        private byte[] GetScaledData(Image<Rgba32> image, string extension)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                switch (extension)
                {
                    case "png":
                        image.SaveAsPng(stream);
                        break;
                    case "jpg":
                    case "jpeg":
                        image.SaveAsJpeg(stream);
                        break;
                    case "gif":
                        image.SaveAsGif(stream);
                        break;
                    case "bmp":
                        image.SaveAsBmp(stream);
                        break;
                }
                return stream.ToArray();
            }
        }
    }
}
