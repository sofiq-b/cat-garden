using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatGarden.Common
{
    public static class EntityValidationConstants
    {
        public static class Article
        {
            public const int TitleMinLength = 5;
            public const int TitleMaxLength = 120;

            public const int ContentMinLength = 500;
            public const int ContentMaxLength = 2000;
        }
    }
}
