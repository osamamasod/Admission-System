using Microsoft.EntityFrameworkCore;
using AdmissionSystem.Core.Models;

namespace AdmissionSystem.Infrastructure.Data;

public class AdmissionDbContext : DbContext
{
    public AdmissionDbContext(DbContextOptions<AdmissionDbContext> options) : base(options)
    {
    }

   
    public DbSet<User> Users { get; set; }
    public DbSet<ApplicantAdmission> ApplicantAdmissions { get; set; }
    public DbSet<Document> Documents { get; set; }
    public DbSet<Passport> Passports { get; set; }
    public DbSet<EducationDocument> EducationDocuments { get; set; }
    public DbSet<FileEntity> Files { get; set; }
    public DbSet<Faculty> Faculties { get; set; }
    public DbSet<Program> Programs { get; set; }
    public DbSet<AdmissionProgram> AdmissionPrograms { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

   
        modelBuilder.Entity<User>()
            .HasDiscriminator<string>("UserType")
            .HasValue<Applicant>("Applicant")
            .HasValue<Manager>("Manager");

     
        modelBuilder.Entity<Document>()
            .HasDiscriminator<string>("DocumentType")
            .HasValue<Passport>("Passport")
            .HasValue<EducationDocument>("EducationDocument");


        modelBuilder.Entity<Applicant>()
            .HasMany(a => a.Documents)
            .WithOne(d => d.Applicant)
            .HasForeignKey(d => d.ApplicantId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Applicant>()
            .HasMany(a => a.Admissions)
            .WithOne(aa => aa.Applicant)
            .HasForeignKey(aa => aa.ApplicantId)
            .OnDelete(DeleteBehavior.Cascade);

 
        modelBuilder.Entity<Faculty>()
            .HasMany(f => f.Programs)
            .WithOne(p => p.Faculty)
            .HasForeignKey(p => p.FacultyId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Faculty>()
            .HasMany(f => f.Managers)
            .WithOne(m => m.Faculty)
            .HasForeignKey(m => m.FacultyId)
            .OnDelete(DeleteBehavior.Restrict);


        modelBuilder.Entity<Manager>()
            .HasMany(m => m.ManagedAdmissions)
            .WithOne(aa => aa.Manager)
            .HasForeignKey(aa => aa.ManagerId)
            .OnDelete(DeleteBehavior.Restrict);

        
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


        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
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
    }
}