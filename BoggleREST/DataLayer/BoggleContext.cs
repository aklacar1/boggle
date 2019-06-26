using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace BoggleREST
{
    public partial class BoggleContext : IdentityDbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BoggleContext(DbContextOptions<BoggleContext> options, IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public virtual DbSet<FirebaseTokens> FirebaseTokens { get; set; }
        public virtual DbSet<GameLetters> GameLetters { get; set; }
        public virtual DbSet<GameParticipants> GameParticipants { get; set; }
        public virtual DbSet<GameRoom> GameRoom { get; set; }
        public virtual DbSet<GameWords> GameWords { get; set; }
        public new virtual DbSet<Users> Users { get; set; }

        public string GetCurrentUserId() {
            return _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<FirebaseTokens>(entity =>
            {
                entity.Property(e => e.Token).IsRequired();

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.FirebaseTokens)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FirebaseTokens_AspNetUsers");
            });

            modelBuilder.Entity<GameLetters>(entity =>
            {
                entity.Property(e => e.Letter)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.HasOne(d => d.GameRoom)
                    .WithMany(p => p.GameLetters)
                    .HasForeignKey(d => d.GameRoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GameLetters_GameRoom");
            });

            modelBuilder.Entity<GameParticipants>(entity =>
            {
                entity.HasIndex(p => new { p.UserId, p.GameRoomId }).IsUnique();

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasMaxLength(450);

                entity.Property(e => e.GameRoomId)
                    .HasColumnName("GameRoomId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.GameParticipants)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_GameParticipants_AspNetUsers");

                entity.HasOne(d => d.GameRoom)
                      .WithMany(p => p.GameParticipants)
                      .HasForeignKey(d => d.GameRoomId)
                      .HasConstraintName("FK_GameParticipants_GameRoom");
            });

            modelBuilder.Entity<GameWords>(entity =>
            {
                //entity.HasIndex(p => new { p.Word, p.GameRoomId, p.UserId }).IsUnique();

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasMaxLength(450);
                entity.Property(e => e.Word)
                    .HasColumnName("Word")
                    .HasMaxLength(17);
                entity.Property(e => e.GameRoomId)
                    .HasColumnName("GameRoomId");
                entity.HasOne(d => d.User)
                    .WithMany(p => p.GameWords)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_GameWords_AspNetUsers");

                entity.HasOne(d => d.GameRoom)
                      .WithMany(p => p.GameWords)
                      .HasForeignKey(d => d.GameRoomId)
                      .HasConstraintName("FK_Gameords_GameRoom");
            });

            modelBuilder.Entity<GameRoom>(entity =>
            {
                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.StartTime).HasColumnType("datetime");
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
