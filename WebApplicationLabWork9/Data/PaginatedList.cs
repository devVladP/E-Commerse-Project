using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace WebApplicationLabWork9.Data
{
	public class PaginatedList
	{
		public int PageNumber { get; set; }
		public int PageSize { get; set; }

		public int TotalItems { get; set; }
		public int TotalPages 
		{ 
			get { return (int)Math.Ceiling((double)TotalItems / PageSize); }
		}
	}
}
