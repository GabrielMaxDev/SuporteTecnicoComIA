using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Helpdesk.Api.Models;

public partial class HelpdeskDBContext : DbContext
{
    public HelpdeskDBContext()
    {
    }

    public HelpdeskDBContext(DbContextOptions<HelpdeskDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TBaseConhecimento> TBaseConhecimentos { get; set; }

    public virtual DbSet<TChamado> TChamados { get; set; }

    public virtual DbSet<TInteracao> TInteracaos { get; set; }

    public virtual DbSet<TPalavraChave> TPalavraChaves { get; set; }

    public virtual DbSet<TPerfil> TPerfils { get; set; }

    public virtual DbSet<TPermissao> TPermissaos { get; set; }

    public virtual DbSet<TPrioridade> TPrioridades { get; set; }

    public virtual DbSet<TSituacao> TSituacaos { get; set; }

    public virtual DbSet<TUsuario> TUsuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TBaseConhecimento>(entity =>
        {
            entity.HasKey(e => e.IdBaseConhecimento).HasName("PK__T_BASE_C__0A13A50DF088931B");

            entity.ToTable("T_BASE_CONHECIMENTO");

            entity.Property(e => e.IdBaseConhecimento).HasColumnName("id_base_conhecimento");
            entity.Property(e => e.DsConteudo)
                .IsUnicode(false)
                .HasColumnName("ds_conteudo");
            entity.Property(e => e.DsTitulo)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ds_titulo");
            entity.Property(e => e.DtAtualizacao).HasColumnName("dt_atualizacao");
            entity.Property(e => e.DtCriacao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("dt_criacao");
            entity.Property(e => e.IdAtualizadoPor).HasColumnName("id_atualizado_por");
            entity.Property(e => e.IdCriadoPor).HasColumnName("id_criado_por");

            entity.HasOne(d => d.IdAtualizadoPorNavigation).WithMany(p => p.TBaseConhecimentoIdAtualizadoPorNavigations)
                .HasForeignKey(d => d.IdAtualizadoPor)
                .HasConstraintName("FK__T_BASE_CO__id_at__59063A47");

            entity.HasOne(d => d.IdCriadoPorNavigation).WithMany(p => p.TBaseConhecimentoIdCriadoPorNavigations)
                .HasForeignKey(d => d.IdCriadoPor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__T_BASE_CO__id_cr__5812160E");

            entity.HasMany(d => d.IdPalavraChaves).WithMany(p => p.IdBaseConhecimentos)
                .UsingEntity<Dictionary<string, object>>(
                    "TBaseConhecimentoPalavraChave",
                    r => r.HasOne<TPalavraChave>().WithMany()
                        .HasForeignKey("IdPalavraChave")
                        .HasConstraintName("FK__T_BASE_CO__id_pa__5FB337D6"),
                    l => l.HasOne<TBaseConhecimento>().WithMany()
                        .HasForeignKey("IdBaseConhecimento")
                        .HasConstraintName("FK__T_BASE_CO__id_ba__5EBF139D"),
                    j =>
                    {
                        j.HasKey("IdBaseConhecimento", "IdPalavraChave").HasName("PK__T_BASE_C__2D89244E03E52C20");
                        j.ToTable("T_BASE_CONHECIMENTO_PALAVRA_CHAVE");
                        j.IndexerProperty<int>("IdBaseConhecimento").HasColumnName("id_base_conhecimento");
                        j.IndexerProperty<int>("IdPalavraChave").HasColumnName("id_palavra_chave");
                    });
        });

        modelBuilder.Entity<TChamado>(entity =>
        {
            entity.HasKey(e => e.IdChamado).HasName("PK__T_CHAMAD__FB9E7C1E56CA9BE3");

            entity.ToTable("T_CHAMADO");

            entity.Property(e => e.IdChamado).HasColumnName("id_chamado");
            entity.Property(e => e.DsDescricao)
                .IsUnicode(false)
                .HasColumnName("ds_descricao");
            entity.Property(e => e.DsTitulo)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ds_titulo");
            entity.Property(e => e.DtAbertura)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("dt_abertura");
            entity.Property(e => e.DtFechamento).HasColumnName("dt_fechamento");
            entity.Property(e => e.IdPrioridade).HasColumnName("id_prioridade");
            entity.Property(e => e.IdSituacao).HasColumnName("id_situacao");
            entity.Property(e => e.IdSolicitante).HasColumnName("id_solicitante");
            entity.Property(e => e.IdTecnicoResponsavel).HasColumnName("id_tecnico_responsavel");

            entity.HasOne(d => d.IdPrioridadeNavigation).WithMany(p => p.TChamados)
                .HasForeignKey(d => d.IdPrioridade)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__T_CHAMADO__id_pr__4F7CD00D");

            entity.HasOne(d => d.IdSituacaoNavigation).WithMany(p => p.TChamados)
                .HasForeignKey(d => d.IdSituacao)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__T_CHAMADO__id_si__4E88ABD4");

            entity.HasOne(d => d.IdSolicitanteNavigation).WithMany(p => p.TChamadoIdSolicitanteNavigations)
                .HasForeignKey(d => d.IdSolicitante)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__T_CHAMADO__id_so__4CA06362");

            entity.HasOne(d => d.IdTecnicoResponsavelNavigation).WithMany(p => p.TChamadoIdTecnicoResponsavelNavigations)
                .HasForeignKey(d => d.IdTecnicoResponsavel)
                .HasConstraintName("FK__T_CHAMADO__id_te__4D94879B");
        });

        modelBuilder.Entity<TInteracao>(entity =>
        {
            entity.HasKey(e => e.IdInteracao).HasName("PK__T_INTERA__FC7DC95ED927B7DB");

            entity.ToTable("T_INTERACAO");

            entity.Property(e => e.IdInteracao).HasColumnName("id_interacao");
            entity.Property(e => e.DsAnexoUrl)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("ds_anexo_url");
            entity.Property(e => e.DsTexto)
                .IsUnicode(false)
                .HasColumnName("ds_texto");
            entity.Property(e => e.DtCriacao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("dt_criacao");
            entity.Property(e => e.IdAutor).HasColumnName("id_autor");
            entity.Property(e => e.IdChamado).HasColumnName("id_chamado");

            entity.HasOne(d => d.IdAutorNavigation).WithMany(p => p.TInteracaos)
                .HasForeignKey(d => d.IdAutor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__T_INTERAC__id_au__534D60F1");

            entity.HasOne(d => d.IdChamadoNavigation).WithMany(p => p.TInteracaos)
                .HasForeignKey(d => d.IdChamado)
                .HasConstraintName("FK__T_INTERAC__id_ch__5441852A");
        });

        modelBuilder.Entity<TPalavraChave>(entity =>
        {
            entity.HasKey(e => e.IdPalavraChave).HasName("PK__T_PALAVR__79A8143305D5B0AE");

            entity.ToTable("T_PALAVRA_CHAVE");

            entity.HasIndex(e => e.DsPalavra, "UQ__T_PALAVR__9CA866C1EE6A3118").IsUnique();

            entity.Property(e => e.IdPalavraChave).HasColumnName("id_palavra_chave");
            entity.Property(e => e.DsPalavra)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ds_palavra");
        });

        modelBuilder.Entity<TPerfil>(entity =>
        {
            entity.HasKey(e => e.IdPerfil).HasName("PK__T_PERFIL__1D1C87689809E123");

            entity.ToTable("T_PERFIL");

            entity.HasIndex(e => e.NmNome, "UQ__T_PERFIL__175E4045B0F6AFF1").IsUnique();

            entity.Property(e => e.IdPerfil).HasColumnName("id_perfil");
            entity.Property(e => e.NmNome)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nm_nome");

            entity.HasMany(d => d.IdPermissaos).WithMany(p => p.IdPerfils)
                .UsingEntity<Dictionary<string, object>>(
                    "TPerfilPermissao",
                    r => r.HasOne<TPermissao>().WithMany()
                        .HasForeignKey("IdPermissao")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__T_PERFIL___id_pe__3E52440B"),
                    l => l.HasOne<TPerfil>().WithMany()
                        .HasForeignKey("IdPerfil")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__T_PERFIL___id_pe__3D5E1FD2"),
                    j =>
                    {
                        j.HasKey("IdPerfil", "IdPermissao").HasName("PK__T_PERFIL__5282C1153615243C");
                        j.ToTable("T_PERFIL_PERMISSAO");
                        j.IndexerProperty<int>("IdPerfil").HasColumnName("id_perfil");
                        j.IndexerProperty<int>("IdPermissao").HasColumnName("id_permissao");
                    });
        });

        modelBuilder.Entity<TPermissao>(entity =>
        {
            entity.HasKey(e => e.IdPermissao).HasName("PK__T_PERMIS__F9E467D5EDBFA832");

            entity.ToTable("T_PERMISSAO");

            entity.HasIndex(e => e.DsChave, "UQ__T_PERMIS__6458A9DB2FEFAC7E").IsUnique();

            entity.Property(e => e.IdPermissao).HasColumnName("id_permissao");
            entity.Property(e => e.DsChave)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ds_chave");
            entity.Property(e => e.DsDescricao)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ds_descricao");
        });

        modelBuilder.Entity<TPrioridade>(entity =>
        {
            entity.HasKey(e => e.IdPrioridade).HasName("PK__T_PRIORI__FFE37015ABE4E387");

            entity.ToTable("T_PRIORIDADE");

            entity.HasIndex(e => e.NmPrioridade, "UQ__T_PRIORI__2ADBE533EF1C66DC").IsUnique();

            entity.Property(e => e.IdPrioridade).HasColumnName("id_prioridade");
            entity.Property(e => e.NmPrioridade)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nm_prioridade");
            entity.Property(e => e.NrNivel).HasColumnName("nr_nivel");
        });

        modelBuilder.Entity<TSituacao>(entity =>
        {
            entity.HasKey(e => e.IdSituacao).HasName("PK__T_SITUAC__E2EF00C1B0E8244F");

            entity.ToTable("T_SITUACAO");

            entity.HasIndex(e => e.NmSituacao, "UQ__T_SITUAC__ABB10D393A83B84B").IsUnique();

            entity.Property(e => e.IdSituacao).HasColumnName("id_situacao");
            entity.Property(e => e.NmSituacao)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nm_situacao");
        });

        modelBuilder.Entity<TUsuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__T_USUARI__4E3E04AD0EDE7E7B");

            entity.ToTable("T_USUARIO");

            entity.HasIndex(e => e.DsUsername, "UQ__T_USUARI__1EB75F12D3A4CA31").IsUnique();

            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.DsSenhaHash)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ds_senha_hash");
            entity.Property(e => e.DsUsername)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ds_username");
            entity.Property(e => e.IdPerfil).HasColumnName("id_perfil");
            entity.Property(e => e.NmNome)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nm_nome");
            entity.Property(e => e.StAtivo)
                .HasDefaultValue(true)
                .HasColumnName("st_ativo");

            entity.HasOne(d => d.IdPerfilNavigation).WithMany(p => p.TUsuarios)
                .HasForeignKey(d => d.IdPerfil)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__T_USUARIO__id_pe__4316F928");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
