namespace FoodSharing.Tools
{
    public static class FileTools
    {
        public static byte[] GetBytes(IFormFile file)
        {
            byte[] imageData = null;

            using (var binaryReader = new BinaryReader(file.OpenReadStream()))
            {
                imageData = binaryReader.ReadBytes((int)file.Length);
            }

            return imageData;
        }
    }
}
