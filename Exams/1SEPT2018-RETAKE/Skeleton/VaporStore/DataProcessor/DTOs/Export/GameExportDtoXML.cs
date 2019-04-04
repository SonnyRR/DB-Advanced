namespace VaporStore.DataProcessor.DTOs.Export
{
    using System.Xml.Serialization;

    [XmlType("Game")]
    public class GameExportDtoXML
    {
        [XmlAttribute("title")]
        public string Title { get; set; }

        [XmlElement("Genre")]
        public string Genre { get; set; }

        [XmlElement("Price")]
        public string Price { get; set; }
    }
}
