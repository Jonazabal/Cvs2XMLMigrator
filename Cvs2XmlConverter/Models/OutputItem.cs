using System.Xml.Serialization;

namespace JonasSoftHouseConverter.Models
{
    // Theese classes are generated online from the .\xml\ExampleXml.xml and then edited with new Constructors
    // Classes represents the objects in the xml scheme and are used to build the xml output of this app. 

    [XmlRoot(ElementName = "address")]
    public class Address
    {
        public Address() { }

        public Address(string[] item) : this()
        {
            if (item.Length > 1)
                Street = item[1].ToString();
            if (item.Length > 2)
                City = item[2].ToString();
            if (item.Length > 3)
                Zip = int.Parse(item[3]);
        }

        [XmlElement(ElementName = "street")]
        public string Street { get; set; }

        [XmlElement(ElementName = "city")]
        public string City { get; set; }

        [XmlElement(ElementName = "zip")]
        public int Zip { get; set; }
    }

    [XmlRoot(ElementName = "phone")]
    public class Phone
    {
        public Phone() { }

        public Phone(string[] item) : this()
        {
            if (item.Length > 1)
                Mobile = item[1].ToString();
            if (item.Length > 2)
                Landline = item[2].ToString();
        }

        [XmlElement(ElementName = "mobile")]
        public string Mobile { get; set; }

        [XmlElement(ElementName = "landline")]
        public string Landline { get; set; }
    }

    [XmlRoot(ElementName = "family")]
    public class Family
    {
        public Family() { }

        public Family(string[] item) : this()
        {
            if (item.Length > 1)
                Name = item[1].ToString();
            if (item.Length > 2)
                Born = int.Parse(item[2]);
        }

        [XmlElement(ElementName = "name")]
        public string Name { get; set; }

        [XmlElement(ElementName = "born")]
        public int Born { get; set; }

        [XmlElement(ElementName = "address")]
        public Address Address { get; set; }

        [XmlElement(ElementName = "phone")]
        public Phone Phone { get; set; }
    }

    [XmlRoot(ElementName = "person")]
    public class Person
    {
        public Person()
        {
            Family = new List<Family>();
        }

        public Person(string[] item) : this()
        {
            if (item.Length > 1)
                Firstname = item[1].ToString();
            if (item.Length > 2)
                Lastname = item[2].ToString();
        }

        [XmlElement(ElementName = "firstname")]
        public string Firstname { get; set; }

        [XmlElement(ElementName = "lastname")]
        public string Lastname { get; set; }

        [XmlElement(ElementName = "address")]
        public Address Address { get; set; }

        [XmlElement(ElementName = "phone")]
        public Phone Phone { get; set; }

        [XmlElement(ElementName = "family")]
        public List<Family> Family { get; set; }
    }

    [XmlRoot(ElementName = "people")]
    public class People
    {
        public People()
        {
            Person = new List<Person>();
        }

        [XmlElement(ElementName = "person")]
        public List<Person> Person { get; set; }
    }
}
