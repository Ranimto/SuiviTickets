using Application.DTOs;
using Application.Interfaces.IRepository;
using Domain.Entities;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Attachements.Commands.CreateAttachement
{
    public class CreateAttachementHandler :IRequestHandler<CreateAttachementCommand,AttachmentDto>
    {
        private readonly IAttachementRepository _attachementRepository;

        public CreateAttachementHandler(IAttachementRepository attachementRepository)
        {
            _attachementRepository = attachementRepository;
        }

        public async Task<AttachmentDto> Handle(CreateAttachementCommand request, CancellationToken cancellationToken)
        {
            if (request == null) 
            { 
                throw new ArgumentNullException(nameof(request));   
            }
            var attachment = new Attachment()
            {
                TicketId = request.TicketId,
                FileType = request.FileType,
                FileUrl = request.FileUrl
            };
           await _attachementRepository.AddAsync(attachment, cancellationToken);

           return attachment.Adapt<AttachmentDto>();
        }
    }
}
