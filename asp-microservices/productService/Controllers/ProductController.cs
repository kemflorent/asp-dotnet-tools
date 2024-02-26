using MediatR;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private IMediator _mediator;

    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

    private readonly ILogger<ProductController> _logger;

    public ProductController(ILogger<ProductController> logger) {
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductCommand command)
    {
        _logger.LogInformation("Create new products");
        return Ok(await Mediator.Send(command));
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("Find all products");
        return Ok(await Mediator.Send(new GetAllProductsQuery()));
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        _logger.LogInformation("Get Product by id {id}", id);
        return Ok(await Mediator.Send(new GetProductByIdQuery { Id = id }));
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        _logger.LogInformation("Delete Product by id {id}", id);
        return Ok(await Mediator.Send(new DeleteProductByIdCommand { Id = id }));
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateProductCommand command)
    {
        _logger.LogInformation("Update Product by id {id}", id);
        if (id != command.Id)
        {
            return BadRequest();
        }
        return Ok(await Mediator.Send(command));
    }
}