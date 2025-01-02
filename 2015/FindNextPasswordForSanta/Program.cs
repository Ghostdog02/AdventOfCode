using System.Text;

namespace FindNextPasswordForSanta
{
    public class Program
    {
        static void Main(string[] args)
        {
            string password = "hepxxyzz";
            var newPassword = new FindNextPassword();
            Console.WriteLine(newPassword.GeneratePassword(password));
        }
    }

    public class FindNextPassword
    {
        private static readonly char[] InvalidLetters = { 'i', 'o', 'l' };
        public string GeneratePassword(string password)
        {
            string currentPassword = password;

            while (true)
            {
                currentPassword = IncrementPassword(currentPassword);
                if (IsValidPassword(currentPassword))
                {
                    return currentPassword;
                }
            }


        }

        private bool HasDoubledLetters(string password)
        {
            var doubledLettersCount = 0;

            for (int i = 0; i < password.Length - 1; i++)
            {
                var currentLetter = password[i];
                var nextLetter = password[i + 1];

                if (currentLetter == nextLetter)
                {
                    doubledLettersCount++;

                    // Skip the next character
                    i++;
                }

                if (doubledLettersCount == 2)
                    return true;
            }

            return false;
        }

        private string IncrementPassword(string password)
        {
            var passwordArray = password.ToCharArray();
            for (int i = passwordArray.Length - 1; i >= 0; i--)
            {
                if (passwordArray[i] == 'z')
                {
                    passwordArray[i] = 'a';
                }

                else
                {
                    passwordArray[i]++;

                    if (InvalidLetters.Contains(passwordArray[i]))
                    {
                        passwordArray[i]++;
                    }

                    break;
                }
            }

            return new string(passwordArray);
        }

        private bool HasThreeConsecutiveLetters(string password)
        {
            for (int i = 0; i < password.Length - 2; i++)
            {
                var firstLetter = password[i];
                var secondLetter = password[i + 1];
                var thirdLetter = password[i + 2];

                if (firstLetter + 1 == secondLetter && secondLetter + 1 == thirdLetter)
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsValidPassword(string password)
        {
            return HasThreeConsecutiveLetters(password) && !ContainsInvalidLetters(password) && HasDoubledLetters(password);
        }

        private bool ContainsInvalidLetters(string password)
        {
            return password.Any(a => InvalidLetters.Contains(a));
        }


    }
}
