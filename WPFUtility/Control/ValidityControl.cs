
using System.Threading.Tasks;
using MvvmValidation;

namespace WPFUtility
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class ValidityControl : Control
    {
        private TextBlock validationErrorsTextBlock;

        public bool? IsValid
        {
            get { return (bool?)GetValue(IsValidProperty); }
            set { SetValue(IsValidProperty, value); }
        }


        public object Content
        {
            get { return (object)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Content.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(object), typeof(ValidityControl), new PropertyMetadata(null));



        // Using a DependencyProperty as the backing store for IsValid.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsValidProperty =
            DependencyProperty.Register("IsValid", typeof(bool?), typeof(ValidityControl), new PropertyMetadata(null,dddd));

        private static void dddd(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        
        }

        public override void OnApplyTemplate()
        {
            validationErrorsTextBlock = this.GetTemplateChild("ValidationErrorsTextBlock") as TextBlock;
        }

        //public ValidationHelper Validator
        //{
        //    get { return (ValidationHelper)GetValue(ValidatorProperty); }
        //    set { SetValue(ValidatorProperty, value); }
        //}

        // Using a DependencyProperty as the backing store for Validator.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty ValidatorProperty =
        //    DependencyProperty.Register("Validator", typeof(ValidationHelper), typeof(ValidityControl), new PropertyMetadata(null));

        static ValidityControl() => DefaultStyleKeyProperty.OverrideMetadata(typeof(ValidityControl), new FrameworkPropertyMetadata(typeof(ValidityControl)));

        public ValidityControl()
        {
            //if (this.Validator != null)
            //    Validator.ResultChanged += OnValidationResultChanged;
            //else
            //    this.Loaded += ValidityControl_Loaded;
        }

        private void ValidityControl_Loaded(object sender, RoutedEventArgs e)
        {
            //if (this.Validator != null)
            //    this.Validator.ResultChanged += OnValidationResultChanged;
        }

        private async void Validate()
        {
            await ValidateAsync();
        }


        private async Task ValidateAsync()
        {
            //var result = await Validator.ValidateAllAsync();

            //UpdateValidationSummary(result);
        }


        //private void OnValidationResultChanged(object sender, ValidationResultChangedEventArgs e)
        //{
        //    if (!IsValid.GetValueOrDefault(true))
        //    {
        //        MvvmValidation.ValidationResult validationResult = Validator.GetResult();

        //        UpdateValidationSummary(validationResult);
        //    }
        //}

        //private void UpdateValidationSummary(MvvmValidation.ValidationResult validationResult)
        //{
        //    IsValid = validationResult.IsValid;
        //    validationErrorsTextBlock.Text = validationResult.ToString();
        //}
    }
}
