using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Context
{
    public class Configration : DbMigrationsConfiguration<MainContext>
    {
        public Configration()
        {
            AutomaticMigrationsEnabled = true; // production'da false yapılacak...
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(MainContext context)
        {
            context.Country.AddOrUpdate(
                new Country() { Id = 1, Name = "İstanbul-Anadolu" },
                new Country() { Id = 2, Name = "İstanbul-Avrupa" },
                new Country() { Id = 3, Name = "Muğla" }

                );

            context.Town.AddOrUpdate(
              new Town() { Id = 1,CountryId=1, Name = "Adalar" },
              new Town() { Id = 2,CountryId=1, Name = "Ataşehir" },
              new Town() { Id = 3,CountryId=1, Name = "Beyköy" },
              new Town() { Id = 4,CountryId=1, Name = "Kadıköy" },
              new Town() { Id = 5,CountryId=1, Name = "Kartal" },

              new Town() { Id = 6, CountryId = 2, Name = "Avcılar" },
              new Town() { Id = 7, CountryId = 2, Name = "Bağcılar" },
              new Town() { Id = 8, CountryId = 2, Name = "Bakırköy" },
              new Town() { Id = 9, CountryId = 2, Name = "Beşiktaş" },

              new Town() { Id = 10, CountryId = 3, Name = "Bodrum" },
              new Town() { Id = 11, CountryId = 3, Name = "Fethiye" },
              new Town() { Id = 12, CountryId = 3, Name = "Marmaris" },
              new Town() { Id = 13, CountryId = 3, Name = "Köyceğiz" }
              );


            context.District.AddOrUpdate(
             new District() { Id = 1, TownId = 1, Name = "Adalar Semt 1" },
             new District() { Id = 2, TownId = 2, Name = "Ataşehir Semt 1" },
             new District() { Id = 3, TownId = 3, Name = "Beyköy Semt 1" },
             new District() { Id = 4, TownId = 3, Name = "Beyköy Semt 2" },
             new District() { Id = 5, TownId = 4, Name = "Kartal Semt 1" }
             );


            base.Seed(context);
        }
    }
}
