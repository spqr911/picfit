using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace picfit.application.Infrastructure.Storage
{
    public interface IStorageService
    {
        Task<bool> AddAsync(string folderName, string fileName, byte[] data);
        Task<bool> UpdateAsync(string folderName, string fileName, byte[] data);
        IEnumerable<string> Remove(string folderName);
        byte[] Get(string folderName, string fileName);
    }
}
