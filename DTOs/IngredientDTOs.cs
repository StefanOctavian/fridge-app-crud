using Crud.Entities.Enums;

namespace Crud.DTOs;

/// <summary>
/// DTO for an ingredient with its quantity and unit.
/// Used when adding a recipe to the database via API request.
/// </summary>
public record IngredientQuantityAddDTO(
    string Ingredient,
    double Quantity,
    Unit Unit
);

/// <summary>
/// DTO for an ingredient in a recipe. Used for returning a recipe in an API response.
/// </summary>
public record IngredientQuantityDTO(
    Guid IngredientId,
    string Ingredient,
    double Quantity,
    Unit Unit
);

/// <summary>
/// DTO for returning an ingredient from the fridge in an API response.
/// </summary>
public record FridgeIngredientDTO(
    Guid IngredientId,
    string Ingredient,
    double Quantity,
    Unit Unit,
    string ImageUrl
);

/// <summary>
/// DTO for an ingredient when putting the whole fridge in API request.
/// </summary>
public record FridgePutIngredientDTO(
    Guid IngredientId,
    double Quantity,
    Unit Unit
);

/// <summary>
/// DTO for an ingredient delta when updating the fridge in API request.
/// </summary>
public record FridgeIngredientDeltaDTO(
    Guid IngredientId,
    double QuantityDelta,
    Unit Unit
);

/// <summary>
/// DTO for an ingredient. Used for returning an ingredient from the API.
/// </summary>
public record IngredientDTO(
    Guid Id,
    string Name,
    string ImageUrl
);

/// <summary>
/// DTO for an ingredient. Used for adding an ingredient to the database.
/// </summary>
public record IngredientAddDTO(
    string Name,
    string ImageUrl
);

/// <summary>
/// DTO for an ingredient. Used for updating an ingredient in the database.
/// </summary>
public record IngredientUpdateDTO(
    string? Name,
    string? ImageUrl
);
