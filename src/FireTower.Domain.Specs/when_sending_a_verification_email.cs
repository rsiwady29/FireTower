using AcklenAvenue.Testing.Moq.ExpectedObjects;
using Machine.Specifications;
using Moq;
using FireTower.Domain.Entities;
using It = Machine.Specifications.It;

namespace FireTower.Domain.Specs
{
    public class when_sending_a_verification_email
    {
        const string EmailAddress = "emailAddress";
        const string VerificationCode = "2344";
        static IVerificationEmailSender _sender;
        static IEmailSender _emailSender;
        static IVerificationCodeGenerator _verificationCodeGenerator;
        static IWriteableRepository _writeableRepository;
        static Verification _expectedVerification;

        Establish context =
            () =>
                {
                    _emailSender = Mock.Of<IEmailSender>();
                    _verificationCodeGenerator = Mock.Of<IVerificationCodeGenerator>();
                    _writeableRepository = Mock.Of<IWriteableRepository>();
                    _sender = new VerificationEmailSender(_verificationCodeGenerator, _emailSender, _writeableRepository);

                    Mock.Get(_verificationCodeGenerator).Setup(x => x.Generate()).Returns(VerificationCode);

                    _expectedVerification = new Verification
                                                {
                                                    EmailAddress = EmailAddress,
                                                    VerificationCode = VerificationCode
                                                };
                };

        Because of =
            () => _sender.Send(EmailAddress);

        It should_save_the_new_verification_code_to_the_repo =
            () => Mock.Get(_writeableRepository)
                      .Verify(x => x.Create(WithExpected.Object(_expectedVerification)));

        It should_send_the_formatted_email =
            () => Mock.Get(_emailSender)
                      .Verify(x => x.Send(WithExpected.Object(_expectedVerification), EmailAddress));
    }
}