using Microsoft.Extensions.Logging;
using picfit.application.Extensions;
using picfit.application.Infrastructure.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace picfit.infrastructure.Storage.FileSystem
{
    public class FileSystemStorageService : IStorageService
    {
        private string _location;
        private readonly ILogger _logger;
        public FileSystemStorageService(string location, ILogger<FileSystemStorageService> logger)
        {
            _location = location;
            _logger = logger;
        }

        public async Task<bool> AddAsync(string folderName, string fileName, byte[] data)
        {
            var folderPath = Path.Combine(_location, folderName);
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);
            var filePath = Path.Combine(folderPath, fileName);
            if (File.Exists(filePath))
                return false;
            using (FileStream fileStream = File.Create(filePath))
            {
                await fileStream.WriteAsync(data, 0, data.Length);
                fileStream.Seek(0, SeekOrigin.Begin);
                return true;
            }
        }

        public byte[] Get(string folderName, string fileName)
        {
            var path = Path.Combine(_location, folderName, fileName);
            using (var fileStream = File.Open(path, FileMode.Open))
            {
                return fileStream.ConvertToByteArray();
            }
        }

        public IEnumerable<string> Remove(string folderName)
        {
            var folderPath = Path.Combine(_location, folderName);
            if (!Directory.Exists(folderPath))
                return null;
            else
            {
                var result = 
                    Directory
                    .GetFiles(folderPath)
                    .Select(s=> Path.GetFileName(s));

                Directory.Delete(folderPath, true);
                return result.ToList();
            }
        }

        public async Task<bool> UpdateAsync(string folderName, string fileName, byte[] data)
        {
            var folderPath = Path.Combine(_location, folderName);
            if (!Directory.Exists(folderPath))
                return false;
            var filePath = Path.Combine(_location, folderPath, fileName);
            if (!File.Exists(filePath))
                return false;
            File.Delete(filePath);
            using (FileStream fileStream = File.Create(filePath))
            {
                await fileStream.WriteAsync(data, 0, data.Length);
                fileStream.Seek(0, SeekOrigin.Begin);
                return true;
            }
        }
    }
}
