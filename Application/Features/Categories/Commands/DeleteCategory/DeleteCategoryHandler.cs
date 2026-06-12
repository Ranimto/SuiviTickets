using Application.Interfaces.IRepository;
using Domain.Entities;
using MediatR;

namespace Application.Features.Categories.Commands.DeleteCategory
{
    /// <summary>
    /// Handler de suppression d'une catégorie.
    /// </summary>
    public class DeleteCategoryHandler: IRequestHandler<DeleteCategoryCommand, Guid>
    {
        private readonly ICategoryRepository _categoryRepository;

        public DeleteCategoryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Guid> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            await _categoryRepository.DeleteByIdAsync( request.categoryId, cancellationToken);

            return request.categoryId;
        }
    }
}