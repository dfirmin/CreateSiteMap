using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace CreateSiteMap
{
    [XmlRoot("urlset", Namespace = "http://ww.sitemaps.org/schemas/sitemap/0.9",
      IsNullable = false)]
    public class SiteMapModel
    {
        [XmlElement("url")]
        public List<SiteMap> SiteMaps { get; set; }
    }

    public class SiteMap
    {
        [XmlElement("loc")]
        public string Location { get; set; }
        [XmlElement("lastmod")]
        public DateTime LastModified { get; set; }

        [XmlElement("changefreq")]
        public Frequency ChangeFrequency { get; set; }
        [XmlElement("priority")]
        public double Priority { get; set; } = 0.5;
    }
    public enum Frequency
    {
        always,
        hourly,
        daily,
        weekly,
        monthly,
        yearly,
        never
    }
}
