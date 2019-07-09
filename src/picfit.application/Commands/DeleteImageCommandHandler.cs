using MediatR;
using Microsoft.Extensions.Logging;
using picfit.application.Infrastructure;
using picfit.application.Infrastructure.Image;
using picfit.application.Infrastructure.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace picfit.application.Commands
{
    public class DeleteImageCommandHandler : IRequestHandler<DeleteImageCommand, DeleteImageCommandResult>
    {
        private readonly IStorageFactory _storageFactory;
        private readonly ILogger _logger;

        public DeleteImageCommandHandler(IStorageFactory storageFactory, ILogger<DeleteImageCommandHandler> logger)
        {
            _storageFactory = storageFactory;
            _logger = logger;
        }
        public async Task<DeleteImageCommandResult> Handle(DeleteImageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogDebug("try to delete images...");
                var storage = _storageFactory.CreateStorage();
                var result = storage.Remove(request.FolderName);
                _logger.LogDebug("images was deleted!");
                return new DeleteImageCommandResult(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "delete image failed!");
                return null;
            }
        }
    }
}
