namespace VaporStore.DataProcessor.DTOs.Export
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlType("User")]
    public class UserExportDto
    {
        [XmlAttribute("username")]
        public string Username { get; set; }

        [XmlArray("Purchases")]
        public List<PurchaseDto> Purchases { get; set; } = new List<PurchaseDto>();
    }
}
