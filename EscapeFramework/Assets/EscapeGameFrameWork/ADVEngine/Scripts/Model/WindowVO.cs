
namespace Qitz.ADVGame
{
    public class WindowVO : IWindowVO
    {
        public string WindowText { get; private set; }
        public ICaracterVO WindowNaviCaracterVO { get; set; }
        public string WindowCharacterName { get; private set; }
        public WindowVO()
        {
            WindowText = "";
            WindowCharacterName = "";
        }
        public void SetWindowCharacterName(string windowCharacterName)
        {
            WindowCharacterName = windowCharacterName;
        }
        public void SetWindowText(string windowText)
        {
            WindowText = windowText;
        }
    }
}
