/// <summary>
/// Model ["The content creator"]
/// The model class holds the data. The model can be referred to as the data file
/// for the front-end of the application
/// </summary>
namespace DesignDashboard.Models
{
    /// <summary>
    /// Main Menu Items
    /// </summary>
    public class MenuItems
    {
        public string MenuName { get; set; }
        public string MenuImage { get; set; }

    }

    /// <summary>
    /// Home Page Items
    /// </summary>
    public class HomeItems
    {
        public string HomeName { get; set; }
        public string HomeImage { get; set; }
    }

    /// <summary>
    /// PC Page Items
    /// </summary>
    public class PCItems
    {
        public string PCName { get; set; }
        public string PCImage { get; set; }
    }

    /// <summary>
    /// Desktop Page Items
    /// </summary>
    public class DesktopItems
    {
        public string DesktopName { get; set; }
        public string DesktopImage { get; set; }
    }

    /// <summary>
    /// Document Page Items
    /// </summary>
    public class DocumentItems
    {
        public string DocumentName { get; set; }
        public string DocumentImage { get; set; }
    }

    /// <summary>
    /// Download Page Items
    /// </summary>
    public class DownloadItems
    {
        public string DownloadName { get; set; }
        public string DownloadImage { get; set; }
    }

    /// <summary>
    /// Picture Page Items
    /// </summary>
    public class PictureItems
    {
        public string PictureName { get; set; }
        public string PictureImage { get; set; }
    }

    /// <summary>
    /// Music Page Items
    /// </summary>
    public class MusicItems 
    {
        public string MusicName { get; set; }
        public string MusicImage { get; set; }
    }

    /// <summary>
    /// Movie Page Items
    /// </summary>
    public class MovieItems
    {
        public string MovieName { get; set; }
        public string MovieImage { get; set; }
    }

    /// <summary>
    /// Trash Page Items
    /// </summary>
    public class TrashItems
    {
        public string TrashName { get; set; }
        public string TrashImage { get; set; }
    }
}
