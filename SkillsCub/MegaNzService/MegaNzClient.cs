using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CG.Web.MegaApiClient;
using SkillsCub.Core;
using SkillsCub.Core.Services;

namespace MegaNzService
{
    public class MegaNzClient : IStorageClient
    {
        private readonly IMegaApiClient _client;

        public MegaNzClient(IMegaApiClient client)
        {
            _client = client;
        }

        public async Task UploadFileAsync(Stream file, string filename, string folderName)
        {

            var token = await _client.LoginAsync("Email", "Password");
            if (token != null)
            {
                var nodes = (await _client.GetNodesAsync()).ToList();
                var parent = nodes.Single(n => n.Type == NodeType.Root);
                var targetNodeParent =
                    nodes.FirstOrDefault(node => node.ParentId.Equals(parent.Id) && node.Name.Equals(folderName)) ??
                    await _client.CreateFolderAsync(folderName, parent);
                var targetNode = await _client.UploadAsync(file, filename, targetNodeParent, new Progress<double>());
            }

            await _client.LogoutAsync();
        }

        public async Task<IEnumerable<AttachedFile>> GetFilesFromNodeAsync(string folderName)
        {
            try
            {
                var token = await _client.LoginAsync("Email", "Password");
                if (token != null)
                {
                    var nodes = (await _client.GetNodesAsync()).ToList();
                    var parent = nodes.Single(n => n.Type == NodeType.Root);
                    var targetNode = nodes.FirstOrDefault(node =>
                        node.ParentId.Equals(parent.Id) && node.Name.Equals(folderName));
                    if (targetNode != null)
                    {
                        var files = nodes.Where(x => x.ParentId == targetNode.Id && x.Type == NodeType.File)
                            .Select(node
                                => new AttachedFile()
                                {
                                    Id = node.Id,
                                    FileName = node.Name,
                                    Link = _client.GetDownloadLink(node),
                                    FileStream = _client.Download(node)
                                }).ToList();
                        await _client.LogoutAsync();
                        return files;
                    }
                }

                await _client.LogoutAsync();
                return null;
            }
            catch (Exception ex)
            {
                await _client.LogoutAsync();
                return null;
            }
        }

        public async Task<Stream> DownloadFileFromNodeAsync(string fileName, string folderName)
        {

            var token = await _client.LoginAsync("Email", "Password");
            if (token != null)
            {
                var nodes = await _client.GetNodesAsync();
                var parent = nodes.Single(n => n.Type == NodeType.Root);
                var targetNode = nodes.FirstOrDefault(n => n.ParentId.Equals(parent.Id) && n.Name.Equals(folderName));

                var node = nodes.FirstOrDefault(n =>
                    n.ParentId.Equals(targetNode.Id) && n.Name.Equals(fileName) && n.Type == NodeType.File);

                var stream = await _client.DownloadAsync(node, new Progress<double>());

                await _client.LogoutAsync();

                return stream;
            }

            await _client.LogoutAsync();

            return null;
        }

        public async Task<Stream> DownloadFolderAsync(string folderName)
        {
            var token = await _client.LoginAsync("Email", "Password");
            if (token != null)
            {
                var nodes = await _client.GetNodesAsync();
                var parent = nodes.Single(n => n.Type == NodeType.Root);

                var targetNode = nodes.FirstOrDefault(n => n.ParentId.Equals(parent.Id) && n.Name.Equals(folderName)) ??
                                 await _client.CreateFolderAsync(folderName, parent);

                var stream = await _client.DownloadAsync(targetNode, new Progress<double>());

                await _client.LogoutAsync();

                return stream;
            }

            _client.Logout();
            return null;
        }

        public async Task<bool> RemoveNodeAsync(string fileName, string folderName)
        {
            try
            {
                var token = await _client.LoginAsync("Email", "Password");
                if (token != null)
                {
                    var nodes = (await _client.GetNodesAsync()).ToList();
                    var parent = nodes.Single(n => n.Type == NodeType.Root);

                    var targetNodeParent =
                        nodes.FirstOrDefault(n => n.ParentId.Equals(parent.Id) && n.Name.Equals(folderName));
                    if (targetNodeParent != null)
                    {
                        var targetNode = nodes.FirstOrDefault(n =>
                            n.ParentId.Equals(targetNodeParent.Id) && n.Name.Equals(fileName));
                        if (targetNode != null)
                        {
                            await _client.DeleteAsync(targetNode);
                            await _client.LogoutAsync();
                            return true;
                        }
                    }
                }

                await _client.LogoutAsync();
                return false;
            }
            catch (Exception ex)
            {
                await _client.LogoutAsync();
                return false;
            }
        }

        public async Task<bool> RemoveFolderAsync(string folderName)
        {
            try
            {
                var token = await _client.LoginAsync("Email", "Password");
                if (token != null)
                {
                    var nodes = (await _client.GetNodesAsync()).ToList();
                    var parent = nodes.Single(n => n.Type == NodeType.Root);

                    var targetNode =
                        nodes.FirstOrDefault(n => n.ParentId.Equals(parent.Id) && n.Name.Equals(folderName));
                    if (targetNode != null)
                    {
                        await _client.DeleteAsync(targetNode);
                        await _client.LogoutAsync();
                        return true;
                    }
                }

                await _client.LogoutAsync();
                return false;
            }
            catch (Exception ex)
            {
                await _client.LogoutAsync();
                return false;
            }
        }
    }
}