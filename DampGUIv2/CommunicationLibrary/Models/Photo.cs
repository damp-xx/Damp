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
        /**
          *  Image
          * 
          * @brief this property is used to get set the final picture
          * @param set(BitmapImage)
          * @return get(BitmapImage)
          */
        public BitmapImage Image { get; set; }
        private BitmapImage image = new BitmapImage();

        /**
          *  Url
          * 
          * @brief this property is used to get set the url 
          * @param set(string)
          * @return get(string)
          */
        public string Url { get; set; }

        /**
          *  IsMade
          * 
          * @brief this property is used to check if the picture is made or not 
          * @param set(bool)
          * @return get(bool)
          */
        public bool IsMade { get; set; }

        /**
          *  Photo
          * 
          * @brief this Constructor sets the Url and sets the IsMade to false
          */
        public Photo(string _url)
        {
            IsMade = false;
            Url = _url;
            //Console.WriteLine("url:::::::" + Url);
        }

        private MemoryStream memoryStream = new MemoryStream();

        /**
          *  LoadPicture
          * 
          * @brief this function gets the picture form the url 
          */
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

        /**
          *  Create
          * 
          * @brief this function creates the final picture in a GUI thread 
          */
        public void Create()
        {
            image.BeginInit();
            memoryStream.Seek(0, SeekOrigin.Begin);

            image.StreamSource = memoryStream;
            image.EndInit();

            Image = image;
            IsMade = true;
        }

        /**
          *  InitiateSSLTrust
          * 
          * @brief this function Change SSL checks so that all checks pass          * @param set(bool)
          */
        public static void InitiateSSLTrust()
        {
            try
            {
                
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


}

