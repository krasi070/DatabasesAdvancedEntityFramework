namespace PlanetHunters.Export
{
    using Data.DbManipulation;
    using System.Linq;
    using System.Xml.Linq;

    public static class XmlExport
    {
        public const string path = "../../exported-data/stars.xml";

        public static void ExportStars()
        {
            var stars = StarStore.GetStars();
            XDocument starsXml = new XDocument();
            XElement starsElement = new XElement("Stars");
            foreach (var star in stars)
            {
                var newStar = new XElement("Star");
                newStar.Add(new XElement("Name", star.Name));
                newStar.Add(new XElement("Temperature", star.Temperature));
                newStar.Add(new XElement("StarSystem", star.HostStarSystem.Name));
                var discoveryInfo = new XElement("DiscoveryInfo");
                var discovery = star.DiscoveriesIncludedIn
                    .OrderBy(s => s.DateMade)
                    .FirstOrDefault();
                discoveryInfo.Add(new XAttribute("DiscoveryDate", discovery.DateMade.ToString()));
                discoveryInfo.Add(new XAttribute("TelescopeName", discovery.TelescopeUsed.Name));
                newStar.Add(discoveryInfo);
                var astronomers = new XElement("Astronomers");
            }

            starsXml.Add(starsElement);
        }
    }
}