using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DampGUI
{
    public class PhotoCollection : List<IPhoto>, IPhotoCollection
    {
        /**
         * PhotoCollection
         * 
         * @brief This constructor takes the list of urls and makes the Photos
         * @param List<string> listUrl
         */
        public PhotoCollection(List<string> listUrl)
        {
            IsMade = false;
            foreach (var url in listUrl)
            {
                if (url != null)
                    Add(new Photo(url));
            }
        }

        /**
          *  IsMade
          * 
          * @brief this property is used to check if the collection is made or not 
          * @param set(bool)
          * @return get(bool)
          */
        public bool IsMade { get; set; }

    }
}
