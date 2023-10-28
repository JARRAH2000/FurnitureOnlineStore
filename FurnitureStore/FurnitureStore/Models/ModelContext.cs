using Microsoft.EntityFrameworkCore;


#nullable disable

namespace FurnitureStore.Models
{
    public partial class ModelContext : DbContext
    {
        public ModelContext()
        {
        }

        public ModelContext(DbContextOptions<ModelContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Bag> Bags { get; set; }
        public virtual DbSet<Bank> Banks { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<ContactInfo> ContactInfos { get; set; }
        public virtual DbSet<Favourite> Favourites { get; set; }
        public virtual DbSet<Furniture> Furnitures { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Offer> Offers { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<ProductPayment> ProductPayments { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Section> Sections { get; set; }
        public virtual DbSet<Testimonial> Testimonials { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseOracle("add connection string here");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("JOR17_USER211")
                .HasAnnotation("Relational:Collation", "USING_NLS_COMP");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("ACCOUNT");

                entity.HasIndex(e => e.Username, "SYS_C00314119")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("PASSWORD");

                entity.Property(e => e.RoleId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ROLE_ID");

                entity.Property(e => e.UserId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("USER_ID");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("USERNAME");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("ROLE_ID_FK");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("USER_ID_FK");
            });

            modelBuilder.Entity<Bag>(entity =>
            {
                entity.ToTable("BAGS");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.FurnitureId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("FURNITURE_ID");

                entity.Property(e => e.Quantity)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("QUANTITY");

                entity.Property(e => e.UserId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("USER_ID");

                entity.HasOne(d => d.Furniture)
                    .WithMany(p => p.Bags)
                    .HasForeignKey(d => d.FurnitureId)
                    .HasConstraintName("FUR_BAG_FK");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Bags)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("USER_BAG_FK");
            });

            modelBuilder.Entity<Bank>(entity =>
            {
                entity.ToTable("BANK");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Balance)
                    .HasColumnType("FLOAT")
                    .HasColumnName("BALANCE");

                entity.Property(e => e.CardCvc)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CARD_CVC");

                entity.Property(e => e.CardNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CARD_NUMBER");

                entity.Property(e => e.ExpirationDate)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("EXPIRATION_DATE");

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("FULL_NAME");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("COMMENTS");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.FurnitureId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("FURNITURE_ID");

                entity.Property(e => e.Publish)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("PUBLISH")
                    .HasDefaultValueSql("('Y') ")
                    .IsFixedLength(true);

                entity.Property(e => e.UserId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("USER_ID");

                entity.HasOne(d => d.Furniture)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.FurnitureId)
                    .HasConstraintName("FUR_CMNT_FK");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("USER_CMNT_FK");
            });

            modelBuilder.Entity<ContactInfo>(entity =>
            {
                entity.ToTable("CONTACT_INFO");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Address)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ADDRESS");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Facebook)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("FACEBOOK");

                entity.Property(e => e.Fax)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("FAX");

                entity.Property(e => e.Instagram)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("INSTAGRAM");

                entity.Property(e => e.Phone)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("PHONE");

                entity.Property(e => e.Twitter)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("TWITTER");

                entity.Property(e => e.Youtube)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("YOUTUBE");
            });

            modelBuilder.Entity<Favourite>(entity =>
            {
                entity.ToTable("FAVOURITE");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.FurnitureId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("FURNITURE_ID");

                entity.Property(e => e.UserId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("USER_ID");

                entity.HasOne(d => d.Furniture)
                    .WithMany(p => p.Favourites)
                    .HasForeignKey(d => d.FurnitureId)
                    .HasConstraintName("FUR_FAV_FK");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Favourites)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("USER_FAV_FK");
            });

            modelBuilder.Entity<Furniture>(entity =>
            {
                entity.ToTable("FURNITURE");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Cost)
                    .HasColumnType("FLOAT")
                    .HasColumnName("COST");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPTION")
                    .HasDefaultValueSql("'NO DESCRIPTION'");

                entity.Property(e => e.Imagepath)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("IMAGEPATH");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

                entity.Property(e => e.Price)
                    .HasColumnType("FLOAT")
                    .HasColumnName("PRICE");

                entity.Property(e => e.Quantity)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("QUANTITY")
                    .HasDefaultValueSql("0 ");

                entity.Property(e => e.SectionId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("SECTION_ID");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.Furnitures)
                    .HasForeignKey(d => d.SectionId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("SECTION_ID_FK");
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.ToTable("IMAGE");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.FurnitureId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("FURNITURE_ID");

                entity.Property(e => e.Imagepath)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("IMAGEPATH");

                entity.HasOne(d => d.Furniture)
                    .WithMany(p => p.Images)
                    .HasForeignKey(d => d.FurnitureId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FURNITURE_IMG_FK");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("MESSAGE");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.ImagePath)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("IMAGE_PATH");

                entity.Property(e => e.SendTime)
                    .HasPrecision(6)
                    .HasColumnName("SEND_TIME");

                entity.Property(e => e.Subject)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("SUBJECT")
                    .HasDefaultValueSql("('NO SUBJECT')");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("TEXT");
            });



			modelBuilder.Entity<Pages>(entity =>
			{
				entity.ToTable("PAGES");

				entity.Property(e => e.Id)
					.HasColumnType("NUMBER(38)")
					.ValueGeneratedOnAdd()
					.HasColumnName("ID");

				entity.Property(e => e.About)
					.IsRequired()
					.HasMaxLength(1000)
					.IsUnicode(false)
					.HasColumnName("ABOUT");

				entity.Property(e => e.AboutImagePath)
					.HasMaxLength(100)
					.IsUnicode(false)
					.HasColumnName("ABOUT_IMAGE_PATH");

				

				entity.Property(e => e.Greeting)
					.HasMaxLength(500)
					.IsUnicode(false)
					.HasColumnName("GREETING")
					.HasDefaultValueSql("('NO SUBJECT')");

				entity.Property(e => e.TopImagePath)
					.HasMaxLength(100)
					.IsUnicode(false)
					.HasColumnName("TOP_IMAGE_PATH");

				entity.Property(e => e.MainLogoPath)
					.HasMaxLength(100)
					.IsUnicode(false)
					.HasColumnName("MAIN_LOGO_PATH");
				entity.Property(e => e.SecLogoPath)
					.HasMaxLength(100)
					.IsUnicode(false)
					.HasColumnName("SEC_LOLO_PATH");
			});




			modelBuilder.Entity<Offer>(entity =>
            {
                entity.ToTable("OFFER");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.EndDate)
                    .HasColumnType("DATE")
                    .HasColumnName("END_DATE");

                entity.Property(e => e.FurnitureId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("FURNITURE_ID");

                entity.Property(e => e.Percentage)
                    .HasColumnType("FLOAT")
                    .HasColumnName("PERCENTAGE");

                entity.Property(e => e.StartDate)
                    .HasColumnType("DATE")
                    .HasColumnName("START_DATE");

                entity.HasOne(d => d.Furniture)
                    .WithMany(p => p.Offers)
                    .HasForeignKey(d => d.FurnitureId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FUR_OFFER");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("PAYMENT");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.BillTime)
                    .HasPrecision(6)
                    .HasColumnName("BILL_TIME");

                entity.Property(e => e.UserId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("USER_ID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("USER_ID_PAY_FK");
            });

            modelBuilder.Entity<ProductPayment>(entity =>
            {
                entity.ToTable("PRODUCT_PAYMENT");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.FurnitureId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("FURNITURE_ID");

                entity.Property(e => e.PaymentId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("PAYMENT_ID");

                entity.Property(e => e.Price)
                    .HasColumnType("FLOAT")
                    .HasColumnName("PRICE");

                entity.Property(e => e.Quantity)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("QUANTITY");

                entity.HasOne(d => d.Furniture)
                    .WithMany(p => p.ProductPayments)
                    .HasForeignKey(d => d.FurnitureId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FUR_ID_FK");

                entity.HasOne(d => d.Payment)
                    .WithMany(p => p.ProductPayments)
                    .HasForeignKey(d => d.PaymentId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("PAY_ID_FK");
            });

            modelBuilder.Entity<Rating>(entity =>
            {
                entity.ToTable("RATING");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.FurnitureId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("FURNITURE_ID");

                entity.Property(e => e.Stars)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("STARS");

                entity.Property(e => e.UserId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("USER_ID");

                entity.HasOne(d => d.Furniture)
                    .WithMany(p => p.Ratings)
                    .HasForeignKey(d => d.FurnitureId)
                    .HasConstraintName("FUR_RATE_FK");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Ratings)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("USER_RATE_FK");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("ROLE");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("NAME");
            });

            modelBuilder.Entity<Section>(entity =>
            {
                entity.ToTable("SECTION");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.ImagePath)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("IMAGE_PATH");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("NAME");
            });

            modelBuilder.Entity<Testimonial>(entity =>
            {
                entity.ToTable("TESTIMONIAL");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Publish)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("PUBLISH")
                    .HasDefaultValueSql("('N') ")
                    .IsFixedLength(true);

                entity.Property(e => e.SendTime)
                    .HasPrecision(6)
                    .HasColumnName("SEND_TIME");

                entity.Property(e => e.SenderId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("SENDER_ID");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("TEXT");

                entity.HasOne(d => d.Sender)
                    .WithMany(p => p.Testimonials)
                    .HasForeignKey(d => d.SenderId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("SEND_TEST_FK");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("USERS");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Birthdate)
                    .HasColumnType("DATE")
                    .HasColumnName("BIRTHDATE")
                    .HasDefaultValueSql("TO_DATE('1990-01-01','YYYY-MM-DD')");

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("FIRSTNAME");

                entity.Property(e => e.Imagepath)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("IMAGEPATH");

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("LASTNAME");

                entity.Property(e => e.Sex)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("SEX");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public DbSet<FurnitureStore.Models.Pages> Pages { get; set; }
    }
}
