using Application.DTOs;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Category.Queries.GetCategoryById
{
    /// <summary>
    /// GetCategoryByIdQuery
    /// </summary>
    /// <param name="Id"></param>
    public record GetCategoryByIdQuery(Guid Id) : IRequest<CategoryDto>;
}
