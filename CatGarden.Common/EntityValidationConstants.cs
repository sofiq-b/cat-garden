namespace CatGarden.Common
{
    public static class EntityValidationConstants
    {

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

        public static class User
        {
            public const int FirstNameMinLength = 1;
            public const int FirstNameMaxLength = 15;

            public const int LastNameMinLength = 1;
            public const int LastNameMaxLength = 15;

            public const int PasswordMinLength = 6;
            public const int PasswordMaxLength = 100;
        }
    }
}
