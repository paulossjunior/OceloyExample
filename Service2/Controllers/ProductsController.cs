using Microsoft.AspNetCore.Mvc;
using Service2.Model;
using Service2.Repositories;

namespace Service2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController:  ControllerBase
{
    private readonly IProductRepository _repository;

    public ProductsController(IProductRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Product>> GetProducts()
    {
        return Ok(_repository.GetAll());
    }

    [HttpGet("{id}")]
    public ActionResult<Product> GetProduct(int id)
    {
        var product = _repository.GetById(id);
        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }

    [HttpPost]
    public ActionResult<Product> CreateProduct(Product product)
    {
        _repository.Add(product);
        return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateProduct(int id, Product product)
    {
        if (id != product.Id)
        {
            return BadRequest();
        }

        var existingProduct = _repository.GetById(id);
        if (existingProduct == null)
        {
            return NotFound();
        }

        _repository.Update(product);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteProduct(int id)
    {
        var product = _repository.GetById(id);
        if (product == null)
        {
            return NotFound();
        }

        _repository.Delete(id);
        return NoContent();
    }
}
