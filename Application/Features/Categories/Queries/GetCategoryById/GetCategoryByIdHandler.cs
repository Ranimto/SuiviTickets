using Application.DTOs;
using Application.Interfaces.IRepository;

using Mapster;
using MediatR;

namespace Application.Features.Category.Queries.GetCategoryById
{
    /// <summary>
    /// Handler de lecture d'un Category.
    /// </summary>
    public class GetCategoryByIdHandler : IRequestHandler<GetCategoryByIdQuery, CategoryDto>
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetCategoryByIdHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<CategoryDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {

            if (request is null)
                throw new ArgumentNullException(nameof(request));

            var category = await _categoryRepository.GetByIdAsync(request.Id);

            if (category is null)
                throw new Exception("category not found");

            return category.Adapt<CategoryDto>();
        }
    }
}