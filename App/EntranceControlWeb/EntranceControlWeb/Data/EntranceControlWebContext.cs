﻿using System;
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
        public virtual DbSet<ActivityStatus> ActivityStatuses { get; set; }
        public virtual DbSet<Authorize> Authorizes { get; set; }
        public virtual DbSet<Door> Doors { get; set; }
        public virtual DbSet<Entrance> Entrances { get; set; }
        public virtual DbSet<LastingStatus> LastingStatuses { get; set; }
        public virtual DbSet<Office> Offices { get; set; }
        public virtual DbSet<Pass> Passes { get; set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<SortingByOffice> SortingByOffices { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Visitor> Visitors { get; set; }
        public virtual DbSet<staff> staff { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-G6IA9JI;Database=EntranceControlWeb;Trusted_Connection=True;");
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

            modelBuilder.Entity<ActivityStatus>(entity =>
            {
                entity.HasKey(e => e.IdActiv);

                entity.ToTable("ActivityStatus");

                entity.Property(e => e.IdActiv).HasColumnName("ID_Activ");

                entity.Property(e => e.TitleActiv)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Authorize>(entity =>
            {
                entity.HasKey(e => e.IdItem);

                entity.ToTable("Authorize");

                entity.Property(e => e.IdItem).HasColumnName("ID_Item");

                entity.Property(e => e.DateAuth).HasColumnType("datetime");

                entity.Property(e => e.IdUser).HasColumnName("ID_User");

                entity.HasOne(d => d.IdUsers)
                    .WithMany(p => p.Authorizes)
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

                entity.HasOne(d => d.IdRooms)
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

                entity.Property(e => e.IdPass).HasColumnName("ID_Pass");

                entity.Property(e => e.IdRoom).HasColumnName("ID_Room");

                entity.Property(e => e.IdStatus).HasColumnName("ID_Status");

                entity.HasOne(d => d.IdDoors)
                    .WithMany(p => p.Entrances)
                    .HasForeignKey(d => d.IdDoor)
                    .HasConstraintName("FK__Entrance__ID_Doo__267ABA7A");

                entity.HasOne(d => d.IdPasses)
                    .WithMany(p => p.Entrances)
                    .HasForeignKey(d => d.IdPass)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Entrance_Passes");

                entity.HasOne(d => d.IdRooms)
                    .WithMany(p => p.Entrances)
                    .HasForeignKey(d => d.IdRoom)
                    .HasConstraintName("FK__Entrance__ID_Roo__25869641");

                entity.HasOne(d => d.IdStatuses)
                    .WithMany(p => p.Entrances)
                    .HasForeignKey(d => d.IdStatus)
                    .HasConstraintName("FK__Entrance__ID_Sta__276EDEB3");
            });

            modelBuilder.Entity<LastingStatus>(entity =>
            {
                entity.HasKey(e => e.IdLong);

                entity.ToTable("LastingStatus");

                entity.Property(e => e.IdLong).HasColumnName("ID_Long");

                entity.Property(e => e.TitleLong)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Office>(entity =>
            {
                entity.HasKey(e => e.IdOffice)
                    .HasName("PK__Offices__F7DAFD3A29D9B49C");

                entity.Property(e => e.IdOffice).HasColumnName("ID_Office");

                entity.Property(e => e.TitleOffice)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Pass>(entity =>
            {
                entity.HasKey(e => e.IdPass);

                entity.Property(e => e.IdPass).HasColumnName("ID_Pass");

                entity.Property(e => e.IdActiv).HasColumnName("ID_Activ");

                entity.Property(e => e.IdLong).HasColumnName("ID_Long");

                entity.HasOne(d => d.IdActivs)
                    .WithMany(p => p.Passes)
                    .HasForeignKey(d => d.IdActiv)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Passes_ActivityStatus");

                entity.HasOne(d => d.IdLongs)
                    .WithMany(p => p.Passes)
                    .HasForeignKey(d => d.IdLong)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Passes_LastingStatus");
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

                entity.Property(e => e.IdRoom)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID_Room");

                entity.Property(e => e.IdLevel).HasColumnName("ID_Level");

                entity.Property(e => e.TitleRoom)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdLevels)
                    .WithOne(p => p.Rooms)
                    .HasForeignKey<Room>(d => d.IdRoom)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Rooms_AccessLevel");
            });

            modelBuilder.Entity<SortingByOffice>(entity =>
            {
                entity.HasKey(e => e.IdItem);

                entity.Property(e => e.IdItem).HasColumnName("ID_Item");

                entity.Property(e => e.IdOffice).HasColumnName("ID_Office");

                entity.Property(e => e.IdPost).HasColumnName("ID_Post");

                entity.Property(e => e.IdStaff).HasColumnName("ID_Staff");

                entity.Property(e => e.WorkPhone)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdOffices)
                    .WithMany(p => p.SortingByOffices)
                    .HasForeignKey(d => d.IdOffice)
                    .HasConstraintName("FK__SortingBy__ID_Of__2B3F6F97");

                entity.HasOne(d => d.IdPosts)
                    .WithMany(p => p.SortingByOffices)
                    .HasForeignKey(d => d.IdPost)
                    .HasConstraintName("FK__SortingBy__ID_Po__2A4B4B5E");

                entity.HasOne(d => d.IdStaffs)
                    .WithMany(p => p.SortingByOffices)
                    .HasForeignKey(d => d.IdStaff)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SortingBy__ID_St__29572725");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser)
                    .HasName("PK__Users__ED4DE44294F49FE9");

                entity.Property(e => e.IdUser)
                    .ValueGeneratedNever()
                    .HasColumnName("ID_User");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Visitor>(entity =>
            {
                entity.HasKey(e => e.Idvisitor);

                entity.Property(e => e.Idvisitor).HasColumnName("IDVisitor");

                entity.Property(e => e.Fio)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("FIO");

                entity.Property(e => e.IdLevel).HasColumnName("ID_Level");

                entity.Property(e => e.IdPass).HasColumnName("ID_Pass");

                entity.Property(e => e.MobilePhone)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdLevels)
                    .WithMany(p => p.Visitors)
                    .HasForeignKey(d => d.IdLevel)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Visitors_AccessLevel");

                entity.HasOne(d => d.IdPasses)
                    .WithMany(p => p.Visitors)
                    .HasForeignKey(d => d.IdPass)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Visitors_Passes");
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

                entity.Property(e => e.IdPass).HasColumnName("ID_Pass");

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

                entity.HasOne(d => d.IdLevels)
                    .WithMany(p => p.staffs)
                    .HasForeignKey(d => d.IdLevel)
                    .HasConstraintName("FK__Staff__ID_Level__1BFD2C07");

                entity.HasOne(d => d.IdPasses)
                    .WithMany(p => p.staffs)
                    .HasForeignKey(d => d.IdPass)
                    .HasConstraintName("FK_Staff_Passes");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
