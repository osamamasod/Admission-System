using Microsoft.EntityFrameworkCore;
using AdmissionSystem.Core.Models;

namespace AdmissionSystem.Infrastructure.Data;

public class AdmissionDbContext : DbContext
{
    public AdmissionDbContext(DbContextOptions<AdmissionDbContext> options) : base(options)
    {
    }

    // DbSets for all entities
    public DbSet<Applicant> Applicants { get; set; }
    public DbSet<ApplicantAdmission> ApplicantAdmissions { get; set; }
    public DbSet<Document> Documents { get; set; }
    public DbSet<Passport> Passports { get; set; }
    public DbSet<EducationDocument> EducationDocuments { get; set; }
    public DbSet<FileEntity> Files { get; set; } // Use FileEntity instead of File
    public DbSet<Manager> Managers { get; set; }
    public DbSet<Faculty> Faculties { get; set; }
    public DbSet<Program> Programs { get; set; }
    public DbSet<AdmissionProgram> AdmissionPrograms { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    // Configure TPH (Table-Per-Hierarchy) for Document inheritance
    modelBuilder.Entity<Document>()
        .HasDiscriminator<string>("DocumentType")
        .HasValue<Passport>("Passport")
        .HasValue<EducationDocument>("EducationDocument");

    // Configure relationships

    // Applicant -> Documents (One-to-Many) - EXCLUDE Passport from this collection
    modelBuilder.Entity<Applicant>()
        .HasMany(a => a.Documents)
        .WithOne(d => d.Applicant)
        .HasForeignKey(d => d.ApplicantId)
        .OnDelete(DeleteBehavior.Cascade);

    // Applicant -> ApplicantAdmissions (One-to-Many)
    modelBuilder.Entity<Applicant>()
        .HasMany(a => a.Admissions)
        .WithOne(aa => aa.Applicant)
        .HasForeignKey(aa => aa.ApplicantId)
        .OnDelete(DeleteBehavior.Cascade);

    // Applicant -> Passport (One-to-One) - Use separate relationship
    
    // Configure that Passport should NOT be in the Documents collection
    modelBuilder.Entity<Applicant>()
        .HasMany(a => a.Documents)
        .WithOne(d => d.Applicant)
        .HasForeignKey(d => d.ApplicantId)
        .OnDelete(DeleteBehavior.Cascade)
        .HasConstraintName("FK_Documents_Applicants");

    // Faculty -> Programs (One-to-Many)
    modelBuilder.Entity<Faculty>()
        .HasMany(f => f.Programs)
        .WithOne(p => p.Faculty)
        .HasForeignKey(p => p.FacultyId)
        .OnDelete(DeleteBehavior.Restrict);

    // Faculty -> Managers (One-to-Many)
    modelBuilder.Entity<Faculty>()
        .HasMany(f => f.Managers)
        .WithOne(m => m.Faculty)
        .HasForeignKey(m => m.FacultyId)
        .OnDelete(DeleteBehavior.Restrict);

    // Manager -> ApplicantAdmissions (One-to-Many)
    modelBuilder.Entity<Manager>()
        .HasMany(m => m.ManagedAdmissions)
        .WithOne(aa => aa.Manager)
        .HasForeignKey(aa => aa.ManagerId)
        .OnDelete(DeleteBehavior.Restrict);

    // Configure many-to-many relationship between ApplicantAdmission and Program
    // using AdmissionProgram as join table
    modelBuilder.Entity<AdmissionProgram>()
        .HasKey(ap => ap.Id);

    modelBuilder.Entity<AdmissionProgram>()
        .HasOne(ap => ap.ApplicantAdmission)
        .WithMany(aa => aa.AdmissionPrograms)
        .HasForeignKey(ap => ap.ApplicantAdmissionId)
        .OnDelete(DeleteBehavior.Cascade);

    modelBuilder.Entity<AdmissionProgram>()
        .HasOne(ap => ap.Program)
        .WithMany(p => p.AdmissionPrograms)
        .HasForeignKey(ap => ap.ProgramId)
        .OnDelete(DeleteBehavior.Restrict);

    // File relationships (optional)
    modelBuilder.Entity<FileEntity>()
        .HasOne(f => f.Applicant)
        .WithMany()
        .HasForeignKey(f => f.ApplicantId)
        .OnDelete(DeleteBehavior.SetNull);

    modelBuilder.Entity<FileEntity>()
        .HasOne(f => f.Document)
        .WithMany()
        .HasForeignKey(f => f.DocumentId)
        .OnDelete(DeleteBehavior.SetNull);

    // Configure unique constraints
    modelBuilder.Entity<Applicant>()
        .HasIndex(a => a.Email)
        .IsUnique();

    modelBuilder.Entity<ApplicantAdmission>()
        .HasIndex(aa => aa.ApplicationNumber)
        .IsUnique();

    modelBuilder.Entity<Faculty>()
        .HasIndex(f => f.Code)
        .IsUnique();

    modelBuilder.Entity<Program>()
        .HasIndex(p => p.Code)
        .IsUnique();

    modelBuilder.Entity<Manager>()
        .HasIndex(m => m.Email)
        .IsUnique();
}
}