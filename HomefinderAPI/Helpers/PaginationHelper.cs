using HomefinderAPI.Interfaces;
using HomefinderAPI.ViewModels.Queries;
using HomefinderAPI.ViewModels.Responses;

namespace HomefinderAPI.Helpers
{
    public class PaginationHelper
	{
		public static PagedResponse<T> CreatePaginatedResponse<T>(IUriRepository uriRepository,List<T> respons, PaginitationQuery pageQuery, AdvertisementQuery addQuery)
		{
			var nextPage = pageQuery.Pagenumber >= 1
			? uriRepository.GetAllAdvertisementsUri(addQuery,new PaginitationQuery(pageQuery.Pagenumber + 1, pageQuery.PageSize)).ToString()
			: null;

			var PreviousPage = pageQuery.Pagenumber - 1 >= 1
				? uriRepository.GetAllAdvertisementsUri(addQuery,new PaginitationQuery(pageQuery.Pagenumber - 1, pageQuery.PageSize)).ToString()
				: null;

			return new PagedResponse<T>
			{
				Data = respons,
				PageNumber = pageQuery.Pagenumber >= 1 ? pageQuery.Pagenumber : (int?)null,
				PageSize = pageQuery.PageSize >= 1 ? pageQuery.PageSize : (int?)null,
				NextPage = respons.Count == pageQuery.PageSize ? nextPage : null,
				PreviousPage = PreviousPage
			};
		}
	}
}