using System.Collections.Generic;

namespace QuizApp.Validators
{
    public interface IValidator<T>
    {
        bool Validate(T value);
        IEnumerable<string> ValidationErrors { get; }
    }
}