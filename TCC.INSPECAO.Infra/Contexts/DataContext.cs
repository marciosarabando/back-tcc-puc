using Microsoft.EntityFrameworkCore;
using TCC.INSPECAO.Domain.Entity;
using TCC.INSPECAO.Domain.Enums;

namespace TCC.INSPECAO.Infra.Contexts
{
    public class DataContext : DbContext
    {
        //Por padrão devemos sobrescrever o construtor da clase base (DbContext)
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        //Definir a estrutura de tabelas e realizando o Mapeamento
        public DbSet<Claims> Claims { get; set; }
        public DbSet<Estabelecimento> Estabelecimento { get; set; }
        public DbSet<Inspecao> Inspecao { get; set; }
        public DbSet<InspecaoItem> InspecaoItem { get; set; }
        public DbSet<InspecaoStatus> InspecaoStatus { get; set; }
        public DbSet<Sistema> Sistema { get; set; }
        public DbSet<SistemaItem> SistemaItem { get; set; }
        public DbSet<Turno> Turno { get; set; }
        public DbSet<UnidadeMedida> UnidadeMedida { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<UsuarioClaims> UsuarioClaims { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Claims>().ToTable("CLAIM");
            modelBuilder.Entity<Claims>().Property(x => x.Id);
            modelBuilder.Entity<Claims>().Property(x => x.Nome).HasMaxLength(50).HasColumnType("varchar(50)");
            modelBuilder.Entity<Claims>().Property(x => x.Valor).HasMaxLength(150).HasColumnType("varchar(150)");

            modelBuilder.Entity<Claims>().HasData(
                new Claims("PerfilAcesso", "Administrador"),
                new Claims("PerfilAcesso", "Supervisor"),
                new Claims("PerfilAcesso", "Técnico"),
                new Claims("PerfilAcesso", "Visitante")
            );

            modelBuilder.Entity<Estabelecimento>().ToTable("ESTABELECIMENTO");
            modelBuilder.Entity<Estabelecimento>().Property(x => x.Id);
            modelBuilder.Entity<Estabelecimento>().Property(x => x.Nome);
            modelBuilder.Entity<Estabelecimento>().Property(x => x.CNPJ);

            modelBuilder.Entity<Estabelecimento>().HasData(
                new Estabelecimento("FUNDAÇÃO SÃO FRANCISCO XAVIER - HC HOSPITAL DE CUBATÃO", "19878404002235")
            );

            modelBuilder.Entity<Inspecao>().ToTable("INSPECAO");
            modelBuilder.Entity<Inspecao>().Property(x => x.Id);
            modelBuilder.Entity<Inspecao>().Property(x => x.DataHoraInicio);
            modelBuilder.Entity<Inspecao>().Property(x => x.DataHoraFim);
            modelBuilder.Entity<Inspecao>().Property(x => x.Observacao);

            modelBuilder.Entity<InspecaoItem>().ToTable("INSPECAO_ITEM");
            modelBuilder.Entity<InspecaoItem>().Property(x => x.Id);
            modelBuilder.Entity<InspecaoItem>().Property(x => x.DataHora);
            modelBuilder.Entity<InspecaoItem>().Property(x => x.Observacao);
            modelBuilder.Entity<InspecaoItem>().Property(x => x.Valor);

            modelBuilder.Entity<InspecaoStatus>().ToTable("INSPECAO_STATUS");
            modelBuilder.Entity<InspecaoStatus>().Property(x => x.Id);
            modelBuilder.Entity<InspecaoStatus>().Property(x => x.Nome);
            modelBuilder.Entity<InspecaoStatus>().Property(x => x.Descricao);

            modelBuilder.Entity<InspecaoStatus>().HasData(
                new InspecaoStatus("INICIADA", "Inspeção em andamento."),
                new InspecaoStatus("FINALIZADA", "Inspeção finalizada.")
            );

            modelBuilder.Entity<Sistema>().ToTable("SISTEMA");
            modelBuilder.Entity<Sistema>().Property(x => x.Id);
            modelBuilder.Entity<Sistema>().Property(x => x.Nome);
            modelBuilder.Entity<Sistema>().Property(x => x.Descricao);
            modelBuilder.Entity<Sistema>().HasData(
                new Sistema("CHILLERS E TORRES", "Breve descrição sobre o sistema", 1),
                new Sistema("ABASTECIMENTO DE ÁGUA", "Breve descrição sobre o sistema", 2),
                new Sistema("GRUPO GERADOR", "Breve descrição sobre o sistema", 3),
                new Sistema("ABASTECIMENTO DE ÁGUA", "Breve descrição sobre o sistema", 4),
                new Sistema("SISTEMA CENTRAL DE AR COMPRIMIDO", "Breve descrição sobre o sistema", 5),
                new Sistema("SISTEMA CENTRAL DE VÁCUO", "Breve descrição sobre o sistema", 6),
                new Sistema("SISTEMA ELÉTRICO DE OXIGÊNIO", "Breve descrição sobre o sistema", 7)
            );

            modelBuilder.Entity<SistemaItem>().ToTable("SISTEMA_ITEM");
            modelBuilder.Entity<SistemaItem>().Property(x => x.Id);
            modelBuilder.Entity<SistemaItem>().Property(x => x.Nome);
            modelBuilder.Entity<SistemaItem>().Property(x => x.Descricao);


            modelBuilder.Entity<Turno>().ToTable("TURNO");
            modelBuilder.Entity<Turno>().Property(x => x.Id);
            modelBuilder.Entity<Turno>().Property(x => x.Nome);
            modelBuilder.Entity<Turno>().Property(x => x.Sigla);
            modelBuilder.Entity<Turno>().Property(x => x.HoraInicio);
            modelBuilder.Entity<Turno>().Property(x => x.HoraFim);
            modelBuilder.Entity<Turno>().HasData(
                new Turno("TURNO A", "A", new System.DateTime(1900, 1, 1, 7, 0, 0), new System.DateTime(1900, 1, 1, 19, 0, 0)),
                new Turno("TURNO B", "B", new System.DateTime(1900, 1, 1, 19, 0, 0), new System.DateTime(1900, 1, 1, 7, 0, 0))
            );

            modelBuilder.Entity<UnidadeMedida>().ToTable("UNIDADE_MEDIDA");
            modelBuilder.Entity<UnidadeMedida>().Property(x => x.Id);
            modelBuilder.Entity<UnidadeMedida>().Property(x => x.Nome);
            modelBuilder.Entity<UnidadeMedida>().Property(x => x.TipoDado);
            modelBuilder.Entity<UnidadeMedida>().HasData(
                new UnidadeMedida("VOLTS", TipoDado.DECIMAL),
                new UnidadeMedida("GRAUS", TipoDado.DECIMAL),
                new UnidadeMedida("LITROS", TipoDado.DECIMAL),
                new UnidadeMedida("ITENS", TipoDado.INTEIRO),
                new UnidadeMedida("METROS", TipoDado.DECIMAL),
                new UnidadeMedida("CHECK", TipoDado.CHECK),
                new UnidadeMedida("OBSERVACAO", TipoDado.TEXTO),
                new UnidadeMedida("PRESSÃO", TipoDado.DECIMAL),
                new UnidadeMedida("KW/H", TipoDado.DECIMAL)
            );

            modelBuilder.Entity<Usuario>().ToTable("USUARIO");
            modelBuilder.Entity<Usuario>().Property(x => x.Id);
            modelBuilder.Entity<Usuario>().Property(x => x.Email).HasMaxLength(50).HasColumnType("varchar(50)");
            modelBuilder.Entity<Usuario>().Property(x => x.Nome).HasMaxLength(50).HasColumnType("varchar(50)");
            modelBuilder.Entity<Usuario>().Property(x => x.IdFirebase);
            modelBuilder.Entity<Usuario>().Property(x => x.Ativo);

            modelBuilder.Entity<UsuarioClaims>().ToTable("USUARIO_CLAIMS");
            modelBuilder.Entity<UsuarioClaims>().HasKey(k => new { k.UsuarioId, k.ClaimId });
            modelBuilder.Entity<UsuarioClaims>().Property(x => x.UsuarioId);
            modelBuilder.Entity<UsuarioClaims>().Property(x => x.ClaimId);


            modelBuilder.Entity<UsuarioClaims>().HasOne(u => u.Usuario)
                                                .WithMany(u => u.UsuarioClaims)
                                                .HasForeignKey(u => u.UsuarioId)
                                                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UsuarioClaims>().HasOne(c => c.Claim)
                                                .WithMany(c => c.UsuarioClaims)
                                                .HasForeignKey(c => c.ClaimId)
                                                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}