using static System.Console;

// ZADANIE 1

/*

Запрограммируйте класс Money (объект класса оперирует 
одной валютой) для работы с деньгами.
В классе должны быть предусмотрены поле для хранения 
целой части денег (доллары, евро, гривны и т.д.) и поле
для хранения копеек (центы, евроценты, копейки и т.д.).
Реализовать методы для вывода суммы на экран, 
задания значений для частей.
На базе класса Money создать класс Product для работы
с продуктом или товаром. Реализовать метод, 
позволяющий уменьшить цену на заданное число.
Для каждого из классов реализовать необходимые
методы и поля.

*/

public class Money
{
    protected char _sign;
    protected int _whole;
    protected int _fract;


    public Money(char sign, int whole, int fract)
    {
        try
        {
            if (fract >= 100 || whole < 0 || fract < 0) 
            {
                throw new MyException("Один или оба из переданных вами числовых аргументов являются недопустимыми.");
            }
            _sign = sign;
            _whole = whole;
            _fract = fract;
        }
        catch (MyException e)
        {
            WriteLine(e.ToString());
        }
    }

    public char Sign
    {
        get { return _sign; }
        set { _sign = value; }
    }

    public int Whole
    {
        get { return _whole; }
        set
        {
            // проверка на отрицательность значения
            if (value < 0)
            {
                throw new MyException("Значение передаваемое для присвоения дцелой части меньше нуля.");
            }

            _whole = value; 
        }
    }

    public int Fractional
    {
        get { return _fract; }
        set
        {
            
            try
            {
                // теперь вместо того чтобы при присвоении
                // значения превышающего 100 прибавлять лишнее
                // в целую часть, идёт выброс иключения
                if (value > 100)
                {
                    throw new MyException("Значение которое вы хотите записать в дробную часть превышает допустимое значение (100).");
                }
                // проверка на отрицательность значения
                if (value < 0)
                {
                    throw new MyException("Значение передаваемое для присвоения дробной части меньше нуля.");
                }

                _fract = value;
            }
            catch (MyException e)
            {
                WriteLine(e.ToString());
            }
        }
    }

    public virtual void print()
    {
        WriteLine($"{_whole}.{_fract} {_sign}");
    }
}

public class Merchandise : Money
{
    string _name;

    public Merchandise(string name, char sign, int whole, int fract) : base(sign, whole, fract)
    {
        _name = name;
    }

    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    public override void print()
    {
        WriteLine($"Название продукта: {_name}");

        base.print();
    }

    // метод повышения цены
    public void rise_price(string price)
    {
        try
        {
            if (price.IndexOf('.') != -1)
            {
                char[] whole_p = new char[char.MaxValue], fract_p = new char[char.MaxValue];

                price.CopyTo(0, whole_p, 0, price.IndexOf('.'));
                price.CopyTo(price.IndexOf('.') + 1, fract_p, 0, price.Length - price.IndexOf('.') - 1);

                // проверка на то, что были переданы отриц. значения
                if (whole_p.Contains('-') || fract_p.Contains('-'))
                {
                    throw new MyException("Один из переданных параметров является отрицательным.");
                }

                _whole += int.Parse(whole_p);

                if ((_fract + int.Parse(fract_p)) >= 100)
                {
                    _whole += (_fract + int.Parse(fract_p)) / 100;

                    _fract = (_fract + int.Parse(fract_p)) - (((_fract + int.Parse(fract_p)) / 100) * 100);
                }
                else
                {
                    _fract += int.Parse(fract_p);
                }
            }
            else
            {
                // проверка на то, что были переданы отриц. значения
                if (price.ToCharArray().Contains('-'))
                {
                    throw new MyException("Один из переданных параметров является отрицательным.");
                }

                _whole += int.Parse(price.ToCharArray());
            }
        }
        catch (MyException e)
        {
            WriteLine(e.ToString());
        }
    }

    public void loose_price(string price)
    {
        try
        {
            if (price.IndexOf('.') != -1)
            {
                char[] whole_m = new char[char.MaxValue], fract_m = new char[char.MaxValue];

                price.CopyTo(0, whole_m, 0, price.IndexOf('.'));
                price.CopyTo(price.IndexOf('.') + 1, fract_m, 0, price.Length - price.IndexOf('.') - 1);

                // проверка на то, что были переданы отриц. значения
                if (whole_m.Contains('-') || fract_m.Contains('-'))
                {
                    throw new MyException("Один из переданных параметров является отрицательным.");
                }

                _whole -= int.Parse(whole_m);

                if ((_fract - int.Parse(fract_m)) <= 0)
                {
                    _whole -= 1;

                    _fract = 100 + (_fract - int.Parse(fract_m));
                }
                else
                {
                    _fract -= int.Parse(fract_m);
                }
            }
            else
            {
                char[] whole_m = new char[char.MaxValue];
                price.CopyTo(0, whole_m, 0, price.Length - 1);

                // проверка на то, что были переданы отриц. значения
                if (whole_m.Contains('-'))
                {
                    throw new MyException("Один из переданных параметров является отрицательным.");
                }

                _whole -= int.Parse(whole_m);
            }
        }
        catch (MyException e)
        {
            WriteLine(e.ToString());
        }
    }
}



public class MyException : Exception
{
    DateTime _time_exc;

    public DateTime Time_exc { get; private set; }

    public MyException() : base("My exception was called")
    {
        _time_exc = DateTime.Now;
    }

    public MyException(string message) : base(message) { }
    public MyException(string message, Exception innerException) : base(message, innerException) { }
    protected MyException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}