using System;
using System.Collections.Generic;

namespace Helpdesk.Api.Models;

public partial class TBaseConhecimento
{
    public int IdBaseConhecimento { get; set; }

    public string DsTitulo { get; set; } = null!;

    public string DsConteudo { get; set; } = null!;

    public DateTime DtCriacao { get; set; }

    public DateTime? DtAtualizacao { get; set; }

    public int IdCriadoPor { get; set; }

    public int? IdAtualizadoPor { get; set; }

    public virtual TUsuario? IdAtualizadoPorNavigation { get; set; }

    public virtual TUsuario IdCriadoPorNavigation { get; set; } = null!;

    public virtual ICollection<TPalavraChave> IdPalavraChaves { get; set; } = new List<TPalavraChave>();
}
