using System;
using VentasApi.Models;
using VentasApi.Models.DTOS.ClienteDTO;

namespace VentasApi.Mapper;

public static class ClienteMapper
{
    public static ClienteDTO ToDTO(this Cliente cliente)
    {
        return new ClienteDTO
        {
          ID = cliente.ID,
          Nombre = cliente.Nombre,
          Apellido = cliente.Apellido,
          Cedula = cliente.Cedula,
          Telefono = cliente.Telefono,
          Email = cliente.Email,
          Direccion = cliente.Direccion  
        };
    }

    public static Cliente ToEntity(this CrearClienteDTO crearClienteDTO)
    {
        return new Cliente
        {
            Nombre = crearClienteDTO.Nombre,
            Apellido = crearClienteDTO.Apellido,
            Cedula = crearClienteDTO.Cedula,
            Telefono = crearClienteDTO.Telefono,
            Email = crearClienteDTO.Email,
            Direccion = crearClienteDTO.Direccion
        };
    }

    public static void UpdateEntity(this Cliente cliente, UpdateClienteDTO updateClienteDTO)
    {
        cliente.Nombre = updateClienteDTO.Nombre;
        cliente.Apellido = updateClienteDTO.Apellido;
        cliente.Cedula = updateClienteDTO.Cedula;
        cliente.Telefono = updateClienteDTO.Telefono;
        cliente.Email = updateClienteDTO.Email;
        cliente.Direccion = updateClienteDTO.Direccion;
    }

    public static List<ClienteDTO> ToDTOListLINQ(this ICollection<Cliente> clientes)
    {
        return clientes.Select(c => c.ToDTO()).ToList();
    }
}
