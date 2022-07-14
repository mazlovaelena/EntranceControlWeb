using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace EntranceControlWeb.Models
{
    public partial class EntranceControlWebContext : DbContext
    {
        public EntranceControlWebContext()
        {
        }

        public EntranceControlWebContext(DbContextOptions<EntranceControlWebContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AccessLevel> AccessLevels { get; set; }
        public virtual DbSet<AccessStatus> AccessStatuses { get; set; }
        public virtual DbSet<Authorize> Authorizes { get; set; }
        public virtual DbSet<Door> Doors { get; set; }
        public virtual DbSet<Entrance> Entrances { get; set; }
        public virtual DbSet<OfficeViewModel> Offices { get; set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<SortingByOffice> SortingByOffices { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<staff> staff { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer("Data Source=DESKTOP-G6IA9JI;initial catalog=EntranceControlWeb; trusted_connection=yes;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<AccessLevel>(entity =>
            {
                entity.HasKey(e => e.IdLevel)
                    .HasName("PK__AccessLe__5642CD8C1A5BAEA8");

                entity.ToTable("AccessLevel");

                entity.Property(e => e.IdLevel).HasColumnName("ID_Level");

                entity.Property(e => e.TitleLevel)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AccessStatus>(entity =>
            {
                entity.HasKey(e => e.IdStatus)
                    .HasName("PK__AccessSt__5AC2A7349F924A9E");

                entity.ToTable("AccessStatus");

                entity.Property(e => e.IdStatus).HasColumnName("ID_Status");

                entity.Property(e => e.TitleStatus)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Authorize>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Authorize");

                entity.Property(e => e.DateAuth).HasColumnType("date");

                entity.Property(e => e.IdUser).HasColumnName("ID_User");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Authorize__ID_Us__15502E78");
            });

            modelBuilder.Entity<Door>(entity =>
            {
                entity.HasKey(e => e.IdDoor)
                    .HasName("PK__Doors__4DC8A17DC09CB36A");

                entity.Property(e => e.IdDoor).HasColumnName("ID_Door");

                entity.Property(e => e.IdRoom).HasColumnName("ID_Room");

                entity.Property(e => e.TitleDoor)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdRoomNavigation)
                    .WithMany(p => p.Doors)
                    .HasForeignKey(d => d.IdRoom)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Doors__ID_Room__21B6055D");
            });

            modelBuilder.Entity<Entrance>(entity =>
            {
                entity.HasKey(e => e.IdRecord)
                    .HasName("PK__Entrance__1070D2CED5AE9388");

                entity.ToTable("Entrance");

                entity.Property(e => e.IdRecord).HasColumnName("ID_Record");

                entity.Property(e => e.DateEntr).HasColumnType("datetime");

                entity.Property(e => e.DateExit).HasColumnType("datetime");

                entity.Property(e => e.IdDoor).HasColumnName("ID_Door");

                entity.Property(e => e.IdRoom).HasColumnName("ID_Room");

                entity.Property(e => e.IdStaff).HasColumnName("ID_Staff");

                entity.Property(e => e.IdStatus).HasColumnName("ID_Status");

                entity.HasOne(d => d.IdDoorNavigation)
                    .WithMany(p => p.Entrances)
                    .HasForeignKey(d => d.IdDoor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Entrance__ID_Doo__267ABA7A");

                entity.HasOne(d => d.IdRoomNavigation)
                    .WithMany(p => p.Entrances)
                    .HasForeignKey(d => d.IdRoom)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Entrance__ID_Roo__25869641");

                entity.HasOne(d => d.IdStaffNavigation)
                    .WithMany(p => p.Entrances)
                    .HasForeignKey(d => d.IdStaff)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Entrance__ID_Sta__24927208");

                entity.HasOne(d => d.IdStatusNavigation)
                    .WithMany(p => p.Entrances)
                    .HasForeignKey(d => d.IdStatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Entrance__ID_Sta__276EDEB3");
            });

            modelBuilder.Entity<OfficeViewModel>(entity =>
            {
                entity.HasKey(e => e.IdOffice)
                    .HasName("PK__Offices__F7DAFD3A29D9B49C");

                entity.Property(e => e.IdOffice).HasColumnName("ID_Office");

                entity.Property(e => e.TitleOffice)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Position>(entity =>
            {
                entity.HasKey(e => e.IdPost)
                    .HasName("PK__Position__B41D0E30A1B2EB58");

                entity.Property(e => e.IdPost).HasColumnName("ID_Post");

                entity.Property(e => e.TitlePost)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasKey(e => e.IdRoom)
                    .HasName("PK__Rooms__43DC0E41839B1C10");

                entity.Property(e => e.IdRoom).HasColumnName("ID_Room");

                entity.Property(e => e.IdLevel).HasColumnName("ID_Level");

                entity.Property(e => e.TitleRoom)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdLevelNavigation)
                    .WithMany(p => p.Rooms)
                    .HasForeignKey(d => d.IdLevel)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Rooms__ID_Level__1ED998B2");
            });

            modelBuilder.Entity<SortingByOffice>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.IdOffice).HasColumnName("ID_Office");

                entity.Property(e => e.IdPost).HasColumnName("ID_Post");

                entity.Property(e => e.IdStaff).HasColumnName("ID_Staff");

                entity.Property(e => e.WorkPhone)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdOfficeNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdOffice)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SortingBy__ID_Of__2B3F6F97");

                entity.HasOne(d => d.IdPostNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdPost)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SortingBy__ID_Po__2A4B4B5E");

                entity.HasOne(d => d.IdStaffNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdStaff)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SortingBy__ID_St__29572725");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser)
                    .HasName("PK__Users__ED4DE44294F49FE9");

                entity.Property(e => e.IdUser).HasColumnName("ID_User");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<staff>(entity =>
            {
                entity.HasKey(e => e.IdStaff)
                    .HasName("PK__Staff__922B8FE683B02010");

                entity.ToTable("Staff");

                entity.Property(e => e.IdStaff).HasColumnName("ID_Staff");

                entity.Property(e => e.Birthday).HasColumnType("date");

                entity.Property(e => e.CorpEmail)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.IdLevel).HasColumnName("ID_Level");

                entity.Property(e => e.Image)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.MobPhone)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdLevelNavigation)
                    .WithMany(p => p.staff)
                    .HasForeignKey(d => d.IdLevel)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Staff__ID_Level__1BFD2C07");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
