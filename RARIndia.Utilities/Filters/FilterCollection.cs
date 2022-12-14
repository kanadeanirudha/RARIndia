using RARIndia.Utilities.Filters;

using System.Collections.Generic;

namespace RARIndia.Utilities.Helper
{
    public class FilterCollection : List<FilterTuple>
	{
		public void Add(string filterName, string filterOperator, string filterValue) => Add(new FilterTuple(filterName, filterOperator, filterValue));
	}
}
