using MediatR;
using Microsoft.Extensions.Logging;
using picfit.application.Infrastructure;
using picfit.application.Infrastructure.Image;
using picfit.application.Infrastructure.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace picfit.application.Commands
{
    public class AddImageCommandHandler : IRequestHandler<AddImageCommand, AddImageCommandResult>
    {
        private readonly IImagePreProcessingFactory _imagePreProcessingFactory;
        private readonly IStorageFactory _storageFactory;
        private readonly ILogger _logger;
        private readonly Regex _regex = new Regex(@"(?<name>[^\\]*)\.(?<extension>(\w+)$)", RegexOptions.Compiled);
        public AddImageCommandHandler(
            IImagePreProcessingFactory imagePreProcessingFactory,
            IStorageFactory storageFactory,
            ILogger<AddImageCommandHandler> logger
            )
        {
            _imagePreProcessingFactory = imagePreProcessingFactory;
            _storageFactory = storageFactory;
            _logger = logger;
        }
        public async Task<AddImageCommandResult> Handle(AddImageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogDebug("try to add image...");
                List<string> scaledImages = new List<string>();
                var match = _regex.Match(request.FileName);
                var name = match.Result("${name}");
                var extension = match.Result("${extension}");
                var storage = _storageFactory.CreateStorage();
                var imagePreProcessing = _imagePreProcessingFactory.CreateImagePreProcessing();
                foreach (var scaledImage in imagePreProcessing.GetScaledImages(request.Data, extension))
                {
                    var storageResult = false;
                    if(!request.RewriteMode)
                        storageResult = await storage.AddAsync(name, $"{scaledImage.Scale}.{ extension}", scaledImage.Data);
                    else
                        storageResult = await storage.UpdateAsync(name, $"{scaledImage.Scale}.{ extension}", scaledImage.Data);
                    if(storageResult == true)
                        scaledImages.Add($"{name}/{scaledImage.Scale}.{extension}");
                    else
                        _logger.LogDebug($"image {name}/{scaledImage.Scale}.{extension} was not added!");
                }
                return new AddImageCommandResult($"{name}", scaledImages);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "add image failed!");
                return null;
            }
        }
    }
}
