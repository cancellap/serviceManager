using SM.Domaiin.Validation;
using Xunit;

namespace ServeceManageTests.Tests.TestLayerDomain
{
    public class ClienteTests
    {
        [Fact]
        public void DeveRetornarFalseQuandoCNPJInvalido()
        {
            var cnpjInvalido = "123456789";

            var resultado = ValidaCnpj.IsCnpj(cnpjInvalido);

            Assert.False(resultado);
        }

      


    }
}
