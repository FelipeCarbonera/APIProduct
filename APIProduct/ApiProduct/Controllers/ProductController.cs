using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiProduct.DataContext;
using ApiProduct.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiProduct.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private DataBaseConnection _db = new DataBaseConnection();

        /// <summary>
        /// Retorna todos os Produtos do DB
        /// </summary>
        /// <remarks>
        ///     Exemplo de Retorno:
        ///     [
        ///         {
        ///             "id": 1,
        ///             "name": "PRODUTO 1",
        ///             "description": "DESCRIÇÃO DO PRODUTO 1",
        ///             "price": 6,67
        ///             "iD_Category": 1,
        ///             "urlImage": "~/imageItem.jpg",
        ///             "date": "2020-09-01T00:00:00"
        ///         },
        ///         {
        ///             "id": 2,
        ///             "name": "PRODUTO 2",
        ///             "description": "DESCRIÇÃO DO PRODUTO 2",
        ///             "price": 7,61
        ///             "iD_Category": 2,
        ///             "urlImage": "~/image2.jpg",
        ///             "date": "2020-09-01T00:00:00"
        ///         }
        ///     ]
        /// </remarks>
        /// <returns>Uma lista de Produtos</returns>
        /// <param>Sem parâmetro</param>
        [HttpGet]
        public ActionResult<List<Product>> Get()
        {
            return _db.GetAllProducts();
        }

        /// <summary>
        /// Retorna um Produto específico por Id
        /// </summary>
        /// <remarks>
        ///     Exemplo de Retorno:
        ///         {
        ///             "id": 1,
        ///             "name": "PRODUTO 1",
        ///             "description": "DESCRIÇÃO DO PRODUTO 1",
        ///             "price": 6,67
        ///             "iD_Category": 1,
        ///             "urlImage": "~/imageItem.jpg",
        ///             "date": "2020-09-01T00:00:00"
        ///         },
        /// </remarks>
        /// <returns>Um objeto de Produto</returns>
        /// <param name="id">O Id do Produto que está buscando</param>
        [HttpGet("{id}")]
        public ActionResult<Product> Get(long id)
        {
            return _db.GetProductById(id);
        }

        /// <summary>
        /// Inserir um novo Produto
        /// </summary>
        /// <remarks>
        ///     Exemplo de Retorno:
        ///         true
        /// </remarks>
        /// <returns>Um booleano</returns>
        /// <param name="prod">O objeto de Produto para inserção</param>
        [HttpPost]
        public bool Post([FromBody] Product prod)
        {
            return _db.InsertProduct(prod);
        }

        /// <summary>
        /// Atualizar um Produto
        /// </summary>
        /// <remarks>
        ///     Exemplo de Retorno:
        ///         true
        /// </remarks>
        /// <returns>Um booleano</returns>
        /// <param name="prod">O objeto de Produto para atualizar</param>
        [HttpPut]
        public bool Put([FromBody] Product prod)
        {
            return _db.UpdateProduct(prod);
        }

        /// <summary>
        /// Deleta um Produto específico por Id
        /// </summary>
        /// <remarks>
        ///     Exemplo de Retorno:
        ///         true
        /// </remarks>
        /// <returns>Um booleano</returns>
        /// <param name="id">O Id do Produto para exclusão</param>
        [HttpDelete("{id}")]
        public bool Delete(long id)
        {
            return _db.DeletProductById(id);
        }
    }
}
