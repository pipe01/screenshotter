using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Screenshotter.Uploaders
{
    public interface IUploader
    {
        void UploadImage();
        bool Authenticate();
    }
}
