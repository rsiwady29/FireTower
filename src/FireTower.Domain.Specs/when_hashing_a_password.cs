using Machine.Specifications;
using FireTower.Domain.Services;

namespace FireTower.Domain.Specs
{
    public class when_hashing_a_password
    {
        const string ClearTextPassword = "some password";
        static IPasswordEncryptor _passwordEncryptor;
        static EncryptedPassword _firstResult;
        static EncryptedPassword _secondResult;

        Establish context =
            () => { _passwordEncryptor = new HashPasswordEncryptor(); };

        Because of =
            () =>
                {
                    _firstResult = _passwordEncryptor.Encrypt(ClearTextPassword);
                    _secondResult = _passwordEncryptor.Encrypt(ClearTextPassword);
                };

        It should_not_return_a_string_that_contains_the_clear_text_password =
            () => _firstResult.Password.ShouldNotContain(ClearTextPassword);

        It should_return_a_different_string_for_the_password =
            () => _firstResult.Password.ShouldNotEqual(ClearTextPassword);

        It should_return_the_same_string_when_hashed_again =
            () => _firstResult.Password.ShouldEqual(_secondResult.Password);
    }
}