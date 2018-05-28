using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SkillsCub.Core.Services
{
    public interface IStorageClient
    {
        Task UploadFileAsync(Stream str, string filename, string folderName);
        Task<IEnumerable<AttachedFile>> GetFilesFromNodeAsync(string folderName);
        Task<Stream> DownloadFileFromNodeAsync(string fileName, string folderName);
        Task<Stream> DownloadFolderAsync(string nodeName);
        Task<bool> RemoveNodeAsync(string fileName, string folderName);
        Task<bool> RemoveFolderAsync(string folderName);
    }
}
