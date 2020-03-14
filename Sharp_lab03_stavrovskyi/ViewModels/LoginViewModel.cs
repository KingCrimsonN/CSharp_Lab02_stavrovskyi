using System;
using System.CodeDom;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Sharp_lab03_stavrovskyi.Exceptions;
using Sharp_lab03_stavrovskyi.Models;
using Sharp_lab03_stavrovskyi.Tools;

namespace Sharp_lab03_stavrovskyi.ViewModels
{
    internal class LoginViewModel : BaseViewModel
    {
        #region Fields

        private Person _currentPerson;
        private string _name;
        private string _surname;
        private string _email;
        private DateTime _date;
        private DateTime _savedDate;

        private string _nameString;
        private string _surnameString;
        private string _emailString;
        private string _adultString;
        private string _dateString;
        private string _ageString;
        private string _cZodiacString;
        private string _wZodiacString;

        private RelayCommand<object> _calculateCommand;

        #endregion

        #region Properties

        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value.Date;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public string Surname
        {
            get { return _surname; }
            set
            {
                _surname = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        public string NameString
        {
            get { return _nameString; }
            set
            {
                _nameString = value;
                OnPropertyChanged();
            }
        }

        public string SurnameString
        {
            get { return _surnameString; }
            set
            {
                _surnameString = value;
                OnPropertyChanged();
            }
        }

        public string EmailString
        {
            get { return _emailString; }
            set
            {
                _emailString = value;
                OnPropertyChanged();
            }
        }

        public string AdultString
        {
            get { return _adultString; }
            set
            {
                _adultString = value;
                OnPropertyChanged();
            }
        }

        public string DateString
        {
            get { return _dateString; }
            set
            {
                _dateString = value;
                OnPropertyChanged();
            }
        }


        public string AgeString
        {
            get { return _ageString; }
            set
            {
                _ageString = value;
                OnPropertyChanged();
            }
        }



        public string CZodiacString
        {
            get { return _cZodiacString; }
            set
            {
                _cZodiacString = value;
                OnPropertyChanged();
            }

        }


        public string WZodiacString
        {
            get { return _wZodiacString; }
            set
            {
                _wZodiacString = value;
                OnPropertyChanged();
            }
        }

        #endregion


        #region Commands



        public RelayCommand<object> CalculateCommand
        {
            get
            {
                return _calculateCommand ?? (_calculateCommand = new RelayCommand<object>(Calculate, o => CanExecuteCommand()));
            }
        }



        private async void Calculate(object o)
        {
            LoaderManager.Instance.ShowLoader();
            try
            {
                if (!_savedDate.Equals(_date))
                {
                    CleanUi();
                    _savedDate = _date;
                    await Task.Run(() => Thread.Sleep(1000));
                    _currentPerson = new Person(_name, _surname, _email, _date);
                    await Task.Run((() =>
                    {
                        DateString = "Birthday: " + _date.Day + "." + _date.Month + "." + _date.Year;
                        AgeString = "Age: " + _currentPerson.Age;
                        WZodiacString = "Western Zodiac " + _currentPerson.SunSign;
                        CZodiacString = "Chinese Zodiac " + _currentPerson.ChineseSign;
                    }));
                    NameString = "Name: " + _currentPerson.Name;
                    SurnameString = "Surname: " + _currentPerson.Surname;
                    EmailString = "Email: " + _currentPerson.Email;
                    AdultString = "Status: ";
                    AdultString += _currentPerson.IsAdult ? "Adult" : "Child";
                    if (_currentPerson.IsBirthday)
                        MessageBox.Show("Happy Birthday!");
                    else MessageBox.Show("Your age and astrological symbols have been calculated, have a look!");
                }
            }
            catch (NotBornException e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                _savedDate = DateTime.Now;
            }
            catch (TooOldException e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                _savedDate = DateTime.Now;
            }
            catch (EmailException e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                _savedDate = DateTime.Now;
            }
            LoaderManager.Instance.HideLoader();
        }

        public bool CanExecuteCommand()
        {
            return (_date != null && _name != null && _surname != null && _email != null);
        }

        private async void CleanUi()
        {
            await Task.Run((() =>
            {
                DateString = "";
                AgeString = "";
                WZodiacString = "";
                CZodiacString = "";
            }));
            NameString = "";
            SurnameString = "";
            EmailString = "";
            AdultString = "";
            AdultString = "";
        }


        private bool DateIsValid()
        {
            int diff = CalculateAge();
            if (diff < 0)
                throw (new NotBornException("The person wasn't born yet"));
            if (diff > 135)
                throw (new TooOldException("The person is too old to be alive"));
            return true;
        }

        private int CalculateAge()
        {
            if (DateTime.Now.Month < _date.Month || (DateTime.Now.Month == _date.Month && DateTime.Now.Day < _date.Day))
                return DateTime.Now.Year - _date.Year - 1;
            return DateTime.Now.Year - _date.Year;
        }



        #endregion
    }
}
