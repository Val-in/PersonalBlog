namespace PersonalBlog.Core.Value_Objects;

/// <summary>
/// Объект, определяемый своими значениями
/// </summary>
public class Email
{
    public string Value { get; }

    public Email(string value)
    {
        if (!value.Contains('@'))
            throw new ArgumentException("Неверный email");
        Value = value;
    }

    /// <summary>
    /// Метод позволяет сравнивать два объекта Email по значению, а не по ссылке.
    /// </summary>
    public override bool Equals(object? obj) //
    {
        return obj is Email other && other.Value == Value;
    }

    public override int GetHashCode() => Value.GetHashCode();
    
}