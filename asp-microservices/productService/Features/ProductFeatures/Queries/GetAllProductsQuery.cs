using MediatR;
using Microsoft.EntityFrameworkCore;
using productService.Context;

public class GetAllProductsQuery : IRequest<IEnumerable<Product>>
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<Product>>
    {
        private readonly AppDbContext _context;
        public GetAllProductsQueryHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> Handle(GetAllProductsQuery query, CancellationToken cancellationToken)
        {
            var products = await _context.Products.ToListAsync();
            if(products == null)
            {
                return null;
            }
            return products.AsReadOnly();
        }
    }
}