
namespace WPFUtility
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using System.Windows.Input;

    using Mvvm;
    using Mvvm.Commands;

    using MvvmValidation;

    public abstract class ValidatableViewModelBase : BindableBase, IValidatable, INotifyDataErrorInfo
    {
        private bool? isValid;
        private string validationErrorsString;

        protected ValidatableViewModelBase()
        {
            this.Validator = new ValidationHelper();
            this.ValidateCommand = new DelegateCommand(this.Validate);
            this.NotifyDataErrorInfoAdapter = new NotifyDataErrorInfoAdapter(this.Validator);
            this.NotifyDataErrorInfoAdapter.ErrorsChanged += this.OnErrorsChanged;
            ConfigureValidationRules();
            Validator.ResultChanged += OnValidationResultChanged;
     
        }


        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged
        {
            add => this.NotifyDataErrorInfoAdapter.ErrorsChanged += value;
            remove => this.NotifyDataErrorInfoAdapter.ErrorsChanged -= value;
        }

        public bool HasErrors => this.NotifyDataErrorInfoAdapter.HasErrors;

        public bool? IsValid
        {
            get => this.isValid;
            private set => base.SetProperty(ref this.isValid, value, nameof(this.IsValid));
        }

        public ICommand ValidateCommand { get; }

        public string ValidationErrorsString
        {
            get => this.validationErrorsString;
            private set => base.SetProperty(ref this.validationErrorsString, value, nameof(this.ValidationErrorsString));
        }

        public ValidationHelper Validator { get; }

        private NotifyDataErrorInfoAdapter NotifyDataErrorInfoAdapter { get; }

        public IEnumerable GetErrors(string propertyName)
        {
            return this.NotifyDataErrorInfoAdapter.GetErrors(propertyName);
        }

        Task<ValidationResult> IValidatable.Validate()
        {
            return this.Validator.ValidateAllAsync();
        }


        protected abstract void ConfigureValidationRules();


        protected override bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            //this.Validator.ValidateAsync(propertyName);
          
            ValidationResult result = this.Validator.ValidateAsync(propertyName).Result;
            this.UpdateValidationSummary(result);
            return base.SetProperty(ref storage, value, propertyName);
        }

        private void OnErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            // Notify the UI that the property has changed so that the validation error gets displayed (or removed).
            this.OnPropertyChanged(e.PropertyName);
        }

        private void OnValidationResultChanged(object sender, ValidationResultChangedEventArgs e)
        {
            if (!this.IsValid.GetValueOrDefault(true))
            {
                ValidationResult validationResult = this.Validator.GetResult();

                this.UpdateValidationSummary(validationResult);
            }
        }

        private void UpdateValidationSummary(ValidationResult validationResult)
        {
            this.IsValid = validationResult.IsValid;
            this.ValidationErrorsString = validationResult.ToString();
        }


        private async void Validate()
        {
            await this.ValidateAsync();
        }

        private async Task ValidateAsync()
        {
            ValidationResult result = await this.Validator.ValidateAllAsync();

            this.UpdateValidationSummary(result);
        }
    }
}
