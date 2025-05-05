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

// создание обьекта класса и испытание изменения его стоимости
// а также последующий вывод для проверки результата

Merchandise test = new Merchandise("test", '$', 12, 55);

test.print();

test.loose_price("5.95");

test.print();

test.loose_price("7.48");

test.print();

public class Money
{
    // знак валюты
    protected char _sign;
    // целая часть
    protected int _whole;
    // дробная
    protected int _fract;


    public Money(char sign, int whole, int fract)
    {
        _sign = sign;
        _whole = whole;
        _fract = fract;
    }

    public char Sign
    {
        get { return _sign; }
        set { _sign = value; }
    }

    public int Whole
    {
        get { return _whole; }
        set { _whole = value; }
    }

    public int Fractional
    {
        get { return _fract; }
        set
        {
            if (value < 100 && value > 0)
            {
                _fract = value;
            }
            // если было передано значение больше ста,
            // то идёт прибавление перевеса в целую часть
            else if (value > 100)
            {
                _whole += value / 100;
                _fract = value - (100 * value / 100);
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
        // если нету точки, то есть разделителя
        // между дробной и целой частью, то значит и
        // самой дробной части нет, поэтому идёт проверка
        // для правильности дальнейших вычислений
        if (price.IndexOf('.') != -1)
        {
            // создаём 2 переменные в виде символьных массивов т.к.
            // метод копирования из строки принимает только массив
            // и сразу задаём им макс. значение на всякий
            char[] whole_p = new char[char.MaxValue], fract_p = new char[char.MaxValue];

            // в целый буфер копируем числа до точки
            price.CopyTo(0, whole_p, 0, price.IndexOf('.'));
            // в дробный после неё и до конца
            price.CopyTo(price.IndexOf('.') + 1, fract_p, 0, price.Length - price.IndexOf('.') - 1);

            // в целый просто прибавляем скопированное
            // и конвертированное число с помощью
            // метода парсе, т.к. с конвертом у мя были проблемы
            _whole += int.Parse(whole_p);

            // если текущая дробная часть плюс прибавляемая
            // равняется больше 100, то также идёт перенос
            // излишка в целую часть денег
            if ((_fract + int.Parse(fract_p)) >= 100)
            {
                _whole += (_fract + int.Parse(fract_p)) / 100;

                _fract = (_fract + int.Parse(fract_p)) - (((_fract + int.Parse(fract_p)) / 100) * 100);
            }
            // если превышения нет, просто прибавляем
            else
            {
                _fract += int.Parse(fract_p);
            }
        }
        // если дробной части нет и вовсе, то
        // просто извлекаем из строки число и прибавляем
        else
        {
            _whole += int.Parse(price.ToCharArray());
        }
    }

    // метод для понижения цены товара
    // (тут всё почти то же самое, поэтому
    // не буду расписывать лишние коммантарии)
    public void loose_price(string price)
    {
        if (price.IndexOf('.') != -1)
        {
            char[] whole_m = new char[char.MaxValue], fract_m = new char[char.MaxValue];

            price.CopyTo(0, whole_m, 0, price.IndexOf('.'));
            price.CopyTo(price.IndexOf('.') + 1, fract_m, 0, price.Length - price.IndexOf('.') - 1);

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

            _whole -= int.Parse(whole_m);
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