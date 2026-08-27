using System.Net;
using Microsoft.AspNetCore.Mvc;
using Pacogroup.Ecommerce.Application.DTO;
using Pacogroup.Ecommerce.Application.Interfaces;
using Pacogroup.Ecommerce.Domain.Entity;
using Pacogroup.Ecommerce.Transversal.Common;
using Swashbuckle.AspNetCore.Annotations;

namespace Pacogroup.Ecommerce.Services.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerTag("Operaciones relacionadas con clientes")]
    public class CustomersController : ControllerBase
    {
        private readonly ICostumersApplication _costumersApplication;

        public CustomersController(ICostumersApplication costumersApplication)
        {
            _costumersApplication = costumersApplication;
        }

        /// <summary>
        /// Endpoint que ejecuta InsertAsync de la capa de aplicacion para insertar nuevos registros a ese dominio
        /// </summary>
        /// <param name="customerDTO"></param>
        /// <returns></returns>
        [HttpPost("insert")]
        [SwaggerOperation(
            Summary = "Registra un cliente",
            Description = "Retorna un objeto generico con el resultado de la operación"
        )]
        [SwaggerResponse(200, "Cliente registrado", typeof(Reponse<bool>))]
        public async Task<IActionResult> InsertAsync([FromBody] CustomerDTO customerDTO)
        {
            if (customerDTO == null) return BadRequest();

            var response = await _costumersApplication.InsertAsync(customerDTO);

            if (response.IsSucces)
            {
                return Ok(response);
            }

            return StatusCode((int)HttpStatusCode.InternalServerError, response);
        }

        /// <summary>
        /// Endpint que ejecuta UpdateAsync de la capa de aplicacion para actualizar informacion de un registro
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="customerDTO"></param>
        /// <returns></returns>
        [HttpPut("update/{customerId}")]
        [SwaggerOperation(
            Summary = "Actualiza un cliente en función de su id",
            Description = "Retorna un objeto generico con el resultado de la operación"
        )]
        [SwaggerResponse(200, "Cliente actualizado", typeof(Reponse<bool>))]
        public async Task<IActionResult> UpdateAsync([FromRoute] string customerId, [FromBody] CustomerDTO customerDTO)
        {
            if (customerDTO == null || customerId == null) return BadRequest();

            if (!customerId.Equals(customerDTO.CustomerId)) return BadRequest();

            var response = await _costumersApplication.UpdateAsync(customerDTO);

            if (response.IsSucces)
            {
                return Ok(response);
            }

            return StatusCode((int)HttpStatusCode.InternalServerError, response);
        }

        /// <summary>
        /// Endpoint que ejecuta DeleteAsync de la capa de aplicacion para eliminar un registro
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [HttpDelete("delete/{customerId}")]
        [SwaggerOperation(
            Summary = "Elimina un cliente en funcion de su id",
            Description = "Retorna un objeto generico con el resultado de la operación"
        )]
        [SwaggerResponse(200, "Cliente eliminado", typeof(Reponse<bool>))]
        public async Task<IActionResult> DeleteAsync([FromRoute] string customerId)
        {
            if (string.IsNullOrEmpty(customerId)) return BadRequest();

            var response = await _costumersApplication.DeleteAsync(customerId);

            if (response.IsSucces) return Ok(response);

            return StatusCode((int)HttpStatusCode.InternalServerError, response);
        }

        /// <summary>
        /// Endpoint que ejecuta GetAsync de la capa de aplicacion para obtener un registro en especifico
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [HttpGet("getbyid/{customerId}")]
        [SwaggerOperation(
            Summary = "Obtiene un cliente en funcion de su id",
            Description = "Retorna un objeto generico con el resultado de la operación"
        )]
        [SwaggerResponse(200, "Cliente encontrado", typeof(Reponse<CustomerDTO>))]
        public async Task<IActionResult> GetAsync([FromRoute] string customerId)
        {
            if (string.IsNullOrEmpty(customerId)) return BadRequest();

            var response = await _costumersApplication.GetAsync(customerId);

            if (response.IsSucces) return Ok(response);

            return StatusCode((int)HttpStatusCode.InternalServerError, response);
        }

        /// <summary>
        /// Endpoint que ejecuta GetAllAsync de la capa de aplicacion para obtener una lista de registros
        /// </summary>
        /// <returns></returns>
        [HttpGet("list")]
        [SwaggerOperation(
            Summary = "Obtiene el listado de clientes completo",
            Description = "Retorna un objeto generico con el resultado de la operación"
        )]
        [SwaggerResponse(200, "Clientes encontrados", typeof(Reponse<IEnumerable<CustomerDTO>>))]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await _costumersApplication.GetAllAsync();

            if (response.IsSucces) return Ok(response);

            return StatusCode((int)HttpStatusCode.InternalServerError, response);
        }

    }
}