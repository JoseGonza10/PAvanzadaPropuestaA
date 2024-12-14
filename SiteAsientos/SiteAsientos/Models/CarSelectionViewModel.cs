namespace SiteAsientos.Models
{
    public class CarSelectionViewModel
    {
        public string SelectedBrand { get; set; }
        public string SelectedModel { get; set; }

        public List<string> Brands { get; set; } = new List<string>();
        public List<string> Models { get; set; } = new List<string>();
    }
}
