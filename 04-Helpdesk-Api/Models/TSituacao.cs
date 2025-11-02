using System;
using System.Collections.Generic;

namespace Helpdesk.Api.Models;

public partial class TSituacao
{
    public int IdSituacao { get; set; }

    public string NmSituacao { get; set; } = null!;

    public virtual ICollection<TChamado> TChamados { get; set; } = new List<TChamado>();
}
