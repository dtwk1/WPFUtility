
namespace Form.DemoWpf.ViewModel
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    using MvvmValidation;

    using WPFUtility;

    public class FormViewModel : ValidatableViewModelBase
    {
        private string email;

        private string firstName;

        private string lastName;

        private string password;

        private string passwordConfirmation;

        private string userName;

        public FormViewModel()
        {
        }

        public string Email
        {
            get => this.email;
            set => this.SetProperty(ref this.email, value);
        }

        public string FirstName
        {
            get => this.firstName;
            set => this.SetProperty(ref this.firstName, value);
        }

        public InterestSelector InterestSelectorViewModel { get; }

        public string LastName
        {
            get => this.lastName;
            set => this.SetProperty(ref this.lastName, value);
        }

        public string Password
        {
            get => this.password;
            set => this.SetProperty(ref this.password, value);
        }

        public string PasswordConfirmation
        {
            get => this.passwordConfirmation;
            set => this.SetProperty(ref this.passwordConfirmation, value);
        }

        public string UserName
        {
            get => this.userName;
            set => this.SetProperty(ref this.userName, value);
        }

        protected override void ConfigureValidationRules()
        {
            this.Validator.AddRequiredRule(() => this.UserName, "User Name is required");

            this.Validator.AddAsyncRule(
                nameof(this.UserName),
                async () =>
                {
                    bool isAvailable =
                        true; /*await UserRegistrationService.IsUserNameAvailable(UserName).ToTask();*/

                    return RuleResult.Assert(
                        isAvailable,
                        string.Format("User Name {0} is taken. Please choose a different one.", this.UserName));
                });

            this.Validator.AddRequiredRule(() => this.FirstName, "First Name is required");

            this.Validator.AddRequiredRule(() => this.LastName, "Last Name is required");

            this.Validator.AddRequiredRule(() => this.Email, "Email is required");

            this.Validator.AddRule(
                nameof(this.Email),
                () =>
                {
                    const string regexPattern =
                        @"^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$";
                    return RuleResult.Assert(
                        Regex.IsMatch(this.Email, regexPattern),
                        "Email must by a valid email address");
                });

            this.Validator.AddRequiredRule(() => this.Password, "Password is required");

            this.Validator.AddRule(
                nameof(this.Password),
                () => RuleResult.Assert(this.Password.Length >= 6, "Password must contain at least 6 characters"));

            this.Validator.AddRule(
                nameof(this.Password),
                () => RuleResult.Assert(
                    !this.Password.All(char.IsLower) && !this.Password.All(char.IsUpper)
                                                     && !this.Password.All(char.IsDigit),
                    "Password must contain both lower case and upper case letters"));

            this.Validator.AddRule(
                nameof(this.Password),
                () =>
                    RuleResult.Assert(this.Password.Any(char.IsDigit), "Password must contain at least one digit"));

            this.Validator.AddRule(
                nameof(this.PasswordConfirmation),
                () =>
                {
                    if (!string.IsNullOrEmpty(this.Password) && string.IsNullOrEmpty(this.PasswordConfirmation))
                    {
                        return RuleResult.Invalid("Please confirm password");
                    }

                    return RuleResult.Valid();
                });

            this.Validator.AddRule(
                nameof(this.Password),
                nameof(this.PasswordConfirmation),
                () =>
                {
                    if (!string.IsNullOrEmpty(this.Password) && !string.IsNullOrEmpty(this.PasswordConfirmation))
                    {
                        return RuleResult.Assert(
                            this.Password == this.PasswordConfirmation,
                            "Passwords do not match");
                    }

                    return RuleResult.Valid();
                });

            this.Validator.AddChildValidatable(() => this.InterestSelectorViewModel);
        }

    }
}