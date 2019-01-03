namespace DrivingSchoolWebApp.Data.Common
{
    using System.IO;
    using System.Threading.Tasks;
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;

    public static class Helpers
    {
        public static async Task<string> UploadImage(Cloudinary cloudinary, IFormFile fileform, string name)
        {
            if (fileform == null)
            {
                return null;
            }

            byte[] customImage;

            using (var memoryStream = new MemoryStream())
            {
                await fileform.CopyToAsync(memoryStream);
                customImage = memoryStream.ToArray();
            }

            var ms = new MemoryStream(customImage);

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(name, ms),
                Transformation = new Transformation().Width(200).Height(250).Crop("fit").SetHtmlWidth(250).SetHtmlHeight(100)
            };

            var uploadResult = cloudinary.Upload(uploadParams);

            ms.Dispose();
            return uploadResult.SecureUri.AbsoluteUri;

        }

        public static Cloudinary SetCloudinary()
        {
            var account = new Account(
                GlobalDataConstants.CloudinarySetup.CloudName,
                GlobalDataConstants.CloudinarySetup.AccApiKey,
                GlobalDataConstants.CloudinarySetup.AccSecret);

            var cloudinary = new Cloudinary(account);

            return cloudinary;
        }
    }
}
