using HomefinderAPI.Interfaces;
using HomefinderAPI.ViewModels.Queries;
using Microsoft.AspNetCore.WebUtilities;

namespace HomefinderAPI.Repositories
{
	public class UriRepository : IUriRepository
	{
		private readonly string _baseUri;
		public UriRepository(string baseUri)
		{
			_baseUri = baseUri;
		}
		
		public Uri GetAdvertisementUri(string advertisementId)
		{
			return new Uri(_baseUri + $"api/v1/advertisement/{advertisementId}");
		}

		public Uri GetAllAdvertisementsUri(PaginitationQuery? pageQuery = null)
		{
			var url = $"{_baseUri}api/v1/advertisements/list";

			if (pageQuery is null)
			{
				return new Uri(url);
			}

			var modifiedUri = QueryHelpers.AddQueryString(url,"pageNumber", pageQuery.Pagenumber.ToString());
			modifiedUri = QueryHelpers.AddQueryString(modifiedUri,"PageSize", pageQuery.PageSize.ToString());

			return new Uri(modifiedUri);
		}
	}
}