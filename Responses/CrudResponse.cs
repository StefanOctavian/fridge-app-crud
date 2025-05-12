using Microsoft.AspNetCore.Mvc;

namespace Crud.Responses;

// <summary>
// We wrap the response in a record to make it easier to deserialize in the client
// as nullable data are hard to handle otherwise.
// </summary>
public record CrudResponse<T>(T? Data);