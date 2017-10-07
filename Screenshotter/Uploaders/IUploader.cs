﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Screenshotter.Uploaders
{
    public interface IUploader
    {
        void UploadImage(Image img, string name);
        bool Authenticate();
    }
}
