using System;
using System.Collections.Generic;

namespace TorneosITM.Data.Models;

public partial class AdministradorItm
{
    public int IdAministradorItm { get; set; }

    public string Documento { get; set; } = null!;

    public string NombreCompleto { get; set; } = null!;

    public string Usuario { get; set; } = null!;

    public string Clave { get; set; } = null!;

    public virtual ICollection<Torneo> Torneos { get; set; } = new List<Torneo>();
}
