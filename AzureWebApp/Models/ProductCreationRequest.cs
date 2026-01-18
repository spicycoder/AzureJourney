namespace AzureWebApp.Models;

public record ProductCreationRequest(
    string Name,
    string Description,
    string Price,
    int Quantity);