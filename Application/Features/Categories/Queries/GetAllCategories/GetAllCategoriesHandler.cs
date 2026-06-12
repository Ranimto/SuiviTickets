using Application.Common.Models;
using Application.DTOs;
using Application.Interfaces.IRepository;
using Domain.Entities;
using Mapster;
using MediatR;

namespace Application.Features.Categories.Queries.GetAllCategories
{
    /// <summary>
    /// Handler de récupération des tickets avec filtres.
    /// </summary>
    public class GetAllCategoriesHandler : IRequestHandler<GetAllCategoriesQuery, PagedResult<CategoryDto>>
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetAllCategoriesHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<PagedResult<CategoryDto>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
          if (request is null) throw new ArgumentNullException(nameof(request));

          var query = await _categoryRepository.GetAllAsync(cancellationToken);

            //Filtering 
            // Pagination 

            int totalCount =query.Count;    
     
            var items= query.Skip((request.PageNumber -1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            var result = items.Adapt<List<CategoryDto>>();

            return new PagedResult<CategoryDto>
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalCount = totalCount,
                Items = result
            };

        }
    }
}