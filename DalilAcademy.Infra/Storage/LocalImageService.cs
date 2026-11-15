using DalilAcademy.Core.Exceptions;
using DalilAcademy.Core.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalilAcademy.Infra.Storage
{
    public class LocalImageService : IImageService
    {
        private readonly string _folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images");

        private static readonly string[] Allowed = [".jpg", ".jpeg", ".png"];

        public async Task<string> SaveCourseImageAsync(Stream content, string fileName, long length) 
        {
            if (length == 0 || length > 2 * 1024 * 1024)
                throw new InvaildFileException("Image must be less than 2MB");
            var ext = Path.GetExtension(fileName).ToLower();
            if (!Allowed.Contains(ext))
                throw new InvaildFileException("bad format");
            Directory.CreateDirectory(_folder);
            var name = $"{Guid.NewGuid()}{ext}";
            await using var file = File.Create(Path.Combine(_folder, name));
            await content.CopyToAsync(file);
            return "/Images" + name;
        }
        public void DeleteCourseImage(string? imageUrl) 
        {
            if (string.IsNullOrEmpty(imageUrl)) return;
            var path = Path.Combine(_folder, Path.GetFileName(imageUrl));
            if(File.Exists(path))
                File.Delete(path); 
        }


    }
}
