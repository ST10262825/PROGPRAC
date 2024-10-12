namespace CMCS.Models
{
    // Models/Document.cs
    public class Document
    {
        public int Id { get; set; }
        public int ClaimId { get; set; }    // Foreign key to the Claim
        public ClaimForm Claim { get; set; }  // Navigation property

        public string FileName { get; set; } // Name of the uploaded file
        public string FilePath { get; set; } // Physical or relative path of the file
        public string FileType { get; set; } // File type (e.g., .pdf, .docx, etc.)
        public DateTime UploadDate { get; set; } = DateTime.Now; // Date when the file was uploaded
    }

}
