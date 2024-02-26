

using MediatR;
using Microsoft.EntityFrameworkCore;
using productService.Context;

public class GetProductByIdQuery : IRequest<Product>
{
    public int Id { get; set; }
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Product>
    {
        private readonly AppDbContext _context;
        public GetProductByIdQueryHandler(AppDbContext context) {
            _context = context;
        }
        public async Task<Product> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            var product = await _context.Products.Where(a => a.Id == query.Id).FirstOrDefaultAsync();
            if(product == null) return null;
            return product;
        }

    }
}