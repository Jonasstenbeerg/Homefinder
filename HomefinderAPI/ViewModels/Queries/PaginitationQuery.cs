namespace HomefinderAPI.ViewModels.Queries
{
	public class PaginitationQuery
	{
		public PaginitationQuery()
		{
			PageSize = 1;
			PageSize = 5;
		}

		public PaginitationQuery(int pageNumber,int pageSize)
		{
			Pagenumber = pageNumber;
			PageSize = pageSize > 20 ? 20 : pageSize;
		}

		public int Pagenumber { get; set; }
		public int PageSize { get; set; }
	}
}