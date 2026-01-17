using Microsoft.EntityFrameworkCore;
using practice.Models;

namespace practice.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Election> Elections { get; set; }
        public DbSet<Vote> Votes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // -------------------- CONFIGURATIONS --------------------

            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
            modelBuilder.Entity<User>().HasIndex(u => u.VoterIdNumber).IsUnique();

            modelBuilder.Entity<Candidate>()
                .HasOne(c => c.User)
                .WithOne(u => u.CandidateProfile)
                .HasForeignKey<Candidate>(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            

            modelBuilder.Entity<Vote>()
                .HasOne(v => v.Election)
                .WithMany(e => e.Votes)
                .HasForeignKey(v => v.ElectionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Vote>()
                .HasOne(v => v.Voter)
                .WithMany(u => u.Votes)
                .HasForeignKey(v => v.VoterId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Vote>()
                .HasOne(v => v.Candidate)
                .WithMany(c => c.Votes)
                .HasForeignKey(v => v.CandidateId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Vote>()
                .HasIndex(v => new { v.ElectionId, v.VoterId })
                .IsUnique();

            // -------------------- CONSTANT VALUES --------------------

            var now = new DateTime(2026, 1, 1);

            // Pre-generated BCrypt hash for "Pass@123"
            const string PasswordHash =
                "$2a$11$seX7Ayuffe0Hdpmt0v2gt.CWydeU.5JDXsKyHadXWPxNFvR8JFzmu";

            // -------------------- ADMIN --------------------

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    FullName = "System Administrator",
                    Email = "admin@votingsystem.com",
                    PhoneNumber = "923001234567",
                    PasswordHash = PasswordHash,
                    Role = "Admin",
                    IsVerified = true,
                    IsActive = true,
                    CreatedAt = now
                }
            );

            // -------------------- ELECTIONS --------------------

            modelBuilder.Entity<Election>().HasData(
                new Election
                {
                    Id = 1,
                    Title = "National Assembly 2025",
                    Description = "Completed General Election",
                    StartDate = now.AddMonths(-2),
                    EndDate = now.AddMonths(-1),
                    ElectionType = "General",
                    IsActive = false,
                    CreatedAt = now
                },
                new Election
                {
                    Id = 2,
                    Title = "Local Council 2026",
                    Description = "Ongoing Local Election",
                    StartDate = now.AddDays(-5),
                    EndDate = now.AddDays(20),
                    ElectionType = "Local",
                    IsActive = true,
                    CreatedAt = now
                }
            );

            // -------------------- CANDIDATES --------------------

            var candidateUsers = new List<User>();
            var candidates = new List<Candidate>();

            for (int i = 1; i <= 10; i++)
            {
                int userId = 10 + i;

                candidateUsers.Add(new User
                {
                    Id = userId,
                    FullName = $"Candidate {i}",
                    Email = $"candidate{i}@party.com",
                    PhoneNumber = $"9230000000{i:D2}",
                    PasswordHash = PasswordHash,
                    Role = "Candidate",
                    IsVerified = true,
                    IsActive = true,
                    cnic = $"42101000000{i:D2}",
                    CreatedAt = now
                });

                candidates.Add(new Candidate
                {
                    Id = i,
                    UserId = userId,
                    PartyName = i % 2 == 0 ? "Democratic Party" : "Republic Alliance",
                    PartySymbol = i % 2 == 0 ? "Eagle" : "Lion",
                    Manifesto = $"Manifesto of candidate {i}",
                    Biography = $"Political experience: {i * 2} years",
                    ElectionId = 1,
                    IsApproved = true,
                    RegisteredAt = now
                });
            }

            modelBuilder.Entity<User>().HasData(candidateUsers);
            modelBuilder.Entity<Candidate>().HasData(candidates);

            // -------------------- VOTERS --------------------

            var voters = new List<User>();

            for (int i = 1; i <= 200; i++)
            {
                voters.Add(new User
                {
                    Id = 100 + i,
                    FullName = $"Voter {i}",
                    Email = $"voter{i}@mail.com",
                    PhoneNumber = $"923110000{i:D3}",
                    PasswordHash = PasswordHash,
                    Role = "Voter",
                    IsVerified = true,
                    IsActive = true,
                    VoterIdNumber = $"VOTER-{1000 + i}",
                    cnic = $"422010000{i:D4}",
                    CreatedAt = now
                });
            }

            modelBuilder.Entity<User>().HasData(voters);

            // -------------------- VOTES (RESULTS) --------------------

            var votes = new List<Vote>();
            int voteId = 1;

            // First 50 voters have voted in completed election
            for (int i = 1; i <= 50; i++)
            {
                votes.Add(new Vote
                {
                    Id = voteId++,
                    ElectionId = 1,
                    VoterId = 100 + i,
                    CandidateId = (i % 5) + 1,
                    VotedAt = now.AddMonths(-1)
                });
            }

            modelBuilder.Entity<Vote>().HasData(votes);
        }
    }
}