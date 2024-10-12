using CMCS.Models;

namespace CMCS.Models
{
    // Models/LecturerClaim.cs
    public class ClaimForm
    {
        public int Id { get; set; }

        public User User { get; set; }  // Navigation property
        public required string Tittle { get; set; }
        public int HoursWorked { get; set; }
        public decimal HourlyRate { get; set; }
        public string Notes { get; set; }
        public ClaimStatus Status { get; set; } = ClaimStatus.Pending; // Default to PendingVerification

        // Navigation property for associated documents
        public ICollection<Document> Documents { get; set; }
    }

    public enum ClaimStatus
    {
        Pending, 
       Verified,    
        Approved,            
        Rejected            
    }
}
