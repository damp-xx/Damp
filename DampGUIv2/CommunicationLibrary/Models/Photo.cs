using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Windows.Media.Imaging;

namespace DampGUI
{
    public class Photo : IPhoto
    {
        public BitmapImage Image { get; set; }
        private BitmapImage image = new BitmapImage();
        public string Url { get; set; }

        public bool IsMade { get; set; }

        public Photo(string _url)
        {
            IsMade = false;
            Url = _url;
            //Console.WriteLine("url:::::::" + Url);
        }

        private MemoryStream memoryStream = new MemoryStream();


        public void LoadPicture()
        {
            int BytesToRead = 100;
            InitiateSSLTrust();
            if (Url.Length != 0)
            {
                WebRequest request = WebRequest.Create(new Uri(Url, UriKind.Absolute));
                request.Timeout = -1;
                WebResponse response = request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                BinaryReader reader = new BinaryReader(responseStream);

                byte[] bytebuffer = new byte[BytesToRead];
                int bytesRead = reader.Read(bytebuffer, 0, BytesToRead);

                while (bytesRead > 0)
                {
                    memoryStream.Write(bytebuffer, 0, bytesRead);
                    bytesRead = reader.Read(bytebuffer, 0, BytesToRead);
                }
                //   File.WriteAllBytes("billed.jpg", memoryStream.ToArray());
            }
        }


        public void Create()
        {
            image.BeginInit();
            memoryStream.Seek(0, SeekOrigin.Begin);

            image.StreamSource = memoryStream;
            image.EndInit();

            Image = image;
            IsMade = true;
        }

        public static void InitiateSSLTrust()
        {
            try
            {
                //Change SSL checks so that all checks pass
                ServicePointManager.ServerCertificateValidationCallback =
                    new RemoteCertificateValidationCallback(
                        delegate { return true; }
                        );
            }
            catch
            {
            }
        }
    }

    public class PhotoCollection : List<Photo>
    {
        public PhotoCollection(List<string> listUrl)
        {
            IsMade = false;
            foreach (var url in listUrl)
            {
                if (url != null)
                    Add(new Photo(url));
            }
        }

        public bool IsMade { get; set; }
    }
}

