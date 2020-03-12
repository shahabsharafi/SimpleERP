﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SimpleERP.Document.API.Infrastructure.Data;

namespace SimpleERP.Document.API.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("SimpleERP.Document.API.Infrastructure.Data.DocumentInfo", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Creator");

                    b.Property<string>("DateOfCreate");

                    b.Property<string>("DateOfModify");

                    b.Property<string>("DateOfRelease");

                    b.Property<long>("DomainId");

                    b.Property<string>("FilePath");

                    b.Property<long>("IssuerId");

                    b.Property<string>("Modifier");

                    b.Property<string>("No");

                    b.Property<string>("Subject");

                    b.Property<string>("Text");

                    b.Property<long>("TypeId");

                    b.HasKey("Id");

                    b.HasIndex("DomainId");

                    b.HasIndex("IssuerId");

                    b.HasIndex("TypeId");

                    b.ToTable("DocumentInfos","ContractManagement");
                });

            modelBuilder.Entity("SimpleERP.Document.API.Infrastructure.Data.Domain", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Hidden");

                    b.Property<bool>("Readonly");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Domain");
                });

            modelBuilder.Entity("SimpleERP.Document.API.Infrastructure.Data.Issuer", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Hidden");

                    b.Property<bool>("Readonly");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Issuer");
                });

            modelBuilder.Entity("SimpleERP.Document.API.Infrastructure.Data.Type", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Hidden");

                    b.Property<bool>("Readonly");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Type");
                });

            modelBuilder.Entity("SimpleERP.Document.API.Infrastructure.Data.DocumentInfo", b =>
                {
                    b.HasOne("SimpleERP.Document.API.Infrastructure.Data.Domain", "Domain")
                        .WithMany()
                        .HasForeignKey("DomainId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SimpleERP.Document.API.Infrastructure.Data.Issuer", "Issuer")
                        .WithMany()
                        .HasForeignKey("IssuerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SimpleERP.Document.API.Infrastructure.Data.Type", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
