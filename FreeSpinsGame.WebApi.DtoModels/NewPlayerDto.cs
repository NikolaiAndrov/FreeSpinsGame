namespace FreeSpinsGame.WebApi.DtoModels
{
    public class NewPlayerDto : PlayerBaseDto
    {
        public string Token { get; set; } = null!;
    }
}
