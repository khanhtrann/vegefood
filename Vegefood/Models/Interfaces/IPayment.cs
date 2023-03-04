using AuthorizeNet.Api.Contracts.V1;

namespace Vegefood.Models.Interfaces
{
	public interface IPayment
	{
		bool Run(double total, creditCardType creditCard, customerAddressType billingAdress);

	}
}
