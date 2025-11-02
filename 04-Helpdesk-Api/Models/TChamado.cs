using System;
using System.Collections.Generic;

namespace Helpdesk.Api.Models;

public partial class TChamado
{
    public int IdChamado { get; set; }

    public string DsTitulo { get; set; } = null!;

    public string DsDescricao { get; set; } = null!;

    public DateTime DtAbertura { get; set; }

    public DateTime? DtFechamento { get; set; }

    public int IdSolicitante { get; set; }

    public int? IdTecnicoResponsavel { get; set; }

    public int IdSituacao { get; set; }

    public int IdPrioridade { get; set; }

    public virtual TPrioridade IdPrioridadeNavigation { get; set; } = null!;

    public virtual TSituacao IdSituacaoNavigation { get; set; } = null!;

    public virtual TUsuario IdSolicitanteNavigation { get; set; } = null!;

    public virtual TUsuario? IdTecnicoResponsavelNavigation { get; set; }

    public virtual ICollection<TInteracao> TInteracaos { get; set; } = new List<TInteracao>();
}
