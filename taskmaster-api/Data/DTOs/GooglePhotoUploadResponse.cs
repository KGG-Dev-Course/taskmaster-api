namespace taskmaster_api.Data.DTOs
{
    public class GooglePhotoUploadResponse
    {
        public List<UploadedFile> Files { get; set; }

        public GooglePhotoUploadResponse()
        {
            Files = new List<UploadedFile>();
        }
    }

    public class UploadedFile
    {
        public string FilePath { get; set; }
        public string FileName { get; set; }
    }
}
