using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Models.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Models.Queries.GetListModel
{
    public class GetListModelQuery : IRequest<ModelListModel>
    {
        public PageRequest PageRequest { get; set; }

        public class GetListQueryHandler : IRequestHandler<GetListModelQuery, ModelListModel>
        {
            private readonly IMapper _mapper;
            private readonly IModelRepository _modelRepository;

            public GetListQueryHandler(IMapper mapper, IModelRepository modelRepository)
            {
                _mapper = mapper;
                _modelRepository = modelRepository;
            }


            public async Task<ModelListModel> Handle(GetListModelQuery request, CancellationToken cancellationToken)
            {   
                            //car models
               IPaginate<Model> models = await _modelRepository.GetListAsync(include: 
                                                    m=>m.Include(c=>c.Brand),
                                                    index: request.PageRequest.Page,
                                                    size:request.PageRequest.PageSize);
                            //dataModel
               ModelListModel mappedModels = _mapper.Map<ModelListModel>(models);
               return mappedModels;
            }
        }
    }
}
