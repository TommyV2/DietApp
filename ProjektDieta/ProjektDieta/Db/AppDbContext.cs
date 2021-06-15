using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ProjektDieta.Models;

#nullable disable

namespace ProjektDieta.Db
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BaseRecipeModel> BaseRecipes { get; set; }
        public virtual DbSet<BaseRecipeProductModel> BaseRecipeProducts { get; set; }
        public virtual DbSet<BodyMeasurement> BodyMeasurements { get; set; }
        public virtual DbSet<CollaborationModel> Collaborations { get; set; }
        public virtual DbSet<CustomerModel> Customers { get; set; }
        public virtual DbSet<DemandModel> Demands { get; set; }
        public virtual DbSet<DemandTemplateModel> DemandsTemplates { get; set; }
        public virtual DbSet<DietPlanModel> DietPlans { get; set; }
        public virtual DbSet<DietPlanForTemplate> DietPlanForTemplates { get; set; }
        public virtual DbSet<DietRecommendationModel> DietRecommendations { get; set; }
        public virtual DbSet<FoodPreference> FoodPreferences { get; set; }
        public virtual DbSet<HomeMeasure> HomeMeasures { get; set; }
        public virtual DbSet<MealModel> Meals { get; set; }
        public virtual DbSet<MealForTemplate> MealForTemplates { get; set; }
        public virtual DbSet<MealRecipe> MealRecipes { get; set; }
        public virtual DbSet<MealRecipeForTemplate> MealRecipeForTemplates { get; set; }
        public virtual DbSet<NutrientModel> Nutrients { get; set; }
        public virtual DbSet<ProductModel> Products { get; set; }
        public virtual DbSet<ProductNutrientModel> ProductNutrients { get; set; }
        public virtual DbSet<RecipeModel> Recipes { get; set; }
        public virtual DbSet<RecipeProductModel> RecipeProducts { get; set; }
        public virtual DbSet<SpecialistModel> Specialists { get; set; }
        public virtual DbSet<InvitationModel> Invitations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localhost;Port=5400;Database=projectdiet; User Id=docker;Password=docker;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "en_US.utf8");

            modelBuilder.Entity<AccountModel>(entity =>
            {
                entity.ToTable("accounts");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Email)
                    .HasMaxLength(256)
                    .IsRequired()
                    .HasColumnName("email");

                entity.Property(e => e.Password)
                    .HasMaxLength(128)
                    .IsRequired()
                    .HasColumnName("password");

                entity.Property(e => e.SpecialistId)
                    .HasColumnName("specialist_id");

                entity.Property(e => e.CustomerId)
                    .HasColumnName("customer_id");
            });

            modelBuilder.Entity<BaseRecipeModel>(entity =>
            {
                entity.ToTable("base_recipe");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Instruction)
                    .HasColumnName("instruction");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .HasColumnName("name");

                entity.Property(e => e.OwnerId).HasColumnName("owner_id");

                entity.Property(e => e.OwnerName)
                    .HasMaxLength(256)
                    .HasColumnName("owner_name");

                entity.Property(e => e.Portions).HasColumnName("portions");

                entity.Property(e => e.Time).HasColumnName("time");
            });

            modelBuilder.Entity<BaseRecipeProductModel>(entity =>
            {
                entity.ToTable("base_recipe_product");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.RecipeId).HasColumnName("recipe_id");

                entity.HasOne(d => d.Product)
                    .WithMany()
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("fk_product_to_base_recipe");

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.RecipeId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_base_recipe_to_product");
            });

            modelBuilder.Entity<BodyMeasurement>(entity =>
            {
                entity.ToTable("body_measurement");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Abdomen).HasColumnName("abdomen");

                entity.Property(e => e.Ankle).HasColumnName("ankle");

                entity.Property(e => e.Calf).HasColumnName("calf");

                entity.Property(e => e.Chest).HasColumnName("chest");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");

                entity.Property(e => e.Height).HasColumnName("height");

                entity.Property(e => e.Hips).HasColumnName("hips");

                entity.Property(e => e.Neck).HasColumnName("neck");

                entity.Property(e => e.Thigh).HasColumnName("thigh");

                entity.Property(e => e.Waist).HasColumnName("waist");

                entity.Property(e => e.Weight).HasColumnName("weight");

                entity.Property(e => e.Wrist).HasColumnName("wrist");

                entity.HasOne(d => d.CustomerModel)
                    .WithMany(p => p.BodyMeasurements)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_customer_to_body_measurement");
            });

            modelBuilder.Entity<CollaborationModel>(entity =>
            {
                entity.ToTable("collaboration");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.SpecialistId).HasColumnName("specialist_id");

                entity.Property(e => e.Status)
                    .HasMaxLength(16)
                    .HasColumnName("status");

                entity.Property(e => e.Type)
                    .HasMaxLength(16)
                    .HasColumnName("type");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Collaborations)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_customer_to_collaboration");

                entity.HasOne(d => d.Specialist)
                    .WithMany(p => p.Collaborations)
                    .HasForeignKey(d => d.SpecialistId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_specialist_to_collaboration");
            });

            modelBuilder.Entity<CustomerModel>(entity =>
            {
                entity.ToTable("customer");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.BirthDate)
                    .HasColumnType("date")
                    .HasColumnName("birth_date");

                entity.Property(e => e.City)
                    .HasMaxLength(128)
                    .HasColumnName("city");

                entity.Property(e => e.Email)
                    .HasMaxLength(256)
                    .HasColumnName("email");

                entity.Property(e => e.Gender)
                    .HasMaxLength(16)
                    .HasColumnName("gender");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .HasColumnName("name");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(64)
                    .HasColumnName("phone_number");

                entity.Property(e => e.PhotoUrl)
                    .HasMaxLength(512)
                    .HasColumnName("photo_url");

                entity.Property(e => e.Surname)
                    .HasMaxLength(256)
                    .HasColumnName("surname");

                entity.Property(e => e.Username)
                    .HasMaxLength(128)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<DemandModel>(entity =>
            {
                entity.ToTable("demands");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.A).HasColumnName("a");

                entity.Property(e => e.B6).HasColumnName("b6");

                entity.Property(e => e.Biotin).HasColumnName("biotin");

                entity.Property(e => e.C).HasColumnName("c");

                entity.Property(e => e.Calcium).HasColumnName("calcium");

                entity.Property(e => e.Carbohydrates).HasColumnName("carbohydrates");

                entity.Property(e => e.Chlorine).HasColumnName("chlorine");

                entity.Property(e => e.Choline).HasColumnName("choline");

                entity.Property(e => e.Cobalamin).HasColumnName("cobalamin");

                entity.Property(e => e.Copper).HasColumnName("copper");

                entity.Property(e => e.D).HasColumnName("d");

                entity.Property(e => e.E).HasColumnName("e");

                entity.Property(e => e.Fat).HasColumnName("fat");

                entity.Property(e => e.Fibre).HasColumnName("fibre");

                entity.Property(e => e.Fluorine).HasColumnName("fluorine");

                entity.Property(e => e.Folate).HasColumnName("folate");

                entity.Property(e => e.Iodine).HasColumnName("iodine");

                entity.Property(e => e.Iron).HasColumnName("iron");

                entity.Property(e => e.K).HasColumnName("k");

                entity.Property(e => e.Kcal).HasColumnName("kcal");

                entity.Property(e => e.Magnesium).HasColumnName("magnesium");

                entity.Property(e => e.Niacin).HasColumnName("niacin");

                entity.Property(e => e.PantothenicAcid).HasColumnName("pantothenic_acid");

                entity.Property(e => e.Phosphorus).HasColumnName("phosphorus");

                entity.Property(e => e.Potassium).HasColumnName("potassium");

                entity.Property(e => e.Protein).HasColumnName("protein");

                entity.Property(e => e.Riboflavin).HasColumnName("riboflavin");

                entity.Property(e => e.Selenium).HasColumnName("selenium");

                entity.Property(e => e.Sodium).HasColumnName("sodium");

                entity.Property(e => e.Timine).HasColumnName("timine");

                entity.Property(e => e.Zinc).HasColumnName("zinc");
            });

            modelBuilder.Entity<DemandTemplateModel>(entity =>
            {
                entity.ToTable("demands_template");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.SpecialistId).HasColumnName("specialist_id");

                entity.Property(e => e.A).HasColumnName("a");

                entity.Property(e => e.B6).HasColumnName("b6");

                entity.Property(e => e.Biotin).HasColumnName("biotin");

                entity.Property(e => e.C).HasColumnName("c");

                entity.Property(e => e.Calcium).HasColumnName("calcium");

                entity.Property(e => e.Carbohydrates).HasColumnName("carbohydrates");

                entity.Property(e => e.Chlorine).HasColumnName("chlorine");

                entity.Property(e => e.Choline).HasColumnName("choline");

                entity.Property(e => e.Cobalamin).HasColumnName("cobalamin");

                entity.Property(e => e.Copper).HasColumnName("copper");

                entity.Property(e => e.D).HasColumnName("d");

                entity.Property(e => e.E).HasColumnName("e");

                entity.Property(e => e.Fat).HasColumnName("fat");

                entity.Property(e => e.Fibre).HasColumnName("fibre");

                entity.Property(e => e.Fluorine).HasColumnName("fluorine");

                entity.Property(e => e.Folate).HasColumnName("folate");

                entity.Property(e => e.Iodine).HasColumnName("iodine");

                entity.Property(e => e.Iron).HasColumnName("iron");

                entity.Property(e => e.K).HasColumnName("k");

                entity.Property(e => e.Kcal).HasColumnName("kcal");

                entity.Property(e => e.Magnesium).HasColumnName("magnesium");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(256);

                entity.Property(e => e.Niacin).HasColumnName("niacin");

                entity.Property(e => e.PantothenicAcid).HasColumnName("pantothenic_acid");

                entity.Property(e => e.Phosphorus).HasColumnName("phosphorus");

                entity.Property(e => e.Potassium).HasColumnName("potassium");

                entity.Property(e => e.Protein).HasColumnName("protein");

                entity.Property(e => e.Riboflavin).HasColumnName("riboflavin");

                entity.Property(e => e.Selenium).HasColumnName("selenium");

                entity.Property(e => e.Sodium).HasColumnName("sodium");

                entity.Property(e => e.Timine).HasColumnName("timine");

                entity.Property(e => e.Zinc).HasColumnName("zinc");

                entity.HasOne(d => d.Specialist)
                    .WithMany(p => p.DemandTemplates)
                    .HasForeignKey(d => d.SpecialistId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_specialist_to_demands_template");
            });

            modelBuilder.Entity<DietPlanModel>(entity =>
            {
                entity.ToTable("diet_plan");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.CollaborationId).HasColumnName("collaboration_id");

                entity.Property(e => e.DemandsId).HasColumnName("demands_id");

                entity.Property(e => e.SendDate)
                    .HasColumnType("date")
                    .HasColumnName("send_date");

                entity.Property(e => e.StartDate)
                    .HasColumnType("date")
                    .HasColumnName("start_date");

                entity.HasOne(d => d.CollaborationModel)
                    .WithMany(p => p.DietPlans)
                    .HasForeignKey(d => d.CollaborationId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_collaboration_to_diet_plan");

                entity.HasOne(d => d.DemandsModel)
                    .WithMany(p => p.DietPlans)
                    .HasForeignKey(d => d.DemandsId)
                    .HasConstraintName("fk_demands_template_to_diet_plan");
            });

            modelBuilder.Entity<DietPlanForTemplate>(entity =>
            {
                entity.ToTable("diet_plan_for_template");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.DemandsId).HasColumnName("demands_id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .HasColumnName("name");

                entity.Property(e => e.SpecialistId).HasColumnName("specialist_id");

                entity.HasOne(d => d.SpecialistModel)
                    .WithMany(p => p.DietPlanForTemplates)
                    .HasForeignKey(d => d.SpecialistId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_specialist_to_diet_plan_templates");
            });

            modelBuilder.Entity<DietRecommendationModel>(entity =>
            {
                entity.ToTable("diet_recommendation");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.CollaborationId).HasColumnName("collaboration_id");

                entity.Property(e => e.DemandsId).HasColumnName("demands_id");

                entity.Property(e => e.SendDate)
                    .HasColumnType("date")
                    .HasColumnName("send_date");

                entity.Property(e => e.Text).HasColumnName("text");

                entity.HasOne(d => d.CollaborationModel)
                    .WithMany(p => p.DietRecommendations)
                    .HasForeignKey(d => d.CollaborationId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_collaboration_to_diet_recommendation");

                entity.HasOne(d => d.DemandsModel)
                    .WithMany(p => p.DietRecommendations)
                    .HasForeignKey(d => d.DemandsId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_demands_template_to_diet_recommendation");
            });

            modelBuilder.Entity<FoodPreference>(entity =>
            {
                entity.ToTable("food_preference");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.Product)
                    .HasMaxLength(256)
                    .HasColumnName("product");

                entity.Property(e => e.RelationType)
                    .HasMaxLength(32)
                    .HasColumnName("relation_type");

                entity.HasOne(d => d.CustomerModel)
                    .WithMany()
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_customer_to_food_preference");
            });

            modelBuilder.Entity<HomeMeasure>(entity =>
            {
                entity.ToTable("home_measure");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Mass).HasColumnName("mass");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .HasColumnName("name");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.HasOne(d => d.Product)
                    .WithMany()
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_product_to_home_measure");
            });

            modelBuilder.Entity<MealModel>(entity =>
            {
                entity.ToTable("meal");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Day).HasColumnName("day");

                entity.Property(e => e.DietPlanId).HasColumnName("diet_plan_id");

                entity.Property(e => e.Position).HasColumnName("position");

                entity.HasOne(d => d.DietPlan)
                    .WithMany(p => p.Meals)
                    .HasForeignKey(d => d.DietPlanId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_diet_plan_to_meal");
            });

            modelBuilder.Entity<MealForTemplate>(entity =>
            {
                entity.ToTable("meal_for_template");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Day).HasColumnName("day");

                entity.Property(e => e.DietPlanId).HasColumnName("diet_plan_id");

                entity.Property(e => e.Position).HasColumnName("position");

                entity.HasOne(d => d.DietPlan)
                    .WithMany(p => p.MealForTemplates)
                    .HasForeignKey(d => d.DietPlanId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_dietplan_to_meal_for_templates");
            });

            modelBuilder.Entity<MealRecipe>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("meal_recipe");

                entity.Property(e => e.MealId).HasColumnName("meal_id");

                entity.Property(e => e.RecipeId).HasColumnName("recipe_id");

                entity.HasOne(d => d.Meal)
                    .WithMany()
                    .HasForeignKey(d => d.MealId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_meal_to_meal_recipe");

                entity.HasOne(d => d.Recipe)
                    .WithMany()
                    .HasForeignKey(d => d.RecipeId)
                    .HasConstraintName("fk_recipe_to_meal_recipe");
            });

            modelBuilder.Entity<MealRecipeForTemplate>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("meal_recipe_for_template");

                entity.Property(e => e.MealId).HasColumnName("meal_id");

                entity.Property(e => e.RecipeId).HasColumnName("recipe_id");

                entity.HasOne(d => d.Meal)
                    .WithMany()
                    .HasForeignKey(d => d.MealId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_meal_to_mealrecipe_for_template");

                entity.HasOne(d => d.Recipe)
                    .WithMany()
                    .HasForeignKey(d => d.RecipeId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_recipe_to_mealrecipe_for_template");
            });

            modelBuilder.Entity<NutrientModel>(entity =>
            {
                entity.ToTable("nutrient");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .HasColumnName("name");

                entity.Property(e => e.PolName)
                    .HasMaxLength(256)
                    .HasColumnName("pol_name");

                entity.Property(e => e.ShortName)
                    .HasMaxLength(32)
                    .HasColumnName("short_name");

                entity.Property(e => e.Unit)
                    .HasMaxLength(64)
                    .HasColumnName("unit");
            });

            modelBuilder.Entity<ProductModel>(entity =>
            {
                entity.ToTable("product");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Carbohydrates).HasColumnName("carbohydrates");

                entity.Property(e => e.Fat).HasColumnName("fat");

                entity.Property(e => e.Fiber).HasColumnName("fiber");

                entity.Property(e => e.Kcal).HasColumnName("kcal");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .HasColumnName("name");

                entity.Property(e => e.OwnerId).HasColumnName("owner_id");

                entity.Property(e => e.Protein).HasColumnName("protein");

                entity.Property(e => e.SaturatedFat).HasColumnName("saturated_fat");

                entity.Property(e => e.Source)
                    .HasMaxLength(64)
                    .HasColumnName("source");

                entity.Property(e => e.Sugar).HasColumnName("sugar");
            });

            modelBuilder.Entity<ProductNutrientModel>(entity =>
            {
                entity.ToTable("product_nutrient");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn(); ;

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.NutrientId).HasColumnName("nutrient_id");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.HasOne(d => d.Nutrient)
                    .WithMany()
                    .HasForeignKey(d => d.NutrientId)
                    .HasConstraintName("fk_nutrient_to_product_nutrient");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductNutrients)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_product_to_product_nutrient");
            });

            modelBuilder.Entity<RecipeModel>(entity =>
            {
                entity.ToTable("recipe");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.Instruction).HasColumnName("instruction");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .HasColumnName("name");

                entity.Property(e => e.Portions).HasColumnName("portions");

                entity.Property(e => e.Time).HasColumnName("time");

                entity.Property(e => e.MealId).HasColumnName("meal_id");

                entity.HasOne(d => d.Meal)
                    .WithMany(p => p.Recipes)
                    .HasForeignKey(d => d.MealId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_meal_to_recipe");
            });

            modelBuilder.Entity<RecipeProductModel>(entity =>
            {
                entity.ToTable("recipe_product");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.RecipeId).HasColumnName("recipe_id");

                entity.HasOne(d => d.Product)
                    .WithMany()
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("fk_product_to_recipe_product");

                entity.HasOne(d => d.Recipe)//////// czy zadziała?
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.RecipeId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_recipe_to_recipe_product");
            });

            modelBuilder.Entity<SpecialistModel>(entity =>
            {
                entity.ToTable("specialist");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.BirthDate)
                    .HasColumnType("date")
                    .HasColumnName("birth_date");

                entity.Property(e => e.City)
                    .HasMaxLength(128)
                    .HasColumnName("city");

                entity.Property(e => e.Email)
                    .HasMaxLength(256)
                    .HasColumnName("email");

                entity.Property(e => e.Gender)
                    .HasMaxLength(16)
                    .HasColumnName("gender");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .HasColumnName("name");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(64)
                    .HasColumnName("phone_number");

                entity.Property(e => e.PhotoUrl)
                    .HasMaxLength(512)
                    .HasColumnName("photo_url");

                entity.Property(e => e.Role)
                    .HasMaxLength(16)
                    .HasColumnName("role");

                entity.Property(e => e.Surname)
                    .HasMaxLength(256)
                    .HasColumnName("surname");

                entity.Property(e => e.Username)
                    .HasMaxLength(128)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<InvitationModel>(entity =>
            {
                entity.ToTable("invitation");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.SpecialistId).HasColumnName("specialist_id");

                entity.Property(e => e.SendDate)
                    .HasColumnType("date")
                    .HasColumnName("send_date");

                entity.Property(e => e.Type)
                    .HasMaxLength(16)
                    .HasColumnName("type");
                
                entity.Property(e => e.InvitedBy)
                    .HasMaxLength(16)
                    .HasColumnName("invited_by");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Invitations)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_customer_to_invitation");

                entity.HasOne(d => d.Specialist)
                    .WithMany(p => p.Invitations)
                    .HasForeignKey(d => d.SpecialistId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_specialist_to_invitation");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
