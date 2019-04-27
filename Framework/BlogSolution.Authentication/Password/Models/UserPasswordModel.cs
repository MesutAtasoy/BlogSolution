namespace BlogSolution.Authentication.Password.Models
{
    public class UserPasswordModel
    {
        //
        // Summary:
        //     Gets or sets the minimum length a password must be. Defaults to 6.
        public int? RequiredLength { get; set; }
        //
        // Summary:
        //     Gets or sets the minimum number of unique chars a password must comprised of.
        //     Defaults to 1.
        public int? RequiredUniqueChars { get; set; }
        //
        // Summary:
        //     Gets or sets a flag indicating if passwords must contain a non-alphanumeric character.
        //     Defaults to true.
        public bool RequireNonAlphanumeric { get; set; }
        //
        // Summary:
        //     Gets or sets a flag indicating if passwords must contain a lower case ASCII character.
        //     Defaults to true.
        public bool RequireLowercase { get; set; }
        //
        // Summary:
        //     Gets or sets a flag indicating if passwords must contain a upper case ASCII character.
        //     Defaults to true.
        public bool RequireUppercase { get; set; }
        //
        // Summary:
        //     Gets or sets a flag indicating if passwords must contain a digit. Defaults to
        //     true.
        public bool RequireDigit { get; set; }

        public bool IsRandomPassword { get; set; }

        public string Password { get; set; }

        public bool SendPasswordMail { get; set; }
    }

}
