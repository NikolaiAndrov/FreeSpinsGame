namespace FreeSpinsGame.WebApi.DtoModels.Player
{
    public class NewPlayerDto : PlayerBaseDto
    {
        public string Token { get; set; } = null!;
    }
}
