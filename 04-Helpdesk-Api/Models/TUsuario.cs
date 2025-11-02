using System;
using System.Collections.Generic;

namespace Helpdesk.Api.Models;

public partial class TUsuario
{
    public int IdUsuario { get; set; }

    public string NmNome { get; set; } = null!;

    public string DsUsername { get; set; } = null!;

    public string DsSenhaHash { get; set; } = null!;

    public bool StAtivo { get; set; }

    public int IdPerfil { get; set; }

    public virtual TPerfil IdPerfilNavigation { get; set; } = null!;

    public virtual ICollection<TBaseConhecimento> TBaseConhecimentoIdAtualizadoPorNavigations { get; set; } = new List<TBaseConhecimento>();

    public virtual ICollection<TBaseConhecimento> TBaseConhecimentoIdCriadoPorNavigations { get; set; } = new List<TBaseConhecimento>();

    public virtual ICollection<TChamado> TChamadoIdSolicitanteNavigations { get; set; } = new List<TChamado>();

    public virtual ICollection<TChamado> TChamadoIdTecnicoResponsavelNavigations { get; set; } = new List<TChamado>();

    public virtual ICollection<TInteracao> TInteracaos { get; set; } = new List<TInteracao>();
}
