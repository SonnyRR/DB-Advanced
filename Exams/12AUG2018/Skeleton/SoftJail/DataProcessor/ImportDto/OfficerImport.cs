namespace SoftJail.DataProcessor.ImportDto
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlType("Officer")]
    public class OfficerImport
    {

        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("Money")]
        public decimal Money { get; set; }

        [XmlElement("Position")]
        public string Position { get; set; }

        [XmlElement("Weapon")]
        public string Weapon { get; set; }

        [XmlElement("DepartmentId")]
        public int DepartmentId { get; set; }

        [XmlArray("Prisoners")]
        public List<PrisonerOfficerImportDto> Prisoners { get; set; }
    }
}
