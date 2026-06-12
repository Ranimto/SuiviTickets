using Application.DTOs;
using Application.Interfaces.IRepository;
using Mapster;
using MediatR;

namespace Application.Features.Categories.Commands.UpdateCategory
{
    /// <summary>
    /// Handler de modification d'une catégorie.
    /// </summary>
    public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, CategoryDto>
    {
        private readonly ICategoryRepository _categoryRepository;

        public UpdateCategoryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<CategoryDto> Handle( UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));


            var category = await _categoryRepository.GetByIdAsync(request.categoryId, cancellationToken);

            if (category is null)
                throw new Exception("Category not found");

            category.Name = request.Name;
            category.Description = request.Description;

            category.UpdatedAt = DateTime.UtcNow;

            var result = await _categoryRepository.UpdateAsync(
                category,
                cancellationToken);

            return result.Adapt<CategoryDto>();
        }
    }
}