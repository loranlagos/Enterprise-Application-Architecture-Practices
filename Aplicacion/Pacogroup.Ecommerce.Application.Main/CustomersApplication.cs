using System.Runtime.CompilerServices;
using AutoMapper;
using Pacogroup.Ecommerce.Application.DTO;
using Pacogroup.Ecommerce.Application.Interfaces;
using Pacogroup.Ecommerce.Domain.Entity;
using Pacogroup.Ecommerce.Domain.Interfaces;
using Pacogroup.Ecommerce.Transversal.Common;

namespace Pacogroup.Ecommerce.Application.Main;

public class CustomersApplication : ICostumersApplication
{
    private readonly ICostumersDomain _costumersDomain;
    private readonly IMapper _mapper;

    public CustomersApplication(IMapper mapper, ICostumersDomain costumersDomain)
    {
        _costumersDomain = costumersDomain;
        _mapper = mapper;
    }

    public async Task<Response<bool>> InsertAsync(CustomerDTO customerDTO)
    {
        var response = new Response<bool>();

        try
        {
            var customer = _mapper.Map<Costumer>(customerDTO);
            response.Data = await _costumersDomain.InsertAsync(customer);

            if (response.Data)
            {
                response.IsSucces = true;
                response.Message = "Registro insertado exitosamente.";
            }
        }
        catch (System.Exception ex)
        {
            response.IsSucces = false;
            response.Message = $"Error al procesar la inserción: {ex.Message}";
        }

        return response;
    }

    public async Task<Response<bool>> DeleteAsync(string customerId)
    {
        var response = new Response<bool>();

        try
        {
            response.Data = await _costumersDomain.DeleteAsync(customerId);

            if (response.Data)
            {
                response.IsSucces = true;
                response.Message = "Registro eliminado exitosamente.";
            }
            else
            {
                response.IsSucces = true;
                response.Message = $"El cliente {customerId} no existe.";
            }
        }
        catch (System.Exception ex)
        {
            response.IsSucces = false;
            response.Message = $"Error al procesar la eliminación: {ex.Message}";
        }

        return response;
    }

    public async Task<Response<IEnumerable<CustomerDTO>>> GetAllAsync()
    {
        var response = new Response<IEnumerable<CustomerDTO>>();

        try
        {
            var costumers = await _costumersDomain.GetAllAsync();
            response.Data = _mapper.Map<IEnumerable<CustomerDTO>>(costumers);

            if (response.Data != null)
            {
                response.IsSucces = true;
                response.Message = "Lista obtenida exitosamente.";
            }
        }
        catch (System.Exception ex)
        {
            response.IsSucces = false;
            response.Message = $"Error al obtener la lista de registros: {ex.Message}.";
        }

        return response;
    }

    public async Task<Response<CustomerDTO>> GetAsync(string customerId)
    {
        var response = new Response<CustomerDTO>();

        try
        {
            var costumer = await _costumersDomain.GetAsync(customerId);
            response.Data = _mapper.Map<CustomerDTO>(costumer);

            if (response.Data != null)
            {
                response.IsSucces = true;
                response.Message = "Registro obtenido exitosamente.";
            }
            else
            {
                response.IsSucces = true;
                response.Message = $"El cliente {customerId} no existe.";
            }

        }
        catch (System.Exception ex)
        {
            response.IsSucces = false;
            response.Message = $"Error al obtener el registro: {ex.Message}";
        }

        return response;
    }

    public async Task<Response<bool>> UpdateAsync(CustomerDTO customerDTO)
    {
        var response = new Response<bool>();

        try
        {
            var customer = _mapper.Map<Costumer>(customerDTO);
            response.Data = await _costumersDomain.UpdateAsync(customer);

            if (response.Data)
            {
                response.IsSucces = true;
                response.Message = "Registro actualizado exitosamente.";
            }
            else
            {
                response.IsSucces = true;
                response.Message = $"El cliente {customerDTO.CustomerId} no existe.";
            }
        }
        catch (System.Exception ex)
        {
            response.IsSucces = false;
            response.Message = $"Error al procesar la actualización: {ex.Message}";
        }

        return response;
    }
}
