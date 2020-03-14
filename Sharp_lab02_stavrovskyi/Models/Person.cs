using System;
using System.ComponentModel.DataAnnotations;
using Sharp_lab03_stavrovskyi.Exceptions;

namespace Sharp_lab03_stavrovskyi.Models
{
    class Person
    {

        #region Fields
        private string _name;
        private string _surname;
        private string _email;
        private DateTime _birthday;
        private int _age;
        private string _sunSign;
        private string _chineseSign;
        #endregion

        #region Properties

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Surname
        {
            get { return _surname; }
            set { _surname = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        private DateTime Birthdate
        {
            get { return _birthday; }
            set { _birthday = value; }
        }

        public int Age
        {
            get
            {
                return _age;
            }
        }

        public bool IsAdult
        {
            get { return (_age >= 18); }
        }

        public string SunSign
        {
            get { return _sunSign; }
        }

        public string ChineseSign
        {
            get { return _chineseSign; }
        }

        public bool IsBirthday
        {
            get { return (_birthday.Month == DateTime.Now.Month && _birthday.Day == DateTime.Now.Day); }
        }

        #endregion

        #region Constructors

        internal Person(string name, string surname, string email, DateTime bd)
        {
            _name = name;
            _surname = surname;
            _email = email;
            CheckEmail();
            _birthday = bd;
            _age = CalculateAge();
            CheckAge();
            _sunSign = CalculateWZodiac();
            _chineseSign = CalculateCZodiac();
        }

        internal Person(String name, string surname, string email):
            this(name, surname, email, DateTime.Now)
        {
        }

        internal Person(String name, string surname, DateTime bd):
            this(name, surname, string.Empty, DateTime.Now)
        {
        }

        #endregion

        #region Commands

        private int CalculateAge()
        {
            if (DateTime.Now.Month < _birthday.Month || (DateTime.Now.Month == _birthday.Month && DateTime.Now.Day < _birthday.Day))
                return DateTime.Now.Year - _birthday.Year - 1;
            return DateTime.Now.Year - _birthday.Year;
        }

        private void CheckAge()
        {
            if (_age > 135)
                throw (new TooOldException("The person is too old to exist"));
            if (_age < 0)
                throw (new NotBornException("The person couldn't've been born"));
        }

        private void CheckEmail()
        {
            if (!(new EmailAddressAttribute().IsValid(_email)))
                throw (new Exceptions.Exceptions("The email is invalid"));

        }

        private string CalculateCZodiac()
        {
            switch (_birthday.Year % 12)
            {
                case 3:
                    return "Pig";

                case 4:
                    return "Rat";

                case 5:
                    return "Ox";

                case 6:
                    return "Tiger";

                case 7:
                    return "Rabbit";

                case 8:
                    return "Dragon";

                case 9:
                    return "Snake";

                case 10:
                    return "Horse";

                case 11:
                    return "Sheep";

                case 0:
                    return "Monkey";

                case 1:
                    return "Rooster";

                default:
                    return "Dog";

            }
        }

        private string CalculateWZodiac()
        {
            if ((_birthday.Month >= 3 && _birthday.Day >= 21) || (_birthday.Month <= 4 && _birthday.Day <= 20))
                return "Aries";
            if ((_birthday.Month >= 4 && _birthday.Day >= 21) || (_birthday.Month <= 5 && _birthday.Day <= 20))
                return "Taurus";
            if ((_birthday.Month >= 5 && _birthday.Day >= 21) || (_birthday.Month <= 6 && _birthday.Day <= 20))
                return "Gemini";
            if ((_birthday.Month >= 6 && _birthday.Day >= 21) || (_birthday.Month <= 7 && _birthday.Day <= 21))
                return "Cancer";
            if ((_birthday.Month >= 7 && _birthday.Day >= 22) || (_birthday.Month <= 8 && _birthday.Day <= 21))
                return "Leo";
            if ((_birthday.Month >= 8 && _birthday.Day >= 22) || (_birthday.Month <= 9 && _birthday.Day <= 21))
                return "Virgo";
            if ((_birthday.Month >= 9 && _birthday.Day >= 22) || (_birthday.Month <= 10 && _birthday.Day <= 21))
                return "Libra";
            if ((_birthday.Month >= 10 && _birthday.Day >= 22) || (_birthday.Month <= 11 && _birthday.Day <= 21))
                return "Scorpio";
            if ((_birthday.Month >= 11 && _birthday.Day >= 22) || (_birthday.Month <= 12 && _birthday.Day <= 21))
                return "Sagittarius";
            if ((_birthday.Month >= 12 && _birthday.Day >= 22) || (_birthday.Month <= 1 && _birthday.Day <= 20))
                return "Capricorn";
            if ((_birthday.Month >= 1 && _birthday.Day >= 21) || (_birthday.Month <= 2 && _birthday.Day <= 19))
                return "Aquarius";
            return "Pisces";
        }

        #endregion
    }
}
