namespace NumberToGeorgianWriter.Features.ConvertNumber
{
    public interface INumberConverter
    {
        Task<string> ConvertNumberToGeorgianAsync(string number);
    }
    public class NumberResultHotFix
    {
        public string addSuffix = string.Empty;
        public string replaceLetterIwith = string.Empty;
        public string joinNumbersWith = string.Empty;
    }
    public class NumberConvertService : INumberConverter
    {
        private readonly Dictionary<int, string> _numberValuePairDict = new();
        private readonly NumberResultHotFix _numberResultHotFix = new();
        public NumberConvertService()
        {
            InitNumberValuePairs();
        }

        public Task<string> ConvertNumberToGeorgianAsync(string inputNum)
        {
            ValidateInput(inputNum);

            int number = int.Parse(inputNum);

            return Task.Run(
                () => ConvertStringFromNumber(number));
        }

        private string ConvertStringFromNumber(int number)
        {
            int numLength = number.ToString().Length;

            return numLength switch
            {
                _ when numLength >= 1 && numLength <= 2
                    => ParseStringFromTwoDigitNumber(number),

                3 => ParseStringFromThreeDigitNumber(number),
                4 => ParseStringFromFourDigitNumber(number),

                _ when numLength >= 5 && numLength <= 6
                    => ParseStringFromFiveOrSixDigitNumber(number),
                _ when numLength >= 7 && numLength <= 9

                    => ParseFromMillionTillBillion(number),

                _ => throw new Exception("Wrong String"),
            };
        }

        private string ParseFromMillionTillBillion(int number)
        {
            string resultString = string.Empty;
            int remainder = number % 1000000;
            int multiplier = number / 1000000;

            resultString += multiplier != 1 ? ConvertStringFromNumber(multiplier) : "";

            resultString += TrimLastCharOfTheNumber(1000000);
            resultString += ConvertStringFromNumber(remainder);

            return resultString;
        }

        private string ParseStringFromFiveOrSixDigitNumber(int number)
        {
            int numLength = number.ToString().Length;

            string resultString = string.Empty;
            int remainder = number % 1000;
            int multiplier = number / 1000;

            resultString += ConvertStringFromNumber(multiplier) + TrimLastCharOfTheNumber(10) + TrimLastCharOfTheNumber(100);
            resultString += ConvertRemainderToString(remainder);

            return resultString;
        }

        private string ParseStringFromThreeDigitNumber(int number)
        {
            if (number.ToString().Length != 3)
                throw new Exception("Not A Three Digit Number");

            if (number == 100)
                return _numberValuePairDict[100];

            string resultString = string.Empty;
            int remainder = number % 100;
            int multiplier = number / 100;

            if (multiplier == 1)
                resultString += TrimLastCharOfTheNumber(100);
            else
                if (multiplier == 8 || multiplier == 9)
                resultString += _numberValuePairDict[multiplier] + TrimLastCharOfTheNumber(100);
            else
                resultString += TrimLastCharOfTheNumber(multiplier) + TrimLastCharOfTheNumber(100);

            resultString += ConvertRemainderToString(remainder);

            return resultString;
        }

        private string ConvertRemainderToString(int remainder)
        {
            if (remainder == 0)
                return _numberResultHotFix.addSuffix;

            return ConvertStringFromNumber(remainder);
        }

        private string ParseStringFromFourDigitNumber(int number)
        {
            int remainder = number % 1000;
            int multiplier = number / 1000;

            string resultString = multiplier != 1
                ? _numberValuePairDict[multiplier] + TrimLastCharOfTheNumber(10) + TrimLastCharOfTheNumber(100)
                : TrimLastCharOfTheNumber(10) + TrimLastCharOfTheNumber(100);

            resultString += ConvertRemainderToString(remainder);

            return resultString;
        }

        private string ParseStringFromTwoDigitNumber(int number)
        {
            if (number == 0)
                return _numberResultHotFix.addSuffix;

            if (number <= 20)
                return _numberValuePairDict[number];

            string resultString = string.Empty;
            int remainder = number % 20;
            int multiplier = number / 20;

            if (multiplier == 0)
                return _numberValuePairDict[remainder];

            if (multiplier != 1)
                resultString += TrimLastCharOfTheNumber(multiplier);

            if (multiplier == 2 || multiplier == 4)
                resultString += _numberResultHotFix.replaceLetterIwith;

            if (remainder != 0)
            {
                resultString += TrimLastCharOfTheNumber(20);
                resultString += _numberResultHotFix.joinNumbersWith;
                resultString += _numberValuePairDict[remainder];
            }
            else
                resultString += _numberValuePairDict[20];

            return resultString;
        }

        private void ValidateInput(string input)
        {
            if (!int.TryParse(input, out int result))
                throw new Exception("Invalid Input");
        }

        private string TrimLastCharOfTheNumber(int number)
            => _numberValuePairDict[number].Remove(_numberValuePairDict[number].Length - 1);

        private void InitNumberValuePairs()
        {
            _numberResultHotFix.addSuffix = "ი";
            _numberResultHotFix.replaceLetterIwith = "მ";
            _numberResultHotFix.joinNumbersWith = "და";

            _numberValuePairDict.Add(0, "ნული");
            _numberValuePairDict.Add(1, "ერთი");
            _numberValuePairDict.Add(2, "ორი");
            _numberValuePairDict.Add(3, "სამი");
            _numberValuePairDict.Add(4, "ოთხი");
            _numberValuePairDict.Add(5, "ხუთი");
            _numberValuePairDict.Add(6, "ექვსი");
            _numberValuePairDict.Add(7, "შვიდი");
            _numberValuePairDict.Add(8, "რვა");
            _numberValuePairDict.Add(9, "ცხრა");
            _numberValuePairDict.Add(10, "ათი");
            _numberValuePairDict.Add(11, "თერთმეტი");
            _numberValuePairDict.Add(12, "თორმეტი");
            _numberValuePairDict.Add(13, "ცამეტი");
            _numberValuePairDict.Add(14, "თოთხმეტი");
            _numberValuePairDict.Add(15, "თხუთმეტი");
            _numberValuePairDict.Add(16, "თექვსმეტი");
            _numberValuePairDict.Add(17, "ჩვიდმეტი");
            _numberValuePairDict.Add(18, "თვრამეტი");
            _numberValuePairDict.Add(19, "ცხრამეტი");
            _numberValuePairDict.Add(20, "ოცი");
            _numberValuePairDict.Add(100, "ასი");
            _numberValuePairDict.Add(1000000, "მილიონი");
        }
    }
}
