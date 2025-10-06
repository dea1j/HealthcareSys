using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PatientService.Domain.Entities;
using PatientService.Domain.ValueObjects;

namespace PatientService.Infrastructure.Persistence.Configurations;

public class PatientConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.FirstName)
            .IsRequired()
            .HasMaxLength(50);
        
        builder.Property(p => p.LastName)
            .IsRequired()
            .HasMaxLength(50);
        
        builder.Property(p => p.Email)
            .HasConversion(
                email => email.Value,
                value => Email.Create(value))
            .IsRequired()
            .HasMaxLength(100);
        
        builder.HasIndex(p => p.Email)
            .IsUnique();
        
        builder.Property(p => p.PhoneNumber)
            .HasConversion(
                phone => phone.Value,
                value => PhoneNumber.Create(value))
            .IsRequired()
            .HasMaxLength(15);
        
        builder.Property(p => p.DateOfBirth)
            .IsRequired();

        builder.Property(p => p.Address)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.CreatedAt)
            .IsRequired();

        builder.Property(p => p.UpdatedAt);
        
        builder.Ignore(p => p.FullName);
        builder.Ignore(p => p.Age);

        builder.ToTable("Patients");
    }
}