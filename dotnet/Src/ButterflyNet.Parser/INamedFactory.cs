namespace ButterflyNet.Parser {
	public interface INamedFactory<T> {
		T Create(string identifier);
	}
}