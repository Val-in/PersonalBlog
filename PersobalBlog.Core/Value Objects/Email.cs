namespace PersobalBlog.Core.Value_Objects;

public class Email //объект, определяемый своими значениями
{
    public string Value { get; }

    public Email(string value)
    {
        if (!value.Contains("@"))
            throw new ArgumentException("Неверный email");
        Value = value;
    }

    public override bool Equals(object obj) //метод позволяет сравнивать два объекта Email по значению, а не по ссылке.
    {
        return obj is Email other && other.Value == Value;
    }

    public override int GetHashCode() => Value.GetHashCode();
    
}