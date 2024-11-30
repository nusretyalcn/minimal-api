using Microsoft.AspNetCore.Http;

namespace Core.Utilities.Helpers;

public interface IFileHelper
{
    public string Upload(IFormFile file,string root);
    void Delete(string filePath,string root);
    string Update(IFormFile file,string filePath,string root);
}