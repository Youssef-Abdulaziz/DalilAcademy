using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalilAcademy.Core.Storage
{
    public interface IImageService
    {
        Task<string> SaveCourseImageAsync(Stream content, string fileName, long length);

        void DeleteCourseImage(string? imageUrl);
    }
}
