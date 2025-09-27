using System.ComponentModel.DataAnnotations;

namespace FreeSpinsGame.WebApi.DtoModels.Spin
{
    public class SpinStatusDto
    {
        [Display(Name = "Current spin usage")]
        public int CurrentSpinUsage { get; set; }

        [Display(Name = "Max allowed spins")]
        public int MaxAllowedSpins { get; set; }
    }
}
