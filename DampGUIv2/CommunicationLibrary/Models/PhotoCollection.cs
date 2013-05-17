using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DampGUI
{
    public class PhotoCollection : List<IPhoto>, IPhotoCollection
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
