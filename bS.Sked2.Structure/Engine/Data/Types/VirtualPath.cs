using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace bS.Sked2.Structure.Engine.Data.Types
{
    /// <summary>
    /// Virtual path (file or folder) rapresentation.
    /// </summary>
    /// <seealso cref="bS.Sked2.Structure.Base.FileSystem.IVirtualPath" />
    public class VirtualPath : Base.FileSystem.IVirtualPath, IXmlSerializable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VirtualPath"/> class.
        /// </summary>
        public VirtualPath()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VirtualPath"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        public VirtualPath(string path)
        {
            Path = path;
        }

        /// <summary>
        /// Gets the path.
        /// </summary>
        /// <value>
        /// The path.
        /// </value>
        public string Path { get; internal set; }

        /// <summary>
        /// Gets the real path.
        /// </summary>
        /// <value>
        /// The real path.
        /// </value>
        public string RealPath => Path;

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            reader.MoveToContent();
            var isEmptyElement = reader.IsEmptyElement; // (1)
            reader.ReadStartElement();
            if (!isEmptyElement) // (1)
            {
                Path = reader.ReadElementString("Path");
                reader.ReadEndElement();
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteElementString("Path", Path);
        }
    }
}