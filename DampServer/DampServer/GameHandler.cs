#region

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Security.Cryptography;
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
        private readonly string _filefolder;
        private const string GamesFolder = "Games/";
        private const string GamePreFix = "TmpGames/";
        private readonly string _filename;
        private readonly string _filepath;
        private readonly ZipEntry _manifest;
        private ZipInputStream _zipStream;

        public GameHandler(string file_)
        {
            // add prefix
            _filepath = GamePreFix + file_;
            _filename = Path.GetFileName(file_);
            
            // path is the exactated folder, filename minus .zip
            if (_filename != null) _filefolder = GamePreFix + _filename.Substring(0, _filename.Length - 4) + "/";

            try
            {
                Extract(_filepath);
            }
            catch (FileNotFoundException e)
            {
                Logger.Log(e.Message);
                return;
            }

            var s = _files.Find(zipEntry => zipEntry.Name.IndexOf("manifest.xml", StringComparison.Ordinal)>0);
          
            if (s == null)
            {
                Console.WriteLine("Can't find manifest file");
                return;
            }

            _manifest = s;
            ParseManifest(s);
     
            

            try
            {
                if (File.Exists(GamesFolder))
                {
                    File.Move(_filepath, GamesFolder + _filename);
                    
                }
                else
                {
                    Directory.CreateDirectory(GamesFolder);
                    File.Move(_filepath, GamesFolder + _filename);

                }

                _filepath = GamesFolder + _filename;
            }
            catch (OperationAbortedException e)
            {
                
                Console.WriteLine("Exception GameHandler 1: {0}", e.Message);
                return;
            }

            try
            {
                Directory.Delete(_filefolder, true);
            }
            catch (Exception e)
            {
                Console.WriteLine("GameHandler Exception 3: {0}",e.Message);
            }


            InsertGameIntoDb(Game);
        }

      

        private void InsertGameIntoDb(Game game)
        {
            var db = new Database();
            db.Open();

            var sqlCmd = db.GetCommand();
            sqlCmd.CommandText = "INSERT INTO Games (title, description, path,picture,genre,recommendedage,developer)" +
                                 "VALUES (@title, @description,@path,@picture,@genre,@recommendedage,@developer)";
            sqlCmd.Parameters.Add("@title", SqlDbType.NVarChar).Value = game.Title;
            sqlCmd.Parameters.Add("@description", SqlDbType.Text).Value = game.Description;
            sqlCmd.Parameters.Add("@path", SqlDbType.NVarChar).Value = _filepath;
            sqlCmd.Parameters.Add("@picture", SqlDbType.NVarChar).Value = game.Picture;
            sqlCmd.Parameters.Add("@genre", SqlDbType.NVarChar).Value = game.Genre;
            sqlCmd.Parameters.Add("@recommendedage", SqlDbType.Int).Value = game.RecommendedAge;
            sqlCmd.Parameters.Add("@developer", SqlDbType.NVarChar).Value = game.Developer;
            sqlCmd.ExecuteNonQuery();

            db.Close();
        }

        private void ParseManifest(ZipEntry entry)
        {
            var xDoc = new XmlDocument();
            xDoc.Load(GamePreFix+entry.Name);
            var element = xDoc.DocumentElement;

            if (element != null)
                foreach (XmlElement e in element)
                {
                    Console.WriteLine(e.Name);
                    if (e.Name.Equals("Game"))
                        Game = ParseGame(e);

                    if (e.Name.Equals("Files"))
                    {
                        if (!ParseFiles(e))
                        {
                            throw new InvalidFileHashException();
                        }
                    }
                }
        }

        private bool ParseFiles(XmlElement xmlElement)
        {
            foreach (XmlElement element in xmlElement)
            {
                using (var cryptoProvider = new SHA1CryptoServiceProvider())
                {
                    string hash = BitConverter
                            .ToString(cryptoProvider.ComputeHash(File.OpenRead(_filefolder+element.InnerText)));
                   // Console.WriteLine(hash.Replace("-", ""));

                    if (!hash.Replace("-", "").Equals(element.GetAttribute("hash").ToUpper()))
                    {
                        Console.WriteLine("HASH FAILEDDDDDDD!!!!");
                        return false;
                    }

                    Console.WriteLine("HASH DIIIIDDD  NOOOOT FAILEDDDDDDD!!!!");
                }
            }

            return true;
        }

        private Game ParseGame(XmlElement element)
        {
            var serializer = new XmlSerializer(typeof(Game));
            var g = (Game)serializer.Deserialize(new StringReader(element.OuterXml));
            g.Path = _filefolder;
            Console.WriteLine(g);
            return g;

        }

        private void Extract(string file)
        {
            

            var fileHandler = File.OpenRead(file);
            Directory.SetCurrentDirectory(GamePreFix);
            using (_zipStream = new ZipInputStream(fileHandler))
            {
                ZipEntry theEntry;
                while ((theEntry = _zipStream.GetNextEntry()) != null)
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
                                int size = _zipStream.Read(data, 0, data.Length);
                                if (size > 0)
                                {
                                    streamWriter.Write(data, 0, size);
                                }
                                else
                                {
                                    break;
                                }
                            }
                            streamWriter.Close();
                        }
                    }
                }
            }

            fileHandler.Close();
            Directory.SetCurrentDirectory("../");

        }
    }
}