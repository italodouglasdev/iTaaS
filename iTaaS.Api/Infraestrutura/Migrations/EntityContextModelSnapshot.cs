﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using iTaaS.Api.Infraestrutura.BancoDeDados;

namespace iTaaS.Api.Migrations
{
    [DbContext(typeof(EntityContext))]
    partial class EntityContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("iTaaS.Api.Dominio.Entidades.LogEntidade", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataHoraRecebimento");

                    b.Property<string>("Hash");

                    b.Property<string>("UrlOrigem");

                    b.Property<string>("Versao");

                    b.HasKey("Id");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("iTaaS.Api.Dominio.Entidades.LogLinhaEntidade", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CacheStatus");

                    b.Property<string>("CaminhoUrl");

                    b.Property<int>("CodigoStatus");

                    b.Property<int>("LogId");

                    b.Property<string>("MetodoHttp");

                    b.Property<int>("TamahoResposta");

                    b.Property<decimal>("TempoResposta");

                    b.HasKey("Id");

                    b.HasIndex("LogId");

                    b.ToTable("LogsLinhas");
                });

            modelBuilder.Entity("iTaaS.Api.Dominio.Entidades.LogLinhaEntidade", b =>
                {
                    b.HasOne("iTaaS.Api.Dominio.Entidades.LogEntidade", "Log")
                        .WithMany("Linhas")
                        .HasForeignKey("LogId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
