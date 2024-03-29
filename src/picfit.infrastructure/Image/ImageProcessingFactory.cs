﻿using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using picfit.application.Infrastructure.Image;
using picfit.infrastructure.Image.ImageSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace picfit.infrastructure.Image
{
    public class ImagePreProcessingFactory : IImageProcessingFactory
    {
        private readonly ImageProcessingConfig _config;
        private readonly ILoggerFactory _loggerFactory;

        public ImagePreProcessingFactory(IOptions<ImageProcessingConfig> config, ILoggerFactory loggerFactory)
        {
            _config = config.Value;
            _loggerFactory = loggerFactory;
        }

        public IImagePreProcessingService CreateImagePreProcessing()
        {
            IImagePreProcessingService imagePreProcessing;
            if (_config.Type == "imagesharp")
                imagePreProcessing = new ImageSharpProcessingService(
                    _config.Scales,
                    _loggerFactory.CreateLogger<ImageSharpProcessingService>());
            else
                throw new ArgumentException("indefined imagepreprocessing type");
            return imagePreProcessing;
        }
    }
}
