using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FeedReader.Model
{
    public partial class NewsCrud_DBContext : DbContext
    {

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ArticleData>(entity =>
            {
                entity.ToTable("articleData");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Body).HasColumnName("body");

                entity.Property(e => e.FirstScrapeDate).HasColumnName("firstScrapeDate");

                entity.Property(e => e.IdHash)
                    .HasColumnName("idHash")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.LastScrapeDate).HasColumnName("lastScrapeDate");

                entity.Property(e => e.PublicationId).HasColumnName("publicationID");

                entity.Property(e => e.PublicationSectionId).HasColumnName("publicationSectionID");

                entity.Property(e => e.PublishDate).HasColumnName("publishDate");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(2000);

                entity.Property(e => e.Url)
                    .HasColumnName("URL")
                    .HasColumnType("varchar(2000)");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.InverseIdNavigation)
                    .HasForeignKey<ArticleData>(d => d.Id)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_articleData_articleData");

                entity.HasOne(d => d.Publication)
                    .WithMany(p => p.ArticleData)
                    .HasForeignKey(d => d.PublicationId)
                    .HasConstraintName("FK_articleData_publication");
            });

            modelBuilder.Entity<Author>(entity =>
            {
                entity.ToTable("author");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Country)
                    .HasColumnName("country")
                    .HasColumnType("varchar(500)");

                entity.Property(e => e.DateOfBirth)
                    .HasColumnName("dateOfBirth")
                    .HasColumnType("date");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<AuthorArticle>(entity =>
            {
                entity.HasKey(e => new { e.AuthorId, e.ArticleId })
                    .HasName("PK_author_article");

                entity.ToTable("author_article");

                entity.Property(e => e.AuthorId).HasColumnName("authorID");

                entity.Property(e => e.ArticleId).HasColumnName("articleID");

                entity.HasOne(d => d.Article)
                    .WithMany(p => p.AuthorArticle)
                    .HasForeignKey(d => d.ArticleId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_author_article_articleData");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.AuthorArticle)
                    .HasForeignKey(d => d.AuthorId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_author_article_author");
            });

            modelBuilder.Entity<Publication>(entity =>
            {
                entity.ToTable("publication");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Code)
                    .HasColumnName("code")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.DisplayEnabled).HasColumnName("displayEnabled");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(250);

                entity.Property(e => e.ScrapeEnabled).HasColumnName("scrapeEnabled");

                entity.Property(e => e.Url)
                    .HasColumnName("URL")
                    .HasColumnType("varchar(2000)");
            });

            modelBuilder.Entity<PublicationSection>(entity =>
            {
                entity.ToTable("publicationSection");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Enabled).HasColumnName("enabled");

                entity.Property(e => e.LastScraped).HasColumnName("lastScraped");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(250);

                entity.Property(e => e.PublicationId).HasColumnName("publicationID");

                entity.Property(e => e.Sequence).HasColumnName("sequence");

                entity.Property(e => e.Url)
                    .HasColumnName("URL")
                    .HasColumnType("varchar(2000)");

                entity.HasOne(d => d.Publication)
                    .WithMany(p => p.PublicationSection)
                    .HasForeignKey(d => d.PublicationId)
                    .HasConstraintName("FK_publicationSection_publication");
            });

            modelBuilder.Entity<ScrapeQueue>(entity =>
            {
                entity.ToTable("scrapeQueue");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CallbackUrl)
                    .HasColumnName("callbackURL")
                    .HasColumnType("varchar(2000)");

                entity.Property(e => e.DateActioned).HasColumnName("dateActioned");

                entity.Property(e => e.DateAdded).HasColumnName("dateAdded");

                entity.Property(e => e.IdHash)
                    .HasColumnName("idHash")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.PublicationCode)
                    .HasColumnName("publicationCode")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.PublicationId).HasColumnName("publicationID");

                entity.Property(e => e.PublicationSectionId).HasColumnName("publicationSectionID");

                entity.Property(e => e.ScrapeErrors)
                    .HasColumnName("scrapeErrors")
                    .HasColumnType("varchar(500)");

                entity.Property(e => e.ScrapeOutcome)
                    .HasColumnName("scrapeOutcome")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Url)
                    .HasColumnName("url")
                    .HasColumnType("varchar(2000)");
            });
        }

        public virtual DbSet<ArticleData> ArticleData { get; set; }
        public virtual DbSet<Author> Author { get; set; }
        public virtual DbSet<AuthorArticle> AuthorArticle { get; set; }
        public virtual DbSet<Publication> Publication { get; set; }
        public virtual DbSet<PublicationSection> PublicationSection { get; set; }
        public virtual DbSet<ScrapeQueue> ScrapeQueue { get; set; }
    }
}