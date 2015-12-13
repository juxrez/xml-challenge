using System.Collections.Generic;

namespace BackEnd.Infrastructure
{
	interface IRepository<T>
	{
		IEnumerable<T> List { get; }
		T FindById(int id);
		void Add(T elm);
		void Update(int id, T element);
		void Delete(int id);
	}
}