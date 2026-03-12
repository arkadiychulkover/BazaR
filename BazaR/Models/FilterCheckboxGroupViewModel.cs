namespace BazaR.Models.ViewModels
{
    public class FilterCheckboxGroupViewModel
    {
        public string Key { get; set; }
        public string DisplayName { get; set; }
        public List<string> Values { get; set; } = new List<string>();
        public List<string> SelectedValues { get; set; } = new List<string>();
    }
}