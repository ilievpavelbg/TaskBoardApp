using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskBoardApp.Data.Entities;
using Task = TaskBoardApp.Data.Entities.Task;

namespace TaskBoardApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        private User GuestUser { get; set; }
        private Board OpenBoard { get; set; }
        private Board InProgressBoard { get; set; }
        private Board DoneBoard { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        private void SeedUsers()
        {
            var hasher = new PasswordHasher<IdentityUser>();

            this.GuestUser = new User()
            {
                UserName = "guest",
                NormalizedUserName = "GUEST",
                Email = "guest@mail.com",
                NormalizedEmail = "GUEST@MAIL.COM",
                FirstName = "Guest",
                LastName = "User"
            };

            this.GuestUser.PasswordHash = hasher.HashPassword(this.GuestUser, "guest");
        }

        private void SeedBoards()
        {
            this.OpenBoard = new Board()
            {
                Id = 1,
                Name = "Open"

            };

            this.InProgressBoard = new Board()
            {
                Id = 2,
                Name = "In Progress"

            };

            this.DoneBoard = new Board()
            {
                Id = 3,
                Name = "Done"

            };

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Task>()
                .HasOne(t => t.Board)
                .WithMany(t => t.Tasks)
                .HasForeignKey(t => t.BoardId)
                .OnDelete(DeleteBehavior.Restrict);

            SeedUsers();

            builder
                .Entity<User>()
                .HasData(this.GuestUser);

            SeedBoards();

            builder
                .Entity<Board>()
                .HasData(this.OpenBoard, this.InProgressBoard, this.DoneBoard);

            builder
                .Entity<Task>()
                .HasData(new Task()
                {
                    Id = 1,
                    Title = "Prepare for the ASP.NET Fundamentals Exam",
                    Description = "Learning to use ASP.NET Identity",
                    CreatedOn = DateTime.Now.AddMonths(-1),
                    OwnerId = this.GuestUser.Id,
                    BoardId = this.OpenBoard.Id
                },
                new Task()
                {
                    Id = 2,
                    Title = "Improve EF core skills",
                    Description = "Learning using EF Core and MS SQL Managment Studio",
                    CreatedOn = DateTime.Now.AddMonths(-5),
                    OwnerId = this.GuestUser.Id,
                    BoardId = this.DoneBoard.Id

                },
                new Task()
                {
                    Id = 3,
                    Title = "Improve ASP NET core skills",
                    Description = "Learning to use ASP.NET CORE Identity",
                    CreatedOn = DateTime.Now.AddDays(-10),
                    OwnerId = this.GuestUser.Id,
                    BoardId = this.InProgressBoard.Id

                },
                new Task()
                {
                    Id = 4,
                    Title = "Prepare for C# Fundamentals Exam",
                    Description = "Prepare by solving Old exams",
                    CreatedOn = DateTime.Now.AddYears(-1),
                    OwnerId = this.GuestUser.Id,
                    BoardId = this.DoneBoard.Id

                });


            base.OnModelCreating(builder);
        }

        public DbSet<Task> Tasks { get; set; }
        public DbSet<Board> Boards { get; set; }
    }
}