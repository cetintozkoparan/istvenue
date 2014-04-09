using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity;
using DAL.Entities;
using myBLOGData.Context;
namespace DAL.Context
{
    public class MainContext : DbContext
    {

        public MainContext() : base("name=MainContext") { }
        public virtual void Commit()
        {
            base.SaveChanges();
        }

        public DbSet<AdminUser> AdminUser { get; set; }
        public DbSet<SocialMedia> SocialMedia { get; set; }
        public DbSet<Analytic> Analytic { get; set; }
        public DbSet<Gallery> Gallery { get; set; }
        public DbSet<GalleryGroup> GalleryGroup { get; set; }
        public DbSet<Institutional> Institutional { get; set; }
        public DbSet<Languages> Languages { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<References> References { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<DocumentGroup> DocumentGroup { get; set; }
        public DbSet<Document> Document { get; set; }
        public DbSet<BankInfo> BankInfo { get; set; }
        public DbSet<ProductSubGroup> ProductSubGroup { get; set; }
        public DbSet<ProductSubbestGroup> ProductSubbestGroup { get; set; }
        public DbSet<ProductSubSubbestGroup> ProductSubSubbestGroup { get; set; }
        public DbSet<ProductGroup> ProductGroup { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ImportantLinks> ImportantLinks { get; set; }
        public DbSet<Projects> Projects { get; set; }
        public DbSet<ProjectGroup> ProjectGroup { get; set; }
        public DbSet<ProjectReferences> ProjectReferences { get; set; }
        public DbSet<ProjectReferenceGroup> ProjectReferenceGroup { get; set; }
        public DbSet<HumanResource> HumanResource { get; set; }
        public DbSet<Teklif> Teklif { get; set; }
        public DbSet<TeklifUrun> TeklifUrun { get; set; }
        public DbSet<MailSetting> MailSetting { get; set; }
        public DbSet<MailUsers> MailUsers { get; set; }
        public DbSet<SolutionPartner> SolutionPartner { get; set; }
        public DbSet<HumanResourcePosition> HumanResourcePosition { get; set; }
        public DbSet<Certificate> Certificate { get; set; }
        public DbSet<Service> Service { get; set; }
        public DbSet<ServiceGroup> ServiceGroup { get; set; }
        public DbSet<OurServices> OurServices { get; set; }
        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<Photo> Photo { get; set; }
        public DbSet<Sector> Sector { get; set; }
        public DbSet<SectorGroup> SectorGroup { get; set; }
        public DbSet<OurSectors> OurSectors { get; set; }
        public DbSet<Log4Net_Error> Log4Net_Error { get; set; }
        public DbSet<ProdCategory> ProdCategory { get; set; }
        public DbSet<Page> Page { get; set; }
        public DbSet<OurTeam> OurTeam { get; set; }
        public DbSet<Estate> Estate { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<Town> Town { get; set; }
        public DbSet<District> District { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            Database.SetInitializer(new DatabaseCreatorClass());
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MainContext, Configration>());


            modelBuilder.Entity<AdminUser>().ToTable("AdminUser");
            modelBuilder.Entity<Gallery>().ToTable("Gallery");
            modelBuilder.Entity<GalleryGroup>().ToTable("GalleryGroup");
            modelBuilder.Entity<Institutional>().ToTable("Institutional");
            modelBuilder.Entity<Languages>().ToTable("Languages");
            modelBuilder.Entity<News>().ToTable("News");
            modelBuilder.Entity<References>().ToTable("References");
            modelBuilder.Entity<Contact>().ToTable("Contact");
            modelBuilder.Entity<DocumentGroup>().ToTable("DocumentGroup");
            modelBuilder.Entity<Document>().ToTable("Document");
            modelBuilder.Entity<BankInfo>().ToTable("BankInfo");
            modelBuilder.Entity<ProductSubGroup>().ToTable("ProductSubGroup");
            modelBuilder.Entity<ProductSubbestGroup>().ToTable("ProductSubbestGroup");
            modelBuilder.Entity<ProductSubSubbestGroup>().ToTable("ProductSubSubbestGroup");
            modelBuilder.Entity<ProductGroup>().ToTable("ProductGroup");
            modelBuilder.Entity<Product>().ToTable("Product");
            modelBuilder.Entity<ImportantLinks>().ToTable("ImportantLinks");
            modelBuilder.Entity<Projects>().ToTable("Projects");
            modelBuilder.Entity<ProjectGroup>().ToTable("ProjectGroup");
            modelBuilder.Entity<ProjectReferences>().ToTable("ProjectReferences");
            modelBuilder.Entity<ProjectReferenceGroup>().ToTable("ProjectReferenceGroup");
            modelBuilder.Entity<HumanResource>().ToTable("HumanResource");
            modelBuilder.Entity<Teklif>().ToTable("Teklif");
            modelBuilder.Entity<TeklifUrun>().ToTable("TeklifUrun");
            modelBuilder.Entity<MailUsers>().ToTable("MailUsers");
            modelBuilder.Entity<MailSetting>().ToTable("MailSetting");
            modelBuilder.Entity<SolutionPartner>().ToTable("SolutionPartner");
            modelBuilder.Entity<HumanResourcePosition>().ToTable("HumanResourcePosition");
            modelBuilder.Entity<Certificate>().ToTable("Certificate");
            modelBuilder.Entity<Service>().ToTable("Service");
            modelBuilder.Entity<ServiceGroup>().ToTable("ServiceGroup");
            modelBuilder.Entity<OurServices>().ToTable("OurServices");
            modelBuilder.Entity<Equipment>().ToTable("Equipment");
            modelBuilder.Entity<Photo>().ToTable("Photo");
            modelBuilder.Entity<Sector>().ToTable("Sector");
            modelBuilder.Entity<SectorGroup>().ToTable("SectorGroup");
            modelBuilder.Entity<OurSectors>().ToTable("OurSectors");
            modelBuilder.Entity<Log4Net_Error>().ToTable("Log4Net_Error");
            modelBuilder.Entity<ProdCategory>().ToTable("ProdCategory");
            modelBuilder.Entity<Page>().ToTable("Page");
            modelBuilder.Entity<OurTeam>().ToTable("OurTeam");
            modelBuilder.Entity<Analytic>().ToTable("Analytic");
            modelBuilder.Entity<SocialMedia>().ToTable("SocialMedia");

            modelBuilder.Entity<Estate>().ToTable("Estate");
            modelBuilder.Entity<Country>().ToTable("Country");
            modelBuilder.Entity<Town>().ToTable("Town");
            modelBuilder.Entity<District>().ToTable("District");

        }
    }
}