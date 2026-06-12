using Application.DTOs;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Categories.Commands.UpdateCategory
{
    public record UpdateCategoryCommand
        (
        Guid categoryId,
         string Name,
         string Description
        ) : IRequest<CategoryDto>;
}
