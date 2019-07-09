
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Form.DemoWpf.ViewModel
{
    using System.ComponentModel;
    using System.Windows.Controls;

    using MvvmValidation;

    using WPFUtility;

    public class InterestItemViewModel : INotifyPropertyChanged //: ViewModelBase
    {
        private bool isSelected;

        public event PropertyChangedEventHandler PropertyChanged;

        public InterestItemViewModel(string name, InterestSelector parentSelector)
        {
            Name = name;
            ParentSelector = parentSelector;
        }

        public string Name { get; private set; }
        private InterestSelector ParentSelector { get; set; }

        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.IsSelected)));
                //RaisePropertyChanged("IsSelected");
                ParentSelector.OnInterestSelectionChanged();
            }
        }
    }

    public class InterestSelector : ValidatableViewModelBase
    {
        private ValidationHelper Validator = new ValidationHelper();

        public InterestSelector()
        {

            //NotifyDataErrorInfoAdapter = new NotifyDataErrorInfoAdapter(Validator);
            //NotifyDataErrorInfoAdapter.ErrorsChanged += OnErrorsChanged;

            this.ItemsSource = new InterestItemViewModel[]
                            {
                                new InterestItemViewModel("Music", this),
                                new InterestItemViewModel("Movies", this),
                                new InterestItemViewModel("Sports", this),
                                new InterestItemViewModel("Shopping", this),
                                new InterestItemViewModel("Hunting", this),
                                new InterestItemViewModel("Books", this),
                                new InterestItemViewModel("Physics", this),
                                new InterestItemViewModel("Comics", this)
                            };

            //ConfigureValidationRules();
        }

        public IEnumerable<InterestItemViewModel> Interests { get; private set; }

        public IEnumerable<InterestItemViewModel> SelectedInterests
        {
            get { return Interests.Where(i => i.IsSelected).ToArray(); }
        }

        public InterestItemViewModel[] ItemsSource { get; }

        //private void ConfigureValidationRules()
        //{
        //    Validator.AddRule(
        //        () => RuleResult.Assert(SelectedInterests.Count() >= 3, "Please select at least 3 interests"));
        //}
        private void OnErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            // Notify the UI that the property has changed so that the validation error gets displayed (or removed).
            // this.OnPropertyChanged(new PropertyChangedEventArgs(e.PropertyName));
        }

        protected override void ConfigureValidationRules()
        {


        }


        public void OnInterestSelectionChanged()
        {
            OnSelectedInterestsChanged();
        }

        #region SelectedInterestsChanged Event

        public event EventHandler SelectedInterestsChanged;

        private void OnSelectedInterestsChanged() => SelectedInterestsChanged?.Invoke(this, EventArgs.Empty);

        public Task<MvvmValidation.ValidationResult> Validate()
        {
            return Validator.ValidateAllAsync();
        }

        #endregion
    }
}
