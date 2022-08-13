namespace ProgrammingTestMacropayCarlosMoo.Models
{
    public class Contacts
    {
        public string id { get; set; }
        public List<string> addressLines { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
    }

    public class Address
    {
        public int id { get; set; }
    }
}
