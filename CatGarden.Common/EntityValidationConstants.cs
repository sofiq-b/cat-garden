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

        public static class Cat
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 50;

            public const int AgeMinLength = 0;
            public const int AgeMaxLength = 30;

            public const int DescriptionMinLength = 10;
            public const int DescriptionMaxLength = 300;
        }

        public static class Cattery
        {
            public const int NameMinLength = 10;
            public const int NameMaxLength = 50;

            public const int AddressMinLength = 20;
            public const int AddressMaxLength = 500;
        }
        public static class CatteryOwner
        {
            public const int EmailMinLength = 5;
            public const int EmailMaxLength = 320;

            public const int PhoneNumberMinLength = 7;
            public const int PhoneNumberMaxLength = 20;
        }

        public static class Review
        {
            public const int CommentMaxLength = 1000;

            public const int RatingMinLength = 0;
            public const int RatingMaxLength = 5;
        }
    }
}
