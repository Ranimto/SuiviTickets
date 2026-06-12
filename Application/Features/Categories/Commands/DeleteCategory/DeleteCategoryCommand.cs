using Application.DTOs;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Categories.Commands.DeleteCategory
{
    public record DeleteCategoryCommand ( Guid categoryId ) : IRequest<Guid>;
}
