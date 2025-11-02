using System;
using System.Collections.Generic;

namespace Helpdesk.Api.Models;

public partial class TPermissao
{
    public int IdPermissao { get; set; }

    public string DsChave { get; set; } = null!;

    public string DsDescricao { get; set; } = null!;

    public virtual ICollection<TPerfil> IdPerfils { get; set; } = new List<TPerfil>();
}
