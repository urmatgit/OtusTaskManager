using AutoMapper;
using BoardService.Domain.Entity;
using Services.Contract.BorderColumn;

namespace Services.Implementation.Mapping
{
    public class BoardColumnMappingProfile : Profile
    {
        public BoardColumnMappingProfile() 
        { 
            CreateMap<BoardColumn, BoardColumnDto>();
        }
    }
}
