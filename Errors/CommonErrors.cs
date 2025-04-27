using Crud.Entities.Enums;

namespace Crud.Errors;

/// <summary>
/// Common errors that may be reused in various places in the code.
/// </summary>
public static class CommonErrors
{
    public static ServerException UserNotFound => 
        new NotFoundException("User doesn't exist!");
    public static ServerException WrongUnit(string unit) => 
        new BadRequestException($"The unit {unit} is not valid! The valid units are: " +
        string.Join(", ", UnitExtensions.GetAllLabels()));
    public static ServerException UnknownError => 
        new InternalServerErrorException("An unknown error occurred, contact the technical support!");
}
