namespace WitnessReports.Model.Entites
{
    public class Witness : Person
    {
        public string Country { get; set; }
        public string PhoneNumber { get; set; }

        public Witness(string name, string country, string phoneNumber) : base(name)
        {
            Country = country;
            PhoneNumber = phoneNumber;
        }
    }
}
