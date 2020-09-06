using System.Collections.Generic;

namespace QuizApp.Validators
{
    public class TitleValidator : IValidator<string>
    {
        public IEnumerable<string> ValidationErrors { get; private set; }

        public bool Validate(string value)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
            {
                ValidationErrors = new List<string>() { "Value cannot be empty" };
                return false;
            }

            return true;
        }

    }
}
