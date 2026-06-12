using Application.DTOs;
using Application.Interfaces.IRepository;
using Mapster;
using MediatR;

namespace Application.Features.Categories.Commands.CreateCategory
{
    /// <summary>
    /// Handler de création d'une catégorie.
    /// </summary>
    public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, CategoryDto>
    {
        private readonly ICategoryRepository _categoryRepository;

        public CreateCategoryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<CategoryDto> Handle(
            CreateCategoryCommand request,
            CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            var category = new Category
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                CreatedAt = DateTime.UtcNow
            };

            var result = await _categoryRepository.AddAsync(category, cancellationToken);

            return result.Adapt<CategoryDto>();
        }
    }
}