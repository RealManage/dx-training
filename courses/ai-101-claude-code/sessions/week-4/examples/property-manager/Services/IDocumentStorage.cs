namespace RealManage.PropertyManager.Services;

public interface IDocumentStorage
{
    Task<string> SaveDocumentAsync(byte[] content, string fileName);
    Task<byte[]> GetDocumentAsync(string documentId);
    Task<bool> DeleteDocumentAsync(string documentId);
}

// TODO: This should be mocked in tests, never implemented!