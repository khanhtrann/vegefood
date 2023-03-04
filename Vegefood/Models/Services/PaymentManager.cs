﻿using AuthorizeNet.Api.Controllers.Bases;
using AuthorizeNet.Api.Controllers;
using Microsoft.AspNetCore.Hosting;
using Vegefood.Models.Interfaces;
using AuthorizeNet.Api.Contracts.V1;

namespace Vegefood.Models.Services
{
	public class PaymentManager : IPayment
	{
		public IConfiguration Configuration { get; }

		public PaymentManager(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		/// <summary>
		/// Makes a API call to Authorize.Net to make a Sandbox transaction
		/// </summary>
		/// <param name="total">Total amount of the transaction</param>
		/// <returns>Boolean to confirm if the payment was successful or not</returns>
		public bool Run(double total, creditCardType creditCard, customerAddressType billingAdress)
		{
			ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;

			string authNetId = Configuration["AUTH_NET_ID"];

			string authNetKey = Configuration["AUTH_NET_KEY"];

			ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
			{
				name = authNetId,
				ItemElementName = ItemChoiceType.transactionKey,
				Item = authNetKey
			};

			var paymentType = new paymentType { Item = creditCard };

			var transactionRequest = new transactionRequestType
			{
				transactionType = transactionTypeEnum.authCaptureTransaction.ToString(),
				amount = (decimal)total,
				payment = paymentType,
				billTo = billingAdress
			};

			var request = new createTransactionRequest { transactionRequest = transactionRequest };

			var controller = new createTransactionController(request);
			controller.Execute();

			var response = controller.GetApiResponse();

			if (response != null)
			{
				if (response.messages.resultCode == messageTypeEnum.Ok)
				{
					return true;
				}
			}
			else
			{
				if (response!.transactionResponse.errors != null)
				{
					return false;
				}
			}
			return false;
		}
	}
}
