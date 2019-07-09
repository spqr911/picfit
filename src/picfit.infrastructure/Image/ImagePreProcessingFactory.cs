using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using picfit.application.Infrastructure.Image;
using picfit.infrastructure.Image.ImageSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace picfit.infrastructure.Image
{
    public class ImagePreProcessingFactory : IImagePreProcessingFactory
    {
        private readonly ImagePreProcessingConfig _config;
        private readonly ILoggerFactory _loggerFactory;

        public ImagePreProcessingFactory(IOptions<ImagePreProcessingConfig> config, ILoggerFactory loggerFactory)
        {
            _config = config.Value;
            _loggerFactory = loggerFactory;
        }

        public IImagePreProcessingService CreateImagePreProcessing()
        {
            IImagePreProcessingService imagePreProcessing;
            if (_config.Type == "imagesharp")
                imagePreProcessing = new ImageSharpPreProcessingService(
                    _config.Scales,
                    _loggerFactory.CreateLogger<ImageSharpPreProcessingService>());
            else
                throw new ArgumentException("indefined imagepreprocessing type");
            return imagePreProcessing;
        }
    }
}
