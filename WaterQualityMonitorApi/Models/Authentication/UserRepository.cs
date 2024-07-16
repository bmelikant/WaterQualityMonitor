namespace WaterQualityMonitorApi.Models.Authentication;

public class UserRepository {

	private static readonly List<UserModel> _users = new() {
		new() { Id = 1, UserName = "bmelikant", Password = "test123", Permissions = new [] { "MyRole", "MyOtherRole" }, ActiveSubscription = true },
		new() { Id = 2, UserName = "bmelikant2", Password = "test123", Permissions = new [] { "MyRole", "MyOtherRole" }, ActiveSubscription = false }
	};

	public static UserModel? Find(string username, string password) {
		return _users
			.Where(user => user.UserName == username && user.Password == password)
			.FirstOrDefault();	
	}

	public static UserModel? FindWithoutPassword(string username) => _users.Where(user => user.UserName == username).FirstOrDefault();
}