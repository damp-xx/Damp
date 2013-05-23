using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DampGUI
{
    public interface IPhotoCollection:IList<IPhoto>
    {
        bool IsMade { get; set; }
    }
}
