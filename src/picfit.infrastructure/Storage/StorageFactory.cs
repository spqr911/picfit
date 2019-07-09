using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using picfit.application.Infrastructure;
using picfit.application.Infrastructure.Storage;
using picfit.infrastructure.Storage.FileSystem;
using System;
using System.Collections.Generic;
using System.Text;

namespace picfit.infrastructure.Storage
{
    public class StorageFactory : IStorageFactory
    {
        private readonly StorageConfig _config;
        private readonly ILoggerFactory _loggerFactory;
        public StorageFactory(IOptions<StorageConfig> config, ILoggerFactory loggerFactory)
        {
            _config = config.Value;
            _loggerFactory = loggerFactory;
        }
        public IStorageService CreateStorage()
        {
            IStorageService storage;
            if (_config.Type == "fs")
                storage = new FileSystemStorageService(
                    _config.Location,
                    _loggerFactory.CreateLogger<FileSystemStorageService>());
            else
                throw new ArgumentException("undefined storage type");
            return storage;
        }
    }
}
