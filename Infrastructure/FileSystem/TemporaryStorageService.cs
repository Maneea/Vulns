using System.Collections.ObjectModel;
using System.Text;

using Microsoft.Extensions.Logging;

namespace Vulns.Infrastructure;

public class TemporaryStorageService : IDisposable
{
    private ICollection<string> _files;
    private ICollection<string> _directories;
    private IDictionary<string, string> _keyedNodes;
    private const string _fsFileChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";
    private readonly ILogger<TemporaryStorageService> _logger;

    public TemporaryStorageService(ILogger<TemporaryStorageService> logger)
    {
        _logger = logger;
        _files = new Collection<string>();
        _directories = new Collection<string>();
        _keyedNodes = new Dictionary<string, string>();
    }

    public string CreateTemporaryFile() => CreateTemporaryFsObject(FileAttributes.Normal);
    public string CreateTemporaryFile(string key)
    {
        key = $"{nameof(TemporaryStorageService)}.{nameof(FileAttributes.Normal)}.{key}";
        if (_keyedNodes.ContainsKey(key))
            return _keyedNodes[key];
        var node = CreateTemporaryFsObject(FileAttributes.Normal);
        _keyedNodes.Add(key, node);
        return node;
    }
    public string CreateTemporaryDirectory() => CreateTemporaryFsObject(FileAttributes.Directory);
    public string CreateTemporaryDirectory(string key)
    {
        key = $"{nameof(TemporaryStorageService)}.{nameof(FileAttributes.Directory)}.{key}";
        if (_keyedNodes.ContainsKey(key))
            return _keyedNodes[key];
        var node = CreateTemporaryFsObject(FileAttributes.Directory);
        _keyedNodes.Add(key, node);
        return node;
    }
    private string CreateTemporaryFsObject(FileAttributes type)
    {

        string fullPath;
        do
        {
            var tmpPath = Path.GetTempPath();
            var tmpObjectName = GenerateRandomName();
            fullPath = $"{tmpPath}{tmpObjectName}";
            _logger.LogTrace($"generated random full path {fullPath}");
        } while (File.Exists(fullPath) || Directory.Exists(fullPath));
        if (type == FileAttributes.Directory)
        {
            Directory.CreateDirectory(fullPath);
            _directories.Add(fullPath);
        }
        else
        {
            File.Create(fullPath).Close();
            _files.Add(fullPath);
        }
        _logger.LogTrace($"created {(type == FileAttributes.Directory ? "directory" : "file")} {fullPath}");
        return fullPath;
    }
    private string GenerateRandomName(string? prefix = null, ushort length = 30)
    {
        if (string.IsNullOrEmpty(prefix))
            prefix = AppDomain.CurrentDomain.FriendlyName;

        StringBuilder sb = new();
        Random random = new Random((int)(DateTime.UtcNow.Ticks & (int.MaxValue)));
        char randomChar;
        do
        {
            randomChar = _fsFileChars.ElementAt(random.Next(_fsFileChars.Length - 1));
            sb.Append(randomChar);
        } while (sb.Length < length);
        return sb.ToString();
    }

    public void Dispose()
    {
        foreach (var file in _files)
        {
            File.Delete(file);
            _logger.LogTrace($"deleted file {file}");
        }
        foreach (var directory in _directories)
        {
            Directory.Delete(directory, true);
            _logger.LogTrace($"deleted directory {directory} recursively");
        }
    }
}