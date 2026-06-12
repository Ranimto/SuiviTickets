using Application.DTOs;
using Domain.Entities;
using MediatR;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Attachements.Commands.CreateAttachement
{
    public record CreateAttachementCommand
        (
        Guid Id,
        Guid TicketId,
        string FileType,
        string FileUrl
        ) :IRequest<AttachmentDto>;
}
