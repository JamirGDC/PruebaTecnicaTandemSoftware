namespace PruebaTecnicaTandemSoftware.Application.Dtos.Response;

public class PostProductResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
}