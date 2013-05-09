using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Net.Security;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace DampGUI
{
    public class Photo : IPhoto
    {
        public BitmapImage Image { get; set; }
        private BitmapImage image = new BitmapImage();
        public string Url { get; set; }
        private bool isMade = false;


        public bool IsMade { get { return isMade; } set{isMade=value;} }

        public Photo(string _url)
        {
            Url = _url;
            Console.WriteLine("url:::::::" + Url);
            
        }

        MemoryStream memoryStream = new MemoryStream();


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
             //   Console.WriteLine("loadPicture er blevet koert???????????");
            }
        }


        public void create()
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
            catch (Exception ex)
            {
            }
        }
    }

    public class PhotoCollection : List<Photo>
    {
        private bool isMade = false;
        public PhotoCollection(List<string> listUrl)
        {
            foreach (var url in listUrl)
            {
                if (url != null)
                    Add(new Photo(url));
            }
        }

        public bool IsMade
        {
            get { return isMade; }
            set { isMade = value; }
        }
    }
}

