using System;
using System.Collections.Generic;

namespace Helpdesk.Api.Models;

public partial class TInteracao
{
    public int IdInteracao { get; set; }

    public string DsTexto { get; set; } = null!;

    public string? DsAnexoUrl { get; set; }

    public DateTime DtCriacao { get; set; }

    public int IdAutor { get; set; }

    public int IdChamado { get; set; }

    public virtual TUsuario IdAutorNavigation { get; set; } = null!;

    public virtual TChamado IdChamadoNavigation { get; set; } = null!;
}
