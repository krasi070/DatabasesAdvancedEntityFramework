namespace PlanetHunters.Import
{
    using Data.DbManipulation;
    using Data.DTOs;
    using System;
    using System.Linq;
    using System.Xml.Linq;

    public static class XmlImport
    {
        private const string starsDataPath = "../../PlanetHuntersDb-Data/stars.xml";
        private const string discoveriesDataPath = "../../PlanetHuntersDb-Data/discoveries.xml";

        public static void ImportStars()
        {
            XDocument starsXml = XDocument.Load(starsDataPath);
            var stars = starsXml.Root.Elements()
                .Select(e => new StarDto()
                {
                    Name = e.Element("Name").Value,
                    Temperature = int.Parse(e.Element("Temperature").Value),
                    StarSystem = e.Element("StarSystem").Value
                });

            StarStore.AddStars(stars);
        }

        public static void ImportDiscoveries()
        {
            XDocument discoveriesXml = XDocument.Load(discoveriesDataPath);
            var discoveries = discoveriesXml.Root.Elements()
                .Select(e => new DiscoveryDto()
                {
                    DateMade = DateTime.Parse(e.Attribute("DateMade").Value),
                    Telescope = e.Attribute("Telescope").Value,
                    Stars = e.Element("Stars").Elements()
                        .Select(s => s.Value)
                        .ToList(),
                    Planets = e.Element("Planets").Elements()
                        .Select(p => p.Value)
                        .ToList(),
                    Pioneers = e.Element("Pioneers").Elements()
                        .Select(p => new AstronomerDto()
                        {
                            FirstName = p.Value.Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries)[0],
                            LastName = p.Value.Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries)[1],
                        })
                        .ToList(),
                    Observers = e.Element("Observers").Elements()
                        .Select(o => new AstronomerDto()
                        {
                            FirstName = o.Value.Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries)[0],
                            LastName = o.Value.Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries)[1],
                        })
                        .ToList()
                });

            DiscoveryStore.AddDiscoveries(discoveries);
        }
    }
}