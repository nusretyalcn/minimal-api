using Microsoft.AspNetCore.Http;

namespace Core.Utilities.Helpers;

public interface IFileHelper
{
    public string Upload(IFormFile file,string root);
    public void Delete(string filePath);
    string Update(IFormFile file,string filePath,string root);
}