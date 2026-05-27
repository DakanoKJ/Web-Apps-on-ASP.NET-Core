using Microsoft.EntityFrameworkCore;
using PersonalAccount.Data.Entities;
using PersonalAccount.Utils;

namespace PersonalAccount.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<AccountEntity> Accounts => Set<AccountEntity>();
    public DbSet<ConfirmationTokenEntity> ConfirmationTokens => Set<ConfirmationTokenEntity>();


    public DbSet<StudentProfileEntity> StudentProfiles => Set<StudentProfileEntity>();
    public DbSet<TeacherProfileEntity> TeacherProfiles => Set<TeacherProfileEntity>();

    public DbSet<TeacherGroupSubjectEntity> TeacherGroupSubjects => Set<TeacherGroupSubjectEntity>();

    public DbSet<GroupEntity> Groups => Set<GroupEntity>();
    public DbSet<SubjectEntity> Subjects => Set<SubjectEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<AccountEntity>(entity =>
        {
            entity.BuildEntity("accounts");

            entity.Property(account => account.Email)
                .HasColumnName("email")
                .HasMaxLength(255)
                .IsRequired();

            entity.Property(account => account.PasswordHash)
                .HasColumnName("password_hash")
                .IsRequired();

            entity.Property(account => account.Role)
                .HasColumnName("role")
                .IsRequired();
        });

        modelBuilder.Entity<SubjectEntity>(entity =>
        {
            entity.BuildEntity("subjects");

            entity.Property(subject => subject.Name)
                .HasColumnName("name")
                .HasMaxLength(255)
                .IsRequired();
        });

        modelBuilder.Entity<GroupEntity>(entity =>
        {
            entity.BuildEntity("groups");

            entity.Property(group => group.Name)
                .HasColumnName("name")
                .HasMaxLength(255)
                .IsRequired();

            entity.Property(group => group.Description)
                .HasColumnName("description")
                .HasMaxLength(2047)
                .IsRequired();

            entity.Property(group => group.ImageUrl)
                .HasColumnName("image_url")
                .HasMaxLength(2047);

            entity.HasMany(group => group.StudentProfiles)
                .WithOne(student => student.Group)
                .HasForeignKey(student => student.GroupId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<TeacherGroupSubjectEntity>(entity =>
        {
            entity.BuildEntity("teacher_group_subjects");
            entity.HasIndex(link => link.TeacherAccountId);

            entity.HasOne(link => link.TeacherAccount)
                .WithMany(teacher => teacher.TeacherGroupSubjects)
                .HasForeignKey(link => link.TeacherAccountId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(link => link.Subject)
                .WithMany(subject => subject.TeacherGroupSubjects)
                .HasForeignKey(link => link.SubjectId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(link => link.Group)
                .WithMany(subject => subject.TeacherGroupSubjects)
                .HasForeignKey(link => link.GroupId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<TeacherProfileEntity>(entity =>
        {
            entity.BuildProfileEntity(
                "teacher_profiles",
                account => account.TeacherProfile
            );
        });

        modelBuilder.Entity<StudentProfileEntity>(entity =>
        {
            entity.BuildProfileEntity(
                "student_profiles",
                account => account.StudentProfile
            );

            entity.Property(student => student.GroupId)
                .HasColumnName("group_id");
        });

        modelBuilder.Entity<ConfirmationTokenEntity>(entity =>
        {
            entity.BuildEntity("confirmation_tokens");

            entity.Property(token => token.AccountId)
                .HasColumnName("account_id")
                .IsRequired();

            entity.Property(token => token.TokenHash)
                .HasColumnName("token_hash")
                .IsRequired();

            entity.Property(token => token.ExpiresAt)
                .HasColumnName("expires_at")
                .IsRequired();

            entity.Property(token => token.ConfirmedAt)
                .HasColumnName("confirmed_at");

            entity.HasOne(token => token.Account)
                .WithMany(student => student.ConfirmationTokens)
                .HasForeignKey(token => token.AccountId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}