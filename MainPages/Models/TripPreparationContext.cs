using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MainPages.Models;

public partial class TripPreparationContext : DbContext
{
    public TripPreparationContext()
    {
    }

    public TripPreparationContext(DbContextOptions<TripPreparationContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BoatType> BoatTypes { get; set; }

    public virtual DbSet<BoatsList> BoatsLists { get; set; }

    public virtual DbSet<Cost> Costs { get; set; }

    public virtual DbSet<DishesForFood> DishesForFoods { get; set; }

    public virtual DbSet<Duty> Duties { get; set; }

    public virtual DbSet<DutyType> DutyTypes { get; set; }

    public virtual DbSet<EquipmentDistribution> EquipmentDistributions { get; set; }

    public virtual DbSet<EquipmentList> EquipmentLists { get; set; }

    public virtual DbSet<FirstAidKit> FirstAidKits { get; set; }

    public virtual DbSet<Food> Foods { get; set; }

    public virtual DbSet<FoodIntakeType> FoodIntakeTypes { get; set; }

    public virtual DbSet<Map> Maps { get; set; }

    public virtual DbSet<ParticipantsList> ParticipantsLists { get; set; }

    public virtual DbSet<Plan> Plans { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductsForFood> ProductsForFoods { get; set; }

    public virtual DbSet<ProductsList> ProductsLists { get; set; }

    public virtual DbSet<RepairKit> RepairKits { get; set; }

    public virtual DbSet<Route> Routes { get; set; }

    public virtual DbSet<Tent> Tents { get; set; }

    public virtual DbSet<TripType> TripTypes { get; set; }

    public virtual DbSet<TripsList> TripsLists { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<WaypointList> WaypointLists { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseLazyLoadingProxies().UseNpgsql("Host=localhost;Port=5432;Database=trip_preparation;Username=postgres;Password=erT5%q1");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BoatType>(entity =>
        {
            entity.HasKey(e => e.BoatTypeNumber).HasName("Boat_types_pk");

            entity.ToTable("Boat_types");

            entity.Property(e => e.BoatTypeNumber)
                .UseIdentityAlwaysColumn()
                .HasColumnName("Boat_type_number");
            entity.Property(e => e.Name).HasColumnType("character varying");
            entity.Property(e => e.PlacesAmount).HasColumnName("Places_amount");
        });

        modelBuilder.Entity<BoatsList>(entity =>
        {
            entity.HasKey(e => e.BoatNumber).HasName("Boats_list_pk");

            entity.ToTable("Boats_list");

            entity.Property(e => e.BoatNumber).HasColumnName("Boat_number");
            entity.Property(e => e.BoatTypeNumber).HasColumnName("Boat_type_number");
            entity.Property(e => e.TripNumber).HasColumnName("Trip_number");

            entity.HasOne(d => d.BoatTypeNumberNavigation).WithMany(p => p.BoatsLists)
                .HasForeignKey(d => d.BoatTypeNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Boats_list_fk1");

            entity.HasOne(d => d.TripNumberNavigation).WithMany(p => p.BoatsLists)
                .HasForeignKey(d => d.TripNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Boats_list_fk0");
        });

        modelBuilder.Entity<Cost>(entity =>
        {
            entity.HasKey(e => new { e.CostName, e.TripNumber }).HasName("Costs_pk");

            entity.Property(e => e.CostName)
                .HasColumnType("character varying")
                .HasColumnName("Cost_name");
            entity.Property(e => e.TripNumber).HasColumnName("Trip_number");
            entity.Property(e => e.Note).HasColumnType("character varying");
            entity.Property(e => e.WhoSpent)
                .HasColumnType("character varying")
                .HasColumnName("Who_spent");

            entity.HasOne(d => d.TripNumberNavigation).WithMany(p => p.Costs)
                .HasForeignKey(d => d.TripNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Costs_fk0");
        });

        modelBuilder.Entity<DishesForFood>(entity =>
        {
            entity.HasKey(e => new { e.DishName, e.FoodNumber }).HasName("Dishes_for_food_pk");

            entity.ToTable("Dishes_for_food");

            entity.Property(e => e.DishName)
                .HasColumnType("character varying")
                .HasColumnName("Dish_name");
            entity.Property(e => e.FoodNumber).HasColumnName("Food_number");

            entity.HasOne(d => d.FoodNumberNavigation).WithMany(p => p.DishesForFoods)
                .HasForeignKey(d => d.FoodNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Dishes_for_food_fk0");
        });

        modelBuilder.Entity<Duty>(entity =>
        {
            entity.HasKey(e => e.DutyNumber).HasName("Duty_pk");

            entity.ToTable("Duty");

            entity.Property(e => e.DutyNumber).HasColumnName("Duty_number");
            entity.Property(e => e.DutyTypeNumber).HasColumnName("Duty_type_number");
            entity.Property(e => e.Note).HasColumnType("character varying");
            entity.Property(e => e.TripNumber).HasColumnName("Trip_number");

            entity.HasOne(d => d.DutyTypeNumberNavigation).WithMany(p => p.Duties)
                .HasForeignKey(d => d.DutyTypeNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Duty_fk1");

            entity.HasOne(d => d.TripNumberNavigation).WithMany(p => p.Duties)
                .HasForeignKey(d => d.TripNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Duty_fk0");

            entity.HasMany(d => d.ParticipantNumbers).WithMany(p => p.DutyNumbers)
                .UsingEntity<Dictionary<string, object>>(
                    "DutyPerson",
                    r => r.HasOne<ParticipantsList>().WithMany()
                        .HasForeignKey("ParticipantNumber")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Duty_people_fk1"),
                    l => l.HasOne<Duty>().WithMany()
                        .HasForeignKey("DutyNumber")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Duty_people_fk0"),
                    j =>
                    {
                        j.HasKey("DutyNumber", "ParticipantNumber").HasName("Duty_people_pk");
                        j.ToTable("Duty_people");
                        j.IndexerProperty<int>("DutyNumber").HasColumnName("Duty_number");
                        j.IndexerProperty<int>("ParticipantNumber").HasColumnName("Participant_number");
                    });
        });

        modelBuilder.Entity<DutyType>(entity =>
        {
            entity.HasKey(e => e.DutyTypeNumber).HasName("Duty_types_pk");

            entity.ToTable("Duty_types");

            entity.Property(e => e.DutyTypeNumber).HasColumnName("Duty_type_number");
            entity.Property(e => e.Type).HasColumnType("character varying");
        });

        modelBuilder.Entity<EquipmentDistribution>(entity =>
        {
            entity.HasKey(e => new { e.EquipmentNumber, e.ParticipantNumber }).HasName("Equipment_distribution_pk");

            entity.ToTable("Equipment_distribution");

            entity.Property(e => e.EquipmentNumber).HasColumnName("Equipment_number");
            entity.Property(e => e.ParticipantNumber).HasColumnName("Participant_number");

            entity.HasOne(d => d.EquipmentNumberNavigation).WithMany(p => p.EquipmentDistributions)
                .HasForeignKey(d => d.EquipmentNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Equipment_distribution_fk0");

            entity.HasOne(d => d.ParticipantNumberNavigation).WithMany(p => p.EquipmentDistributions)
                .HasForeignKey(d => d.ParticipantNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Equipment_distribution_fk1");
        });

        modelBuilder.Entity<EquipmentList>(entity =>
        {
            entity.HasKey(e => e.EquipmentNumber).HasName("Equipment_list_pk");

            entity.ToTable("Equipment_list");

            entity.Property(e => e.EquipmentNumber).HasColumnName("Equipment_number");
            entity.Property(e => e.Name).HasColumnType("character varying");
            entity.Property(e => e.Note).HasColumnType("character varying");
            entity.Property(e => e.Owner).HasColumnType("character varying");
            entity.Property(e => e.TripNumber).HasColumnName("Trip_number");

            entity.HasOne(d => d.TripNumberNavigation).WithMany(p => p.EquipmentLists)
                .HasForeignKey(d => d.TripNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Equipment_list_fk0");
        });

        modelBuilder.Entity<FirstAidKit>(entity =>
        {
            entity.HasKey(e => new { e.MedicineName, e.TripNumber }).HasName("First_aid_kit_pk");

            entity.ToTable("First_aid_kit");

            entity.Property(e => e.MedicineName)
                .HasColumnType("character varying")
                .HasColumnName("Medicine_name");
            entity.Property(e => e.TripNumber).HasColumnName("Trip_number");
            entity.Property(e => e.Note).HasColumnType("character varying");
            entity.Property(e => e.Purpose).HasColumnType("character varying");

            entity.HasOne(d => d.TripNumberNavigation).WithMany(p => p.FirstAidKits)
                .HasForeignKey(d => d.TripNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("First_aid_kit_fk0");
        });

        modelBuilder.Entity<Food>(entity =>
        {
            entity.HasKey(e => e.FoodNumber).HasName("Food_pk");

            entity.ToTable("Food");

            entity.Property(e => e.FoodNumber).HasColumnName("Food_number");
            entity.Property(e => e.FoodIntakeTypeNumber).HasColumnName("Food_intake_type_number");
            entity.Property(e => e.Note).HasColumnType("character varying");
            entity.Property(e => e.TripNumber).HasColumnName("Trip_number");

            entity.HasOne(d => d.FoodIntakeTypeNumberNavigation).WithMany(p => p.Foods)
                .HasForeignKey(d => d.FoodIntakeTypeNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Food_fk1");

            entity.HasOne(d => d.TripNumberNavigation).WithMany(p => p.Foods)
                .HasForeignKey(d => d.TripNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Food_fk0");
        });

        modelBuilder.Entity<FoodIntakeType>(entity =>
        {
            entity.HasKey(e => e.FoodIntakeTypeNumber).HasName("Food_intake_type_pk");

            entity.ToTable("Food_intake_type");

            entity.HasIndex(e => e.Name, "Food_intake_type_Name_key").IsUnique();

            entity.Property(e => e.FoodIntakeTypeNumber).HasColumnName("Food_intake_type_number");
            entity.Property(e => e.Name).HasColumnType("character varying");
        });

        modelBuilder.Entity<Map>(entity =>
        {
            entity.HasKey(e => e.MapNumber).HasName("Maps_pk");

            entity.Property(e => e.MapNumber).HasColumnName("Map_number");
            entity.Property(e => e.Map1).HasColumnName("Map");
            entity.Property(e => e.TripNumber).HasColumnName("Trip_number");

            entity.HasOne(d => d.TripNumberNavigation).WithMany(p => p.Maps)
                .HasForeignKey(d => d.TripNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Maps_fk0");
        });

        modelBuilder.Entity<ParticipantsList>(entity =>
        {
            entity.HasKey(e => e.ParticipantNumber).HasName("Participants_list_pk");

            entity.ToTable("Participants_list");

            entity.Property(e => e.ParticipantNumber)
                .UseIdentityAlwaysColumn()
                .HasIdentityOptions(15L, null, null, null, null, null)
                .HasColumnName("Participant_number");
            entity.Property(e => e.BoatNumber).HasColumnName("Boat_number");
            entity.Property(e => e.TentNumber).HasColumnName("Tent_number");
            entity.Property(e => e.TripNumber).HasColumnName("Trip_number");
            entity.Property(e => e.UserNumber).HasColumnName("User_number");

            entity.HasOne(d => d.BoatNumberNavigation).WithMany(p => p.ParticipantsLists)
                .HasForeignKey(d => d.BoatNumber)
                .HasConstraintName("Participants_list_fk3");

            entity.HasOne(d => d.TentNumberNavigation).WithMany(p => p.ParticipantsLists)
                .HasForeignKey(d => d.TentNumber)
                .HasConstraintName("Participants_list_fk2");

            entity.HasOne(d => d.TripNumberNavigation).WithMany(p => p.ParticipantsLists)
                .HasForeignKey(d => d.TripNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Participants_list_fk1");

            entity.HasOne(d => d.UserNumberNavigation).WithMany(p => p.ParticipantsLists)
                .HasForeignKey(d => d.UserNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Participants_list_fk0");

            entity.HasMany(d => d.PostNumbers).WithMany(p => p.ParticipantNumbers)
                .UsingEntity<Dictionary<string, object>>(
                    "ParticipantPost",
                    r => r.HasOne<Post>().WithMany()
                        .HasForeignKey("PostNumber")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Participant_posts_fk1"),
                    l => l.HasOne<ParticipantsList>().WithMany()
                        .HasForeignKey("ParticipantNumber")
                        .HasConstraintName("Participant_posts_fk0"),
                    j =>
                    {
                        j.HasKey("ParticipantNumber", "PostNumber").HasName("Participant_posts_pk");
                        j.ToTable("Participant_posts");
                        j.IndexerProperty<int>("ParticipantNumber").HasColumnName("Participant_number");
                        j.IndexerProperty<int>("PostNumber").HasColumnName("Post_number");
                    });
        });

        modelBuilder.Entity<Plan>(entity =>
        {
            entity.HasKey(e => e.EventNumber).HasName("Plan_pk");

            entity.ToTable("Plan");

            entity.Property(e => e.EventNumber).HasColumnName("Event_number");
            entity.Property(e => e.Description).HasColumnType("character varying");
            entity.Property(e => e.Name).HasColumnType("character varying");
            entity.Property(e => e.TripNumber).HasColumnName("Trip_number");

            entity.HasOne(d => d.TripNumberNavigation).WithMany(p => p.Plans)
                .HasForeignKey(d => d.TripNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Plan_fk0");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.PostNumber).HasName("Post_pk");

            entity.ToTable("Post");

            entity.HasIndex(e => e.Name, "Post_Name_key").IsUnique();

            entity.HasIndex(e => e.Responsibilities, "Post_Responsibilities_key").IsUnique();

            entity.Property(e => e.PostNumber).HasColumnName("Post_number");
            entity.Property(e => e.Name).HasColumnType("character varying");
            entity.Property(e => e.Responsibilities).HasColumnType("character varying");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductNumber).HasName("Product_pk");

            entity.ToTable("Product");

            entity.Property(e => e.ProductNumber).HasColumnName("Product_number");
            entity.Property(e => e.ProductName)
                .HasColumnType("character varying")
                .HasColumnName("Product_name");
        });

        modelBuilder.Entity<ProductsForFood>(entity =>
        {
            entity.HasKey(e => new { e.ProductNumber, e.FoodNumber }).HasName("Products_for_food_pk");

            entity.ToTable("Products_for_food");

            entity.Property(e => e.ProductNumber).HasColumnName("Product_number");
            entity.Property(e => e.FoodNumber).HasColumnName("Food_number");

            entity.HasOne(d => d.FoodNumberNavigation).WithMany(p => p.ProductsForFoods)
                .HasForeignKey(d => d.FoodNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Products_for_food_fk1");

            entity.HasOne(d => d.ProductNumberNavigation).WithMany(p => p.ProductsForFoods)
                .HasForeignKey(d => d.ProductNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Products_for_food_fk0");
        });

        modelBuilder.Entity<ProductsList>(entity =>
        {
            entity.HasKey(e => new { e.ProductNumber, e.TripNumber }).HasName("Products_list_pk");

            entity.ToTable("Products_list");

            entity.Property(e => e.ProductNumber).HasColumnName("Product_number");
            entity.Property(e => e.TripNumber).HasColumnName("Trip_number");
            entity.Property(e => e.Note).HasColumnType("character varying");

            entity.HasOne(d => d.ProductNumberNavigation).WithMany(p => p.ProductsLists)
                .HasForeignKey(d => d.ProductNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Products_list_fk0");

            entity.HasOne(d => d.TripNumberNavigation).WithMany(p => p.ProductsLists)
                .HasForeignKey(d => d.TripNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Products_list_fk1");
        });

        modelBuilder.Entity<RepairKit>(entity =>
        {
            entity.HasKey(e => new { e.ToolName, e.TripNumber }).HasName("Repair_kit_pk");

            entity.ToTable("Repair_kit");

            entity.Property(e => e.ToolName)
                .HasColumnType("character varying")
                .HasColumnName("Tool_name");
            entity.Property(e => e.TripNumber).HasColumnName("Trip_number");
            entity.Property(e => e.Note).HasColumnType("character varying");

            entity.HasOne(d => d.TripNumberNavigation).WithMany(p => p.RepairKits)
                .HasForeignKey(d => d.TripNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Repair_kit_fk0");
        });

        modelBuilder.Entity<Route>(entity =>
        {
            entity.HasKey(e => new { e.PointNumber, e.TripNumber }).HasName("Route_pk");

            entity.ToTable("Route");

            entity.Property(e => e.PointNumber).HasColumnName("Point_number");
            entity.Property(e => e.TripNumber).HasColumnName("Trip_number");
            entity.Property(e => e.Description).HasColumnType("character varying");

            entity.HasOne(d => d.PointNumberNavigation).WithMany(p => p.Routes)
                .HasForeignKey(d => d.PointNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Route_fk0");

            entity.HasOne(d => d.TripNumberNavigation).WithMany(p => p.Routes)
                .HasForeignKey(d => d.TripNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Route_fk1");
        });

        modelBuilder.Entity<Tent>(entity =>
        {
            entity.HasKey(e => e.TentNumber).HasName("Tents_pk");

            entity.Property(e => e.TentNumber).HasColumnName("Tent_number");
            entity.Property(e => e.PlacesAmount).HasColumnName("Places_amount");
            entity.Property(e => e.TripNumber).HasColumnName("Trip_number");

            entity.HasOne(d => d.TripNumberNavigation).WithMany(p => p.Tents)
                .HasForeignKey(d => d.TripNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Tents_fk0");
        });

        modelBuilder.Entity<TripType>(entity =>
        {
            entity.HasKey(e => e.TripTypeNumber).HasName("Trip_types_pk");

            entity.ToTable("Trip_types");

            entity.Property(e => e.TripTypeNumber).HasColumnName("Trip_type_number");
            entity.Property(e => e.Type).HasColumnType("character varying");
        });

        modelBuilder.Entity<TripsList>(entity =>
        {
            entity.HasKey(e => e.TripNumber).HasName("Trips_list_pk");

            entity.ToTable("Trips_list");

            entity.Property(e => e.TripNumber)
                .UseIdentityAlwaysColumn()
                .HasIdentityOptions(5L, null, null, null, null, null)
                .HasColumnName("Trip_number");
            entity.Property(e => e.EndDate).HasColumnName("End_date");
            entity.Property(e => e.Place).HasColumnType("character varying");
            entity.Property(e => e.Region).HasColumnType("character varying");
            entity.Property(e => e.StartDate).HasColumnName("Start_date");
            entity.Property(e => e.TripTypeNumber).HasColumnName("Trip_type_number");

            entity.HasOne(d => d.TripTypeNumberNavigation).WithMany(p => p.TripsLists)
                .HasForeignKey(d => d.TripTypeNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Trips_list_fk0");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserNumber).HasName("User_pk");

            entity.ToTable("User");

            entity.HasIndex(e => e.EmailAddress, "User_Email_address_key").IsUnique();

            entity.HasIndex(e => e.PhoneNumber, "User_Phone_number_key").IsUnique();

            entity.Property(e => e.UserNumber)
                .UseIdentityAlwaysColumn()
                .HasIdentityOptions(15L, null, null, null, null, null)
                .HasColumnName("User_number");
            entity.Property(e => e.Allergies).HasColumnType("character varying");
            entity.Property(e => e.EmailAddress)
                .HasColumnType("character varying")
                .HasColumnName("Email_address");
            entity.Property(e => e.MedicationsTaken)
                .HasColumnType("character varying")
                .HasColumnName("Medications_taken");
            entity.Property(e => e.Name).HasColumnType("character varying");
            entity.Property(e => e.Password).HasColumnType("character varying");
            entity.Property(e => e.Patronymic).HasColumnType("character varying");
            entity.Property(e => e.PhoneNumber)
                .HasColumnType("character varying")
                .HasColumnName("Phone_number");
            entity.Property(e => e.Surname).HasColumnType("character varying");
            entity.Property(e => e.СhronicDiseases)
                .HasColumnType("character varying")
                .HasColumnName("Сhronic_diseases");

            entity.HasMany(d => d.DutyTypeNumbers).WithMany(p => p.UserNumbers)
                .UsingEntity<Dictionary<string, object>>(
                    "DutyTypeList",
                    r => r.HasOne<DutyType>().WithMany()
                        .HasForeignKey("DutyTypeNumber")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Duty_type_list_fk1"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserNumber")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Duty_type_list_fk0"),
                    j =>
                    {
                        j.HasKey("UserNumber", "DutyTypeNumber").HasName("Duty_type_list_pk");
                        j.ToTable("Duty_type_list");
                        j.IndexerProperty<int>("UserNumber").HasColumnName("User_number");
                        j.IndexerProperty<int>("DutyTypeNumber").HasColumnName("Duty_type_number");
                    });

            entity.HasMany(d => d.FoodIntakeTypeNumbers).WithMany(p => p.UserNumbers)
                .UsingEntity<Dictionary<string, object>>(
                    "FoodIntakeTypeList",
                    r => r.HasOne<FoodIntakeType>().WithMany()
                        .HasForeignKey("FoodIntakeTypeNumber")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Food_intake_type_list_fk1"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserNumber")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Food_intake_type_list_fk0"),
                    j =>
                    {
                        j.HasKey("UserNumber", "FoodIntakeTypeNumber").HasName("Food_intake_type_list_pk");
                        j.ToTable("Food_intake_type_list");
                        j.IndexerProperty<int>("UserNumber").HasColumnName("User_number");
                        j.IndexerProperty<int>("FoodIntakeTypeNumber").HasColumnName("Food_intake_type_number");
                    });

            entity.HasMany(d => d.PostNumbers).WithMany(p => p.UserNumbers)
                .UsingEntity<Dictionary<string, object>>(
                    "PostList",
                    r => r.HasOne<Post>().WithMany()
                        .HasForeignKey("PostNumber")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Post_list_fk1"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserNumber")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Post_list_fk0"),
                    j =>
                    {
                        j.HasKey("UserNumber", "PostNumber").HasName("Post_list_pk");
                        j.ToTable("Post_list");
                        j.IndexerProperty<int>("UserNumber").HasColumnName("User_number");
                        j.IndexerProperty<int>("PostNumber").HasColumnName("Post_number");
                    });
        });

        modelBuilder.Entity<WaypointList>(entity =>
        {
            entity.HasKey(e => e.PointNumber).HasName("Waypoint_list_pk");

            entity.ToTable("Waypoint_list");

            entity.Property(e => e.PointNumber).HasColumnName("Point_number");
            entity.Property(e => e.Coordinates).HasColumnType("character varying");
            entity.Property(e => e.Name).HasColumnType("character varying");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
