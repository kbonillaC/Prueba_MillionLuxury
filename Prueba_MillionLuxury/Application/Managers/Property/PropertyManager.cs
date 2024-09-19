using System.Text.RegularExpressions;

namespace Luxury.Api.Application.Managers.Property
{
    public class PropertyManager : IPropertyManager
    {
        public byte[] getByteImage(string image)
        {
            // Opcional: Validar que el Base64 sea una imagen
            var regex = new Regex(@"^data:image\/[a-zA-Z]+;base64,");
            //if (!regex.IsMatch(model.Base64Image))
            //{
            //    return BadRequest("Invalid image format.");
            //}

            // Eliminar el prefijo del Base64
            var base64Data = regex.Replace(image, string.Empty);
            byte[] imageData = Convert.FromBase64String(base64Data);

            return imageData;
        }
    }
}
