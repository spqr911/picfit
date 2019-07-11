using MediatR;
using Microsoft.Extensions.Logging;
using picfit.application.Infrastructure;
using picfit.application.Infrastructure.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace picfit.application.Queries
{
    public class GetScaledImageQueryHandler : IRequestHandler<GetScaledImageQuery, GetImageViewModel>
    {
        private readonly IStorageFactory _storageFactory;
        private readonly ILogger _logger;

        public GetScaledImageQueryHandler(IStorageFactory storageFactory, ILogger<GetScaledImageQueryHandler> logger)
        {
            _storageFactory = storageFactory;
            _logger = logger;
        }

        public async Task<GetImageViewModel> Handle(GetScaledImageQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogDebug("try to get image...");
                var storage = _storageFactory.CreateStorage();
                var data = storage.Get(request.FolderName, request.FileName);
                _logger.LogDebug("image was recieved!");
                return new GetImageViewModel(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "get image failed!");
                return null;
            }
        }
    }
}
