using CMCS.Data;
using CMCS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

[Authorize]
public class ClaimController : Controller
{
    private readonly CMCSContext _context;
    private readonly UserManager<User> _userManager;

    public ClaimController(CMCSContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [HttpGet]
    public IActionResult SubmitClaim()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SubmitClaim(ClaimForm claimForm, List<IFormFile> SupportingDocuments)
    {
        // Get the current user ID
        var userId = _userManager.GetUserId(User);
        claimForm.User = await _context.Users.FindAsync(userId);  // Associate the claim with the logged-in user

        // Initialize an error message holder
        string errorMessage = null;

        // Validation logic
        if (claimForm.HoursWorked <= 0)
        {
            errorMessage = "Hours worked must be greater than zero.";
        }
        else if (claimForm.HourlyRate <= 0)
        {
            errorMessage = "Hourly rate must be greater than zero.";
        }
        // Check if no documents are uploaded
        else if (SupportingDocuments == null || !SupportingDocuments.Any())
        {
            errorMessage = "You must upload at least one supporting document.";
        }

        // Check if there's an error
        if (errorMessage == null)
        {
            // If no errors, save the claim to the database first
            _context.Claims.Add(claimForm);
            await _context.SaveChangesAsync();

            // Handle file uploads
            foreach (var file in SupportingDocuments)
            {
                if (file.Length > 0)
                {
                    // Save file to the server
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", file.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    // Create a new Document entry and associate it with the claim
                    var document = new Document
                    {
                        ClaimId = claimForm.Id, // Ensure that claim.Id is populated after saving the claim
                        FileName = file.FileName,
                        FilePath = "/uploads/" + file.FileName, // Relative path for web access
                        FileType = file.ContentType,
                        UploadDate = DateTime.Now
                    };

                    // Save document info to the database
                    _context.Documents.Add(document);
                }
            }

            // Save all document changes to the database
            await _context.SaveChangesAsync();

            // Redirect to Dashboard after successful submission
            return RedirectToAction("Index");
        }
        else
        {
            // If there's an error, display the error message on the view
            ViewBag.ErrorMessage = errorMessage;
            return View(claimForm);
        }
    }

    // Dashboard action to list all claims submitted by the current user
    public async Task<IActionResult> Index()
    {
        var userId = _userManager.GetUserId(User);  // Get the logged-in user's ID
        var claims = await _context.Claims
            .Include(c => c.Documents)  // Include documents associated with each claim
            .Where(c => c.User.Id == userId)  // Only show claims belonging to the current user
            .ToListAsync();

        return View(claims);  // Pass the claims to the view
    }


    // Coordinator View for verifying claims
    [Authorize(Roles = "ProgrammeCoordinator")]
    public async Task<IActionResult> VerifyClaims()
    {
        var claims = await _context.Claims
            .Where(c => c.Status == ClaimStatus.Pending) // Only pending claims
            .Include(c => c.User)  // Include user info
            .ToListAsync();

        return View(claims);
    }

    // Verify Claim Action
    [HttpPost]
    [Authorize(Roles = "ProgrammeCoordinator")]
    public async Task<IActionResult> VerifyClaim(int id)
    {
        var claim = await _context.Claims.FindAsync(id);
        if (claim != null)
        {
            claim.Status = ClaimStatus.Verified; // Update the claim status
            await _context.SaveChangesAsync();
        }

        return RedirectToAction("VerifyClaims");
    }

    

    // Manager View for approving claims
    [Authorize(Roles = "AcademicManager")]
    public async Task<IActionResult> ApproveClaims()
    {
        var claims = await _context.Claims
            .Where(c => c.Status == ClaimStatus.Verified) // Only verified claims
            .Include(c => c.User)  // Include user info
            .ToListAsync();

        return View(claims);
    }

    // Approve Claim Action
    [HttpPost]
    [Authorize(Roles = "AcademicManager")]
    public async Task<IActionResult> ApproveClaim(int id)
    {
        var claim = await _context.Claims.FindAsync(id);
        if (claim != null)
        {
            claim.Status = ClaimStatus.Approved; // Update the claim status
            await _context.SaveChangesAsync();
        }

        return RedirectToAction("ApproveClaims");
    }

   
}


