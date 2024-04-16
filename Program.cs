namespace NumberToGeorgianWriter
{
    public class ResultHotFix
    {
        public string addSuffix = string.Empty;
        public string replaceLetterIwith = string.Empty;
        public string joinNumbersWith = string.Empty;
    }
    public class Program
    {
        private static Dictionary<int, string> _numberValues = new();
        private static ResultHotFix _numberResultHotFix = new();

        static void Main(string[] args)
        {
            InitNumberValuePairs();

            Console.WriteLine("Input Number");
            string inputNum = Console.ReadLine()
                ??throw new Exception("Empty String");

            var answer = NumberToGeo(inputNum);
            Console.WriteLine(answer);
        }

        private static void InitNumberValuePairs()
        {
            _numberResultHotFix.addSuffix = "i";
            _numberResultHotFix.replaceLetterIwith = "m";
            _numberResultHotFix.joinNumbersWith = "da";

            _numberValues.Add(0, "nuli");
            _numberValues.Add(1, "erTi");
            _numberValues.Add(2, "ori");
            _numberValues.Add(3, "sami");
            _numberValues.Add(4, "oTxi");
            _numberValues.Add(5, "xuTi");
            _numberValues.Add(6, "eqvsi");
            _numberValues.Add(7, "Svidi");
            _numberValues.Add(8, "rva");
            _numberValues.Add(9, "cxra");
            _numberValues.Add(10, "aTi");
            _numberValues.Add(11, "TerTmeti");
            _numberValues.Add(12, "Tormeti");
            _numberValues.Add(13, "cameti");
            _numberValues.Add(14, "ToTxmeti");
            _numberValues.Add(15, "TxuTmeti");
            _numberValues.Add(16, "Teqvsmeti");
            _numberValues.Add(17, "Cvidmeti");
            _numberValues.Add(18, "Tvrameti");
            _numberValues.Add(19, "Cxrameti");
            _numberValues.Add(20, "oci");
            _numberValues.Add(100, "asi");
        }

        private static string NumberToGeo(string inputNum)
        {
            int number = int.Parse(inputNum);
            if (number <= 20)
                return _numberValues[number];

            if(inputNum.Length==2)
                return ParseFromTwoDigitNumber(number);
            else if(inputNum.Length==3)
                return ParseFromThreeDigitNumber(number);
            else if (inputNum.Length == 4)
                return ParseFromFourDigitNumber(number);
            else if (inputNum.Length == 5)
                return ParseFromFiveDigitNumber(number);
            else if (inputNum.Length == 6)
                return ParseFromSixDigitNumber(number);

            return "Wrong String";
        }

        private static string ParseFromSixDigitNumber(int number)
        {
            if (number.ToString().Length != 6)
                throw new Exception("Not A Six Digit Number");

            string resultString = string.Empty;
            int remainder = number % 1000;
            int multiplier = number / 1000;

            resultString += ParseFromThreeDigitNumber(multiplier);
            resultString += TrimLastChar(10) + TrimLastChar(100);
            resultString += ConvertRemainderToString(remainder);

            return resultString;
        }

        private static string ParseFromFiveDigitNumber(int number)
        {
            if (number.ToString().Length != 5)
                throw new Exception("Not A Five Digit Number");

            string resultString = string.Empty;
            int remainder = number % 1000;
            int multiplier = number / 1000;

            string prefix = ParseFromTwoDigitNumber(multiplier);
            resultString += prefix + TrimLastChar(10)+TrimLastChar(100);
            resultString += ConvertRemainderToString(remainder);

            return resultString;
        }

        private static string ParseFromFourDigitNumber(int number)
        {
            if (number.ToString().Length != 4)
                throw new Exception("Not A Four Digit Number");

            string resultString;
            int remainder = number % 1000;
            int multiplier = number / 1000;

            if(multiplier != 1)
                resultString = _numberValues[multiplier] +TrimLastChar(10) + TrimLastChar(100);
            else
                resultString = TrimLastChar(10) + TrimLastChar(100);

            resultString += ConvertRemainderToString(remainder);

            return resultString;
        }

        private static string ParseFromThreeDigitNumber(int number)
        {
            if (number.ToString().Length != 3)
                throw new Exception("Not A Three Digit Number");

            if (number == 100)
                return _numberValues[100];

            string resultString = string.Empty;
            int remainder = number % 100;
            int multiplier = number / 100;

            if (multiplier == 1)
                resultString += TrimLastChar(100);
            else
                if(multiplier == 8 || multiplier == 9)
                    resultString += _numberValues[multiplier] + TrimLastChar(100);
                else
                    resultString += TrimLastChar(multiplier) + TrimLastChar(100);

            resultString += ConvertRemainderToString(remainder);

            return resultString;
        }

        private static string ParseFromTwoDigitNumber(int number)
        {
            if (number == 0) return _numberResultHotFix.addSuffix;
            if (number.ToString().Length != 2)
                throw new Exception("Not A Two Digit Number");

            string resultString = string.Empty;
            int remainder = number % 20;
            int multiplier = number / 20;

            if (multiplier == 0)
                return _numberValues[remainder];

            if (multiplier != 1)
                resultString += TrimLastChar(multiplier);

            if (multiplier == 2 || multiplier == 4)
                resultString += _numberResultHotFix.replaceLetterIwith;

            if (remainder != 0)
            {
                resultString += TrimLastChar(20);
                resultString += _numberResultHotFix.joinNumbersWith;
                resultString += _numberValues[remainder];
            }
            else
                resultString += _numberValues[20];

            return resultString;
        }

        private static string ConvertRemainderToString(int remainder)
        {
            int remainderLength = remainder.ToString().Length;

            if (remainder == 0)
                return _numberResultHotFix.addSuffix;

            if (remainder <= 20)
                return _numberValues[remainder];

            if (remainderLength == 2)
                return ParseFromTwoDigitNumber(remainder);

            if (remainderLength == 3)
                return ParseFromThreeDigitNumber(remainder);

            else
                return ParseFromFourDigitNumber(remainder);
        }

        private static string TrimLastChar(int number)
            => _numberValues[number].Remove(_numberValues[number].Length - 1);
    }
}
