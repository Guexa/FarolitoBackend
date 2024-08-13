﻿using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace FarolitoAPIs.Models;

public partial class Recetum
    {
        public int Id { get; set; }

        public string? Nombrelampara { get; set; }

        public bool? Estatus { get; set; }
        public string? Imagen { get; set; }
        [JsonIgnore]
        public virtual ICollection<Componentesrecetum> Componentesreceta { get; set; } = new List<Componentesrecetum>();
        [JsonIgnore]
        public virtual ICollection<Inventariolampara> Inventariolamparas { get; set; } = new List<Inventariolampara>();
        [JsonIgnore]
        public virtual ICollection<Solicitudproduccion> Solicitudproduccions { get; set; } = new List<Solicitudproduccion>();
        [JsonIgnore]
        public virtual ICollection<Carrito> Carritos { get; set; } = new List<Carrito>();
    }
