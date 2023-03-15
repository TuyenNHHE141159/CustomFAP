using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BusinessObject.Models;

public partial class MyFapContext : DbContext
{
    public MyFapContext()
    {
    }

    public MyFapContext(DbContextOptions<MyFapContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Mark> Marks { get; set; }

    public virtual DbSet<News> News { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<StudentEnrollment> StudentEnrollments { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    public virtual DbSet<TeacherEnrollment> TeacherEnrollments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("MyDB"));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.ToTable("Account");

            entity.Property(e => e.AccountId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("account_id");
            entity.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.UserName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("user_name");

            entity.HasMany(d => d.Roles).WithMany(p => p.Accounts)
                .UsingEntity<Dictionary<string, object>>(
                    "AccountRole",
                    r => r.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Account_Role_Role"),
                    l => l.HasOne<Account>().WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Account_Role_Account"),
                    j =>
                    {
                        j.HasKey("AccountId", "RoleId");
                        j.ToTable("Account_Role");
                        j.IndexerProperty<string>("AccountId")
                            .HasMaxLength(10)
                            .IsFixedLength()
                            .HasColumnName("account_id");
                        j.IndexerProperty<string>("RoleId")
                            .HasMaxLength(10)
                            .IsFixedLength()
                            .HasColumnName("role_id");
                    });
        });

        modelBuilder.Entity<Class>(entity =>
        {
            entity.ToTable("Class");

            entity.Property(e => e.ClassId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("class_id");
            entity.Property(e => e.ClassName)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("class_name");
        });

        modelBuilder.Entity<Mark>(entity =>
        {
            entity.HasKey(e => new { e.StudentId, e.SubjectId });

            entity.ToTable("Mark");

            entity.Property(e => e.StudentId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("student_id");
            entity.Property(e => e.SubjectId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("subject_id");
            entity.Property(e => e.Fe).HasColumnName("fe");
            entity.Property(e => e.Lab).HasColumnName("lab");
            entity.Property(e => e.Pe).HasColumnName("pe");
            entity.Property(e => e.ProgrestTest).HasColumnName("progrest_test");

            entity.HasOne(d => d.Student).WithMany(p => p.Marks)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Mark_Student");

            entity.HasOne(d => d.Subject).WithMany(p => p.Marks)
                .HasForeignKey(d => d.SubjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Mark_Subject");
        });

        modelBuilder.Entity<News>(entity =>
        {
            entity.Property(e => e.NewsId)
                .ValueGeneratedNever()
                .HasColumnName("news_id");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("created_date");
            entity.Property(e => e.Desciption)
                .HasMaxLength(250)
                .HasColumnName("desciption");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .HasColumnName("title");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");

            entity.Property(e => e.RoleId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("role_id");
            entity.Property(e => e.RoleName)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("role_name");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.ToTable("Student");

            entity.Property(e => e.StudentId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("student_id");
            entity.Property(e => e.StudentName)
                .HasMaxLength(50)
                .HasColumnName("student_name");

            entity.HasOne(d => d.StudentNavigation).WithOne(p => p.Student)
                .HasForeignKey<Student>(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Student_Account");
        });

        modelBuilder.Entity<StudentEnrollment>(entity =>
        {
            entity.HasKey(e => new { e.StudentId, e.SubjectId }).HasName("PK_StudentEnrollment_1");

            entity.ToTable("StudentEnrollment");

            entity.Property(e => e.StudentId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("student_id");
            entity.Property(e => e.SubjectId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("subject_id");
            entity.Property(e => e.ClassId)
                .IsRequired()
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("class_id");

            entity.HasOne(d => d.Class).WithMany(p => p.StudentEnrollments)
                .HasForeignKey(d => d.ClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudentEnrollment_Class");

            entity.HasOne(d => d.Student).WithMany(p => p.StudentEnrollments)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudentEnrollment_Student");

            entity.HasOne(d => d.Subject).WithMany(p => p.StudentEnrollments)
                .HasForeignKey(d => d.SubjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudentEnrollment_Subject");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.ToTable("Subject");

            entity.Property(e => e.SubjectId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("subject_id");
            entity.Property(e => e.SubjectName)
                .HasMaxLength(50)
                .HasColumnName("subject_name");
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.ToTable("Teacher");

            entity.Property(e => e.TeacherId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("teacher_id");
            entity.Property(e => e.TeacherName)
                .HasMaxLength(50)
                .HasColumnName("teacher_name");

            entity.HasOne(d => d.TeacherNavigation).WithOne(p => p.Teacher)
                .HasForeignKey<Teacher>(d => d.TeacherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Teacher_Account");
        });

        modelBuilder.Entity<TeacherEnrollment>(entity =>
        {
            entity.HasKey(e => new { e.ClassId, e.SubjectId }).HasName("PK_Teacher_Enrollment_1");

            entity.ToTable("Teacher_Enrollment");

            entity.Property(e => e.ClassId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("class_id");
            entity.Property(e => e.SubjectId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("subject_id");
            entity.Property(e => e.TeacherId)
                .IsRequired()
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("teacher_id");

            entity.HasOne(d => d.Class).WithMany(p => p.TeacherEnrollments)
                .HasForeignKey(d => d.ClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Teacher_Enrollment_Class");

            entity.HasOne(d => d.Subject).WithMany(p => p.TeacherEnrollments)
                .HasForeignKey(d => d.SubjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Teacher_Enrollment_Subject");

            entity.HasOne(d => d.Teacher).WithMany(p => p.TeacherEnrollments)
                .HasForeignKey(d => d.TeacherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Teacher_Enrollment_Teacher");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
