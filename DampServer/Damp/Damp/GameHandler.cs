#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using ICSharpCode.SharpZipLib.Zip;

#endregion

namespace Damp
{
    public class GameHandler
    {
        public Game Game { get; private set; }
        private readonly List<ZipEntry> _files = new List<ZipEntry>();

        public GameHandler(string file)
        {
            Extract(file);

            var s = _files.Find(zipEntry => zipEntry.Name.IndexOf("manifest.xml", StringComparison.Ordinal)>0);
          
            if (s == null)
            {
                Console.WriteLine("Can't find manifest file");
                return;
            }

            ParseManifest(s);
        }

        private void ParseManifest(ZipEntry entry)
        {
            var xDoc = new XmlDocument();
            xDoc.Load(entry.Name);
            var element = xDoc.DocumentElement;

            if (element != null)
                foreach (XmlElement e in element)
                {
                    Console.WriteLine(e.Name);
                    if (e.Name.Equals("Game"))
                        Game = ParseGame(e);

                    if (e.Name.Equals("Files"))
                        ParseFiles(e);
                }
        }

        private void ParseFiles(XmlElement xmlElement)
        {
            throw new NotImplementedException();
        }

        private Game ParseGame(XmlElement element)
        {
            var serializer = new XmlSerializer(typeof(Game));
            var g = (Game)serializer.Deserialize(new StringReader(element.OuterXml));
            Console.WriteLine(g);
            return g;

        }

        private void Extract(string file)
        {
            using (var s = new ZipInputStream(File.OpenRead(file)))
            {
                ZipEntry theEntry;
                while ((theEntry = s.GetNextEntry()) != null)
                {
                    Console.WriteLine(theEntry.Name);
                    _files.Add(theEntry);

                    string directoryName = Path.GetDirectoryName(theEntry.Name);
                    string fileName = Path.GetFileName(theEntry.Name);

                    // create directory
                    if (!string.IsNullOrEmpty(directoryName))
                    {
                        Directory.CreateDirectory(directoryName);
                    }

                    if (fileName != String.Empty)
                    {
                        using (FileStream streamWriter = File.Create(theEntry.Name))
                        {
                            var data = new byte[2048];
                            while (true)
                            {
                                int size = s.Read(data, 0, data.Length);
                                if (size > 0)
                                {
                                    streamWriter.Write(data, 0, size);
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}