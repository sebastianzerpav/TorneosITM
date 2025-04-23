using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TorneosITM.Data.Models;

public partial class Torneo
{
    public int IdTorneos { get; set; }

    public int IdAdministradorItm { get; set; }

    public string TipoTorneo { get; set; } = null!;

    public string NombreTorneo { get; set; } = null!;

    public string NombreEquipo { get; set; } = null!;

    public int ValorInscripcion { get; set; }

    public DateOnly FechaTorneo { get; set; }

    public string Integrantes { get; set; } = null!;
    [JsonIgnore]
    public virtual AdministradorItm? IdAdministradorItmNavigation { get; set; }
}
