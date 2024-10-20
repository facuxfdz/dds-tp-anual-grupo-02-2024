﻿using AccesoAlimentario.Core.DAL;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AccesoAlimentario.Operations.Heladeras;

public static class BajaHeladera
{
    public class BajaHeladeraCommand : IRequest<IResult>
    {
        public Guid Id { get; set; } = Guid.Empty;
    }
    
    public class Handler : IRequestHandler<BajaHeladeraCommand, IResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Handler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IResult> Handle(BajaHeladeraCommand request, CancellationToken cancellationToken)
        {
            var heladera = await _unitOfWork.HeladeraRepository.GetByIdAsync(request.Id);
            if (heladera == null)
            {
                return Results.NotFound("La heladera no existe");
            }

            await _unitOfWork.HeladeraRepository.RemoveAsync(heladera);
            await _unitOfWork.SaveChangesAsync();

            return Results.Ok();
        }
    }
}