using System;
using System.Collections.Generic;

namespace Helpdesk.Api.Models;

public partial class TPalavraChave
{
    public int IdPalavraChave { get; set; }

    public string DsPalavra { get; set; } = null!;

    public virtual ICollection<TBaseConhecimento> IdBaseConhecimentos { get; set; } = new List<TBaseConhecimento>();
}
