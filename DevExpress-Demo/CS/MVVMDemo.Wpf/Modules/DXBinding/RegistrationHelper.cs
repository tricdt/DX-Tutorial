namespace MVVMDemo.DXBindingDemo {
    public static class RegistrationHelper {
        public static bool CanRegister(string userName, bool acceptTerms) {
            return !string.IsNullOrEmpty(userName) && acceptTerms;
        }
    }
}
