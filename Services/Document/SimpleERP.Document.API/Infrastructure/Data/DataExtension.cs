using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleERP.Document.API.Infrastructure.Data
{
    public static class DataExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Issuer>().HasData(
                new Issuer { Id = 1, Readonly = true, Hidden = false, Title = "سازمان برنامه و بودجه" },
                new Issuer { Id = 2, Readonly = true, Hidden = false, Title = "سازمان نظام مهندسی" },
                new Issuer { Id = 3, Readonly = true, Hidden = false, Title = "وزارت راه و شهرسازی" },
                new Issuer { Id = 4, Readonly = true, Hidden = false, Title = "سازمان اداری و استخدامی" },
                new Issuer { Id = 5, Readonly = true, Hidden = false, Title = "وزارت اقتصاد و دارایی" },
                new Issuer { Id = 6, Readonly = true, Hidden = false, Title = "هیأت دولت" },
                new Issuer { Id = 7, Readonly = true, Hidden = false, Title = "قانون مصوب" }
            );
            modelBuilder.Entity<Domain>().HasData(
                new Domain { Id = 1, Readonly = true, Hidden = false, Title = "اداری" },
                new Domain { Id = 2, Readonly = true, Hidden = false, Title = "مالی" },
                new Domain { Id = 3, Readonly = true, Hidden = false, Title = "فناوری اطلاعات" },
                new Domain { Id = 4, Readonly = true, Hidden = false, Title = "فنی" }
            );
            modelBuilder.Entity<Type>().HasData(
                new Type { Id = 1, Readonly = true, Hidden = false, Title = "آئین‌نامه" },
                new Type { Id = 2, Readonly = true, Hidden = false, Title = "بخشنامه" },
                new Type { Id = 3, Readonly = true, Hidden = false, Title = "دستورالعمل" }
            );
        }
    }
}
