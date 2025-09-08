using Microsoft.EntityFrameworkCore;
using PersonalBlog.Core.Models;

namespace PersonalBlog.Infrastructure;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Article> Articles { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<ArticleTag>  ArticleTags { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRoles> UserRoles { get; set; }
    
    /// <summary>
    /// Конфигурация модели базы данных для EF Core.
    /// Здесь задаются правила отображения сущностей и их свойств на таблицы и колонки в базе.
    /// </summary>
    /// <remarks>
    /// 1. User.Email — Value Object, хранится в колонке "Email".
    /// 2. ArticleTag — join-entity для связи многие-ко-многим между Article и Tag, составной первичный ключ (ArticleId + TagId).
    /// 3. UserRoles — join-entity для связи многие-ко-многим между User и Role, составной первичный ключ (UserId + RoleId).
    /// </remarks>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(us =>
        {
            us.OwnsOne(u => u.Email, em =>
            {
                em.Property(e => e.Value).HasColumnName("Email");
            });
            
            us.HasMany(u => u.UserRoles)
                .WithOne(ur => ur.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
        });
        
        modelBuilder.Entity<ArticleTag>(ar =>
        {
            ar.HasKey(at => new { at.ArticleId, at.TagId });

            ar.HasOne(at => at.Article)
                .WithMany(a => a.ArticleTags)
                .HasForeignKey(at => at.ArticleId)
                .IsRequired();

            ar.HasOne(at => at.Tag)
                .WithMany(t => t.ArticleTags)
                .HasForeignKey(at => at.TagId)
                .IsRequired();
        });

        modelBuilder.Entity<UserRoles>(us =>
        {
            us.HasKey(ur => new { ur.UserId, ur.RoleId });

            us.HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();

            us.HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();
        });
    }
}