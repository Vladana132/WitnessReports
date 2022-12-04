namespace WitnessReports.Model.Entites
{
    public class Fugitive : Person
    {
        public string Link { get; set; }

        public Fugitive(string name, string link) : base(name)
        {
            Link = link;
        }
    }
}
