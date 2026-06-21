function login(user, pass) {
  if (user == "admin" && pass == "123456") {
    console.log("Logged in");
  }

  // ❌ Hardcoded secret
  const apiKey = "sk_test_1234567890";

  return apiKey;
}