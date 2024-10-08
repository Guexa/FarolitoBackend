﻿using System.Text.Json.Serialization;

namespace FarolitoAPIs.Models;

public partial class Mermacomponente
{
    public int Id { get; set; }

    public int? Cantidad { get; set; }

    public string? Descripcion { get; set; }

    public DateOnly? Fecha { get; set; }

    public string UsuarioId { get; set; }

    public int InventariocomponentesId { get; set; }
    
    public virtual Inventariocomponente Inventariocomponentes { get; set; } = null!;
    
    public virtual Usuario Usuario { get; set; } = null!;
}
