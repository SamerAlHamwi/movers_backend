using static Mofleet.Enums.Enum;

namespace Mofleet.Advertisiments.Dto
{
    public class CreateAdvertisimentPositionDto
    {
        public PositionForAdvertisiment Position { get; set; }
        public Screen Screen { get; set; }
    }
}
