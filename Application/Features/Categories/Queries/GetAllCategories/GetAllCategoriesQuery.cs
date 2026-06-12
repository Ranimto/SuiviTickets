using Application.Common.Models;
using Application.DTOs;
using MediatR;

namespace Application.Features.Categories.Queries.GetAllCategories
{
    /// <summary>
    /// Récupération des tickets avec pagination et filtres.
    /// </summary>
    public record GetAllCategoriesQuery(
        int PageNumber = 1,
        int PageSize = 10
        ) : IRequest<PagedResult<CategoryDto>>;
}