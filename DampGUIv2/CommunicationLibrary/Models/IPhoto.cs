using System.Windows.Media.Imaging;

namespace DampGUI
{
    public interface IPhoto
    {
        BitmapImage Image { get; set; }
        string Url { get; set; }
        bool IsMade { get; set; }
        void LoadPicture();
        void Create();
    }
}
