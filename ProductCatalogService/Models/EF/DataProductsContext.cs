using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ProductCatalogService.Models.EF
{
    public partial class DataProductsContext : DbContext
    {
        public DataProductsContext()
        {
        }

        public DataProductsContext(DbContextOptions<DataProductsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Advertising> Advertising { get; set; }
        public virtual DbSet<CatRatings> CatRatings { get; set; }
        public virtual DbSet<CatSizes> CatSizes { get; set; }
        public virtual DbSet<CatTypeDetails> CatTypeDetails { get; set; }
        public virtual DbSet<CatTypeProduct> CatTypeProduct { get; set; }
        public virtual DbSet<ChangesOnProduct> ChangesOnProduct { get; set; }
        public virtual DbSet<Comments> Comments { get; set; }
        public virtual DbSet<DetailProduct> DetailProduct { get; set; }
        public virtual DbSet<HistorialT> HistorialT { get; set; }
        public virtual DbSet<ImagesProduct> ImagesProduct { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Qualification> Qualification { get; set; }
        public virtual DbSet<SimilarProduct> SimilarProduct { get; set; }
        public virtual DbSet<SizeForProduct> SizeForProduct { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        // Unable to generate entity type for table 'dbo.Cards'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string db_products_server = Environment.GetEnvironmentVariable("db_products_server");
                string db_products_user = Environment.GetEnvironmentVariable("db_products_user");
                string db_products_password = Environment.GetEnvironmentVariable("db_products_password");
                string db_products_name_db = Environment.GetEnvironmentVariable("db_products_name_db");

                optionsBuilder.UseSqlServer(
                    "data source=" + db_products_server + "; " +
                    "initial catalog=" + db_products_name_db + "; " +
                    "user id=" + db_products_user + "; " +
                    "password=" + db_products_password
                );
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Advertising>(entity =>
            {
                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(300)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CatRatings>(entity =>
            {
                entity.HasKey(e => e.IdRating);

                entity.Property(e => e.Code).IsRequired();

                entity.Property(e => e.Description).IsRequired();
            });

            modelBuilder.Entity<CatSizes>(entity =>
            {
                entity.HasKey(e => e.IdSize);

                entity.HasIndex(e => e.IdType)
                    .HasName("IX_FK_CatTypeProductCatSizes");

                entity.Property(e => e.Code).IsRequired();

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.Unity).IsRequired();

                entity.Property(e => e.Value).IsRequired();
            });

            modelBuilder.Entity<CatTypeDetails>(entity =>
            {
                entity.HasKey(e => e.IdTypeDetail);

                entity.HasIndex(e => e.IdType)
                    .HasName("IX_FK_CatTypeProductCatTypeDetails");

                entity.Property(e => e.Code).IsRequired();

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<CatTypeProduct>(entity =>
            {
                entity.HasKey(e => e.IdType);

                entity.Property(e => e.Code).IsRequired();

                entity.Property(e => e.DateUpdate).HasColumnType("datetime");

                entity.Property(e => e.Description).IsRequired();
            });

            modelBuilder.Entity<ChangesOnProduct>(entity =>
            {
                entity.HasKey(e => e.IdLog);
            });

            modelBuilder.Entity<Comments>(entity =>
            {
                entity.HasKey(e => e.IdComent);

                entity.HasIndex(e => e.IdProduct)
                    .HasName("IX_FK_ProductsComments");

                entity.HasIndex(e => e.IdRating)
                    .HasName("IX_FK_CatRatingsComments");

                entity.Property(e => e.Comment).IsRequired();

                entity.Property(e => e.DateCommnet).HasColumnType("datetime");

                entity.Property(e => e.UserName).IsRequired();

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.IdProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductsComments");

                entity.HasOne(d => d.IdRatingNavigation)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.IdRating)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CatRatingsComments");
            });

            modelBuilder.Entity<DetailProduct>(entity =>
            {
                entity.HasKey(e => e.IdDetail);

                entity.HasIndex(e => e.IdProduct)
                    .HasName("IX_FK_ProductsDetailProduct");

                entity.HasIndex(e => e.IdTypeDetail)
                    .HasName("IX_FK_CatTypeDetailsDetailProduct");

                entity.Property(e => e.DateUpdate).IsRequired();

                entity.Property(e => e.Description).IsRequired();

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.DetailProduct)
                    .HasForeignKey(d => d.IdProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductsDetailProduct");

                entity.HasOne(d => d.IdTypeDetailNavigation)
                    .WithMany(p => p.DetailProduct)
                    .HasForeignKey(d => d.IdTypeDetail)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CatTypeDetailsDetailProduct");
            });

            modelBuilder.Entity<HistorialT>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.VideoId)
                    .HasColumnName("videoId")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.VideoName)
                    .HasColumnName("videoName")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ImagesProduct>(entity =>
            {
                entity.HasKey(e => e.IdImage);

                entity.HasIndex(e => e.IdImageProduct)
                    .HasName("IX_FK_ProductsImagesProduct");

                entity.Property(e => e.DateUpdate).IsRequired();

                entity.Property(e => e.Decription).IsRequired();

                entity.Property(e => e.Image).IsRequired();

                entity.Property(e => e.IsEnabled).IsRequired();

                entity.HasOne(d => d.IdImageProductNavigation)
                    .WithMany(p => p.ImagesProduct)
                    .HasForeignKey(d => d.IdImageProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductsImagesProduct");
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.Property(e => e.CurrencyCode)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.DateUpdate).HasColumnType("datetime");

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.Keywords).IsRequired();

                entity.Property(e => e.MemberDiscount).HasColumnType("decimal(3, 2)");

                entity.Property(e => e.Nombre).IsRequired();

                entity.Property(e => e.Picture).IsRequired();

                entity.Property(e => e.PriceClient).HasColumnType("decimal(38, 2)");

                entity.Property(e => e.Title).IsRequired();
            });

            modelBuilder.Entity<Qualification>(entity =>
            {
                entity.HasIndex(e => e.IdProduct)
                    .HasName("IX_FK_QualificationProducts");

                entity.HasIndex(e => e.IdRating)
                    .HasName("IX_FK_CatRatingsQualification");

                entity.Property(e => e.DateUpdate).HasColumnType("datetime");

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.Qualification)
                    .HasForeignKey(d => d.IdProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QualificationProducts");

                entity.HasOne(d => d.IdRatingNavigation)
                    .WithMany(p => p.Qualification)
                    .HasForeignKey(d => d.IdRating)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CatRatingsQualification");
            });

            modelBuilder.Entity<SimilarProduct>(entity =>
            {
                entity.HasKey(e => e.IdSimilarProduct);

                entity.HasIndex(e => e.IdProduct)
                    .HasName("IX_FK_ProductsSimilarProduct");

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.SimilarProduct)
                    .HasForeignKey(d => d.IdProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductsSimilarProduct");
            });

            modelBuilder.Entity<SizeForProduct>(entity =>
            {
                entity.HasKey(e => e.IdSizeProduct);

                entity.HasIndex(e => e.IdProduct)
                    .HasName("IX_FK_ProductsSizeForProduct");

                entity.HasIndex(e => e.IdSize)
                    .HasName("IX_FK_CatSizesSizeForProduct");

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.SizeForProduct)
                    .HasForeignKey(d => d.IdProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductsSizeForProduct");

                entity.HasOne(d => d.IdSizeNavigation)
                    .WithMany(p => p.SizeForProduct)
                    .HasForeignKey(d => d.IdSize)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CatSizesSizeForProduct");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(16);

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Usuario)
                    .IsRequired()
                    .HasMaxLength(15);
            });
        }
    }
}
