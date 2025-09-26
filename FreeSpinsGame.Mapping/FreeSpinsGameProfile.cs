using AutoMapper;
using FreeSpinsGame.Data.Models;
using FreeSpinsGame.WebApi.DtoModels;

namespace FreeSpinsGame.Mapping
{
    public class FreeSpinsGameProfile : Profile 
    {
        public FreeSpinsGameProfile()
        {
            // Source => Target, From => To
            this.CreateMap<RegisterPlayerDto, Player>();
            this.CreateMap<Player, NewPlayerDto>();
        }
    }
}
