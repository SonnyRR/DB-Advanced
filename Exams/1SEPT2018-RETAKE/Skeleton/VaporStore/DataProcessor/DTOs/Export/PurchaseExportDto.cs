namespace VaporStore.DataProcessor.DTOs.Export
{
    using System;
    using System.Xml.Serialization;

    [XmlType("Purchase")]
    public class PurchaseExportDto
    {
        [XmlElement("Card")]
        public string Card { get; set; }

        [XmlElement("Cvc")]
        public string Cvc { get; set; }

        [XmlElement("Date")]
        public string Date { get; set; }

        public GameExportDtoXML Game { get; set; }
    }
}
