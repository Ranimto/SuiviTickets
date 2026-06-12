using Application.DTOs;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Categories.Commands.CreateCategory
{
    public record CreateCategoryCommand
        (
         string Name,
         string Description
        ) : IRequest<CategoryDto>;
}
