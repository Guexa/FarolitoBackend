﻿using FarolitoAPIs.Data;
using FarolitoAPIs.DTOs;
using FarolitoAPIs.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FarolitoAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComponenteController : ControllerBase
    {
        private readonly FarolitoDbContext _baseDatos;
        public ComponenteController(FarolitoDbContext baseDatos)
        {
            _baseDatos = baseDatos;
        }
        //[Authorize(Roles = "Administrador,Almacen")]
        [HttpGet("componentes")]
        public async Task<IActionResult> ListaComponentes()
        {
            var listaComponentes = await _baseDatos.Componentes.ToListAsync();

            if (listaComponentes == null || !listaComponentes.Any())
            {
                return NotFound(new AuthResponseDTO
                {
                    IsSuccess = false,
                    Message = "No se encontraron componentes"
                });
            }

            return Ok(listaComponentes);
        }

        //[Authorize(Roles = "Administrador,Almacen")]
        [HttpPost("componente")]
        public async Task<IActionResult> AgregarComponente([FromBody] ComponenteDTO nuevoComponente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthResponseDTO
                {
                    IsSuccess = false,
                    Message = "El modelo es inválido"
                });
            }

            var componente = new Componente
            {
                Nombre = nuevoComponente.Nombre,
                estatus = true
            };

            _baseDatos.Componentes.Add(componente);
            await _baseDatos.SaveChangesAsync();

            return Ok(new AuthResponseDTO
            {
                IsSuccess = true,
                Message = "Componente agregado exitosamente"
            });
        }

        [AllowAnonymous]
        [HttpGet("proveedorComponentes")]
        public async Task<IActionResult> ProveedorComponentes([FromQuery] int idProveedor)
        {
            var listaComponentes = await _baseDatos.Productoproveedors.Include(p => p.Proveedor).Include(p=>p.Componentes).Where(p=>p.ProveedorId == idProveedor).ToListAsync();

            if (listaComponentes == null || !listaComponentes.Any())
            {
                return NotFound(new AuthResponseDTO
                {
                    IsSuccess = false,
                    Message = "No se encontraron componentes"
                });
            }

            return Ok(listaComponentes.Select(c=> new
            {
                c.ProveedorId,
                c.Proveedor.NombreEmpresa,
                c.Componentes.Nombre
            }));
        }

        //[Authorize(Roles = "Administrador,Almacen")]
        [HttpPut("componente")]
        public async Task<IActionResult> EditarComponente([FromBody] ComponenteDTO editarComponente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthResponseDTO
                {
                    IsSuccess = false,
                    Message = "El modelo es inválido"
                });
            }

            var componenteExistente = await _baseDatos.Componentes
                .FirstOrDefaultAsync(c => c.Id == editarComponente.Id);

            if (componenteExistente == null)
            {
                return NotFound(new AuthResponseDTO
                {
                    IsSuccess = false,
                    Message = "Componente no encontrado"
                });
            }

            componenteExistente.Nombre = editarComponente.Nombre;
            componenteExistente.estatus = true;

            await _baseDatos.SaveChangesAsync();

            return Ok(new AuthResponseDTO
            {
                IsSuccess = true,
                Message = "Componente actualizado exitosamente"
            });
        }

        //[Authorize(Roles = "Administrador,Almacen")]
        [HttpPatch("componente")]
        public async Task<IActionResult> ActualizarEstatusComponente([FromBody] PatchComponenteDTO estatusDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthResponseDTO
                {
                    IsSuccess = false,
                    Message = "El modelo es inválido"
                });
            }

            var componenteExistente = await _baseDatos.Componentes
                .FirstOrDefaultAsync(c => c.Id == estatusDTO.Id);

            if (componenteExistente == null)
            {
                return NotFound(new AuthResponseDTO
                {
                    IsSuccess = false,
                    Message = "Componente no encontrado"
                });
            }

            componenteExistente.estatus = estatusDTO.estatus;

            await _baseDatos.SaveChangesAsync();

            return Ok(new AuthResponseDTO
            {
                IsSuccess = true,
                Message = "Estatus del componente actualizado exitosamente"
            });
        }
    }
}
