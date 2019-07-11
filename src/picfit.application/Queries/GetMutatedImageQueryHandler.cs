using MediatR;
using Microsoft.Extensions.Logging;
using picfit.application.Infrastructure.Image;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace picfit.application.Queries
{
    public class GetMutatedImageQueryHandler : IRequestHandler<GetMutatedImageQuery, GetImageViewModel>
    {
        private readonly IImageProcessingFactory _imageProcessingFactory;
        private readonly ILogger _logger;
        public GetMutatedImageQueryHandler(IImageProcessingFactory imageProcessingFactory, ILogger<GetMutatedImageQueryHandler> logger)
        {
            _imageProcessingFactory = imageProcessingFactory;
            _logger = logger;
        }

        public async Task<GetImageViewModel> Handle(GetMutatedImageQuery request, CancellationToken cancellationToken)
        {
            var imageProcessing = _imageProcessingFactory.CreateImageProcessing();
            byte[] data = null;

            imageProcessing.Mutate(data, request.Width, request.Height, request.Extension);
            return null;
        }
    }
}
