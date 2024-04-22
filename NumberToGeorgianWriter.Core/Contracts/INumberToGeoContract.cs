using NumberToGeorgianWriter.Core.Responses;

namespace NumberToGeorgianWriter.Core.Contracts
{
    public interface INumberToGeoContract
    {
        Task<NumberParseResponse> ConvertNumberAsync(string number);
    }
}
