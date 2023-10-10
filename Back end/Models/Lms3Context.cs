using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LMS.Models;

public partial class Lms3Context : DbContext
{
    public Lms3Context()
    {
    }

    public Lms3Context(DbContextOptions<Lms3Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<EmployeeCardDetail> EmployeeCardDetails { get; set; }

    public virtual DbSet<EmployeeCredential> EmployeeCredentials { get; set; }

    public virtual DbSet<EmployeeIssueDetail> EmployeeIssueDetails { get; set; }

    public virtual DbSet<EmployeeMaster> EmployeeMasters { get; set; }

    public virtual DbSet<ItemMaster> ItemMasters { get; set; }

    public virtual DbSet<LoanCardMaster> LoanCardMasters { get; set; }

    public virtual DbSet<LoanRequest> LoanRequests { get; set; }

    public virtual DbSet<Material> Materials { get; set; }
    public virtual DbSet<ItemPurchaseDto> ItemPurchaseDtos { get; set; }

    public virtual DbSet<LoanApplicationDto> LoanApplications { get; set; }

    public virtual DbSet<LoanRequestDto> LoanRequestDtos { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=WINDOWS-BVQNF6J;Database=LMS3;Trusted_Connection=True;Encrypt=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<LoanRequestDto>().HasNoKey();
        modelBuilder.Entity<ItemPurchaseDto>().HasNoKey();
        modelBuilder.Entity<LoanApplicationDto>().HasNoKey();
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Category1).HasName("PK__categori__F7F53CC38DEB8C89");

            entity.ToTable("categories");

            entity.Property(e => e.Category1)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("category");

            entity.HasMany(d => d.Materials).WithMany(p => p.Categories)
                .UsingEntity<Dictionary<string, object>>(
                    "CategoryMaterial",
                    r => r.HasOne<Material>().WithMany()
                        .HasForeignKey("Material")
                        .HasConstraintName("FK__category___mater__29572725"),
                    l => l.HasOne<Category>().WithMany()
                        .HasForeignKey("Category")
                        .HasConstraintName("FK__category___categ__286302EC"),
                    j =>
                    {
                        j.HasKey("Category", "Material").HasName("PK__category__AA1898F75A318365");
                        j.ToTable("category_material");
                        j.IndexerProperty<string>("Category")
                            .HasMaxLength(100)
                            .IsUnicode(false)
                            .HasColumnName("category");
                        j.IndexerProperty<string>("Material")
                            .HasMaxLength(100)
                            .IsUnicode(false)
                            .HasColumnName("material");
                    });
        });

        modelBuilder.Entity<EmployeeCardDetail>(entity =>
        {
            entity.HasKey(e => e.EmployeeCardId).HasName("PK_EMPLOYEE_CARD_DETAILS");

            entity.ToTable("employee_card_details");

            entity.Property(e => e.EmployeeCardId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("employee_card_id");
            entity.Property(e => e.CardIssueDate)
                .HasDefaultValueSql("(CONVERT([date],getdate()))")
                .HasColumnType("date")
                .HasColumnName("card_issue_date");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.LoanId).HasColumnName("loan_id");

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeCardDetails)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__employee___emplo__440B1D61");

            entity.HasOne(d => d.Loan).WithMany(p => p.EmployeeCardDetails)
                .HasForeignKey(d => d.LoanId)
                .HasConstraintName("FK__employee___loan___44FF419A");
        });

        modelBuilder.Entity<EmployeeCredential>(entity =>
        {
            entity.HasKey(e => e.EmployeeEmail).HasName("PK__employee__0A874BCE30637485");

            entity.ToTable("employee_credentials");

            entity.Property(e => e.EmployeeEmail)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("employee_email");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.EmployeePassword)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("employee_password");
            entity.Property(e => e.EmployeeRole)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("employee_role");

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeCredentials)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__employee___emplo__36B12243");
        });

        modelBuilder.Entity<EmployeeIssueDetail>(entity =>
        {
            entity.HasKey(e => e.IssueId).HasName("PK__employee__D6185C39BF49EA9E");

            entity.ToTable("employee_issue_details");

            entity.Property(e => e.IssueId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("issue_id");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.IssueDate)
                .HasDefaultValueSql("(CONVERT([date],getdate()))")
                .HasColumnType("date")
                .HasColumnName("issue_date");
            entity.Property(e => e.ItemId).HasColumnName("item_id");
            entity.Property(e => e.ReturnDate)
                .HasColumnType("date")
                .HasColumnName("return_date");

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeIssueDetails)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__employee___emplo__3B75D760");

            entity.HasOne(d => d.Item).WithMany(p => p.EmployeeIssueDetails)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__employee___item___3C69FB99");
        });

        modelBuilder.Entity<EmployeeMaster>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__employee__C52E0BA803F75520");

            entity.ToTable("employee_master");

            entity.Property(e => e.EmployeeId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("employee_id");
            entity.Property(e => e.DateOfBirth)
                .HasColumnType("date")
                .HasColumnName("date_of_birth");
            entity.Property(e => e.DateOfJoining)
                .HasDefaultValueSql("(CONVERT([date],getdate()))")
                .HasColumnType("date")
                .HasColumnName("date_of_joining");
            entity.Property(e => e.Department)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("department");
            entity.Property(e => e.Designation)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("designation");
            entity.Property(e => e.EmployeeName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("employee_name");
            entity.Property(e => e.Gender)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("gender");
        });

        modelBuilder.Entity<ItemMaster>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("PK__item_mas__52020FDD54EE1823");

            entity.ToTable("item_master");

            entity.Property(e => e.ItemId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("item_id");
            entity.Property(e => e.IssueStatus)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("issue_status");
            entity.Property(e => e.ItemCategory)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("item_category");
            entity.Property(e => e.ItemDescription)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("item_description");
            entity.Property(e => e.ItemMake)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("item_make");
            entity.Property(e => e.ItemValuation).HasColumnName("item_valuation");

            entity.HasOne(d => d.ItemCategoryNavigation).WithMany(p => p.ItemMasters)
                .HasForeignKey(d => d.ItemCategory)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__item_mast__item___2F10007B");

            entity.HasOne(d => d.ItemMakeNavigation).WithMany(p => p.ItemMasters)
                .HasForeignKey(d => d.ItemMake)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__item_mast__item___2E1BDC42");
        });

        modelBuilder.Entity<LoanCardMaster>(entity =>
        {
            entity.HasKey(e => e.LoanId).HasName("PK__loan_car__A1F7955494B39180");

            entity.ToTable("loan_card_master");

            entity.HasIndex(e => new { e.LoanType, e.DurationInYears }, "UC_LoanType_Duration").IsUnique();

            entity.Property(e => e.LoanId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("loan_id");
            entity.Property(e => e.DurationInYears).HasColumnName("duration_in_years");
            entity.Property(e => e.LoanType)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("loan_type");

            entity.HasOne(d => d.LoanTypeNavigation).WithMany(p => p.LoanCardMasters)
                .HasForeignKey(d => d.LoanType)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__loan_card__loan___412EB0B6");
        });

        modelBuilder.Entity<LoanRequest>(entity =>
        {
            entity.HasKey(e => e.RequestId);

            entity.ToTable("loan_request");

            entity.HasIndex(e => new { e.EmployeeId, e.ItemId }, "UC_EmployeeItem").IsUnique();

            entity.Property(e => e.RequestId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("request_id");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.ItemId).HasColumnName("item_id");
            entity.Property(e => e.LoanId).HasColumnName("loan_id");

            entity.HasOne(d => d.Employee).WithMany(p => p.LoanRequests)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__loan_requ__emplo__48CFD27E");

            entity.HasOne(d => d.Item).WithMany(p => p.LoanRequests)
                .HasForeignKey(d => d.ItemId)
                .HasConstraintName("FK__loan_requ__item___49C3F6B7");

            entity.HasOne(d => d.Loan).WithMany(p => p.LoanRequests)
                .HasForeignKey(d => d.LoanId)
                .HasConstraintName("FK__loan_requ__loan___4AB81AF0");
        });

        modelBuilder.Entity<Material>(entity =>
        {
            entity.HasKey(e => e.Material1).HasName("PK__material__DEDA4344B6DFAE9F");

            entity.ToTable("materials");

            entity.Property(e => e.Material1)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("material");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
