using System;
using System.Collections.Generic;

namespace Helpdesk.Api.Models;

public partial class TPrioridade
{
    public int IdPrioridade { get; set; }

    public string NmPrioridade { get; set; } = null!;

    public int NrNivel { get; set; }

    public virtual ICollection<TChamado> TChamados { get; set; } = new List<TChamado>();
}
