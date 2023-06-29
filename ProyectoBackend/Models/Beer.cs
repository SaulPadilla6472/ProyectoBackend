using System;
using System.Collections.Generic;

namespace ProyectoBackend.Models;

public partial class Beer
{
    public int BeerId { get; set; }

    public string Name { get; set; } = null!;

    public int BrandId { get; set; }
}
