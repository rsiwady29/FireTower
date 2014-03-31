using FireTower.Domain.Entities;

namespace FireTower.Domain
{
    public class VerificationEmailSender : IVerificationEmailSender
    {
        readonly IVerificationCodeGenerator _verificationCodeGenerator;
        readonly IEmailSender _emailSender;
        readonly IWriteableRepository _writeableRepository;

        public VerificationEmailSender(IVerificationCodeGenerator verificationCodeGenerator, IEmailSender emailSender, IWriteableRepository writeableRepository)
        {
            _verificationCodeGenerator = verificationCodeGenerator;
            _emailSender = emailSender;
            _writeableRepository = writeableRepository;
        }

        public void Send(string emailAddress)
        {
            var code = _verificationCodeGenerator.Generate();
            var verification = new Verification
                                   {
                                       EmailAddress = emailAddress,
                                       VerificationCode = code
                                   };
            _writeableRepository.Create(verification);
            _emailSender.Send(verification, emailAddress);
        }
    }
}