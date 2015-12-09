using System.Configuration;

namespace SubDags
{
    public class GraphSection : ConfigurationSection
    {
        [ConfigurationProperty("graphsection", Options = ConfigurationPropertyOptions.IsRequired)]
        public GraphElement FilePath
        {
            get { return (GraphElement)this["graphfilepath"]; }
        }
    }

    public class GraphElement : ConfigurationElement
    {
        [ConfigurationProperty("graphfilelocation", IsRequired = true, IsKey = true)]
        public string FilePath
        {
            get { return (string)this["graphfilelocation"]; }
        }
    }
}
