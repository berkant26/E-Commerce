﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Helpers.FileHelpers
{
    public interface IFileHelper
    {
        string Upload(IFormFile file, string root);
        void Delete(string path);
        string Update(IFormFile file, string path, string root);
    }
}
