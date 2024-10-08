namespace Fantasy.Backend.Helpers;

public class FileStorage : IFileStorage
{
    private readonly string _baseDirectory;

    public FileStorage(IConfiguration configuration)
    {
        //_baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        _baseDirectory = configuration.GetValue<string>("FileStorageBasePath")!;
    }

    public async Task RemoveFileAsync(string path, string containerName)
    {
        //var client = new BlobContainerClient(_connectionString, containerName);
        //await client.CreateIfNotExistsAsync();
        //var fileName = Path.GetFileName(path);
        //var blob = client.GetBlobClient(fileName);
        //await blob.DeleteIfExistsAsync();
        var relativePath = Path.Combine(containerName, path);
        var fullPath = Path.Combine(_baseDirectory, relativePath);
        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }
        await Task.Delay(1);
    }

    public async Task<string> SaveFileAsync(byte[] content, string extention, string containerName)
    {
        //var client = new BlobContainerClient(_connectionString, containerName);
        //await client.CreateIfNotExistsAsync();
        //client.SetAccessPolicy(PublicAccessType.Blob);
        //var blob = client.GetBlobClient(fileName);
        //using (var ms = new MemoryStream(content))
        //{
        //    await blob.UploadAsync(ms);
        //}
        //return blob.Uri.ToString();

        var fileName = $"{Guid.NewGuid()}{extention}";
        var relativePath = Path.Combine(containerName, fileName);
        var fullPath = Path.Combine(_baseDirectory, relativePath);
        await File.WriteAllBytesAsync(fullPath, content);
        return fileName;
    }
}