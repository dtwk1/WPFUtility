using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFUtility
{
    using System.Windows;
    using System.Windows.Controls;

    public class Form :Control
    {

        static Form() =>     DefaultStyleKeyProperty.OverrideMetadata(typeof(Form), new FrameworkPropertyMetadata(typeof(Form)));


        //public string UserName
        //{
        //    get { return (string)GetValue(UserNameProperty); }
        //    set { SetValue(UserNameProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for UserName.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty UserNameProperty = DependencyProperty.Register("UserName", typeof(string), typeof(Form), new PropertyMetadata(null));




        //public string UserName
        //{
        //    get { return userName; }
        //    set
        //    {
        //        userName = value;
        //        RaisePropertyChanged(nameof(UserName));
        //        Validator.ValidateAsync(nameof(UserName));
        //    }
        //}

        //public string FirstName
        //{
        //    get { return nameInfo.FirstName; }
        //    set
        //    {
        //        nameInfo.FirstName = value;
        //        RaisePropertyChanged(nameof(FirstName));
        //        Validator.Validate(nameof(FirstName));
        //    }
        //}

        //public string LastName
        //{
        //    get { return nameInfo.LastName; }
        //    set
        //    {
        //        nameInfo.LastName = value;
        //        RaisePropertyChanged(nameof(LastName));
        //        Validator.Validate(nameof(LastName));
        //    }
        //}

        //public string Email
        //{
        //    get { return email; }
        //    set
        //    {
        //        email = value;
        //        RaisePropertyChanged(nameof(Email));
        //        Validator.Validate(nameof(Email));
        //    }
        //}

        //public string Password
        //{
        //    get { return password; }
        //    set
        //    {
        //        password = value;
        //        RaisePropertyChanged(nameof(Password));
        //        Validator.Validate(nameof(Password));
        //    }
        //}

        //public string PasswordConfirmation
        //{
        //    get { return passwordConfirmation; }
        //    set
        //    {
        //        passwordConfirmation = value;
        //        RaisePropertyChanged(nameof(PasswordConfirmation));
        //        Validator.Validate(nameof(PasswordConfirmation));
        //    }
        //}


    }
}
