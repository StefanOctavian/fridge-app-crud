using Crud.DTOs;

namespace Crud.Services.Interfaces;

public interface IIngredientService
{
    public Task<IngredientDTO> Create(IngredientAddDTO ingredient);
    public Task<List<IngredientDTO>> Read();
    public Task<IngredientDTO> Read(Guid id);
    public Task<IngredientDTO> Read(string name);
    public Task<IngredientDTO> Update(Guid id, IngredientUpdateDTO ingredient);
    public Task Delete(Guid id);
}