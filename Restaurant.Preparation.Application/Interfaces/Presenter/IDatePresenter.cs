namespace Restaurant.Preparation.Application.Interfaces.Presenter;

public interface IDatePresenter
{

    DateTime? ToTimeZone(DateTime? dateTime);

}
