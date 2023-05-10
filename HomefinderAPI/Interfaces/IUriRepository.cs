using HomefinderAPI.ViewModels.Queries;

namespace HomefinderAPI.Interfaces
{
	public interface IUriRepository
	{
		Uri GetAdvertisementUri (string advertisementId);
		Uri GetAllAdvertisementsUri (PaginitationQuery? pageQuery = null);
	}
}