using System;
using System.Collections.Generic;

namespace Helpdesk.Api.Models;

public partial class TPerfil
{
    public int IdPerfil { get; set; }

    public string NmNome { get; set; } = null!;

    public virtual ICollection<TUsuario> TUsuarios { get; set; } = new List<TUsuario>();

    public virtual ICollection<TPermissao> IdPermissaos { get; set; } = new List<TPermissao>();
}
