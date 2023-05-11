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

		public Uri GetAllAdvertisementsUri(AdvertisementQuery addQuery, PaginitationQuery pageQuery)
		{
			var url = $"{_baseUri}api/v1/advertisements/list";

			var modifiedUri = QueryHelpers.AddQueryString(url,"pageNumber", pageQuery.Pagenumber.ToString());
			modifiedUri = QueryHelpers.AddQueryString(modifiedUri,"PageSize", pageQuery.PageSize.ToString());
			modifiedUri = addQuery.Address is null ? modifiedUri: QueryHelpers.AddQueryString(modifiedUri,"Address", addQuery.Address.ToString());
			modifiedUri = addQuery.MinPrice == 0 ? modifiedUri : QueryHelpers.AddQueryString(modifiedUri,"MinPrice", addQuery.MinPrice.ToString());
			modifiedUri = addQuery.MaxPrice == 0 ? modifiedUri : QueryHelpers.AddQueryString(modifiedUri,"MaxPrice", addQuery.MaxPrice.ToString());

			return new Uri(modifiedUri);
		}
	}
}