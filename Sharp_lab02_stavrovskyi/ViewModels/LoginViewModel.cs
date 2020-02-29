using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Sharp_lab02_stavrovskyi.Models;
using Sharp_lab02_stavrovskyi.Tools;

namespace Sharp_lab02_stavrovskyi.ViewModels
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
            if (!DateIsValid())
            {
                MessageBox.Show("Invalid date, please try again");
                return;
            }

            if (!_savedDate.Equals(_date))
            {
                _savedDate = _date;
                LoaderManager.Instance.ShowLoader();
                await Task.Run(() => Thread.Sleep(2000));
                _currentPerson = new Person(_name, _surname, _email, _date);
                await Task.Run((() =>
                {
                    DateString = "Birthday: " + _date.Day + "." +  _date.Month + "." + _date.Year;
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
                LoaderManager.Instance.HideLoader();
            }
        }

        public bool CanExecuteCommand()
        {
            return (_date != null && _name!=null && _surname!=null && _email!=null);
        }

        private bool DateIsValid()
        {
            int diff = CalculateAge();
            if (diff < 0 || diff > 135)
                return false;
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
