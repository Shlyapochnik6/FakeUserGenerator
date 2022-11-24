using System.Text;
using FakeUserGenerator.Application.Common.Conditions;

namespace FakeUserGenerator.Application.Common.Generators;

public class Error
{
    private readonly double _count;
    private readonly string _country;
    private readonly Random _random;

    public Error(string country, int seed, double count)
    {
        _country = country;
        _count = count;
        _random = new Random(seed);
    }

    public string[] Generate(params string[] lines)
    {
        var numberErrors = GetNumberIntegerErrors(_count);
        var probability = GetNumberNonintegerErrors(_count);
        if (numberErrors == 0)
        {
            return lines;
        }
        for (var i = 0; i < numberErrors; i++)
        {
            var index = _random.Next(lines.Length);
            var operation = SetRandomError();
            lines[index] = operation(lines[index]);
        }
        probability = probability.ToString().Length == 1 ? probability * 10 : probability;
        if (_random.Next(1, 101) <= probability)
        {
            var index = _random.Next(lines.Length);
            var operation = SetRandomError();
            lines[index] = operation(lines[index]);
        }
        return lines;
    }
    
    private Func<string, string> SetRandomError()
    {
        var caseIndex = _random.NextDouble() * 100;
        return caseIndex switch
        {
            > 0 and <= 33.333 => RemoveCharacter,
            > 33.333 and <= 66.666 => SwapAround,
            > 66.666 and <= 100 => InsertRandomCharacter,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private string RemoveCharacter(string line)
    {
        if (line.Length <= 15) return line;
        var index = _random.Next(line.Length);
        return line.Remove(index, 1);
    }

    private string InsertRandomCharacter(string line)
    {
        if (line.Length <= 15) return line;
        var data = _random.Next(1, 3) == 1 
            ? new CountryLanguage().GetAlphabet(_country)
            : _random.Next(1, 11).ToString();
        var s = data[_random.Next(data!.Length)].ToString();
        return line.Insert(_random.Next(line.Length), s);
    }

    private string SwapAround(string line)
    {
        if (line.Length <= 10) return line;
        var builder = new StringBuilder(line);
        var p = _random.Next(line.Length - 2);
        var f = builder[p];
        var s = builder[p + 1];
        builder[p] = s;
        builder[p + 1] = f;
        return builder.ToString();
    }

    public int GetNumberIntegerErrors(double count)
    {
        var e = count.ToString().Split('.', ',')[0];
        return int.Parse(e);
    }

    public int GetNumberNonintegerErrors(double count)
    {
        var e = count.ToString().Split('.', ',');
        var probability = 0;
        if (e.Length > 1)
        {
            probability = int.Parse(e[1]);
        }
        return probability;
    }
}