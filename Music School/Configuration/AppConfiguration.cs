using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_School.Configuration
{
    internal static class AppConfiguration
    {
        public static string MusicSchoolPath = Path.Combine(Directory.GetCurrentDirectory(), "MusicSchool.xml");
    }
}
