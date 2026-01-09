namespace Restaurant.Preparation.Application.Interfaces.Presenter;

public interface IJsonPresenter
{

    string? Serialize(object? obj);

    T? Deserialize<T>(string? json);

}
