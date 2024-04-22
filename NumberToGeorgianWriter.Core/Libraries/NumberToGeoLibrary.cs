using NumberToGeorgianWriter.Core.Contracts;
using NumberToGeorgianWriter.Core.Responses;
using NumberToGeorgianWriter.Features.ConvertNumber;

namespace NumberToGeorgianWriter.Core.Libraries
{
    public class NumberToGeoLibrary : INumberToGeoContract
    {
        private readonly INumberConverter _numberConverterService;
        public NumberToGeoLibrary(INumberConverter numberConverterService)
        {
            _numberConverterService = numberConverterService;
        }
        public async Task<NumberParseResponse> ConvertNumberAsync(string number)
        {
            string convertedResult = await _numberConverterService.ConvertNumberToGeorgianAsync(number);

            return new()
            {
                Input = number,
                Output = convertedResult
            };
        }
    }
}
